using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Application.Common.Interfaces.CurrentUser;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Entities.Identity;
using System;

namespace CleanArchitecture.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User,UserRole,Guid>
    {
        private readonly ICurrentUserService currentUserService;

        public ApplicationDbContext()
        {

        }
        //public ApplicationDbContext(ICurrentUserService currentUserService)
        //{
        //    this.currentUserService = currentUserService;
        //}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRoleClaim> UserRoleClaim { get; set; }
        public DbSet<UserUserClaim> UserUserClaim { get; set; }
        public DbSet<UserUserRole> UserUserRole { get; set; }
        public DbSet<UserUserLogin> UserUserLogin { get; set; }
        public DbSet<UserUserToken> UserUserToken { get; set; }


        public DbSet<Product> Products { get; set; }
        public DbSet<PermissionStore> PermissionStore { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SalesTargets>()
            //    .HasOne<Product>(st => st.Product)
            //    .WithMany(p => p.SalesTargets)
            //    .HasForeignKey(f => f.ProductId);

            base.OnModelCreating(modelBuilder);
        }
        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        //{
        //    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        //    {
        //        switch (entry.State)
        //        {
        //            case EntityState.Detached:
        //                break;
        //            case EntityState.Unchanged:
        //                break;
        //            case EntityState.Deleted:
        //                break;
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedBy = currentUserService.UserId.ToString();
        //                entry.Entity.LastModified = DateTime.UtcNow;
        //                break;
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = currentUserService.UserId.ToString();
        //                entry.Entity.Created = DateTime.UtcNow;
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    var result = await base.SaveChangesAsync(cancellationToken);


        //    return result; 
        //}

    }
}