﻿using System.ComponentModel.DataAnnotations;

namespace Flipster.Modules.Identity.Domain.Token.Entities;

public class Token
{
    [Key]
    public string UserId { get; set; } = null!;
    public string? Value { get; set; }
    public DateTime? Expiry { get; set; }

    public bool Expired()
    {
        return DateTime.UtcNow >= Expiry;
    }
}