using Microsoft.EntityFrameworkCore;
using Summer.App.Contracts.Dtos;
using Summer.App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Summer.App.Contracts.IServices;

namespace Summer.App.Services
{
    internal class BaseCrudService<TEntity, TDto> : BaseCrudService<TEntity, BasePagedReqDto, TDto, TDto, TDto>
        where TEntity : BaseEntity
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }

    internal class BaseCrudService<TEntity, TPagedReqDto, TDto> : BaseCrudService<TEntity, TPagedReqDto, TDto, TDto, TDto>
        where TEntity : BaseEntity
        where TPagedReqDto : BasePagedReqDto
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }

    internal class BaseCrudService<TEntity, TPagedReqDto, TCreateDto, TUpdateDto, TDto> : BaseService, IBaseCrudService<TPagedReqDto, TCreateDto, TUpdateDto, TDto>
        where TEntity : BaseEntity
        where TPagedReqDto : BasePagedReqDto
        where TCreateDto : class
        where TUpdateDto : class
        where TDto : class
    {
        public BaseCrudService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public virtual Expression<Func<TEntity, bool>> GetQueryWhere(BasePagedReqDto value)
        {
            return p => true;
        }

        public virtual string[] GetInclude()
        {
            return null;
        }

        public virtual async Task<BaseDto<BasePagedDto<TDto>>> Get(TPagedReqDto value)
        {
            var models = SummerDbContext.Set<TEntity>().Where(GetQueryWhere(value));

            var include = GetInclude();
            if (include != null)
            {
                models = include.Aggregate(models, (current, s) => current.Include(s));
            }

            var pagedDto = new BasePagedDto<TDto> { Total = models.Count() };

            models = models.OrderByDescending(p => p.CreateTime).Skip((value.PageIndex - 1) * value.PageSize)
                .Take(value.PageSize);
            var data = await models.ToListAsync();

            pagedDto.List = Mapper.Map<IEnumerable<TDto>>(data);

            return BaseDto<BasePagedDto<TDto>>.CreateOkInstance(pagedDto);
        }

        public virtual async Task<BaseDto<TDto>> Get(Guid id)
        {
            var models = SummerDbContext.Set<TEntity>().AsNoTracking();

            var include = GetInclude();
            if (include != null)
            {
                models = include.Aggregate(models, (current, s) => current.Include(s));
            }

            var model = await models.SingleOrDefaultAsync(p => p.Id == id);

            return BaseDto<TDto>.CreateOkInstance(Mapper.Map<TDto>(model));
        }

        public virtual async Task<BaseDto<TDto>> Create(TCreateDto value)
        {
            var model = Mapper.Map<TEntity>(value);
            await SummerDbContext.Set<TEntity>().AddAsync(model);

            return await SummerDbContext.SaveChangesAsync() > 0
                ? BaseDto<TDto>.CreateOkInstance(Mapper.Map<TDto>(model))
                : BaseDto<TDto>.CreateFailInstance(null);
        }

        public virtual async Task<BaseDto<TDto>> Update(Guid id, TUpdateDto value)
        {
            var model = await SummerDbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id);
            model = Mapper.Map(value, model);

            return await SummerDbContext.SaveChangesAsync() > 0
                ? BaseDto<TDto>.CreateOkInstance(Mapper.Map<TDto>(model))
                : BaseDto<TDto>.CreateFailInstance(null);
        }

        public virtual async Task<BaseDto<TDto>> Delete(Guid id)
        {
            var model = await SummerDbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id);
            SummerDbContext.Set<TEntity>().Remove(model);

            return await SummerDbContext.SaveChangesAsync() > 0
                ? BaseDto<TDto>.CreateOkInstance(Mapper.Map<TDto>(model))
                : BaseDto<TDto>.CreateFailInstance(null);
        }

    }
}
