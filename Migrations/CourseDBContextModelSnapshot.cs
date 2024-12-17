﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProblemAssignmnet2_SruthiKamisetti.Entities;

#nullable disable

namespace ProblemAssignmnet2_SruthiKamisetti.Migrations
{
    [DbContext(typeof(CourseDBContext))]
    partial class CourseDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProblemAssignmnet2_SruthiKamisetti.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instructor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            CourseName = "ASP.NET",
                            Instructor = "Manny Singh",
                            RoomNumber = "3G15",
                            StartDate = new DateTime(2024, 12, 8, 12, 2, 16, 913, DateTimeKind.Local).AddTicks(9402)
                        },
                        new
                        {
                            CourseId = 2,
                            CourseName = "C#",
                            Instructor = "Sukhbir Tatla",
                            RoomNumber = "3G15",
                            StartDate = new DateTime(2024, 12, 8, 12, 2, 16, 913, DateTimeKind.Local).AddTicks(9455)
                        },
                        new
                        {
                            CourseId = 3,
                            CourseName = "DBMS",
                            Instructor = "John Smith",
                            RoomNumber = "3G15",
                            StartDate = new DateTime(2024, 12, 8, 12, 2, 16, 913, DateTimeKind.Local).AddTicks(9457)
                        });
                });

            modelBuilder.Entity("ProblemAssignmnet2_SruthiKamisetti.Entities.Student", b =>
                {
                    b.Property<int?>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("StudentId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CourseId = 1,
                            Status = 0,
                            StudentEmail = "Sruthi@gmail.com",
                            StudentName = "Sruthi"
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 2,
                            Status = 0,
                            StudentEmail = "Sai@gmail.com",
                            StudentName = "Sai"
                        },
                        new
                        {
                            StudentId = 3,
                            CourseId = 2,
                            Status = 0,
                            StudentEmail = "Twinkle@gmail.com",
                            StudentName = "Twinkle"
                        },
                        new
                        {
                            StudentId = 4,
                            CourseId = 3,
                            Status = 0,
                            StudentEmail = "Jothi@gmail.com",
                            StudentName = "Jothi"
                        });
                });

            modelBuilder.Entity("ProblemAssignmnet2_SruthiKamisetti.Entities.Student", b =>
                {
                    b.HasOne("ProblemAssignmnet2_SruthiKamisetti.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ProblemAssignmnet2_SruthiKamisetti.Entities.Course", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
