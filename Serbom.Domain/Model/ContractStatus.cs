using System;
using System.Collections.Generic;

namespace Serbom.Domain.Model;

public partial class ContractStatus
{
    public sbyte Id { get; set; }

    public string Description { get; set; } = null!;
}
