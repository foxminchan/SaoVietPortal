﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portal.Infrastructure;

#nullable disable

namespace Portal.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230327182511_alter-table_FK-Users-Staff")]
    partial class altertable_FKUsersStaff
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("imageUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ImageUrl");

                    b.Property<string>("staffId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("studentId")
                        .HasColumnType("char(10)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("staffId");

                    b.HasIndex("studentId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.Branch", b =>
                {
                    b.Property<string>("branchId")
                        .HasColumnType("char(8)")
                        .HasColumnName("Id");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Address");

                    b.Property<string>("branchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("phone")
                        .HasColumnType("char(10)")
                        .HasColumnName("Phone");

                    b.HasKey("branchId");

                    b.ToTable("Branch", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.Class", b =>
                {
                    b.Property<string>("classId")
                        .HasColumnType("char(10)")
                        .HasColumnName("Id");

                    b.Property<string>("branchId")
                        .HasColumnType("char(8)");

                    b.Property<string>("courseId")
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime?>("endDate")
                        .HasColumnType("date")
                        .HasColumnName("EndDate");

                    b.Property<double?>("fee")
                        .HasColumnType("float")
                        .HasColumnName("Fee");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("date")
                        .HasColumnName("StartDate");

                    b.HasKey("classId");

                    b.HasIndex("branchId");

                    b.HasIndex("courseId");

                    b.ToTable("Class", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.Course", b =>
                {
                    b.Property<string>("courseId")
                        .HasColumnType("varchar(10)")
                        .HasColumnName("Id");

                    b.Property<string>("courseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.HasKey("courseId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseEnrollment", b =>
                {
                    b.Property<string>("studentId")
                        .HasColumnType("char(10)");

                    b.Property<string>("classId")
                        .HasColumnType("char(10)");

                    b.HasKey("studentId", "classId");

                    b.HasIndex("classId");

                    b.ToTable("CourseEnrollment", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseRegistration", b =>
                {
                    b.Property<Guid>("courseRegistrationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("appointmentDate")
                        .HasColumnType("date")
                        .HasColumnName("AppointmentDate");

                    b.Property<string>("classId")
                        .HasColumnType("char(10)");

                    b.Property<decimal>("discountAmount")
                        .HasColumnType("decimal(4,2)")
                        .HasColumnName("DiscountAmount");

                    b.Property<byte?>("paymentMethodId")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("registerDate")
                        .HasColumnType("date")
                        .HasColumnName("RegisterDate");

                    b.Property<double>("registerFee")
                        .HasColumnType("float")
                        .HasColumnName("RegisterFee");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("Status");

                    b.Property<string>("studentId")
                        .HasColumnType("char(10)");

                    b.HasKey("courseRegistrationId");

                    b.HasIndex("paymentMethodId");

                    b.HasIndex("studentId", "classId");

                    b.ToTable("CourseRegistration", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.PaymentMethod", b =>
                {
                    b.Property<byte>("paymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("paymentMethodId"));

                    b.Property<string>("paymentMethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("paymentMethodId");

                    b.ToTable("PaymentMethod", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.Position", b =>
                {
                    b.Property<int>("positionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("positionId"));

                    b.Property<string>("positionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("positionId");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.ReceiptsExpenses", b =>
                {
                    b.Property<Guid>("receiptExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<double>("amount")
                        .HasColumnType("float")
                        .HasColumnName("Amount");

                    b.Property<string>("branchId")
                        .HasColumnType("char(8)");

                    b.Property<DateTime>("date")
                        .HasColumnType("date")
                        .HasColumnName("Date");

                    b.Property<string>("note")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Note");

                    b.Property<bool>("type")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Type");

                    b.HasKey("receiptExpenseId");

                    b.HasIndex("branchId");

                    b.ToTable("ReceiptsExpenses", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.Staff", b =>
                {
                    b.Property<string>("staffId")
                        .HasColumnType("varchar(20)")
                        .HasColumnName("Id");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Address");

                    b.Property<string>("branchId")
                        .HasColumnType("char(8)");

                    b.Property<DateTime?>("dob")
                        .HasColumnType("date")
                        .HasColumnName("Dob");

                    b.Property<DateTime?>("dsw")
                        .HasColumnType("date")
                        .HasColumnName("Dsw");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Fullname");

                    b.Property<string>("managerId")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("positionId")
                        .HasColumnType("int");

                    b.HasKey("staffId");

                    b.HasIndex("branchId");

                    b.HasIndex("managerId");

                    b.HasIndex("positionId");

                    b.ToTable("Staff", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.Student", b =>
                {
                    b.Property<string>("studentId")
                        .HasColumnType("char(10)")
                        .HasColumnName("Id");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Address");

                    b.Property<DateTime?>("dob")
                        .HasColumnType("date")
                        .HasColumnName("Dob");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Fullname");

                    b.Property<bool>("gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Gender");

                    b.Property<string>("occupation")
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Occupation");

                    b.Property<string>("pod")
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("Pod");

                    b.Property<string>("socialNetwork")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SocialNetwork");

                    b.HasKey("studentId");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("Portal.Domain.Entities.StudentProgress", b =>
                {
                    b.Property<Guid>("progressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<string>("classId")
                        .HasColumnType("char(10)");

                    b.Property<string>("lessonContent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("LessonContent");

                    b.Property<DateTime?>("lessonDate")
                        .HasColumnType("date")
                        .HasColumnName("LessonDate");

                    b.Property<string>("lessonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("LessonName");

                    b.Property<double>("lessonRating")
                        .HasColumnType("float")
                        .HasColumnName("LessonRating");

                    b.Property<string>("progressStatus")
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("ProgressStatus");

                    b.Property<string>("staffId")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("studentId")
                        .HasColumnType("char(10)");

                    b.HasKey("progressId");

                    b.HasIndex("staffId");

                    b.HasIndex("studentId", "classId");

                    b.ToTable("StudentProgress", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Portal.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Portal.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Portal.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Portal.Domain.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Portal.Domain.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Staff", "staff")
                        .WithMany("users")
                        .HasForeignKey("staffId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Users_Staff");

                    b.HasOne("Portal.Domain.Entities.Student", "student")
                        .WithMany("users")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Users_Students");

                    b.Navigation("staff");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Class", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Branch", "branch")
                        .WithMany("classes")
                        .HasForeignKey("branchId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Class_Branch");

                    b.HasOne("Portal.Domain.Entities.Course", "course")
                        .WithMany("classes")
                        .HasForeignKey("courseId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Class_Course");

                    b.Navigation("branch");

                    b.Navigation("course");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseEnrollment", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Class", "class")
                        .WithMany("courseEnrollments")
                        .HasForeignKey("classId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_CourseEnrollment_Class");

                    b.HasOne("Portal.Domain.Entities.Student", "student")
                        .WithMany("courseEnrollments")
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CourseEnrollment_Student");

                    b.Navigation("class");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseRegistration", b =>
                {
                    b.HasOne("Portal.Domain.Entities.PaymentMethod", "paymentMethod")
                        .WithMany("courseRegistrations")
                        .HasForeignKey("paymentMethodId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_CourseRegistration_PaymentMethod");

                    b.HasOne("Portal.Domain.Entities.CourseEnrollment", "courseEnrollment")
                        .WithMany("courseRegistrations")
                        .HasForeignKey("studentId", "classId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_CourseRegistrations_CourseEnrollment");

                    b.Navigation("courseEnrollment");

                    b.Navigation("paymentMethod");
                });

            modelBuilder.Entity("Portal.Domain.Entities.ReceiptsExpenses", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Branch", "branch")
                        .WithMany("receiptsExpenses")
                        .HasForeignKey("branchId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_ReceiptsExpenses_Branch");

                    b.Navigation("branch");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Staff", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Branch", "branch")
                        .WithMany("staffs")
                        .HasForeignKey("branchId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Staff_Branch");

                    b.HasOne("Portal.Domain.Entities.Staff", "manager")
                        .WithMany("staffs")
                        .HasForeignKey("managerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_Staff_Manager");

                    b.HasOne("Portal.Domain.Entities.Position", "position")
                        .WithMany("staffs")
                        .HasForeignKey("positionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Staff_Position");

                    b.Navigation("branch");

                    b.Navigation("manager");

                    b.Navigation("position");
                });

            modelBuilder.Entity("Portal.Domain.Entities.StudentProgress", b =>
                {
                    b.HasOne("Portal.Domain.Entities.Staff", "staff")
                        .WithMany("studentProgresses")
                        .HasForeignKey("staffId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_StudentProgress_Staff");

                    b.HasOne("Portal.Domain.Entities.CourseEnrollment", "courseEnrollment")
                        .WithMany("studentProgresses")
                        .HasForeignKey("studentId", "classId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_StudentProgress_CourseEnrollment");

                    b.Navigation("courseEnrollment");

                    b.Navigation("staff");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Branch", b =>
                {
                    b.Navigation("classes");

                    b.Navigation("receiptsExpenses");

                    b.Navigation("staffs");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Class", b =>
                {
                    b.Navigation("courseEnrollments");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Course", b =>
                {
                    b.Navigation("classes");
                });

            modelBuilder.Entity("Portal.Domain.Entities.CourseEnrollment", b =>
                {
                    b.Navigation("courseRegistrations");

                    b.Navigation("studentProgresses");
                });

            modelBuilder.Entity("Portal.Domain.Entities.PaymentMethod", b =>
                {
                    b.Navigation("courseRegistrations");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Position", b =>
                {
                    b.Navigation("staffs");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Staff", b =>
                {
                    b.Navigation("staffs");

                    b.Navigation("studentProgresses");

                    b.Navigation("users");
                });

            modelBuilder.Entity("Portal.Domain.Entities.Student", b =>
                {
                    b.Navigation("courseEnrollments");

                    b.Navigation("users");
                });
#pragma warning restore 612, 618
        }
    }
}
