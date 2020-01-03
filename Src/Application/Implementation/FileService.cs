using Application.Interfaces;
using AutoMapper;
using Domain.Common.ValidationAttributes;
using Domain.Entities;
using Domain.RequestModels;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Implementation
{
    public class FileService : IFileService
    {
        readonly IConfiguration _config;
        readonly IFileUploaderDbContext _context;
        readonly IMapper _mapper;
        readonly IHostingEnvironment _hostingEnvironment;
        readonly FilePolicy _fileSetting;

        public FileService(IConfiguration configuration, IFileUploaderDbContext context, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _config = configuration;
            _context = context;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;

            _fileSetting = new FilePolicy();
            _config.GetSection("FilePolicy").Bind(_fileSetting);
        }

        public async Task<IList<FileViewModel>> GetAll()
        {
            return await _context.Files.Select(x => _mapper.Map<FileViewModel>(x)).ToListAsync();
        }

        public async Task Create(FileRequestModel model)
        {
            var fileEntity = await CreateFile(model);
            if(fileEntity != null)
            {
                _context.Files.Add(fileEntity);
                await _context.SaveChangesAsync(model.Username);
            }            
        }

        private async Task<UploadFile> CreateFile(FileRequestModel model)
        {
            var extension = Path.GetExtension(model.File.FileName).Trim('.');
            var systemName = await UploadFile(model.File, extension);

            if (!string.IsNullOrWhiteSpace(systemName))
            {
                var fileEntity = new UploadFile
                {
                    Extension = extension,
                    Name = string.IsNullOrWhiteSpace(model.Name) ? model.File.FileName : model.Name,
                    Size = model.File.Length,
                    SystemName = systemName
                };

                return fileEntity;
            }
            return null;            
        }

        public async Task<FileDownloadViewModel> Download(int id)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.FileId == id);
            if (file == null)
                return null;

            var filePath = BuildFilePath(file.SystemName, file.Extension);

            return new FileDownloadViewModel()
            {
                FileName = file.Name,
                FilePath = filePath,
                ContentType = GetContentType(filePath)
            };
        }

        private async Task<string> UploadFile(IFormFile file, string extension)
        {
            if (file.Length > 0)
            {
                var fileSystemName = FileRequestModel.GenerateSystemName();

                var filePath = BuildFilePath(fileSystemName, extension);

                using (var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
                return fileSystemName;
            }
            return null;
        }

        private string BuildFilePath(string name, string extension)
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, _fileSetting.FolderPath, extension);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return Path.Combine(path, $"{name}.{extension}");
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        public FilePolicyViewModel GetFilePolicy()
        {
            return _mapper.Map<FilePolicyViewModel>(_fileSetting);
        }
    }
}
