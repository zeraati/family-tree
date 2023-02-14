﻿// <auto-generated />
using System;
using FamilyTree.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FamilyTree.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("FamilyTree.Entity.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("FamilyTree.Entity.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("FamilyTree.Entity.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<int>("GenderId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<int?>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("LocationId");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("FamilyTree.Entity.PersonFamily", b =>
                {
                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FatherId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MotherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PersonId");

                    b.HasIndex("FatherId");

                    b.HasIndex("MotherId");

                    b.ToTable("PersonFamily");
                });

            modelBuilder.Entity("FamilyTree.Entity.PersonSpouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("SpouseId");

                    b.ToTable("PersonSpouse");
                });

            modelBuilder.Entity("FamilyTree.Entity.Person", b =>
                {
                    b.HasOne("FamilyTree.Entity.Job", "Job")
                        .WithMany("Person")
                        .HasForeignKey("JobId")
                        .HasConstraintName("FK_Person_Job");

                    b.HasOne("FamilyTree.Entity.Location", "Location")
                        .WithMany("Person")
                        .HasForeignKey("LocationId")
                        .HasConstraintName("FK_Person_Location");

                    b.Navigation("Job");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("FamilyTree.Entity.PersonFamily", b =>
                {
                    b.HasOne("FamilyTree.Entity.Person", "Father")
                        .WithMany("FatherOfPersons")
                        .HasForeignKey("FatherId")
                        .HasConstraintName("FK_PersonFamily_Father");

                    b.HasOne("FamilyTree.Entity.Person", "Mother")
                        .WithMany("MotherOfPersons")
                        .HasForeignKey("MotherId")
                        .HasConstraintName("FK_PersonFamily_Mother");

                    b.HasOne("FamilyTree.Entity.Person", "Person")
                        .WithOne("PersonFamily")
                        .HasForeignKey("FamilyTree.Entity.PersonFamily", "PersonId")
                        .IsRequired()
                        .HasConstraintName("FK_PersonFamily_Person");

                    b.Navigation("Father");

                    b.Navigation("Mother");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("FamilyTree.Entity.PersonSpouse", b =>
                {
                    b.HasOne("FamilyTree.Entity.Person", "Person")
                        .WithMany("PersonSpouse")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PersonSpouse_Person");

                    b.HasOne("FamilyTree.Entity.Person", "Spouse")
                        .WithMany("SpousePerson")
                        .HasForeignKey("SpouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_PersonSpouse_Spouse");

                    b.Navigation("Person");

                    b.Navigation("Spouse");
                });

            modelBuilder.Entity("FamilyTree.Entity.Job", b =>
                {
                    b.Navigation("Person");
                });

            modelBuilder.Entity("FamilyTree.Entity.Location", b =>
                {
                    b.Navigation("Person");
                });

            modelBuilder.Entity("FamilyTree.Entity.Person", b =>
                {
                    b.Navigation("FatherOfPersons");

                    b.Navigation("MotherOfPersons");

                    b.Navigation("PersonFamily");

                    b.Navigation("PersonSpouse");

                    b.Navigation("SpousePerson");
                });
#pragma warning restore 612, 618
        }
    }
}