
using FundooModel.Labels;
using FundooModel.Notes;
using FundooModel.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace FundooRepository.Context
{

    public class UserDbContext : IdentityDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<Register> Register { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<label> Labels { get; set; }
    }

}
