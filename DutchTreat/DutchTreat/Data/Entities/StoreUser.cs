using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data.Entities
{
    public class StoreUser : IdentityUser   //inherting for default idenity  - entity core
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
