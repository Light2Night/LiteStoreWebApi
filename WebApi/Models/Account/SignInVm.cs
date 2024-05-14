namespace WebApi.Models.Account;

public class SignInVm {
	/// <summary>
	/// Електронна пошта
	/// </summary>
	/// <example>admin@gmail.com</example>
	public string Email { get; set; } = null!;
	/// <summary>
	/// Пароль
	/// </summary>
	/// <example>admin</example>
	public string Password { get; set; } = null!;
}
