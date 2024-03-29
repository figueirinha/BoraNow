﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Properties;
using Recodme.RD.BoraNow.DataLayer.Feedbacks;
using Recodme.RD.BoraNow.DataLayer.Meteo;
using Recodme.RD.BoraNow.DataLayer.Newsletters;
using Recodme.RD.BoraNow.DataLayer.Quizzes;
using Recodme.RD.BoraNow.DataLayer.Users;
using System;

namespace Recodme.RD.BoraNow.DataAccessLayer.Context
{
    public class BoraNowContext : IdentityDbContext<User, Role, Guid>
    {
        public BoraNowContext() : base()
        {

        }

        public BoraNowContext(DbContextOptions<BoraNowContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Resources.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CategoryInterestPoint> Category { get; set; }
        public DbSet<InterestPoint> InterestPoint  { get; set; }
        public DbSet<InterestPointCategoryInterestPoint> InterestPointCategory  { get; set; }
        public DbSet<Quiz> Quiz  { get; set; }
        public DbSet<QuizAnswer> QuizAnswer  { get; set; }
        public DbSet<QuizQuestion> QuizQuestion  { get; set; }
        public DbSet<Result> Result  { get; set; }
        public DbSet<ResultInterestPoint> ResultInterestPoint  { get; set; }

        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<InterestPointNewsletter> InterestPointNewsletter { get; set; }

        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Meteorology> Meteorology { get; set; }

        public DbSet<Visitor> Visitor { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Profile> Profile { get; set; }


    }
}
