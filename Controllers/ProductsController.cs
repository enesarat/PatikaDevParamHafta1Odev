using Microsoft.AspNetCore.Mvc;
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
            return null;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            return null;
        }

        [HttpPost]
        public IActionResult Create([FromBody] string value)
        {
            return null;
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] string value)
        {
            return null;
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            return null;
        }
    }
}
