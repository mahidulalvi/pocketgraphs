﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using PocketGraphs.Models.Domain;
using System.Configuration;

namespace PocketGraphs.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Post> AllOfMyPosts { get; set; }
        public virtual List<Comment> AllOfMyComments { get; set; }

        public ApplicationUser()
        {
            AllOfMyPosts = new List<Post>();
            AllOfMyComments = new List<Comment>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PocketGraphsDBConnectionString", throwIfV1Schema: false)
        {
        }

        public DbSet<Post> Posts { set; get; }

        public DbSet<Comment> Comments { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}