namespace BookShopSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        private string editionType;
        
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(50)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public ICollection<Book> RelatedBooks { get; set; } = new HashSet<Book>();

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public string Edition
        {
            get;
            set;
        }

        public string AgeRestriction
        {
            get;
            set;
        }
    }
}
