using System;
using System.Collections.Generic;

namespace LogServer_OCSS.Domain.Entities;

public partial class Log
{
    public int Id { get; set; }

    public DateTime CreatTime { get; set; }

    public string Level { get; set; } = null!;

    public string Msg { get; set; } = null!;

    public string Controller { get; set; } = null!;

    public string Action { get; set; } = null!;

    public int? UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }
}
