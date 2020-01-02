using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _fileService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var file = await _fileService.GetById(id);

            if (file == null)
                return NotFound();

            return Ok(file);
        }

        [HttpGet("getbytype/{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            return Ok(await _fileService.GetByType(type));
        }


        [HttpPost("{name}")]
        public async Task<IActionResult> Create(string name, IFormFile file, [FromForm]string username)
        {
            var model = new FileRequestModel()
            {
                File = file,
                Name = name,
                Username = username
            };
            await _fileService.Create(model);
            return NoContent();
        }

        [HttpGet]
        [Route("download/{id}")]
        public async Task<IActionResult> Download(int id)        
        {
            var file = await _fileService.Download(id);
            if (file == null || !System.IO.File.Exists(file.FilePath))
                return NotFound();

            return PhysicalFile(file.FilePath, file.ContentType, file.FileName);
        }

        [HttpGet("getvalidationsrules")]
        public IActionResult GetValidations()
        {
            return Ok(_fileService.GetFileValidations());
        }
    }
}