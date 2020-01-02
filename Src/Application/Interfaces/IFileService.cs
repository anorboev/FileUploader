using Domain.RequestModels;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task<FileViewModel> GetById(int id);

        Task<IList<FileViewModel>> GetByType(string type);

        Task<IList<FileViewModel>> GetAll();

        Task Create(FileRequestModel model);

        Task<FileDownloadViewModel> Download(int id);

        FileSettingsViewModel GetFileValidations();
    }
}
