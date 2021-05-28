using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Summer.App.Base.Entities;
using Summer.App.Contracts.Base.Dtos;
using Summer.App.Contracts.Base.IServices;

namespace Summer.App.Base.Services
{
    internal class BaseCrudService<TEntity, TDto> : BaseCrudService<TEntity, PagedInputDto, TDto, TDto, TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    internal class
        BaseCrudService<TEntity, TPagedInputDto, TDto> : BaseCrudService<TEntity, TPagedInputDto, TDto, TDto, TDto>
        where TEntity : BaseEntity
        where TPagedInputDto : PagedInputDto
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }

    internal class BaseCrudService<TEntity, TPagedInputDto, TCreateDto, TUpdateDto, TDto> : BaseService,
        IBaseCrudService<TPagedInputDto, TCreateDto, TUpdateDto, TDto>
        where TEntity : BaseEntity
        where TPagedInputDto : PagedInputDto
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
        public virtual IQueryable<TEntity> GetQueryable(TPagedInputDto pagedReqDto = null)
        {
            return AppDbContext.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<OutputDto<PagedOutputDto<TDto>>> Get(TPagedInputDto pagedReqDto)
        {
            var models = GetQueryable(pagedReqDto);

            var pagedDto = new PagedOutputDto<TDto> { Total = models.Count() };

            models = models.OrderByDescending(p => p.CreateTime)
                .Skip((pagedReqDto.PageIndex - 1) * pagedReqDto.PageSize)
                .Take(pagedReqDto.PageSize);
            var data = await models.ToListAsync();

            pagedDto.List = Mapper.Map<List<TDto>>(data);

            return Ok(pagedDto);
        }

        public virtual async Task<OutputDto<TDto>> Get(Guid id)
        {
            var models = GetQueryable();

            var model = await models.SingleOrDefaultAsync(p => p.Id == id);

            return Ok(Mapper.Map<TDto>(model));
        }

        public virtual async Task<OutputDto<TDto>> Create(TCreateDto value)
        {
            var model = Mapper.Map<TEntity>(value);
            await AppDbContext.Set<TEntity>().AddAsync(model);

            return await AppDbContext.SaveChangesAsync() > 0
                ? Ok(Mapper.Map<TDto>(model))
                : Fail<TDto>(null);
        }

        public virtual async Task<OutputDto<TDto>> Update(Guid id, TUpdateDto value)
        {
            var model = await GetQueryable().SingleOrDefaultAsync(p => p.Id == id);
            model = Mapper.Map(value, model);

            return await AppDbContext.SaveChangesAsync() > 0
                ? Ok(Mapper.Map<TDto>(model))
                : Fail<TDto>(null);
        }

        public virtual async Task<OutputDto<TDto>> Delete(Guid id)
        {
            var model = await GetQueryable().SingleOrDefaultAsync(p => p.Id == id);
            AppDbContext.Set<TEntity>().Remove(model);

            return await AppDbContext.SaveChangesAsync() > 0
                ? Ok(Mapper.Map<TDto>(model))
                : Fail<TDto>(null);
        }
    }
}