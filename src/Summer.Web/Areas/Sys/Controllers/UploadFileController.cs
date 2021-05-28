using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Business.Dtos;
using Summer.App.Contracts.Business.IServices;

namespace Summer.Web.Areas.Sys.Controllers
{
    [Area("Sys")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IUploadFileService _uploadFileService;

        public UploadFileController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }

        [HttpGet]
        public async Task<OutputDto<PagedOutputDto<UploadFileOutputDto>>> Get([FromQuery] PagedInputDto value)
        {
            return await _uploadFileService.Get(value);
        }

        [HttpGet("{id}")]
        public async Task<OutputDto<UploadFileOutputDto>> Get(Guid id)
        {
            return await _uploadFileService.Get(id);
        }

        [HttpPost]
        public async Task<OutputDto<UploadFileOutputDto>> Post(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            return await _uploadFileService.Create(new UploadFileDto()
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = memoryStream.ToArray()
            });
        }

        [HttpDelete("{id}")]
        public async Task<OutputDto<UploadFileOutputDto>> Delete(Guid id)
        {
            return await _uploadFileService.Delete(id);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> Download(Guid id)
        {
            var result = await _uploadFileService.GetContent(id);

            if (result.Code != 1) return NotFound();

            var memoryStream = new MemoryStream(result.Data.Content);
            return File(memoryStream, result.Data.ContentType);
        }

    }
}
