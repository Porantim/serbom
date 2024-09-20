using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class Amendment
{
    public int Id { get; set; }

    public int Contract { get; set; }

    public bool Deleted { get; set; }

    public string Number { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public decimal Value { get; set; }

    public string? Conditions { get; set; }
}
