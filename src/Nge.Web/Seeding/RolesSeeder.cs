using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nge.Web.Data;
using Nge.Web.Models;

namespace Nge.Web.Seeding
{
    public static class RolesSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await EnsureRole(serviceProvider, Roles.Administrator);
            }
        }

        private static async Task EnsureRole(IServiceProvider serviceProvider, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                 await roleManager.CreateAsync(new ApplicationRole(role));
            }
        }
    }
}
