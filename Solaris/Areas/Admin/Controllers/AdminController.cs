using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Solaris.Areas.Admin.Models;
using Solaris.Core.Entities;
using Solaris.Service.Services;
using Solaris.Service.Services.Interfaces;
using System.Data;




namespace Solaris.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{Constants.Roles.Master},{Constants.Roles.Admin}")]
    public class AdminController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        public AdminController(UserManager<ApplicationUser> userManager, IUserServices userServices, IRoleServices roleServices)
        {
            _userManager = userManager;
            _userServices = userServices;
            _roleServices = roleServices;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> UsersManager()
        {
            var users = await _userServices.GetUsers();

            return View(users);
        }

        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> AddUser()
        {
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            addUserViewModel.Id = Guid.NewGuid().ToString();
            return View(addUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Id = addUserViewModel.Id,
                    FirstName = addUserViewModel.FirstName,
                    LastName = addUserViewModel.LastName,
                    Email = addUserViewModel.Email,
                    NormalizedEmail = addUserViewModel.Email.ToUpper(),
                    UserName = addUserViewModel.Email,
                    NormalizedUserName = addUserViewModel.Email.ToUpper()

                };
                PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, addUserViewModel.Password);

                await _userServices.AddUser(user);

            }

            return RedirectToAction(nameof(UsersManager));
        }

        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> SoftDeleteUser(string id)
        {
            await _userServices.DeleteUser(id);

            return RedirectToAction(nameof(UsersManager));
        }

        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> RecoverUser(string id)
        {
            var user = await _userServices.GetUser(id);
            await _userServices.RecoverUser(user);

            return RedirectToAction(nameof(UsersManager));
        }

        [Authorize(Policy = Constants.Policies.RequireMaster)]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userServices.GetUser(id);
            var roles = await _roleServices.GetRoles();

            var userRoles = await _userManager.GetRolesAsync(user);

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
                var userRolesInDb = await _userManager.GetRolesAsync(user);

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
                            rolesToAdd.Add(role.Text);
                        }
                    }
                    else
                    {
                        if (assignedInDb != null)
                        {         
                            rolesToDelete.Add(role.Text);
                        }
                    }
                }

                if (rolesToAdd.Any())
                {
                    await _userManager.AddToRolesAsync(user, rolesToAdd);
                }

                if (rolesToDelete.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToDelete);
                }

                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.Email = data.Email;
                user.NormalizedEmail = data.Email.ToUpper();
                user.UserName = data.Email.ToLower();
                if (!String.IsNullOrWhiteSpace(data.Password))
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, data.Password);
                }
                await _userServices.UpdateUser(user);
            }

            return View(data);
        }
    }
}
