using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recodme.RD.BoraNow.DataAccessLayer.Properties;
using Recodme.RD.BoraNow.DataLayer.Quizzes;

namespace Recodme.RD.BoraNow.DataAccessLayer.Context
{
    class BoraNowContext : IdentityDbContext
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

        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryQuizAnswer> CategoryQuizAnswer  { get; set; }
        public DbSet<InterestPoint> InterestPoint  { get; set; }
        public DbSet<InterestPointCategory> InterestPointCategory  { get; set; }
        public DbSet<Quiz> Quiz  { get; set; }
        public DbSet<QuizAnswer> QuizAnswer  { get; set; }
        public DbSet<QuizQuestion> QuizQuestion  { get; set; }
        public DbSet<Result> Result  { get; set; }
        public DbSet<ResultInterestPoint> ResultInterestPoint  { get; set; }

    }
}
