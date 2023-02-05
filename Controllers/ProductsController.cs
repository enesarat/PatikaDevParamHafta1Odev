using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatikaDevParamHafta1Odev.Models;

namespace PatikaDevParamHafta1Odev.Controllers
{
    [Route("api/[controller]")]
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
            try
            {
                var products = _dbContext.Products;
                if (products != null)
                {
                    return Ok(_dbContext.Products.ToList()); // 200 + data
                }
                return NotFound(); // 404
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    return Ok(_dbContext.Products.Find(id)); // 200 + data
                }
                return NotFound(); // 404
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(product);
                    _dbContext.SaveChanges();
                    return CreatedAtAction(nameof(GetById), new { id = product.Id }, product); // 201 + data + header info for data location
                }
                return BadRequest(ModelState); // 400
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                var productU = _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
                if (productU != null)
                {
                    product.Id= productU.Id;
                    _dbContext.Products.Update(product);
                    _dbContext.SaveChanges();
                    return Ok(product); // 200 + data
                }
                return NotFound(); // 404 
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    _dbContext.Products.Remove(product);
                    _dbContext.SaveChanges();
                    return Ok(); // 200
                }
                return BadRequest(); // 400
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Product> patchDocument)
        {
            try
            {
                if (patchDocument == null)
                {
                    return BadRequest(ModelState);
                }

                var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                patchDocument.ApplyTo(product, ModelState);

                if (ModelState.IsValid)
                {
                    _dbContext.Products.Update(product);
                    _dbContext.SaveChanges();
                    return Ok(product);
                }

                return BadRequest(ModelState);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
    }
}
