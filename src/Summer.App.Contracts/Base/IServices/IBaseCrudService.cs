using System;
using System.Threading.Tasks;
using Summer.App.Contracts.Base.Dtos;

namespace Summer.App.Contracts.Base.IServices
{
    public interface IBaseCrudService<TDto> : IBaseCrudService<PagedInputDto, TDto, TDto, TDto>
        where TDto : class
    {

    }

    public interface IBaseCrudService<in TPagedReqDto, TDto> : IBaseCrudService<TPagedReqDto, TDto, TDto, TDto>
        where TPagedReqDto : PagedInputDto
        where TDto : class
    {

    }

    public interface IBaseCrudService<in TPagedReqDto, in TCreateDto, in TUpdateDto, TDto> : IBaseService
        where TPagedReqDto : PagedInputDto
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        Task<OutputDto<PagedOutputDto<TDto>>> Get(TPagedReqDto value);

        Task<OutputDto<TDto>> Get(Guid id);

        Task<OutputDto<TDto>> Create(TCreateDto value);

        Task<OutputDto<TDto>> Update(Guid id, TUpdateDto value);

        Task<OutputDto<TDto>> Delete(Guid id);
    }
}