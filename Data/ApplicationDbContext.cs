using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UmkConstructor.Data.DatabaseTables;
using UmkConstructor.Data.AdditionalModels;

namespace UmkConstructor.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<AcademicYear> AcademicYears { get; set; }

        public virtual DbSet<BusinessRole> BusinessRoles { get; set; }

        public virtual DbSet<Curriculum> Curricula { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Organization> Organizations { get; set; }

        public virtual DbSet<Semester> Semesters { get; set; }
        
        public virtual DbSet<Discipline> Disciplines { get; set; }

        public virtual DbSet<DisciplineRealSemester> DisciplineRealSemester { get; set; }

        public virtual DbSet<SemesterCurriculum> SemesterCurriculum { get; set; }

        public virtual DbSet<SemesterTypeStudyYear> SemesterTypeStudyYear { get; set; }

        public virtual DbSet<SemesterType> SemesterTypes { get; set; }

        public virtual DbSet<Specialty> Specialties { get; set; }

        public virtual DbSet<StudyYear> StudyYears { get; set; }

        public virtual DbSet<Template> Templates { get; set; }
        
        public virtual DbSet<TemplateOrganization> TemplateOrganizations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Discipline>()
                .Property(e => e.Code)
                .HasConversion(v => v.Name, v => new Code(v));

            modelBuilder.Entity<Curriculum>()
                .Property(e => e.Edition)
                .HasConversion(v => v.Name, v => new Edition(v));

            modelBuilder.Entity<Template>()
                .Property(e => e.Edition)
                .HasConversion(v => v.Name, v => new Edition(v));

            modelBuilder.Entity<BusinessRole>(entity =>
            {
                entity.HasIndex(e => e.SpecialtyId, "IX_BusinessRoles_SpecialtyId");
                entity.HasOne(d => d.Specialty).WithMany(p => p.BusinessRoles).HasForeignKey(d => d.SpecialtyId);
            });

            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.HasIndex(e => e.AcademicYearId, "IX_Curricula_AcademicYearId");
                entity.HasIndex(e => e.BusinessRoleId, "IX_Curricula_BusinessRoleId");
                entity.HasOne(d => d.AcademicYear).WithMany(p => p.Curricula).HasForeignKey(d => d.AcademicYearId);
                entity.HasOne(d => d.BusinessRole).WithMany(p => p.Curricula).HasForeignKey(d => d.BusinessRoleId);
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.SemesterTypeStudyYearId, "IX_Semesters_SemesterTypeStudyYearId");
                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");
                entity.HasOne(d => d.SemesterTypeStudyYear).WithMany(p => p.Semesters).HasForeignKey(d => d.SemesterTypeStudyYearId);
            });

            modelBuilder.Entity<SemesterCurriculum>(entity =>
            {
                entity.HasKey(e => e.RelationId);
                entity.HasIndex(e => e.CurriculumId, "IX_SemesterCurriculum_CurriculumId");
                entity.HasIndex(e => e.SemesterId, "IX_SemesterCurriculum_SemesterId");
                entity.HasOne(d => d.Curriculum).WithMany(p => p.SemesterCurriculum).HasForeignKey(d => d.CurriculumId);
                entity.HasOne(d => d.Semester).WithMany(p => p.SemesterCurriculum).HasForeignKey(d => d.SemesterId);
            });

            modelBuilder.Entity<SemesterTypeStudyYear>(entity =>
            {
                entity.HasKey(e => e.RelationId);
                entity.HasIndex(e => e.SemesterTypeId, "IX_SemesterTypeStudyYear_SemesterTypeId");
                entity.HasIndex(e => e.StudyYearId, "IX_SemesterTypeStudyYear_StudyYearId");
                entity.HasOne(d => d.SemesterType).WithMany(p => p.SemesterTypeStudyYear).HasForeignKey(d => d.SemesterTypeId);
                entity.HasOne(d => d.StudyYear).WithMany(p => p.SemesterTypeStudyYear).HasForeignKey(d => d.StudyYearId);
            });

            modelBuilder.Entity<DisciplineRealSemester>(entity =>
            {
                entity.HasKey(e => e.RelationId);
                entity.HasIndex(e => e.DisciplineId, "IX_DisciplineRealSemester_DisciplineId");
                entity.HasIndex(e => e.SemesterCurriculumId, "IX_DisciplineRealSemester_SemesterCurriculumId");
                entity.HasOne(d => d.Discipline).WithMany(p => p.DisciplineRealSemester).HasForeignKey(d => d.DisciplineId);
                entity.HasOne(d => d.SemesterCurriculum).WithMany(p => p.DisciplineRealSemester).HasForeignKey(d => d.SemesterCurriculumId);
            });

            modelBuilder.Entity<TemplateOrganization>(entity =>
            {
                entity.HasKey(e => e.RelationId);
                entity.HasIndex(e => e.TemplateId, "IX_TemplateOrganization_TemplateId");
                entity.HasIndex(e => e.OrganizationId, "IX_TemplateOrganization_OrganizationId");
                entity.HasOne(d => d.Template).WithMany(p => p.TemplateOrganization).HasForeignKey(d => d.TemplateId);
                entity.HasOne(d => d.Organization).WithMany(p => p.TemplateOrganization).HasForeignKey(d => d.OrganizationId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
