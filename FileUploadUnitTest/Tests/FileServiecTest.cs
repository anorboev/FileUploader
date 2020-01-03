using Application.Interfaces;
using Domain.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace FileUploadUnitTest.Tests
{
    [TestClass]
    public class FileServiecTest
    {
        [TestMethod]
        public async Task UploadFile_GivenValidRequest_ShouldSuccess()
        {
            var mockModel = BuildModel("pdf");
            var mockFileService= new Mock<IFileService>();
            mockFileService.Setup(x => x.Create(mockModel)).Returns(Task.CompletedTask);

            var controller = new FileController(mockFileService.Object);

            //Act
            var result = await controller.Create(mockModel);
            var okResult = result as ObjectResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IActionResult));
        }

        [TestMethod]
        public async Task UploadFile_GivenInValidRequest_ShouldFail()
        {
            var mockModel = BuildModel("pdf");
            var mockFileService = new Mock<IFileService>();
            mockFileService.Setup(x => x.Create(mockModel)).Returns(Task.CompletedTask);

            var controller = new FileController(mockFileService.Object);
            controller.ModelState.AddModelError("File", "Extension invalid");

            //Act
            var result = await controller.Create(mockModel);
            var okResult = result as ObjectResult;

            //Assert
            Assert.IsInstanceOfType(result, typeof(IActionResult));
            Assert.IsTrue(okResult.StatusCode == StatusCodes.Status400BadRequest);
        }

        private FileRequestModel BuildModel(string extension)
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test." + extension;
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);


            return new FileRequestModel { 
                File = fileMock.Object,
                Name = fileName,
                Username = "test"
            };
        }        
    }
}
