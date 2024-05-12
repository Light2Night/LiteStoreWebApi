﻿namespace WebApi.Constants;

public static class Roles {
	public const string Admin = "Admin";
	public const string User = "User";

	public static readonly IReadOnlyList<string> All = new List<string>() {
		Admin,
		User
	};
}
