using Microsoft.EntityFrameworkCore;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.Repository.MySQL.System
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
        }
        public virtual DbSet<Area> Area { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("area");

                entity.Property(e => e.AreaId).HasColumnType("varchar(50)");

                entity.Property(e => e.AreaCode).HasColumnType("varchar(50)");

                entity.Property(e => e.AreaName).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.DeleteMark).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.EnabledMark).HasColumnType("int(11)");

                entity.Property(e => e.Layer).HasColumnType("int(11)");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.ParentId).HasColumnType("varchar(50)");

                entity.Property(e => e.QuickQuery).HasColumnType("varchar(200)");

                entity.Property(e => e.SimpleSpelling).HasColumnType("varchar(200)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");
            });
        }

    }
}
