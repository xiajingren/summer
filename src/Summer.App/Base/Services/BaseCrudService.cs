using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.App.Base.Entities;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;

namespace Summer.App.Base.Services
{
    internal class BaseCrudService<TEntity, TDto> : BaseCrudService<TEntity, BasePagedReqDto, TDto, TDto, TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    internal class
        BaseCrudService<TEntity, TPagedReqDto, TDto> : BaseCrudService<TEntity, TPagedReqDto, TDto, TDto, TDto>
        where TEntity : BaseEntity
        where TPagedReqDto : BasePagedReqDto
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    internal class BaseCrudService<TEntity, TPagedReqDto, TCreateDto, TUpdateDto, TDto> : BaseService,
        IBaseCrudService<TPagedReqDto, TCreateDto, TUpdateDto, TDto>
        where TEntity : BaseEntity
        where TPagedReqDto : BasePagedReqDto
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// 用作 条件过滤，导航属性Include ...
        /// </summary>
        /// <param name="pagedReqDto"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetQueryable(TPagedReqDto pagedReqDto = null)
        {
            return AppDbContext.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<BaseDto<BasePagedDto<TDto>>> Get(TPagedReqDto pagedReqDto)
        {
            var models = GetQueryable(pagedReqDto);

            var pagedDto = new BasePagedDto<TDto> {Total = models.Count()};

            models = models.OrderByDescending(p => p.CreateTime)
                .Skip((pagedReqDto.PageIndex - 1) * pagedReqDto.PageSize)
                .Take(pagedReqDto.PageSize);
            var data = await models.ToListAsync();

            pagedDto.List = Mapper.Map<IEnumerable<TDto>>(data);

            return Ok(pagedDto);
        }

        public virtual async Task<BaseDto<TDto>> Get(Guid id)
        {
            var models = GetQueryable();

            var model = await models.SingleOrDefaultAsync(p => p.Id == id);

            return Ok(Mapper.Map<TDto>(model));
        }

        public virtual async Task<BaseDto<TDto>> Create(TCreateDto value)
        {
            var model = Mapper.Map<TEntity>(value);
            await AppDbContext.Set<TEntity>().AddAsync(model);

            return await AppDbContext.SaveChangesAsync() > 0
                ? Ok(Mapper.Map<TDto>(model))
                : Fail<TDto>(null);
        }

        public virtual async Task<BaseDto<TDto>> Update(Guid id, TUpdateDto value)
        {
            var model = await GetQueryable().SingleOrDefaultAsync(p => p.Id == id);
            model = Mapper.Map(value, model);

            return await AppDbContext.SaveChangesAsync() > 0
                ? Ok(Mapper.Map<TDto>(model))
                : Fail<TDto>(null);
        }

        public virtual async Task<BaseDto<TDto>> Delete(Guid id)
        {
            var model = await GetQueryable().SingleOrDefaultAsync(p => p.Id == id);
            AppDbContext.Set<TEntity>().Remove(model);

            return await AppDbContext.SaveChangesAsync() > 0
                ? Ok(Mapper.Map<TDto>(model))
                : Fail<TDto>(null);
        }
    }
}