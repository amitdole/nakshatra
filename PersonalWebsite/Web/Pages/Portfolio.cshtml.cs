using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;

namespace AspnetRun.Web.Pages
{
    public class PortfolioModel : PageModel
    {
        public void OnPost(Contact contact)
        {
            var emailAddress = contact.Email;
            // do something with emailAddress
        }
    }
}