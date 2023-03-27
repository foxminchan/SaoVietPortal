using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Infrastructure.Helper;

namespace Portal.Infrastructure
{
    /**
    * @Project ASP.NET Core
    * @Author: Nguyen Xuan Nhan
    * @Copyright (C) 2023 FoxMinChan. All rights reserved
    * @License MIT
    * @Create date Mon 27 Mar 2023 00:00:00 AM +07
    */

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student>? students { get; set; }
        public DbSet<Branch>? branches { get; set; }
        public DbSet<Course>? courses { get; set; }
        public DbSet<Class>? classes { get; set; }
        public DbSet<CourseEnrollment>? courseEnrollments { get; set; }
        public DbSet<Staff>? staffs { get; set; }
        public DbSet<Position>? positions { get; set; }
        public DbSet<ReceiptsExpenses>? receiptsExpenses { get; set; }
        public DbSet<StudentProgress>? studentProgresses { get; set; }
        public DbSet<CourseRegistration>? courseRegistrations { get; set; }
        public DbSet<PaymentMethod>? paymentMethods { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>(entity =>
            {
                entity.ToTable("Students");
                entity.HasKey(e => e.studentId);
                entity.Property(e => e.studentId)
                    .HasColumnName("Id")
                    .HasColumnType("char(10)");
                entity.Property(e => e.fullname)
                    .HasColumnName("Fullname")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
                entity.Property(e => e.gender)
                    .HasColumnName("Gender")
                    .HasColumnType("bit")
                    .HasDefaultValue(false)
                    .IsRequired();
                entity.Property(e => e.address)
                    .HasColumnName("Address")
                    .HasColumnType("nvarchar(80)");
                entity.Property(e => e.dob)
                    .HasConversion<StringConverter>()
                    .HasColumnName("Dob")
                    .HasColumnType("date");
                entity.Property(e => e.pod)
                    .HasColumnName("Pod")
                    .HasColumnType("nvarchar(80)");
                entity.Property(e => e.occupation)
                    .HasColumnName("Occupation")
                    .HasColumnType("nvarchar(80)");
                entity.Property(e => e.socialNetwork)
                    .HasConversion<JsonConverter>()
                    .HasColumnName("SocialNetwork")
                    .HasColumnType("nvarchar(max)");
            });

            builder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");
                entity.HasKey(e => e.branchId);
                entity.Property(e => e.branchId)
                    .HasColumnName("Id")
                    .HasColumnType("char(8)");
                entity.Property(e => e.branchName)
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
                entity.Property(e => e.address)
                    .HasColumnName("Address")
                    .HasColumnType("nvarchar(80)");
                entity.Property(e => e.phone)
                    .HasColumnName("Phone")
                    .HasColumnType("char(10)");
            });

            builder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");
                entity.HasKey(e => e.courseId);
                entity.Property(e => e.courseId)
                    .HasColumnName("Id")
                    .HasColumnType("varchar(10)");
                entity.Property(e => e.courseName)
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
                entity.Property(e => e.description)
                    .HasColumnName("Description")
                    .HasColumnType("nvarchar(max)");
            });

            builder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");
                entity.HasKey(e => e.classId);
                entity.Property(e => e.classId)
                    .HasColumnName("Id")
                    .HasColumnType("char(10)");
                entity.Property(e => e.startDate)
                    .HasConversion<StringConverter>()
                    .HasColumnName("StartDate")
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(e => e.endDate)
                    .HasConversion<StringConverter>()
                    .HasColumnName("EndDate")
                    .HasColumnType("date");
                entity.Property(e => e.fee)
                    .HasColumnName("Fee")
                    .HasColumnType("float");
                entity.HasOne(e => e.branch)                    
                    .WithMany(e => e.classes)
                    .HasForeignKey(e => e.branchId)
                    .HasConstraintName("FK_Class_Branch")
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.course)
                    .WithMany(e => e.classes)
                    .HasForeignKey(e => e.courseId)
                    .HasConstraintName("FK_Class_Course")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.imageUrl)
                    .HasColumnName("ImageUrl")
                    .HasColumnType("nvarchar(max)");
                entity.HasOne(e => e.staff)
                    .WithMany(e => e.users)
                    .HasForeignKey(e => e.staffId)
                    .HasConstraintName("FK_Users_Staff")
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.student)
                    .WithMany(e => e.users)
                    .HasForeignKey(e => e.studentId)
                    .HasConstraintName("FK_Users_Students")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<CourseEnrollment>(entity =>
            {
                entity.ToTable("CourseEnrollment");
                entity.HasKey(e => new { e.studentId, e.classId });
                entity.HasOne(e => e.student)
                    .WithMany(e => e.courseEnrollments)
                    .HasForeignKey(e => e.studentId)
                    .HasConstraintName("FK_CourseEnrollment_Student")
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.@class)
                    .WithMany(e => e.courseEnrollments)
                    .HasForeignKey(e => e.classId)
                    .HasConstraintName("FK_CourseEnrollment_Class")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");
                entity.HasKey(e => e.positionId);
                entity.Property(e => e.positionId)
                    .HasColumnName("Id")
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.positionName)
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
            });

            builder.Entity<Staff>(entity =>
            {
                entity.ToTable("Staff");
                entity.HasKey(e => e.staffId);
                entity.Property(e => e.staffId)
                    .HasColumnName("Id")
                    .HasColumnType("varchar(20)");
                entity.Property(e => e.fullname)
                    .HasColumnName("Fullname")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
                entity.Property(e => e.dob)
                    .HasConversion<StringConverter>()
                    .HasColumnName("Dob")
                    .HasColumnType("date");
                entity.Property(e => e.address)
                    .HasColumnName("Address")
                    .HasColumnType("nvarchar(80)");
                entity.Property(e => e.dsw)
                    .HasConversion<StringConverter>()
                    .HasColumnName("Dsw")
                    .HasColumnType("date");
                entity.HasOne(e => e.position)
                    .WithMany(e => e.staffs)
                    .HasForeignKey(e => e.positionId)
                    .HasConstraintName("FK_Staff_Position")
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.branch)
                    .WithMany(e => e.staffs)
                    .HasForeignKey(e => e.branchId)
                    .HasConstraintName("FK_Staff_Branch")
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.manager)
                    .WithMany(e => e.staffs)
                    .HasForeignKey(e => e.managerId)
                    .HasConstraintName("FK_Staff_Manager")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<StudentProgress>(entity =>
            {
                entity.ToTable("StudentProgress");
                entity.HasKey(e => e.progressId);
                entity.Property(e => e.progressId)
                    .HasColumnName("Id")
                    .HasColumnType("Uniqueidentifier");
                entity.Property(e => e.lessonName)
                    .HasColumnName("LessonName")
                    .HasColumnType("nvarchar(80)")
                    .IsRequired();
                entity.Property(e => e.lessonContent)
                    .HasColumnName("LessonContent")
                    .HasColumnType("nvarchar(max)");
                entity.Property(e => e.lessonDate)
                    .HasConversion<StringConverter>()
                    .HasColumnName("LessonDate")
                    .HasColumnType("date");
                entity.Property(e => e.progressStatus)
                    .HasColumnName("ProgressStatus")
                    .HasColumnType("nvarchar(15)");
                entity.Property(e => e.lessonRating)
                    .HasColumnName("LessonRating")
                    .HasColumnType("float");
                entity.HasOne(e => e.staff)
                    .WithMany(e => e.studentProgresses)
                    .HasForeignKey(e => e.staffId)
                    .HasConstraintName("FK_StudentProgress_Staff")
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.courseEnrollment)
                    .WithMany(e => e.studentProgresses)
                    .HasForeignKey(e => new { e.studentId, e.classId })
                    .HasConstraintName("FK_StudentProgress_CourseEnrollment")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ReceiptsExpenses>(entity =>
            {
                entity.ToTable("ReceiptsExpenses");
                entity.HasKey(e => e.receiptExpenseId);
                entity.Property(e => e.receiptExpenseId)
                    .HasColumnName("Id")
                    .HasColumnType("Uniqueidentifier");
                entity.Property(e => e.type)
                    .HasColumnName("Type")
                    .HasColumnType("Bit")
                    .HasDefaultValue(false)
                    .IsRequired();
                entity.Property(e => e.date)
                    .HasConversion<StringConverter>()
                    .HasColumnName("Date")
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(e => e.amount)
                    .HasColumnName("Amount")
                    .HasColumnType("float")
                    .IsRequired();
                entity.Property(e => e.note)
                    .HasColumnName("Note")
                    .HasColumnType("nvarchar(max)");
                entity.HasOne(e => e.branch)
                    .WithMany(e => e.receiptsExpenses)
                    .HasForeignKey(e => e.branchId)
                    .HasConstraintName("FK_ReceiptsExpenses_Branch")
                    .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");
                entity.HasKey(e => e.paymentMethodId);
                entity.Property(e => e.paymentMethodId)
                    .HasColumnName("Id")
                    .HasColumnType("tinyint")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.paymentMethodName)
                    .HasColumnName("Name")
                    .HasColumnType("nvarchar(50)")
                    .IsRequired();
            });

            builder.Entity<CourseRegistration>(entity =>
            {
                entity.ToTable("CourseRegistration");
                entity.HasKey(e => e.courseRegistrationId);
                entity.Property(e => e.courseRegistrationId)
                    .HasColumnName("Id")
                    .HasColumnType("Uniqueidentifier");
                entity.Property(e => e.status)
                    .HasColumnName("Status")
                    .HasColumnType("nvarchar(15)");
                entity.Property(e => e.registerDate)
                    .HasConversion<StringConverter>()
                    .HasColumnName("RegisterDate")
                    .HasColumnType("date");
                entity.Property(e => e.appointmentDate)
                    .HasConversion<StringConverter>()
                    .HasColumnName("AppointmentDate")
                    .HasColumnType("date");
                entity.Property(e => e.registerFee)
                    .HasColumnName("RegisterFee")
                    .HasColumnType("float");
                entity.Property(e => e.discountAmount)
                    .HasColumnName("DiscountAmount")
                    .HasColumnType("decimal(4,2)");
                entity.HasOne(e => e.paymentMethod)
                    .WithMany(e => e.courseRegistrations)
                    .HasForeignKey(e => e.paymentMethodId)
                    .HasConstraintName("FK_CourseRegistration_PaymentMethod")
                    .OnDelete(DeleteBehavior.SetNull);
                entity.HasOne(e => e.courseEnrollment)
                    .WithMany(e => e.courseRegistrations)
                    .HasForeignKey(e => new { e.studentId, e.classId })
                    .HasConstraintName("FK_CourseRegistrations_CourseEnrollment")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
