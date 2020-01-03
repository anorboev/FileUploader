using Domain.RequestModels;
using Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileService
    {

        Task<IList<FileViewModel>> GetAll();

        Task Create(FileRequestModel model);

        Task<FileDownloadViewModel> Download(int id);

        FilePolicyViewModel GetFilePolicy();
    }
}
