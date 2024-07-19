
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Implementations
{
    public class DesignRepository : GenericRepository<Design>, IDesignRepository
    {
        private readonly JPOS_DatabaseContext context;

        public DesignRepository(JPOS_DatabaseContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<Design>?> GetAllDesign()
        {
            return await context.Designs.OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        public async Task<Design?> GetLastDesign()
        {
            var lastRequest = await context.Designs
               .OrderByDescending(r => r.DesignId)
               .FirstOrDefaultAsync();
            return lastRequest;
        }
    }
}
