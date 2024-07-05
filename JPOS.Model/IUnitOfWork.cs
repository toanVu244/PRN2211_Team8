using JPOS.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBlogRepository Blogs { get; }
        IRequestRepository Requests { get; }
        IDesignRepository Designs { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        //IProductMaterialRepository ProductMaterials { get; }
        IFeedbackRepository Feedbacks { get; }
        IMaterialRepository Materials { get; }
        IPolicyRepository Policies { get; }

        IProductMaterialRepository ProductMaterials { get; }
        ITransactionRepo Transactions { get; }
        Task<int> CompleteAsync();
    }
}
