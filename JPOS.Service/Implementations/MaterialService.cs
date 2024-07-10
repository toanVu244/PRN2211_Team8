using AutoMapper;
using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Model.Models.AppConfig;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class MaterialService : IMaterialService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public MaterialService(IUnitOfWork unitOfWork, IMapper map)
        {
            _unitOfWork = unitOfWork;
            _map = map;
        }
        public async Task<bool?> CreateMaterial(MaterialModel material)
        {
            try
            {
                if (material == null)
                {
                    return false;
                }
               return await _unitOfWork.Materials.CreateMaterial(_map.Map<Material>(material));


            }catch (Exception ex)
            {
                Console.WriteLine("Create Material service : ", ex.ToString());
                return false;   
            }
        }

        public async Task<bool?> DeleteMaterial(int id)
        {
            try
            {
                var material = await _unitOfWork.Materials.GetByIdAsync(id);
                material.Status = "Unavailable";
                return await _unitOfWork.Materials.UpdateMaterial(id,material);
            }
            catch (Exception ex)
            {
                throw;
                
            }

        }
       
        public async Task<List<Material>?> GetAllmaterial()
        {
            try
            {
               return await _unitOfWork.Materials.GetAllMaterial();

            }catch (Exception ex)
            {
                Console.WriteLine("get all Material : ", ex.ToString());
                return new List<Material>();
            }
        }

        public async Task<Material?> GetmaterialByID(int id)
        {
            try { 
                if(id == null)
                {
                    return new Material();
                }
                return await _unitOfWork.Materials.GetMaterialById(id);
            
            }catch (Exception ex)
            {
                Console.WriteLine("get a Material service : ", ex.ToString());
                return new Material();
            }
        }

        public async Task<bool?> UpdateMaterial(MaterialModel material)
        {
            try
            {
                if(material != null) { 
                return await _unitOfWork.Materials.UpdateMaterial(material.MaterialId,_map.Map<Material>(material));
                }
                return false;
            }catch (Exception ex)
            {
                Console.WriteLine("update Material : ", ex.ToString());
                return false;
            }
        }
    }
}
