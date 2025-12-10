namespace TodoApi.Models
{
	// Models/User.cs (para demo, usuario simple)
	public class User
	{
		public int Id { get; set; }
		public string Email { get; set; } = "";
		public string PasswordHash { get; set; } = ""; // para producción: hash
	}
}
