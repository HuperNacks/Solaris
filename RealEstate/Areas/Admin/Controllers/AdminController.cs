using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Areas.Admin.Models;
using RealEstate.Areas.Identity.Data;
using RealEstate.Core;
using RealEstate.Core.Repositories.IRepositories;
using System.Data;



namespace RealEstate.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Constants.Roles.Master},{Constants.Roles.Admin}")]
    public class AdminController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AdminController(IUnitOfWork unitOfWork, SignInManager<ApplicationUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
        
        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();

            return View(users);
        }

        
        
        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();


            var userModel = new EditAdminViewModel
            {
                User = user,
                Roles = roleItems,

            };

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditAdminViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);


            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>();


            foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {

                        //await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                        //Add role
                        rolesToAdd.Add(role.Text);

                    }
                }
                else
                {
                    if (assignedInDb != null)
                    {
                        //Remove Role
                        rolesToDelete.Add(role.Text);

                        //await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
                    }
                }
            }

            if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user, rolesToAdd);
            }

            if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }

            user.FirstName = data.User.FirstName;
            user.LastName = data.User.LastName;
            user.Email = data.User.Email;


            _unitOfWork.User.UpdateUser(user);

            return RedirectToAction("Edit", new { id = user.Id });
        }
    }
}
