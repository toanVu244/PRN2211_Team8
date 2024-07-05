using JPOS.Model.Entities;
using JPOS.Model.Repositories.Implementations;
using JPOS.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JPOS_ProjectContext _context;

        public UnitOfWork(JPOS_ProjectContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Blogs = new BlogRepository(_context);
            Requests = new RequestRepository(_context);
            Designs = new DesignRepository(_context);
            Products = new ProductRepository(_context);
            Feedbacks = new FeedbackRepository(_context);
            Categories = new CategoryRepository(_context);
            ProductMaterials = new ProductMaterialRepository(_context);
            Materials = new MaterialRepository(_context);
            Policies = new PolicyRepository(_context);
            Transactions = new TransactionRepo(_context);
        }
        public IUserRepository Users { get; private set; }
        public IBlogRepository Blogs { get; private set; }
        public IRequestRepository Requests { get; private set; }
        public IDesignRepository Designs { get; private set; }
        public IProductRepository Products { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IFeedbackRepository Feedbacks { get; private set; }
        public IProductMaterialRepository ProductMaterials { get; private set; }
        public IMaterialRepository Materials { get; private set; }

        public IPolicyRepository Policies { get; private set; }
        public ITransactionRepo Transactions { get; private set; }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
