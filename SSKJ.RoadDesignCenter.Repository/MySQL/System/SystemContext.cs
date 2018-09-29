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
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleButton> Modulebutton { get; set; }
        public virtual DbSet<ModuleColumn> Modulecolumn { get; set; }
        public virtual DbSet<ModuleForm> Moduleform { get; set; }
        public virtual DbSet<SysLog> Syslog { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserProject> UserProject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("area");

                entity.Property(e => e.AreaId).HasColumnType("varchar(50)");

                entity.Property(e => e.AreaCode).HasColumnType("varchar(50)");

                entity.Property(e => e.AreaName).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreateUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.DeleteMark).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.EnabledMark).HasColumnType("int(11)");

                entity.Property(e => e.Layer).HasColumnType("int(11)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.ModifyUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.ParentId).HasColumnType("varchar(50)");

                entity.Property(e => e.QuickQuery).HasColumnType("varchar(200)");

                entity.Property(e => e.SimpleSpelling).HasColumnType("varchar(200)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("module");

                entity.Property(e => e.ModuleId).HasColumnType("varchar(50)");

                entity.Property(e => e.AllowDelete).HasColumnType("int(11)");

                entity.Property(e => e.AllowEdit).HasColumnType("int(11)");

                entity.Property(e => e.AllowExpand).HasColumnType("int(11)");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreateUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.DeleteMark).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.EnabledMark).HasColumnType("int(11)");

                entity.Property(e => e.FullName).HasColumnType("varchar(50)");

                entity.Property(e => e.Icon).HasColumnType("varchar(50)");

                entity.Property(e => e.IsMenu).HasColumnType("int(11)");

                entity.Property(e => e.IsPublic).HasColumnType("int(11)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.ModifyUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.ParentId).HasColumnType("varchar(50)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");

                entity.Property(e => e.Target).HasColumnType("varchar(20)");

                entity.Property(e => e.UrlAddress).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<ModuleButton>(entity =>
            {
                entity.ToTable("modulebutton");

                entity.Property(e => e.ModuleButtonId).HasColumnType("varchar(50)");

                entity.Property(e => e.ActionAddress).HasColumnType("varchar(200)");

                entity.Property(e => e.FullName).HasColumnType("varchar(50)");

                entity.Property(e => e.Icon).HasColumnType("varchar(50)");

                entity.Property(e => e.ModuleId).HasColumnType("varchar(50)");

                entity.Property(e => e.ParentId).HasColumnType("varchar(50)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<ModuleColumn>(entity =>
            {
                entity.ToTable("modulecolumn");

                entity.Property(e => e.ModuleColumnId).HasColumnType("varchar(50)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.FullName).HasColumnType("varchar(50)");

                entity.Property(e => e.ModuleId).HasColumnType("varchar(50)");

                entity.Property(e => e.ParentId).HasColumnType("varchar(50)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<ModuleForm>(entity =>
            {
                entity.HasKey(e => e.FormId);

                entity.ToTable("moduleform");

                entity.Property(e => e.FormId).HasColumnType("varchar(50)");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreateUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.DeleteMark).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.EnabledMark).HasColumnType("int(11)");

                entity.Property(e => e.FormJson).HasColumnType("longtext");

                entity.Property(e => e.FullName).HasColumnType("varchar(50)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.ModifyUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.ModuleId).HasColumnType("varchar(50)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<SysLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("syslog");

                entity.Property(e => e.LogId).HasColumnType("varchar(50)");

                entity.Property(e => e.Browser).HasColumnType("varchar(50)");

                entity.Property(e => e.CategoryId).HasColumnType("int(11)");

                entity.Property(e => e.DeleteMark).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.EnabledMark).HasColumnType("int(11)");

                entity.Property(e => e.ExecuteResult).HasColumnType("int(11)");

                entity.Property(e => e.ExecuteResultJson).HasColumnType("longtext");

                entity.Property(e => e.Host).HasColumnType("varchar(200)");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.IpaddressName)
                    .HasColumnName("IPAddressName")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Module).HasColumnType("varchar(50)");

                entity.Property(e => e.ModuleId).HasColumnType("varchar(50)");

                entity.Property(e => e.OperateAccount).HasColumnType("varchar(50)");

                entity.Property(e => e.OperateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.OperateType).HasColumnType("varchar(50)");

                entity.Property(e => e.OperateTypeId).HasColumnType("varchar(50)");

                entity.Property(e => e.OperateUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.SourceContentJson).HasColumnType("longtext");

                entity.Property(e => e.SourceObjectId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnType("varchar(50)");

                entity.Property(e => e.Account).HasColumnType("varchar(50)");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.DeleteMark).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.EnabledMark).HasColumnType("int(11)");

                entity.Property(e => e.FirstVisit)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Gender).HasColumnType("int(11)");

                entity.Property(e => e.HeadIcon).HasColumnType("varchar(200)");

                entity.Property(e => e.LastVisit)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.LogOnCount).HasColumnType("int(11)");

                entity.Property(e => e.Mobile).HasColumnType("varchar(50)");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'0000-00-00 00:00:00'");

                entity.Property(e => e.Password).HasColumnType("varchar(50)");

                entity.Property(e => e.RealName).HasColumnType("varchar(50)");

                entity.Property(e => e.Secretkey).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<UserProject>(entity =>
            {
                entity.HasKey(e => e.UserPrjId);

                entity.ToTable("user_project");

                entity.Property(e => e.UserPrjId).HasColumnType("varchar(50)");

                entity.Property(e => e.ProjectId).HasColumnType("varchar(50)");

                entity.Property(e => e.PrjDataBase).HasColumnType("varchar(50)");

                entity.Property(e => e.PrjIdentification).HasColumnType("varchar(50)");

                entity.Property(e => e.UserId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");
            });
        }

    }
}
