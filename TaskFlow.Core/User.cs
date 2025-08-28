using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Core;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Email { get; set; }


    // Navigation
    public ICollection<Project>? Projects { get; set; }
}


