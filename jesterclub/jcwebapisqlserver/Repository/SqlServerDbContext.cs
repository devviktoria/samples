using System;
using System.Collections.Generic;
using jcwebapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace jcwebapi.Repository {
    public class SqlServerDbContext : DbContext {
        public DbSet<Joke> Jokes { get; set; }

        public SqlServerDbContext (DbContextOptions<SqlServerDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            DefineJokeEntity (modelBuilder);
            DefineUserEntity (modelBuilder);
            DefineTagEntity (modelBuilder);
            DefineEmotionCounterEntity (modelBuilder);
            DefineResponseStatisticEntity (modelBuilder);
            //DefineJokeTagConnectionEntitiy (modelBuilder);

            //Define Relationships
            // modelBuilder.Entity (nameof (Joke) + nameof (Tag), e => {
            //     e.HasOne (typeof (Tag), null)
            //         .WithMany ()
            //         .HasForeignKey (nameof (Tag.TagId))
            //         .OnDelete (DeleteBehavior.Cascade)
            //         .IsRequired ();

            //     e.HasOne (typeof (Joke), null)
            //         .WithMany ()
            //         .HasForeignKey (nameof (Joke.JokeId))
            //         .OnDelete (DeleteBehavior.Cascade)
            //         .IsRequired ();
            // });
            // modelBuilder
            //     .Entity<Joke> ()
            //     .HasMany (j => j.Tags)
            //     .WithMany (t => t.Jokes)
            //     .UsingEntity (j => j.ToTable ((nameof (Joke) + nameof (Tag))));
            modelBuilder.Entity<Joke> ()
                .HasMany (j => j.Tags)
                .WithMany (t => t.Jokes)
                .UsingEntity<Dictionary<string, object>> (
                    nameof (Joke) + nameof (Tag),
                    jt => jt
                    .HasOne<Tag> ()
                    .WithMany ()
                    .HasForeignKey (nameof (Tag.TagId))
                    .HasConstraintName ("FK_JokeTag_Tag_TagId")
                    .OnDelete (DeleteBehavior.Cascade),
                    jt => jt
                    .HasOne<Joke> ()
                    .WithMany ()
                    .HasForeignKey (nameof (Joke.JokeId))
                    .HasConstraintName ("FK_JokeTag_Joke_JokeId")
                    .OnDelete (DeleteBehavior.ClientCascade));

            modelBuilder.Entity<Joke> ()
                .HasMany (j => j.EmotionCounters)
                .WithOne ();

            modelBuilder.Entity<Joke> ()
                .HasMany (j => j.ResponseStatistics)
                .WithOne ();

            modelBuilder.Entity<Joke> ()
                .HasOne (j => j.User)
                .WithMany (u => u.Jokes);

        }

        private void DefineJokeEntity (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Joke> ()
                .Property (j => j.JokeId)
                .HasColumnName (nameof (Joke.JokeId))
                .ValueGeneratedOnAdd ()
                .HasColumnType ("int")
                .HasAnnotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Joke> ()
                .Property (j => j.JokeText)
                .HasColumnName (nameof (Joke.JokeText))
                .HasColumnType ("nvarchar(600)")
                .IsRequired ();

            modelBuilder.Entity<Joke> ()
                .Property (j => j.Source)
                .HasColumnName (nameof (Joke.Source))
                .HasColumnType ("nvarchar(300)");

            modelBuilder.Entity<Joke> ()
                .Property (j => j.Copyright)
                .HasColumnName (nameof (Joke.Copyright))
                .HasColumnType ("nvarchar(200)");

            modelBuilder.Entity<Joke> ()
                .Property (j => j.CreationDate)
                .HasColumnName (nameof (Joke.CreationDate))
                .HasColumnType ("datetime2")
                .IsRequired ();

            modelBuilder.Entity<Joke> ()
                .Property (j => j.ReleasedDate)
                .HasColumnName (nameof (Joke.ReleasedDate))
                .HasColumnType ("datetime2");

            modelBuilder.Entity<Joke> ()
                .Property (j => j.ResponseSum)
                .HasColumnName (nameof (Joke.ResponseSum))
                .HasColumnType ("int")
                .HasDefaultValue (0);

            modelBuilder.Entity<Joke> ().HasKey (j => j.JokeId);

            modelBuilder.Entity<Joke> ().ToTable (nameof (Joke));
        }

        private void DefineUserEntity (ModelBuilder modelBuilder) {
            modelBuilder.Entity<User> ()
                .Property (u => u.UserId)
                .HasColumnName (nameof (User.UserId))
                .ValueGeneratedOnAdd ()
                .HasColumnType ("int")
                .HasAnnotation ("SqlServer : ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<User> ()
                .Property (u => u.UserEmail)
                .HasColumnName (nameof (User.UserEmail))
                .HasColumnType ("nvarchar(320)")
                .IsRequired ();

            modelBuilder.Entity<User> ()
                .Property (u => u.UserName)
                .HasColumnName (nameof (User.UserName))
                .HasColumnType ("nvarchar(100)")
                .IsRequired ();

            modelBuilder.Entity<User> ().HasKey (u => u.UserId);

            modelBuilder.Entity<User> ().ToTable (nameof (User));

        }

        private void DefineTagEntity (ModelBuilder modelBuilder) {
            modelBuilder.Entity<Tag> ()
                .Property (t => t.TagId)
                .HasColumnName (nameof (Tag.TagId))
                .ValueGeneratedOnAdd ()
                .HasColumnType ("int")
                .HasAnnotation ("SqlServer : ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<Tag> ()
                .Property (t => t.Name)
                .HasColumnName (nameof (Tag.Name))
                .HasColumnType ("nvarchar(150)")
                .IsRequired ();

            modelBuilder.Entity<Tag> ().HasKey (t => t.TagId);

            modelBuilder.Entity<Tag> ().ToTable (nameof (Tag));
        }

        private void DefineEmotionCounterEntity (ModelBuilder modelBuilder) {
            modelBuilder.Entity<EmotionCounter> ()
                .Property<int> (nameof (Joke.JokeId))
                .HasColumnName (nameof (Joke.JokeId))
                .HasColumnType ("int")
                .IsRequired ();

            modelBuilder.Entity<EmotionCounter> ()
                .Property (e => e.Emotion)
                .HasColumnName (nameof (EmotionCounter.Emotion))
                .HasColumnType ("nvarchar(50)")
                .IsRequired ();
            modelBuilder.Entity<EmotionCounter> ()
                .Property (e => e.Counter)
                .HasColumnName (nameof (EmotionCounter.Counter))
                .HasColumnType ("int")
                .IsRequired ();

            modelBuilder.Entity<EmotionCounter> ().HasKey (nameof (Joke.JokeId), nameof (EmotionCounter.Emotion));

            modelBuilder.Entity<EmotionCounter> ().ToTable (nameof (EmotionCounter));
        }
        private void DefineResponseStatisticEntity (ModelBuilder modelBuilder) {
            modelBuilder.Entity<ResponseStatistic> ()
                .Property<int> (nameof (Joke.JokeId))
                .HasColumnName (nameof (Joke.JokeId))
                .HasColumnType ("int")
                .IsRequired ();

            modelBuilder.Entity<ResponseStatistic> ()
                .Property (r => r.Day)
                .HasColumnName (nameof (ResponseStatistic.Day))
                .HasColumnType ("int")
                .IsRequired ();
            modelBuilder.Entity<ResponseStatistic> ()
                .Property (r => r.Counter)
                .HasColumnName (nameof (EmotionCounter.Counter))
                .HasColumnType ("int")
                .IsRequired ();

            modelBuilder.Entity<ResponseStatistic> ().HasKey (nameof (Joke.JokeId), nameof (ResponseStatistic.Day));

            modelBuilder.Entity<ResponseStatistic> ().ToTable (nameof (ResponseStatistic));
        }

        private void DefineJokeTagConnectionEntitiy (ModelBuilder modelBuilder) {
            modelBuilder.Entity (nameof (Joke) + nameof (Tag), e => {
                e.Property<int> (nameof (Joke.JokeId))
                    .HasColumnType ("int")
                    .IsRequired ();

                e.Property<int> (nameof (Tag.TagId))
                    .HasColumnType ("int")
                    .IsRequired ();

                e.HasKey (nameof (Joke.JokeId), nameof (Tag.TagId));

                e.ToTable (nameof (Joke) + nameof (Tag));
            });

        }

    }
}