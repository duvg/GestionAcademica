﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestionAcademica.Models;
namespace GestionAcademica.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Override default AspNet Identity table names
            modelBuilder.Entity<AppUser>(entity => { entity.ToTable(name: "User"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
        }

        public DbSet<AppUser> User { get; set; }
        public DbSet<Category> Category { get; set; }
    }

    
}
