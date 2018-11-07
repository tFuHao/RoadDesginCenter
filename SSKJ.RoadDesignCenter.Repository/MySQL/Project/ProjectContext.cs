using Microsoft.EntityFrameworkCore;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Repository.MySQL.Project
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        public virtual DbSet<AddStake> AddStake { get; set; }
        public virtual DbSet<Authorize> Authorize { get; set; }
        public virtual DbSet<BrokenChainage> BrokenChainage { get; set; }
        public virtual DbSet<CrossSectionGroundLine> CrossSectionGroundLine { get; set; }
        public virtual DbSet<CrossSectionGroundLineData> CrossSectionGroundLineData { get; set; }
        public virtual DbSet<FlatCurve> FlatCurve { get; set; }
        public virtual DbSet<FlatCurve_CurveElement> FlatCurve_CurveElement { get; set; }
        public virtual DbSet<FlatCurve_Intersection> FlatCurve_Intersection { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<SampleLine> SampleLine { get; set; }
        public virtual DbSet<Stake> Stake { get; set; }
        public virtual DbSet<VerticalCurve> VerticalCurve { get; set; }
        public virtual DbSet<VerticalCurve_CurveElement> VerticalCurve_CurveElement { get; set; }
        public virtual DbSet<VerticalCurve_GradeChangePoint> VerticalCurve_GradeChangePoint { get; set; }
        public virtual DbSet<VerticalSectionGroundLine> VerticalSectionGroundLine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddStake>(entity =>
            {
                entity.ToTable("addstake");

                entity.Property(e => e.AddStakeId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<Authorize>(entity =>
            {
                entity.ToTable("authorize");

                entity.Property(e => e.AuthorizeId).HasColumnType("varchar(50)");

                entity.Property(e => e.Category).HasColumnType("int(11)");

                entity.Property(e => e.IsHalf).HasColumnType("int(1)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasColumnType("varchar(50)");

                entity.Property(e => e.ItemId).HasColumnType("varchar(50)");

                entity.Property(e => e.ItemType).HasColumnType("int(11)");

                entity.Property(e => e.ObjectId).HasColumnType("varchar(50)");

                entity.Property(e => e.SortCode).HasColumnType("int(11)");
            });

            modelBuilder.Entity<BrokenChainage>(entity =>
            {
                entity.HasKey(e => e.BrokenId);

                entity.ToTable("brokenchainage");

                entity.Property(e => e.BrokenId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.BrokenType).HasColumnType("int(11)");

                entity.Property(e => e.AfterStake).HasColumnType("double(18,4)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.FrontStake).HasColumnType("double(18,4)");

                entity.Property(e => e.RawStakeBack).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<CrossSectionGroundLine>(entity =>
            {
                entity.ToTable("crosssectiongroundline");

                entity.Property(e => e.CrossSectionGroundLineId).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<CrossSectionGroundLineData>(entity =>
            {
                entity.ToTable("crosssectiongroundlinedata");

                entity.Property(e => e.CrossSectionGroundLineDataId).HasColumnType("varchar(50)");

                entity.Property(e => e.CrossSectionGroundLineId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.Dist).HasColumnType("double(18,4)");

                entity.Property(e => e.H).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<FlatCurve>(entity =>
            {
                entity.ToTable("flatcurve");

                entity.Property(e => e.FlatCurveId).HasColumnType("varchar(50)");

                entity.Property(e => e.BeginStake).HasColumnType("double(18,4)");

                entity.Property(e => e.CurveNumber).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.EndStake).HasColumnType("double(18,4)");

                entity.Property(e => e.FlatCurveLength).HasColumnType("double(18,4)");

                entity.Property(e => e.FlatCurveType).HasColumnType("int(11)");

                entity.Property(e => e.IntersectionNumber).HasColumnType("int(11)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<FlatCurve_CurveElement>(entity =>
            {
                entity.HasKey(e => e.CurveElementId);

                entity.ToTable("flatcurve_curveelement");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.CurveElementId).HasColumnType("varchar(50)");

                entity.Property(e => e.Azimuth).HasColumnType("double(18,4)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.R).HasColumnType("double(18,4)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");

                entity.Property(e => e.TurnTo).HasColumnType("int(11)");

                entity.Property(e => e.X).HasColumnType("double(18,4)");

                entity.Property(e => e.Y).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<FlatCurve_Intersection>(entity =>
            {
                entity.HasKey(e => e.IntersectionPointId);

                entity.ToTable("flatcurve_intersection");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.IntersectionPointId).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.IntersectionName).HasColumnType("varchar(50)");

                entity.Property(e => e.Ls1).HasColumnType("double(18,4)");

                entity.Property(e => e.Ls1R).HasColumnType("double(18,4)");

                entity.Property(e => e.Ls2).HasColumnType("double(18,4)");

                entity.Property(e => e.Ls2R).HasColumnType("double(18,4)");

                entity.Property(e => e.R).HasColumnType("double(18,4)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");

                entity.Property(e => e.X).HasColumnType("double(18,4)");

                entity.Property(e => e.Y).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.ToTable("route");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.Description).HasColumnType("varchar(200)");

                entity.Property(e => e.DesignSpeed).HasColumnType("varchar(50)");

                entity.Property(e => e.EndStake).HasColumnType("double(18,4)");

                entity.Property(e => e.ParentId).HasColumnType("varchar(50)");

                entity.Property(e => e.ProjectId).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteLength).HasColumnType("double(18,4)");

                entity.Property(e => e.RouteName).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteType).HasColumnType("int(11)");

                entity.Property(e => e.IntersectionNumber).HasColumnType("int(11)");

                entity.Property(e => e.CureNumber).HasColumnType("int(11)");

                entity.Property(e => e.GradeChangeNumber).HasColumnType("int(11)");

                entity.Property(e => e.StartStake).HasColumnType("double(18,4)");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<SampleLine>(entity =>
            {
                entity.ToTable("sampleline");

                entity.Property(e => e.SampleLineId).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.LeftOffset).HasColumnType("double(18,4)");

                entity.Property(e => e.RightOffset).HasColumnType("double(18,4)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<Stake>(entity =>
            {
                entity.ToTable("stake");

                entity.Property(e => e.StakeId).HasColumnType("varchar(50)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.Offset).HasColumnType("double(18,4)");

                entity.Property(e => e.StakeName).HasColumnType("double(18,4)");

                entity.Property(e => e.RightCorner).HasColumnType("double(18,4)");
            });

            modelBuilder.Entity<VerticalCurve>(entity =>
            {
                entity.ToTable("verticalcurve");

                entity.Property(e => e.VerticalCurveId).HasColumnType("varchar(50)");

                entity.Property(e => e.BeginStake).HasColumnType("double(18,4)");

                entity.Property(e => e.CurveNumber).HasColumnType("int(11)");

                entity.Property(e => e.Description).HasColumnType("varchar(50)");

                entity.Property(e => e.EndStake).HasColumnType("double(18,4)");

                entity.Property(e => e.GradeChangePointNumber).HasColumnType("int(11)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.VerticalCurveLength).HasColumnType("double(18,4)");

                entity.Property(e => e.VerticalCurveType).HasColumnType("int(11)");
            });

            modelBuilder.Entity<VerticalCurve_CurveElement>(entity =>
            {
                entity.HasKey(e => e.CurveElementId);

                entity.ToTable("verticalcurve_curveelement");

                entity.Property(e => e.CurveElementId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.H).HasColumnType("double(18,4)");

                entity.Property(e => e.I).HasColumnType("double(18,4)");

                entity.Property(e => e.Length).HasColumnType("double(18,4)");

                entity.Property(e => e.R).HasColumnType("double(18,4)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<VerticalCurve_GradeChangePoint>(entity =>
            {
                entity.HasKey(e => e.GradeChangePointId);

                entity.ToTable("verticalcurve_gradechangepoint");

                entity.Property(e => e.GradeChangePointId).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.H).HasColumnType("double(18,4)");

                entity.Property(e => e.R).HasColumnType("double(18,4)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<VerticalSectionGroundLine>(entity =>
            {
                entity.ToTable("verticalsectiongroundline");

                entity.Property(e => e.Id).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("int(11)");

                entity.Property(e => e.H).HasColumnType("double(18,4)");

                entity.Property(e => e.RouteId).HasColumnType("varchar(50)");

                entity.Property(e => e.Stake).HasColumnType("double(18,4)");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
