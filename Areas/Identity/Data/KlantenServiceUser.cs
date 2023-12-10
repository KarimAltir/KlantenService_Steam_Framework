using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KlantenService_Steam_Framework.Areas.Identity.Data;

// Add profile data for application users by adding properties to the KlantenServiceUser class
public class KlantenServiceUser : IdentityUser
{
    //adding first name
    public string FirstName { get; set; }

    //adding last name
    public string LastName { get; set; }
}

