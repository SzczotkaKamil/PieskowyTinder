using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PieskowyTinder.Data;
namespace PieskowyTinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DataContext _context;

        public DogsController(DataContext context)
        {
            _context=context; ;
        }
        [HttpGet]
        public async Task<ActionResult<List<Dogs>>> Get()
        {

            return Ok(await _context.Dogs.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Dogs>>> Get(int id)
        {
            var dogs =await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);
            if (dogs != null)
            {
                return Ok(dogs);
            }
            else return BadRequest("Dogs not found!");
        }
        [HttpPost]
        public async Task<ActionResult<List<Dogs>>> AddDog(Dogs dogs)
        {
            _context.Dogs.Add(dogs);
           await _context.SaveChangesAsync();
            return Ok(await _context.Dogs.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Dogs>>> UpdateDog(Dogs request)
        {
            var dogs =await _context.Dogs.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (dogs != null)
            {
                dogs.Name = request.Name;
                dogs.Description = request.Description;
                dogs.DateOfBirth = request.DateOfBirth;
                dogs.Character = request.Character;
                await _context.SaveChangesAsync();
                return Ok(await _context.Dogs.ToListAsync());
            }
            else return BadRequest("Dog not found!");
            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Dogs>>> DeleteDog(int id)
        {
            var dogs =await _context.Dogs.FirstOrDefaultAsync(x => x.Id == id);
            if (dogs != null)
            {
                _context.Dogs.Remove(dogs);
               await _context.SaveChangesAsync();
                return Ok(await _context.Dogs.ToListAsync());
            }
            else return BadRequest("Dog not found!");

        }
    }
}
