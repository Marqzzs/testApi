using Feirinha.Domains;
using System.Reflection.Metadata.Ecma335;

namespace Feirinha.Interfaces
{
    public interface IProductsRepository
    {
        public List<Products> Get();

        public Products GetById(Guid id);

        public void Post(Products products);

        public void Patch(Products products, Guid id);

        public void Delete(Guid id);
    }
}
