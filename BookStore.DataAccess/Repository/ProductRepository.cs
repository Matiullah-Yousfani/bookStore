using bookStore.Data;
using bookStore.DataAccess.Repository;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product p)
        {
            var formDB = _db.Products.FirstOrDefault(u => u.ProdID == p.ProdID);
            if (formDB != null)
            {
                formDB.Title = p.Title;
                formDB.Description = p.Description;
                formDB.ISBN = p.ISBN;
                formDB.Author = p.Author;
                formDB.Price = p.Price;
                formDB.ListPrice = p.ListPrice;
                formDB.CategoryID = p.CategoryID;
                formDB.Price50 = p.Price50;
                formDB.Price100 = p.Price100;
                if (p.ImageURL != null)
                {
                    formDB.ImageURL = p.ImageURL;
                }
            }
        }
    }
}
