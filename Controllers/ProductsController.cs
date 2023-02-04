using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaDevParamHafta1Odev.Models;

namespace PatikaDevParamHafta1Odev.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ProductsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            if (_dbContext.Products != null)
            {
                return Ok(_dbContext.Products.ToList()); // 200 + data
            }
            return NotFound(); // 404
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            if (_dbContext.Products.Find(id) != null)
            {
                return Ok(_dbContext.Products.Find(id)); // 200 + data
            }
            return NotFound(); // 404
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = _dbContext.Add(product);
                return CreatedAtAction("Get", new { id = product.Id }, newProduct); // 201 + data + header info for data location
            }
            return BadRequest(ModelState); // 400
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            if (_dbContext.Products.Find(id)!= null)
            {
                return Ok(_dbContext.Products.Update(product)); // 200 + data
            }
            return NotFound(); // 404 
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (_dbContext.Products.Find(id) != null)
            {
                var product =_dbContext.Products.Find(id);
                _dbContext.Products.Remove(product);
                return Ok(); // 200
            }
            return BadRequest(); // 400
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest(ModelState);
            }

            var product = _dbContext.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(product,ModelState);
           
            if (ModelState.IsValid)
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges();
                return Ok(product);
            }

            return BadRequest(ModelState);
        }
    }
}
