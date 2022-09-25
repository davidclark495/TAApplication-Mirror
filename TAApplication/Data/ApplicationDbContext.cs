/*
	Author: Robert Davidson
	Partner: David Clark
	Date: 09/23/2022
	Course: CS 4540, University of Utah, School of Computing
	Copyright: CS 4540, David Clark and Robert Davidson - This work may not be copied for use in Academic Coursework.
	
  	I, David Clark, certify that I wrote this code from scratch and did not copy it in part or whole from 
  	another source.  Any references used in the completion of the assignment are cited in my README file.

	I, Robert Davidson, certify that I wrote this code from scratch and did not copy it in part or whole from
	another source. Any references used in the completion of the assignment are cited in my README file.

	File Contents

		TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO TODO 
*/

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using TAApplication.Areas.Identity.Data;

namespace TAApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<TAUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public async Task InitializeUsers(UserManager<TAUser> um, RoleManager<IdentityRole> rm)
        {
            this.Database.EnsureCreated();

            // Look for any TAUsers.
            if (um.Users.Any<TAUser>())
            {
                return;   // DB has been seeded
            }

            // Code snippet: https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users
            string[] roleNames = { "Admin", "Professor", "Applicant" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await rm.RoleExistsAsync(roleName);
                if (!roleExist)
                    await rm.CreateAsync(new IdentityRole(roleName));
            }
            // End code snippet

            // Adds Applicants to db and assigns role
            var users = new TAUser[]
            {
                new TAUser{Name="Jill Germain",Unid=7654321,ReferredTo="Pro",Email="professor@utah.edu"},
                new TAUser{Name="Jim Germain",Unid=1234567,ReferredTo="Admin Dude",Email="admin@utah.edu"},
                new TAUser{Name="User 0",Unid=0000000,ReferredTo="U0",Email="u0000000@utah.edu"},
                new TAUser{Name="User 1",Unid=0000001,ReferredTo="U1",Email="u0000001@utah.edu"},
                new TAUser{Name="User 2",Unid=0000002,ReferredTo="U2",Email="u0000002@utah.edu"}
            };
            foreach (TAUser user in users)
            {
                user.UserName = user.Email;
                user.EmailConfirmed = true;
                await um.CreateAsync(user, "123ABC!@#def");
                if (user.Unid == 1234567)
                    await um.AddToRoleAsync(user, "Admin");
                else if (user.Unid == 7654321)
                    await um.AddToRoleAsync(user, "Professor");
                else
                    await um.AddToRoleAsync(user, "Applicant");
            }
            //await um.CreateAsync(new TAUser { Name = "User 2", Unid = 0000002, ReferredTo = "U2", Email = "u0000002@utah.edu", UserName = "u0000002@utah.edu" }, "123ABC!@#def");
            this.SaveChanges();

            //foreach (TAUser u in um.Users)
            //{
            //    await um.AddPasswordAsync(u, "123ABC!@#def");
            //    string token = await um.GenerateEmailConfirmationTokenAsync(u);
            //    await um.ConfirmEmailAsync(u, token);

            //    if (u.Unid == 1234567)
            //        await um.AddToRoleAsync(u, "Admin");
            //    else if (u.Unid == 7654321)
            //        await um.AddToRoleAsync(u, "Professor");
            //    else
            //        await um.AddToRoleAsync(u, "Applicant");
            //}
            //this.SaveChanges();
        }
    }
}