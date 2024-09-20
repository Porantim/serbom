using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class Contract
{
    public int Id { get; set; }

    public int Client { get; set; }

    public sbyte Type { get; set; }

    public string Number { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public decimal Value { get; set; }

    public sbyte Status { get; set; }

    public string? Conditions { get; set; }
}
