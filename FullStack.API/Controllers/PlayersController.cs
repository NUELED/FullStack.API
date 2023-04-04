using FullStack.API.Data;
using FullStack.API.Migrations;
using FullStack.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [Route ("api/[controller]")]      
    [ApiController]
    public class PlayersController : Controller
    {

        private readonly FullstackDbContext _context;  
        
        public PlayersController( FullstackDbContext context)
        {
            _context = context; 
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPlayers()
        {
          var players = await _context.players.ToListAsync();

            return Ok(players);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPlayer([FromBody] Player playerRequest)
        {
            playerRequest.id = Guid.NewGuid();
            await _context.players.AddAsync(playerRequest);
            await _context.SaveChangesAsync(); 

            return Ok(playerRequest);   
        }
    }
}
