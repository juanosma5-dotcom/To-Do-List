using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;


public class TodosControllerTests
{
	[Fact]
	public async Task GetAll_ReturnsSeededTodos()
	{
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(Guid.NewGuid().ToString())
			.Options;

		using var db = new AppDbContext(options);
		db.Todos.Add(new TodoItem { Title = "t1" });
		db.SaveChanges();

		var ctrl = new TodosController(db);

		var result = await ctrl.GetAll() as OkObjectResult;

		var list = Assert.IsType<List<TodoItem>>(result.Value);

		Assert.Equal(1, list.Count);
	}
}
