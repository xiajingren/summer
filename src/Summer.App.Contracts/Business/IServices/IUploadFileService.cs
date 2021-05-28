using System;
using System.Threading.Tasks;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;
using Summer.App.Contracts.Business.Dtos;

namespace Summer.App.Contracts.Business.IServices
{
    public interface IUploadFileService : IBaseCrudService<PagedInputDto, UploadFileDto, UploadFileDto, UploadFileOutputDto>
    {
        Task<OutputDto<UploadFileDto>> GetContent(Guid id);
    }
}