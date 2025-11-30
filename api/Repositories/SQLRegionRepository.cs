using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly ApplicationDBContext dBContext;

        public SQLRegionRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dBContext.Region.AddAsync(region);
            await dBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dBContext.Region.FindAsync(id);
            if (existingRegion == null)
            {
                return null;
            }
            dBContext.Region.Remove(existingRegion);
            await dBContext.SaveChangesAsync();
            return existingRegion; ;
        }

        //Repository
        public async Task<List<Region>> GetAllAsync()
        {
            return await dBContext.Region.ToListAsync();

        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dBContext.Region.FindAsync(id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dBContext.Region.FindAsync(id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dBContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}