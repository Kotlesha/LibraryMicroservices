﻿namespace Auth.DAL.Models;

public class Account
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
}
