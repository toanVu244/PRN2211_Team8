using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IDesignService
    {
        public Task<List<Design>> GetAllDesignAsync();
        public Task<Design> GetDesignByIdAsync(int id);
        public Task<bool> CreateDesignAsync(Design design, int idProduct);
        public Task<bool> UpdateDesignAsync(Design design);
        public Task<bool> DeleteDesignAsync(int id);
    }
}
