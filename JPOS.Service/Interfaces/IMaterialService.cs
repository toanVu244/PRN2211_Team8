using JPOS.Model.Entities;
using JPOS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IMaterialService 
    {
        public Task<Material?> GetmaterialByID(int id);
        public Task<List<Material>?> GetAllmaterial();
        public Task<bool?> CreateMaterial(MaterialModel material);

        public Task<bool?> UpdateMaterial(MaterialModel material);
        public Task<bool?> DeleteMaterial(int id);

    }
}
