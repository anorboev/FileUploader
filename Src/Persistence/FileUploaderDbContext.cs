using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public class FileUploaderDbContext : DbContext, IFileUploaderDbContext
    {
        public FileUploaderDbContext(DbContextOptions<FileUploaderDbContext> options)
            : base(options)
        {
        }

        public DbSet<UploadFile> Files { get; set; }

        public Task<int> SaveChangesAsync(string username)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = username;
                        entry.Entity.Created = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = username;
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync();
        }
    }
}
