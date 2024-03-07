using dotnet.Context;
using dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(DbAppContext context) : ControllerBase
{
    private readonly DbAppContext _context = context;

    private bool UserExists(int dni)
    {
        return _context.Users.Any(u => u.Dni == dni);
    }

    [HttpGet]
    public async Task<ActionResult<User[]>> GetAll()
    {
        return Ok(
            await _context.Users.ToArrayAsync()
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetOne([FromRoute] int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound($"User with ID {id} not found");
        }

        return Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<User>> Create(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetOne),
                new { id = user.Id },
                user
            );
        }
        catch (DbUpdateException e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Update(int id, User user)
    {
        if (id != user.Dni)
        {
            return BadRequest("DNI in URL and body do not match");
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        catch (DbUpdateConcurrencyException e)
        {
            if (!UserExists(id))
            {
                return NotFound($"User with DNI {id} not found");
            }
            else
            {
                return BadRequest(e.InnerException?.Message);
            }
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound($"User with DNI {id} not found");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
