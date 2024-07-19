using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Interfaces
{
    public interface IMaterialRepository : IGenericRepository<Material>
    {
        public Task<Material?> GetMaterialById(int id);
        public Task<List<Material>?> GetAllMaterial();
        public Task<bool?> CreateMaterial(Material material);

        public Task<bool?> UpdateMaterial(int id,Material material);
        Task<bool?> DeleteMaterial(int id);
    }
}
