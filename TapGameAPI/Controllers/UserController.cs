using System.Collections;
using DiamondsEFCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User.API.Model;

namespace User.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateUser(UserModel newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Accepted();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            return await _context.Users.ToListAsync();

        }

        [HttpDelete("all")]
        public async Task<ActionResult<UserModel>> DeleteAllUsers()
        {

            var allDiamonds = await _context.Users.ToListAsync();
            _context.Users.RemoveRange(allDiamonds);
            await _context.SaveChangesAsync();

            return Accepted("Apagado com sucesso");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteDiamondsbyId(int id)
        {
            var a = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (a == null)
            {
                return NotFound("Usuário não encontrados com esse id");
            }
            _context.Users.Remove(a);
            await _context.SaveChangesAsync();

            return Accepted("Apagado com sucesso");
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<UserModel>> GetUsersByEmail(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);


            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return user;
            }
        }
        [HttpPost("{login}")]
        public async Task<ActionResult<UserModel>> VerifyLogin(UserModel login)
        {
            Console.WriteLine(login.Email);
            Console.WriteLine(login.Password);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == login.Email);

            if (user == null) return Unauthorized("Usuário não existe");

            if (login.Password != user.Password) return Unauthorized("Senha incorreta");

            return user;

        }

    }




}