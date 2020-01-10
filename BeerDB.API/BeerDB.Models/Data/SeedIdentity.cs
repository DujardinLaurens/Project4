using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeerDB.Models.Data
{
    public class SeedIdentity
    {
        private readonly UserManager<BeerDbUser> userMgr;
        private readonly RoleManager<IdentityRole> roleMgr;

        public SeedIdentity(UserManager<BeerDbUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            this.userMgr = userMgr;
            this.roleMgr = roleMgr;
        }

        public async Task SeedIdentityMobileAppsAPI()
        {
            var user = await userMgr.FindByNameAsync("LaurensDujardin");

            // Add User
            if (user == null)
            {
                if (!(await roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                    await roleMgr.CreateAsync(role);
                }

                user = new BeerDbUser()
                {
                    UserName = "LaurensDujardin",
                    FirstName = "Laurens",
                    LastName = "Dujardin",
                    Email = "Laurens.Dujardin@howest.be" 
                };

                var userResult = await userMgr.CreateAsync(user, "LaurensDujardinP@ssw0rd");
                var roleResult = await userMgr.AddToRoleAsync(user, "Admin");

                if (!userResult.Succeeded || !roleResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }
        }
    }
}
