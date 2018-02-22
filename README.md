# Myand
Sample Domain Driven Design with all best practices design and architetural patterns as DDD, CrossCutting IoC, SOLID, etc

# Feateure 
1. Shared Functions (Commons) include (Domain, Repository, UnitOfWork, AppService (Async / Non Async), Map, etc) 
2. .NET Core 2.0
3. EntityFrameworkCore
4. Web Api
5. Swagger
etc

# Installation
1. Build
2. Change appsettings.json => 	"ConnectionStrings": {
		"MyAndConnection": "YourConnection" 
	}, // if you want change 
3. Tools –> NuGet Package Manager –> Package Manager Console
					type Update-Database in Myand.Infrastructure.Database
4. Set Starup Project Myand.API, Run  
