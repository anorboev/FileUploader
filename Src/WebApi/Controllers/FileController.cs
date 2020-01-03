using System.Threading.Tasks;
using Application.Interfaces;
using Domain.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;

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


        [HttpPost]
        public async Task<IActionResult> Create([FromForm]FileRequestModel model)
        {
            if (ModelState.IsValid)
            {
                await _fileService.Create(model);
                return NoContent();
            }

            return new BadRequestObjectResult(this.ModelState);
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

        [HttpGet("getuploadpolicy")]
        public IActionResult GetUploadPolicy()
        {
            return Ok(_fileService.GetFilePolicy());
        }
    }
}