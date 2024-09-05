using System.Collections;
using DiamondsEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Upgrades.API.Model;
using User.API.Model;
using UserUpgrade.API.Model;

namespace User.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpgradesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UpgradesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUpgrades(List<UserUpgradeResponseModel> upgrades, int userId)
        {

            List<UserUpgradeModel> userUpgrades = await _context.UserUpgrade.Where(e => e.UserId == userId).ToListAsync();

            foreach (var itemNovo in upgrades)
            {
                foreach (var item in userUpgrades)
                {
                    if (item.UpgradeId == itemNovo.Id)
                    {
                        item.Amount = itemNovo.Amount;
                        item.Current_cost = itemNovo.Cost;
                    }

                    await _context.SaveChangesAsync();
                }
            }


            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserUpgradeResponseModel>>> GetUpgradesByUserId(int userId)
        {
            var userUpgrades = await (from u in _context.Upgrades
                                      join uu in _context.UserUpgrade on u.Id equals uu.UpgradeId
                                      where uu.UserId == userId
                                      select new UserUpgradeResponseModel
                                      {
                                          Id = u.Id,
                                          Name = u.Name,
                                          Description = u.Description,
                                          Cost = uu.Current_cost,
                                          Cost_increment = u.Cost_increment,
                                          Amount = uu.Amount,
                                          Modifier = u.Modifier,
                                          Diamonds_increment = u.Diamonds_increment,
                                          Type = u.Type
                                      }).ToListAsync();

            return Ok(userUpgrades);
        }

    }
}