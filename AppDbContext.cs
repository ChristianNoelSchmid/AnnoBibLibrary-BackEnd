using System.Collections.Generic;
using System.Threading.Tasks;
using AnnoBibLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AnnoBibLibrary
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Annotation> Annotations { get; set; }

        public DbSet<AnnotationLink> AnnotationLinks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) 
            { }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);

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
                        Fields = $"<<title:{(int)FieldType.Proper}>>The Lord of the Rings: the Fellowship of the Ring<<author:{(int)FieldType.Name}>>J.R.R. Tolkien",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {
                        Id = 2,
                        Type = "Simple",
                        Fields = $"<<title:{(int)FieldType.Proper}>>The Lord of the Rings: the Two Towers<<author:{(int)FieldType.Name}>>J.R.R. Tolkien",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {
                        Id = 3,
                        Type = "Simple",
                        Fields = $"<<title:{(int)FieldType.Proper}>>The Lord of the Rings: the Return of the King<<author:{(int)FieldType.Name}>>J.R.R. Tolkien",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {                            
                        Id = 4,
                        Type = "Simple",
                        Fields = $"<<title:{(int)FieldType.Proper}>>The Chronicles of Narnia: the Lion, the Witch, and the Wardrobe<<author:{(int)FieldType.Name}>>C.S. Lewis",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {                            
                        Id = 5,
                        Type = "Simple",
                        Fields = $"<<title:{(int)FieldType.Proper}>>The Chronicles of Narnia: Prince Caspian<<author:{(int)FieldType.Name}>>C.S. Lewis",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {                            
                        Id = 6,
                        Type = "Simple",
                        Fields = $"<<title:{(int)FieldType.Proper}>>The Chronicles of Narnia: the Magician's Nephew<<author:{(int)FieldType.Name}>>C.S. Lewis",
                        Annotations = new List<Annotation>()
                    },
                    new Source
                    {
                        Id = 7,
                        Type = "Simple",
                        Fields = $"<<title:{(int)FieldType.Proper}>>Learning Biblical Hebrew<<author:{(int)FieldType.Name}>>Karl Kutz;;Rebekah Jospberger"
                    }
                );

            builder.Entity<Annotation>()
                .HasData(
                    new Annotation
                    {
                        Id = 1,
                        SourceId = 1,
                        Notes = "Fellowship",
                        QuoteData = "<<24:eggs;;30;;basket>>There are 30 dozen eggs in the basket."
                    },
                    new Annotation
                    {
                        Id = 2,
                        SourceId = 2,
                        Notes = "Towers",
                        QuoteData = ""
                    },
                    new Annotation
                    {
                        Id = 3,
                        SourceId = 3,
                        Notes = "King",
                        QuoteData = ""
                    },
                    new Annotation
                    {
                        Id = 4,
                        SourceId = 4,
                        Notes = "Lion",
                        QuoteData = ""
                    },
                    new Annotation
                    {
                        Id = 5,
                        SourceId = 5,
                        Notes = "Prince",
                        QuoteData = ""
                    },                     
                    new Annotation
                    {
                        Id = 6,
                        SourceId = 6,
                        Notes = "Magician",
                        QuoteData = ""
                    },
                    new Annotation
                    {
                        Id = 7,
                        SourceId = 7,
                        Notes = "Hebrew",
                        QuoteData = ""
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
                    },
                    new AnnotationLink
                    {
                        Id = 7,
                        LibraryId = 2,
                        AnnotationId = 7,
                        KeywordValues = ""
                    }
                );
        }
 
    }
}