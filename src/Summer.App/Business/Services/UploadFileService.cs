using Microsoft.EntityFrameworkCore;
using Summer.App.Base.Services;
using Summer.App.Business.Entities;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Business.Dtos;
using Summer.App.Contracts.Business.IServices;
using System;
using System.Threading.Tasks;

namespace Summer.App.Business.Services
{
    internal class UploadFileService : BaseCrudService<UploadFile, PagedInputDto, UploadFileDto, UploadFileDto, UploadFileOutputDto>, IUploadFileService
    {
        public UploadFileService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<OutputDto<UploadFileDto>> GetContent(Guid id)
        {
            var model = await AppDbContext.UploadFiles.SingleOrDefaultAsync(p => p.Id == id);
            return model == null ? Fail<UploadFileDto>(null) : Ok(Mapper.Map<UploadFileDto>(model));
        }

    }
}