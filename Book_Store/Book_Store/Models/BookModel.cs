using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Book_Store.Models
{
    public class BookModel
    {
        public int ID { get; set; }
        [StringLength(100,MinimumLength =5)]
        [Required(ErrorMessage ="Please Enter the title of your book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please Enter the Author name")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 30)]
        public string Description { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please Enter the number of total pages")]
        public int? TotalPages { get; set; }
        [Display(Name ="Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }

        public string CoverImageUrl { get; set; }
        [Display(Name = "Choose the gallery image of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
        [Display(Name = "Upload Your Book In Pdf format")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
