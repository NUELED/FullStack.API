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
        [Route("create-player")]
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
        {
            var player = await _context.players.FirstOrDefaultAsync(u => u.id == id);

            if (player == null)
            { 
                return NotFound();
            }

            return Ok(player);  
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdatePlayer([FromRoute] Guid id, Player updatePlayerRequest)
        {
            var player = await _context.players.FirstOrDefaultAsync(u => u.id == id);

            if (player == null)
            {
                return NotFound();
            }

            player.Name = updatePlayerRequest.Name; 
            player.Email = updatePlayerRequest.Email;   
            player.Salary = updatePlayerRequest.Salary; 
            player.Phone = updatePlayerRequest.Phone;
            player.Club = updatePlayerRequest.Club;

            _context.players.Update(player);
            await _context.SaveChangesAsync();
            return Ok(player);

        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
        {
            var player = await _context.players.FirstOrDefaultAsync(u => u.id == id);

            if (player == null)
            {
                return NotFound();
            }

            _context.players.Remove(player);
            await _context.SaveChangesAsync();
            return Ok(player);

        }




    }
}
