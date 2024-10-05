using System;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Repository.IRepository;

namespace Villa_VillaAPI.Repository;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly ApplicationDBContext _dbContext;
    public VillaNumberRepository(ApplicationDBContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _dbContext.VillaNumbers.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}
