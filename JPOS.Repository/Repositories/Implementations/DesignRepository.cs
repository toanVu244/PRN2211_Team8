
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
        private static DesignRepository _instance;

        public static DesignRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DesignRepository();
                }
                return _instance;
            }
        }

        public async Task<List<Design>?> GetAllDesign()
        {
            return await _context.Designs.OrderByDescending(p => p.CreateDate).ToListAsync();
        }

        public async Task<Design?> GetLastDesign()
        {
            var lastRequest = await _context.Designs
               .OrderByDescending(r => r.DesignId)
               .FirstOrDefaultAsync();
            return lastRequest;
        }
    }
}
