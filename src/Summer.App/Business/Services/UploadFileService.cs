using Summer.App.Base.Services;
using Summer.App.Business.Entities;
using Summer.App.Contracts.Business.Dtos;
using Summer.App.Contracts.Business.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Summer.App.Contracts.Base.Dtos;

namespace Summer.App.Business.Services
{
    internal class UploadFileService : BaseCrudService<UploadFile, PagedInputDto, UploadFileDto, UploadFileDto, UploadFileOutputDto>, IUploadFileService
    {
        private IHttpContextAccessor HttpContextAccessor { get; }

        public UploadFileService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            HttpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        public override async Task<OutputDto<UploadFileOutputDto>> Get(Guid id)
        {
            var result = await base.Get(id);

            if (result.Data != null)
            {
                result.Data.FileUrl = GetFileUrl(result.Data.Id);
            }

            return result;
        }

        public override async Task<OutputDto<PagedOutputDto<UploadFileOutputDto>>> Get(PagedInputDto pagedReqDto)
        {
            var result = await base.Get(pagedReqDto);

            if (result.Data != null && result.Data.List.Any())
            {
                result.Data.List.ForEach(p => p.FileUrl = GetFileUrl(p.Id));
            }

            return result;
        }

        public override async Task<OutputDto<UploadFileOutputDto>> Create(UploadFileDto value)
        {
            var result = await base.Create(value);

            if (result.Data != null)
            {
                result.Data.FileUrl = GetFileUrl(result.Data.Id);
            }

            return result;
        }

        public override async Task<OutputDto<UploadFileOutputDto>> Update(Guid id, UploadFileDto value)
        {
            var result = await base.Update(id, value);

            if (result.Data != null)
            {
                result.Data.FileUrl = GetFileUrl(result.Data.Id);
            }

            return result;
        }

        public override async Task<OutputDto<UploadFileOutputDto>> Delete(Guid id)
        {
            var result = await base.Delete(id);

            if (result.Data != null)
            {
                result.Data.FileUrl = GetFileUrl(result.Data.Id);
            }

            return result;
        }

        public async Task<OutputDto<UploadFileDto>> GetContent(Guid id)
        {
            var model = await AppDbContext.UploadFiles.SingleOrDefaultAsync(p => p.Id == id);
            return model == null ? Fail<UploadFileDto>(null) : Ok(Mapper.Map<UploadFileDto>(model));
        }

        private string GetFileUrl(Guid? id)
        {
            return $"{HttpContextAccessor?.HttpContext?.Request.Scheme}://{HttpContextAccessor?.HttpContext?.Request.Host}" +
                   $"{HttpContextAccessor?.HttpContext?.Request.PathBase}/api/Sys/UploadFile/Download/{id}";
        }

    }
}