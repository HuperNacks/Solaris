using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstate.Areas.Admin.Models;
using RealEstate.Core.Entities;
using RealEstate.Service.Services.Interfaces;
using System.Data;




namespace RealEstate.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Constants.Roles.Master},{Constants.Roles.Admin}")]
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        public AdminController(SignInManager<ApplicationUser> signInManager, IUserServices userServices, IRoleServices roleServices)
        {
            _signInManager = signInManager;
            _userServices = userServices;
            _roleServices = roleServices;
        }


        public async Task<IActionResult> Index()
        {

          
            var users = await _userServices.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> SoftDeleteUser(string id)
        {

            
            await _userServices.DeleteUser(id);
            
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> RecoverUser(string id)
        {
            var user = await _userServices.GetUser(id);
            await _userServices.RecoverUser(user);

            return RedirectToAction("Index", "Admin");
        }
        
        
        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userServices.GetUser(id);
            var roles = await _roleServices.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

            var roleItems = roles.Select(role =>
                new SelectListItem(
                    role.Name,
                    role.Id,
                    userRoles.Any(ur => ur.Contains(role.Name)))).ToList();


            var userModel = new EditAdminViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = roleItems,
            };

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAdminViewModel data)
        {

           var user = await _userServices.GetUser(data.Id);
           


            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);


                var rolesToAdd = new List<string>();
                var rolesToDelete = new List<string>();

                var passwordHasher = new PasswordHasher<ApplicationUser>();

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

                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.Email = data.Email;
                user.NormalizedEmail = data.Email.ToUpper();
                if (!String.IsNullOrWhiteSpace(data.Password))
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, data.Password);
                }

                await _userServices.UpdateUser(user);
                
                
                

            }

            //return RedirectToAction("Edit", new { id = user.Id });
            return View(data);

        }
    }
}
