using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Summer.App.Contracts.Dtos;

namespace Summer.App.Contracts.IServices
{
    public interface IBaseCrudService<in TPagedReqDto, in TCreateDto, in TUpdateDto, TDto> : IBaseService
        where TPagedReqDto : BasePagedReqDto
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        Task<BaseDto<BasePagedDto<TDto>>> Get(TPagedReqDto value);

        Task<BaseDto<TDto>> Get(Guid id);

        Task<BaseDto<TDto>> Create(TCreateDto value);

        Task<BaseDto<TDto>> Update(Guid id, TUpdateDto value);

        Task<BaseDto<TDto>> Delete(Guid id);
    }
}