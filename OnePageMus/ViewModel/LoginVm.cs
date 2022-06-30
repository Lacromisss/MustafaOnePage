using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnePageMus.ViewModel
{
    public class LoginVm
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
