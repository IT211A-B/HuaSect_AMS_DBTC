using System.ComponentModel.DataAnnotations;
using HuaSect_AMS_DBTCclasslib.Models;
using Microsoft.AspNetCore.Identity;

namespace HuaSect_AMS_DBTCclasslib;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string? MiddleName { get; set; }

    public string? Suffix { get; set; }

    public ApplicationUser()
    {

    }

    public void Update(int id, DateTime date, bool status)
    {
    }
}
