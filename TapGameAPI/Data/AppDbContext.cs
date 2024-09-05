using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diamonds.API.Model;
using User.API.Model;
using Upgrades.API.Model;
using Microsoft.EntityFrameworkCore;
using UserUpgrade.API.Model;

namespace DiamondsEFCore.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<DiamondsModel> Diamonds { get; set; }
        public DbSet<UpgradesModel> Upgrades { get; set; }
        public DbSet<UserUpgradeModel> UserUpgrade { get; set; }
        public object UserModel { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("integer").ValueGeneratedOnAdd();
                entity.Property(e => e.Username).HasColumnName("username").HasColumnType("string");
                entity.Property(e => e.Email).HasColumnName("email").HasColumnType("string");
                entity.Property(e => e.Password).HasColumnName("password").HasColumnType("string");
            });

            modelBuilder.Entity<UpgradesModel>(entity =>
            {
                entity.ToTable("Upgrades");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("integer").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasColumnName("name").HasColumnType("string");
                entity.Property(e => e.Description).HasColumnName("description").HasColumnType("string");
                entity.Property(e => e.Cost).HasColumnName("cost").HasColumnType("int");
                entity.Property(e => e.Cost_increment).HasColumnName("cost_increment").HasColumnType("int");
                entity.Property(e => e.Modifier).HasColumnName("modifier").HasColumnType("char");
                entity.Property(e => e.Diamonds_increment).HasColumnName("diamonds_increment").HasColumnType("double");
                entity.Property(e => e.Type).HasColumnName("type").HasColumnType("string");
            });

            modelBuilder.Entity<DiamondsModel>(entity =>
            {
                entity.ToTable("Diamonds");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("integer").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").HasColumnType("integer");
                entity.Property(e => e.Diamonds).HasColumnName("total_diamonds").HasColumnType("double");
                entity.Property(e => e.DiamondsPerTap).HasColumnName("diamonds_per_tap").HasColumnType("double");
                entity.Property(e => e.DiamondsPerSecond).HasColumnName("diamonds_per_second").HasColumnType("double");
            });

            modelBuilder.Entity<UserUpgradeModel>(entity =>
            {
                entity.ToTable("User_Upgrade");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id").HasColumnType("integer").ValueGeneratedOnAdd();
                entity.Property(e => e.UserId).HasColumnName("user_id").HasColumnType("integer");
                entity.Property(e => e.UpgradeId).HasColumnName("upgrade_id").HasColumnType("integer");
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("integer");
                entity.Property(e => e.Current_cost).HasColumnName("current_cost").HasColumnType("integer");
            });

        }

    }
}