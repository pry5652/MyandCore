﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Myand.Commons.Map
{
	public class MapConfigurationFactory
	{
		public static MapperConfiguration CreateConfiguration(IList<Type> types)
		{
			MapperConfiguration config = new MapperConfiguration(cfg => { LoadAllMappings(cfg, types); });
			return config;
		}

		public static void Scan<TType>(Func<AssemblyName, bool> assemblyFilter = null)
		{
			var target = typeof(TType).Assembly;

			bool LoadAllFilter(AssemblyName x) => true;

			var assembliesToLoad = target.GetReferencedAssemblies()
				.Where(assemblyFilter ?? LoadAllFilter)
				.Select(Assembly.Load)
				.ToList();

			assembliesToLoad.Add(target);

			LoadMapsFromAssemblies(assembliesToLoad.ToArray());
		}

		public static void LoadMapsFromAssemblies(params Assembly[] assemblies)
		{
			var types = assemblies.SelectMany(a => a.GetExportedTypes()).ToArray();
			Mapper.Initialize(cfg => LoadAllMappings(cfg, types));
		}


		public static void LoadAllMappings(IList<Type> types)
		{
			Mapper.Initialize(
				cfg =>
				{
					LoadStandardMappings(cfg, types);
					LoadCustomMappings(cfg, types);
				});
		}


		public static void LoadAllMappings(IMapperConfigurationExpression config, IList<Type> types)
		{
			LoadStandardMappings(config, types);
			LoadCustomMappings(config, types);
		}


		public static void LoadCustomMappings(IMapperConfigurationExpression config, IList<Type> types)
		{
			var instancesToMap = (from t in types
								  from i in t.GetInterfaces()
								  where typeof(ICustomMap).IsAssignableFrom(t) &&
										!t.IsAbstract &&
										!t.IsInterface
								  select (ICustomMap)Activator.CreateInstance(t)).ToArray();


			foreach (var map in instancesToMap)
			{
				map.CustomMap(config);
			}
		}

		public static void LoadStandardMappings(IMapperConfigurationExpression config, IList<Type> types)
		{
			var mapsFrom = (from t in types
							from i in t.GetInterfaces()
							where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
								  !t.IsAbstract &&
								  !t.IsInterface
							select new
							{
								Source = i.GetGenericArguments()[0],
								Destination = t
							}).ToArray();


			foreach (var map in mapsFrom)
			{
				config.CreateMap(map.Source, map.Destination);
			}


			var mapsTo = (from t in types
						  from i in t.GetInterfaces()
						  where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
								!t.IsAbstract &&
								!t.IsInterface
						  select new
						  {
							  Source = i.GetGenericArguments()[0],
							  Destination = t
						  }).ToArray();


			foreach (var map in mapsTo)
			{
				config.CreateMap(map.Source, map.Destination);
			}
		}
	}
}
