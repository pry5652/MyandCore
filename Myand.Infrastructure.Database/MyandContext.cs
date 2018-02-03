using Microsoft.EntityFrameworkCore;
using Myand.Commons.Domain;
using Myand.Domain;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Myand.Infrastructure.Database
{
	public class MyandContext : DbContext
	{
		public MyandContext(DbContextOptions<MyandContext> options) : base(options) { }
		public virtual DbSet<MstUser> MstUser { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{			
			modelBuilder.Entity<MstUser>(entity =>
			{
				entity.ToTable("Mst_User");

				entity.Property(e => e.Id).ValueGeneratedNever();

				entity.Property(e => e.Description).IsUnicode(false);				

				entity.Property(e => e.Name)
					.IsRequired()
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.Password)
					.IsRequired()
					.HasMaxLength(50)
					.IsUnicode(false);

				entity.Property(e => e.Username)
					.IsRequired()
					.HasMaxLength(50)
					.IsUnicode(false)
					;

				entity.Property(e => e.CreatedBy)
					.HasMaxLength(50)
					.IsUnicode(false)
					;

				entity.Property(e => e.CreatedDate).HasColumnType("datetime");

				entity.Property(e => e.UpdatedBy)
					.HasMaxLength(50)
					.IsUnicode(false)
					;

				entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
				
				
			});
		}
		public override int SaveChanges()
		{
			Audit();
			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
		{
			Audit();
			return await base.SaveChangesAsync();
		}

		private void Audit()
		{
			var entries = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));
			foreach (var entry in entries)
			{
				if (entry.State == EntityState.Added)
				{
					((EntityBase)entry.Entity).CreatedDate = DateTime.UtcNow;
				}
			((EntityBase)entry.Entity).UpdatedDate = DateTime.UtcNow;
			}
		}
	}
}
