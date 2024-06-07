using OceanCareChat.Data;
using OceanCareChat.Dtos;
using OceanCareChat.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OceanCareChat.Dtos.User;
using static OceanCareChat.Dtos.User.LoginDTO;

namespace OceanCareChat.Controller
{
    [Route("/users")]
    [ApiController]
    public class OceanUserController : ControllerBase
    {
        private readonly DataContext _context;

        public OceanUserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("/list")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var users = await _context.OceanUser.ToListAsync();
            
            var usersDTO = users.Select(u => new UserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                ReportedTrash = u.ReportedTrash
            });

            return Ok(usersDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _context.OceanUser.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var userDTO = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                ReportedTrash = user.ReportedTrash
            };

            return Ok(userDTO);
        }

        [HttpPost("/register")]
        public async Task<ActionResult<UserDTO>> AddUser(RegisterUserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new OceanUser
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                ReportedTrash = "0"
            };

            _context.OceanUser.Add(newUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        [HttpPut("/update/{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, UpdateUserDTO user)
        {
            var userToUpdate = await _context.OceanUser.FirstOrDefaultAsync(x => x.Id == id);
            if (userToUpdate == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;

            await _context.SaveChangesAsync();
            return Ok(userToUpdate);
        }

        [HttpDelete("/delete/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.OceanUser.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.OceanUser.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDto user)
        {
            var userToLogin = await _context.OceanUser.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            if (userToLogin == null)
            {
                return Unauthorized();
            }

            var loggedUser = new UserDTO
            {
                Id = userToLogin.Id,
                Name = userToLogin.Name,
                Email = userToLogin.Email,
                ReportedTrash = userToLogin.ReportedTrash
            };

            return Ok(loggedUser);
        }
    }
}