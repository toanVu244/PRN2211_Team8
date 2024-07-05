using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Interfaces
{
    public interface IBlogRepository:IGenericRepository<Blog>   
    {
        public Task<Blog> GetBlogByID(int blogID);  
    }
}
