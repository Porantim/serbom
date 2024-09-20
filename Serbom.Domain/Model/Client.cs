using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string? State { get; set; }

    public string City { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Document { get; set; } = null!;
}
