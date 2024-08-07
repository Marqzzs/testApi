using Feirinha.Contexts;
using Feirinha.Domains;
using Feirinha.Interfaces;

namespace Feirinha.Repositorys
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly FeirinhaContexts _contexts;
        public ProductsRepository(FeirinhaContexts ctx)
        {
  
        _contexts = ctx;
        }
        public void Delete(Guid id)
        {
            var product = _contexts.Products.Find(id);
            if (product != null)
            {
                _contexts.Products.Remove(product);
                _contexts.SaveChanges();
            }
        }

        public List<Products> Get()
        {
            return _contexts.Products.ToList();
        }

        public void Patch(Products products, Guid id)
        {
            var produtoBuscado = _contexts.Products.FirstOrDefault(x => x.IdProduct == id);
            if (produtoBuscado != null)
            {
                // Atualiza as propriedades que não são nulas do produtoBuscado com os valores fornecidos
                if (!string.IsNullOrEmpty(products.Name))
                {
                    produtoBuscado.Name = products.Name;
                }

                if (products.Price != 0)
                {
                    produtoBuscado.Price = products.Price;
                }

                _contexts.Products.Update(produtoBuscado);
                _contexts.SaveChanges();
            }
        }


        public void Post(Products products)
        {
            _contexts.Products.Add(products);
            _contexts.SaveChanges();
        }

        Products IProductsRepository.GetById(Guid id)
        {
            return _contexts.Products.FirstOrDefault(x => x.IdProduct == id)!;
        }
    }
}
