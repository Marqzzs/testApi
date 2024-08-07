﻿using Feirinha.Domains;
using Feirinha.Interfaces;
using Feirinha.Repositorys;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestApixUnit
{
    public class ProductsTest
    {
        [Fact]//indica que um metodo e de teste de unidade
        public void Get()
        {
            //Arrange : Cenario

            //Lista de Produtos
            var product = new List<Products>
            {
                new Products {IdProduct = Guid.NewGuid(), Name = "Melancia", Price = 10},
                new Products {IdProduct = Guid.NewGuid(), Name = "Melao", Price = 7},
                new Products {IdProduct = Guid.NewGuid(), Name = "Banana", Price = 1},
            };

            //cria um obj de simulacao do tipo repository
            var mockRepository = new Mock<IProductsRepository>();

            //configura o metodo get para retornar a lista de produtos "mock"
            mockRepository.Setup(x => x.Get()).Returns(product);

            //Act : Agir

            //chama o metodo get() e armazena o resultado em result
            var result = mockRepository.Object.Get();

            //Assert: Provar

            //prova se o resultado esperado e igual ao resultado obtido atraves da busca
            Assert.Equal(3, result.Count);
        }

        [Fact]//indica que um metodo e de teste de unidade
        public void GetById()
        {
            //Arrange : Cenario

            //Lista de Produtos
            Products products = new Products { IdProduct = Guid.NewGuid(), Name = "Kiwi", Price = 0.50m };

            var productList = new List<Products>();

            //cria um obj de simulacao do tipo repository
            var mockRepository = new Mock<IProductsRepository>();

            //configura o metodo get para retornar a lista de produtos "mock"
            mockRepository.Setup(x => x.GetById(products.IdProduct)).Returns(products);

            //Act : Agir

            //chama o metodo get() e armazena o resultado em result
            var result = mockRepository.Object.GetById(products.IdProduct);

            //Assert: Provar

            //prova se o resultado esperado e igual ao resultado obtido atraves da busca
            Assert.Equal(products, result);
        }

        [Fact]
        public void Post()
        {
            Products products = new Products { IdProduct = Guid.NewGuid(), Name = "Kiwi", Price = 0.50m };

            var productList = new List<Products>();

            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.Post(products)).Callback<Products>(p => productList.Add(products));

            mockRepository.Object.Post(products);

            Assert.Contains(products, productList);
        }

        [Fact]
        public void Delete()
        {
            Products products = new Products { IdProduct = Guid.NewGuid(), Name = "Kiwi", Price = 0.50m };

            var productList = new List<Products>();

            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.Delete(products.IdProduct)).Callback<Guid>(p => productList.Remove(products));

            mockRepository.Object.Delete(products.IdProduct);

            Assert.DoesNotContain(products, productList);
        }

        [Fact]
        public void Patch()
        {
            var originalProduct = new Products { IdProduct = Guid.NewGuid(), Name = "Melancia", Price = 10 };
            var updatedProduct = new Products { IdProduct = Guid.NewGuid(), Name = "Melancia Atualizada", Price = 12 };

            var productList = new List<Products> { originalProduct };

            var mockRepository = new Mock<IProductsRepository>();

            mockRepository.Setup(x => x.Patch(originalProduct,originalProduct.IdProduct)).Callback<Products, Guid>((prod, Guid) =>
            {
                var productUpdate = productList.Find(p => p.IdProduct == updatedProduct.IdProduct);

                if (productUpdate != null)
                {
                    productUpdate.Name = updatedProduct.Name;
                    productUpdate.Price = updatedProduct.Price;
                }
            });

            var productInList = productList.Find(p => p.IdProduct == updatedProduct.IdProduct);
            Assert.NotNull(productInList);
            Assert.Equal("Melancia Atualizada", productInList.Name);
            Assert.Equal(12, productInList.Price);
        }
    }
}