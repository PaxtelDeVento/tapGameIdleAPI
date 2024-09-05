using Diamonds.API.Model;
using DiamondsEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace User.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiamondsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DiamondsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<DiamondsModel>> CreateDiamonds(DiamondsModel newDiamond)
        {
            _context.Diamonds.Add(newDiamond);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiamondsModel>>> GetDiamonds()
        {
            return await _context.Diamonds.ToListAsync();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDiamonds(DiamondsModel diamonds)
        {
            var d = await _context.Diamonds.FindAsync(diamonds.Id);
            if (d == null)
            {
                return NotFound();
            }

            d.Diamonds = diamonds.Diamonds;
            d.DiamondsPerTap = diamonds.DiamondsPerTap;
            d.DiamondsPerSecond = diamonds.DiamondsPerSecond;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("all")]
        public async Task<ActionResult<DiamondsModel>> DeleteAllDiamonds()
        {

            var allDiamonds = await _context.Diamonds.ToListAsync();
            _context.Diamonds.RemoveRange(allDiamonds);
            await _context.SaveChangesAsync();

            return Accepted("Apagado com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DiamondsModel>> DeleteDiamondsbyId(int id)
        {
            var a = await _context.Diamonds.SingleOrDefaultAsync(u => u.Id == id);
            if (a == null)
            {
                return NotFound("Diamantes não encontrados com esse id");
            }
            _context.Diamonds.Remove(a);
            await _context.SaveChangesAsync();

            return Accepted("Apagado com sucesso");
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<DiamondsModel>> GetDiamondsByUserId(int userId)
        {
            var diamonds = await _context.Diamonds.SingleOrDefaultAsync(u => u.UserId == userId);
            System.Console.WriteLine(diamonds);
            if (diamonds == null)
            {
                return NotFound();
            }
            else
            {
                return diamonds;
            }
        }
    }
}