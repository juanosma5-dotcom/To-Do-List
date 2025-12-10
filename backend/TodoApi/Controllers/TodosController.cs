using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
	private readonly AppDbContext _db;
	public TodosController(AppDbContext db) { _db = db; }

	[HttpGet]
	public async Task<IActionResult> GetAll() => Ok(await _db.Todos.ToListAsync());

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var item = await _db.Todos.FindAsync(id);
		return item == null ? NotFound() : Ok(item);
	}

	[HttpPost]
	public async Task<IActionResult> Create(TodoItem input)
	{
		_db.Todos.Add(input);
		await _db.SaveChangesAsync();
		return CreatedAtAction(nameof(Get), new { id = input.Id }, input);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, TodoItem updated)
	{
		var item = await _db.Todos.FindAsync(id);
		if (item == null) return NotFound();
		item.Title = updated.Title;
		item.Description = updated.Description;
		item.IsCompleted = updated.IsCompleted;
		await _db.SaveChangesAsync();
		return NoContent();
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var item = await _db.Todos.FindAsync(id);
		if (item == null) return NotFound();
		_db.Todos.Remove(item);
		await _db.SaveChangesAsync();
		return NoContent();
	}
}
