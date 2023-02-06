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

        // This endpoint provides to get all data of product entity as a list.
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
                    return Ok(products); // 200 + data
                }
                return NotFound(); // 404
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        //-------------------------------------------------------------------- GET FROM BODY ----------------------------------------------------//
        // This endpoint provides to get the data of product which exist with given id information. (with binding over body)
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    return Ok(product); // 200 + data
                }
                return NotFound(); // 404
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        //-------------------------------------------------------------------- GET FROM QUERY ----------------------------------------------------//
        // This endpoint provides to get the data of product which exist according to given id information. (with binding over query string)
        [HttpGet]
        [Route("GetByIdFromQuery")]
        public IActionResult Get([FromQuery] int id)
        {

            try
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    return Ok(product); // 200 + data
                }
                return NotFound(); // 404
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        //-------------------------------------------------------------------- FILTER FROM BODY ----------------------------------------------------//
        // This endpoint provides to get the data of product which exist according to given properties as filtered list. (with binding over body)
        [HttpGet]
        [Route("{name}/{saleStatus}")]
        public IActionResult Get(string name, string categoryName, int price, int quantity, bool saleStatus)
        {
            try
            {
                List<Product> products = _dbContext.Products.ToList();
                if (name != null)
                {
                    products = products.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
                }
                if (categoryName != null)
                {
                    products = products.Where(x => x.CategoryName.ToUpper().Contains(categoryName.ToUpper())).ToList();

                }
                if (price != 0)
                {
                    products = products.Where(x => x.Price == price).ToList();

                }
                if (quantity != 0)
                {
                    products = products.Where(x => x.Quantity == quantity).ToList();
                }
                products = products.Where(x => x.SaleStatus == saleStatus).ToList();

                return Ok(products);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        //-------------------------------------------------------------------- FILTER FROM QUERY ----------------------------------------------------//
        // This endpoint provides to get the data of product which exist according to given properties as filtered list. (with binding over query string)
        [HttpGet]
        [Route("GetByFilterFromQuery")]
        public IActionResult GetByFilter([FromQuery] string name, [FromQuery] string categoryName, [FromQuery] int price, [FromQuery] int quantity, [FromQuery] bool saleStatus)
        {
            try
            {
                List<Product> products = _dbContext.Products.ToList();
                if (name != null)
                {
                    products = products.Where(x => x.Name.ToUpper().Contains(name.ToUpper())).ToList();
                }
                if (categoryName != null)
                {
                    products = products.Where(x => x.CategoryName.ToUpper().Contains(categoryName.ToUpper())).ToList();

                }
                if (price != 0)
                {
                    products = products.Where(x => x.Price == price).ToList();

                }
                if (quantity != 0)
                {
                    products = products.Where(x => x.Quantity == quantity).ToList();
                }
                products = products.Where(x => x.SaleStatus == saleStatus).ToList();

                return Ok(products);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

        //-------------------------------------------------------------------- CREATE FROM BODY ----------------------------------------------------//
        // This endpoint provides to create a record of product according to given properties. (with binding over body)
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

        //-------------------------------------------------------------------- CREATE FROM QUERY ----------------------------------------------------//
        // This endpoint provides to create a record of product according to given properties. (with binding over query string)
        [HttpPost]
        [Route("CreateFromQuery")]

        public IActionResult CreateFromQuery([FromQuery] Product product)
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

        //-------------------------------------------------------------------- UPDATE FROM BODY ----------------------------------------------------//
        // This endpoint provides to update the record of product according to given id which exist with given product data. (with binding over body)
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                var productU = _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
                if (productU != null)
                {
                    product.Id = productU.Id;
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

        //-------------------------------------------------------------------- UPDATE FROM QUERY ----------------------------------------------------//
        // This endpoint provides to update the record of product according to given id which exist with given product data. (with binding over query string)
        [HttpPut]
        [Route("UpdateFromQuery")]
        public IActionResult UpdateFromQuery([FromQuery] Product product)
        {
            try
            {
                var productU = _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);
                if (productU != null)
                {
                    product.Id = productU.Id;
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

        //-------------------------------------------------------------------- DELETE FROM BODY ----------------------------------------------------//
        // This endpoint provides to delete the record of product according to given id which exist with given product data. (with binding over body)
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

        //-------------------------------------------------------------------- DELETE FROM QUERY ----------------------------------------------------//
        // This endpoint provides to delete the record of product according to given id which exist with given product data. (with binding over query string)
        [HttpDelete]
        [Route("DeleteFromQuery")]
        public IActionResult DeleteFromQuery([FromQuery] int id)
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

        //-------------------------------------------------------------------- PATCH FROM BODY ----------------------------------------------------//
        // This endpoint provides to patch(to local update in this endpoint) the record of product according to given id which exist with given product data. (with binding over body)
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

        //-------------------------------------------------------------------- PATCH FROM QUERY ----------------------------------------------------//
        // This endpoint provides to patch(to local update in this endpoint) the record of product according to given id which exist with given product data. (with binding over query string)
        [HttpPatch]
        [Route("PatchFromQuery")]
        public IActionResult PatchFromQuery([FromQuery] int id, [FromQuery] JsonPatchDocument<Product> patchDocument)
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
