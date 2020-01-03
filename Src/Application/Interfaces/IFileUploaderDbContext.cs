using Domain.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IFileUploaderDbContext
    {
        DbSet<UploadFile> Files { get; set; }

        Task<int> SaveChangesAsync(string username);
    }
}
