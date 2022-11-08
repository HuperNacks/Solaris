using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Areas.Identity.Data;

namespace RealEstate.Areas.Admin.Models
{
    public class EditAdminViewModel
    {
        public ApplicationUser User { get; set; }
        public IList<SelectListItem> Roles { get; set; }
    }
}
