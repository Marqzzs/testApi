using Feirinha.Domains;
using Feirinha.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsRepository repository;

        public ProductController(IProductsRepository productRepository)
        {
            repository = productRepository;
        }


        [HttpGet]
        public ActionResult GetAll() => Ok(repository.Get());

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {

            try
            {
                var content = repository.GetById(id);
                return Ok(content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return NotFound();
                throw;
            }

        }

        [HttpDelete]
        public ActionResult DeleteById(Guid id)
        {
            try
            {
                repository.Delete(id);
                return NoContent();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return NotFound();
                throw;
            }
        }


        [HttpPost]
        public ActionResult Create(Products product)
        {

            try
            {
                repository.Post(product);
                return StatusCode(201, product);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                throw;
            }
        }


        [HttpPatch("{id}")]
        public ActionResult Update(Guid id, Products product)
        {
            try
            {
                if (id != product.IdProduct)
                {
                    return BadRequest("The id and the object's id must be the same");
                }

                // Chama o método Patch no repositório
                repository.Patch(product, id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return NotFound();
            }
        }
    }
}