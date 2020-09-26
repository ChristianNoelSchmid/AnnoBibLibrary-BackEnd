using System.Collections.Generic;
using AnnoBibLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnoBibLibrary
{
    public class AppDbContext : DbContext
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Annotation> Annotations { get; set; }

        public DbSet<AnnotationLink> AnnotationLinks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Library>()
                .HasData(
                    new Library
                    {
                        Id = 1,
                        Title = "J.R.R. Tolkien Library",
                        Description = "The collected works of J.R.R. Tolkien, a 20th century fantasy writer.",
                        KeywordGroups = "['Places', 'People', 'Concepts']",
                    },
                    new Library
                    {
                        Id = 2,
                        Title = "C.S. Lewis Library",
                        Description = "The collected works of C.S. Lewis, a 20th century fantasy/science-fiction/non-fiction writer.",
                        KeywordGroups = "['Places', 'People', 'Concepts']",
                    }
                );
            builder.Entity<Source>()
                .HasData(
                    new Source
                    {
                        Id = 1,
                        Type = "Simple",
                        Fields = "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Lord of the Rings: the Fellowship of the Ring\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"J.R.R. Tolkien\"] }]}",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {
                        Id = 2,
                        Type = "Simple",
                        Fields = "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Lord of the Rings: the Two Towers\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"J.R.R. Tolkien\"] }]}",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {
                        Id = 3,
                        Type = "Simple",
                        Fields = "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Lord of the Rings: the Return of the King\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"J.R.R. Tolkien\"] }]}",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {                            
                        Id = 4,
                        Type = "Simple",
                        Fields = "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Chronicles of Narnia: the Lion, the Witch, and the Wardrobe\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"C.S. Lewis\"] }]}",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {                            
                        Id = 5,
                        Type = "Simple",
                        Fields = "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Chronicles of Narnia: Prince Caspian\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"C.S. Lewis\"] }]}",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {                            
                        Id = 6,
                        Type = "Simple",
                        Fields = "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Chronicles of Narnia: the Magician's Nephew\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"C.S. Lewis\"] }]}",
                        Annotations = new List<Annotation>()
                    }
                );

            builder.Entity<Annotation>()
                .HasData(
                    new Annotation
                    {
                        Id = 1,
                        SourceId = 1,
                        Notes = "Fellowship",
                        QuoteData = "[]"
                    },
                    new Annotation
                    {
                        Id = 2,
                        SourceId = 2,
                        Notes = "Towers",
                        QuoteData = "[]"
                    },
                    new Annotation
                    {
                        Id = 3,
                        SourceId = 3,
                        Notes = "King",
                        QuoteData = "[]"
                    },
                    new Annotation
                    {
                        Id = 4,
                        SourceId = 4,
                        Notes = "Lion",
                        QuoteData = "[]"
                    },
                    new Annotation
                    {
                        Id = 5,
                        SourceId = 5,
                        Notes = "Prince",
                        QuoteData = "[]"
                    },                     
                    new Annotation
                    {
                        Id = 6,
                        SourceId = 6,
                        Notes = "Magician",
                        QuoteData = "[]"
                    }
                );

            builder.Entity<AnnotationLink>()
                .HasData(
                    new AnnotationLink
                    {
                        Id = 1,
                        LibraryId = 1,
                        AnnotationId = 1,
                        KeywordValues = ""
                    },
                    new AnnotationLink
                    {
                        Id = 2,
                        LibraryId = 1,
                        AnnotationId = 2,
                        KeywordValues = ""
                    },                     
                    new AnnotationLink
                    {
                        Id = 3,
                        LibraryId = 1,
                        AnnotationId = 3,
                        KeywordValues = ""
                    },
                    new AnnotationLink
                    {
                        Id = 4,
                        LibraryId = 2,
                        AnnotationId = 4,
                        KeywordValues = ""
                    },
                    new AnnotationLink
                    {
                        Id = 5,
                        LibraryId = 2,
                        AnnotationId = 5,
                        KeywordValues = ""
                    },
                    new AnnotationLink
                    {
                        Id = 6,
                        LibraryId = 2,
                        AnnotationId = 6,
                        KeywordValues = ""
                    }
                );
        }
    }
}