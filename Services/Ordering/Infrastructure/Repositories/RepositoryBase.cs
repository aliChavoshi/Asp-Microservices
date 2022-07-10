using System.Linq.Expressions;
using Application.Contracts.Persistence;
using Domain.Common;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly OrderContext Context;

    protected RepositoryBase(OrderContext context)
    {
        Context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync().ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().Where(predicate).ToListAsync().ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = Context.Set<T>();
        if (disableTracking) query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

        if (predicate != null) query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync().ConfigureAwait(false);
        return await query.ToListAsync().ConfigureAwait(false);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id).ConfigureAwait(false);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityEntry = await Context.Set<T>().AddAsync(entity);
        await Save();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Save();
    }

    public async Task DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Save();
    }

    public async Task<int> Save()
    {
        return await Context.SaveChangesAsync();
    }
}