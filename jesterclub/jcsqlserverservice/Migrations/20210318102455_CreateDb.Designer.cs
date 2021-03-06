﻿// <auto-generated />
using System;
using jcsqlserverservice.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace jcsqlserverservice.Migrations {
    [DbContext (typeof (SqlServerDbContext))]
    [Migration ("20210318102455_CreateDb")]
    partial class CreateDb {
        protected override void BuildTargetModel (ModelBuilder modelBuilder) {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation ("Relational:MaxIdentifierLength", 128)
                .HasAnnotation ("ProductVersion", "5.0.4")
                .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity ("JokeTag", b => {
                b.Property<int> ("JokeId")
                    .HasColumnType ("int");

                b.Property<int> ("TagId")
                    .HasColumnType ("int");

                b.HasKey ("JokeId", "TagId");

                b.HasIndex ("TagId");

                b.ToTable ("JokeTag");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.EmotionCounter", b => {
                b.Property<int> ("JokeId")
                    .HasColumnType ("int")
                    .HasColumnName ("JokeId");

                b.Property<string> ("Emotion")
                    .HasColumnType ("nvarchar(50)")
                    .HasColumnName ("Emotion");

                b.Property<int> ("Counter")
                    .HasColumnType ("int")
                    .HasColumnName ("Counter");

                b.HasKey ("JokeId", "Emotion");

                b.ToTable ("EmotionCounter");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.Joke", b => {
                b.Property<int> ("JokeId")
                    .ValueGeneratedOnAdd ()
                    .HasColumnType ("int")
                    .HasColumnName ("JokeId")
                    .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string> ("Copyright")
                    .HasColumnType ("nvarchar(200)")
                    .HasColumnName ("Copyright");

                b.Property<DateTime> ("CreationDate")
                    .HasColumnType ("datetime2")
                    .HasColumnName ("CreationDate");

                b.Property<string> ("JokeText")
                    .IsRequired ()
                    .HasColumnType ("nvarchar(600)")
                    .HasColumnName ("JokeText");

                b.Property<DateTime?> ("ReleasedDate")
                    .HasColumnType ("datetime2")
                    .HasColumnName ("ReleasedDate");

                b.Property<int> ("ResponseSum")
                    .ValueGeneratedOnAdd ()
                    .HasColumnType ("int")
                    .HasDefaultValue (0)
                    .HasColumnName ("ResponseSum");

                b.Property<string> ("Source")
                    .HasColumnType ("nvarchar(300)")
                    .HasColumnName ("Source");

                b.Property<int?> ("UserId")
                    .HasColumnType ("int");

                b.HasKey ("JokeId");

                b.HasIndex ("UserId");

                b.ToTable ("Joke");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.ResponseStatistic", b => {
                b.Property<int> ("JokeId")
                    .HasColumnType ("int")
                    .HasColumnName ("JokeId");

                b.Property<int> ("Day")
                    .HasColumnType ("int")
                    .HasColumnName ("Day");

                b.Property<int> ("Counter")
                    .HasColumnType ("int")
                    .HasColumnName ("Counter");

                b.HasKey ("JokeId", "Day");

                b.ToTable ("ResponseStatistic");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.Tag", b => {
                b.Property<int> ("TagId")
                    .ValueGeneratedOnAdd ()
                    .HasColumnType ("int")
                    .HasColumnName ("TagId")
                    .HasAnnotation ("SqlServer : ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                    .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string> ("Name")
                    .IsRequired ()
                    .HasColumnType ("nvarchar(150)")
                    .HasColumnName ("Name");

                b.HasKey ("TagId");

                b.ToTable ("Tag");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.User", b => {
                b.Property<int> ("UserId")
                    .ValueGeneratedOnAdd ()
                    .HasColumnType ("int")
                    .HasColumnName ("UserId")
                    .HasAnnotation ("SqlServer : ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                    .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string> ("UserEmail")
                    .IsRequired ()
                    .HasColumnType ("nvarchar(320)")
                    .HasColumnName ("UserEmail");

                b.Property<string> ("UserName")
                    .IsRequired ()
                    .HasColumnType ("nvarchar(100)")
                    .HasColumnName ("UserName");

                b.HasKey ("UserId");

                b.ToTable ("User");
            });

            modelBuilder.Entity ("JokeTag", b => {
                b.HasOne ("jcsqlserverservice.Models.Joke", null)
                    .WithMany ()
                    .HasForeignKey ("JokeId")
                    .HasConstraintName ("FK_JokeTag_Joke_JokeId")
                    .OnDelete (DeleteBehavior.ClientCascade)
                    .IsRequired ();

                b.HasOne ("jcsqlserverservice.Models.Tag", null)
                    .WithMany ()
                    .HasForeignKey ("TagId")
                    .HasConstraintName ("FK_JokeTag_Tag_TagId")
                    .OnDelete (DeleteBehavior.Cascade)
                    .IsRequired ();
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.EmotionCounter", b => {
                b.HasOne ("jcsqlserverservice.Models.Joke", null)
                    .WithMany ("EmotionCounters")
                    .HasForeignKey ("JokeId")
                    .OnDelete (DeleteBehavior.Cascade)
                    .IsRequired ();
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.Joke", b => {
                b.HasOne ("jcsqlserverservice.Models.User", "User")
                    .WithMany ("Jokes")
                    .HasForeignKey ("UserId");

                b.Navigation ("User");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.ResponseStatistic", b => {
                b.HasOne ("jcsqlserverservice.Models.Joke", null)
                    .WithMany ("ResponseStatistics")
                    .HasForeignKey ("JokeId")
                    .OnDelete (DeleteBehavior.Cascade)
                    .IsRequired ();
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.Joke", b => {
                b.Navigation ("EmotionCounters");

                b.Navigation ("ResponseStatistics");
            });

            modelBuilder.Entity ("jcsqlserverservice.Models.User", b => {
                b.Navigation ("Jokes");
            });
#pragma warning restore 612, 618
        }
    }
}