using Book_Store.Data;
using Book_Store.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Repository
{
    public class BookRepository
    {
        private BookStoreContex _context = null;
        public BookRepository(BookStoreContex contex)
        {
            _context = contex;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                Language = model.Language,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl=model.BookPdfUrl
            };
            newBook.bookGallery = new List<BookGallery>();
           //// var gallery = new List<BookGallery>();
            foreach(var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name=file.Name,
                    URL=file.URL,
                });
            }
         await   _context.Books.AddAsync(newBook);
            await  _context.SaveChangesAsync();
            return newBook.ID;

        }
        public  async Task<List<BookModel>> GetallBook()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if(allbooks?.Any()==true)
            {
                foreach (var book in allbooks)
                {

                    books.Add(new BookModel()
                    {
                        Author=book.Author,
                        Category=book.Category,
                        Description=book.Description,
                        ID=book.ID,
                        Language=book.Language,
                        Title=book.Title,
                        TotalPages=book.TotalPages,
                        CoverImageUrl=book.CoverImageUrl


                    });


                }

            }
            return books;
        }
        public async Task<List<BookModel>> GetTopBooksAsync()
        {
            return await _context.Books
                  .Select(book => new BookModel()
                  {
                      Author = book.Author,
                      Category = book.Category,
                      Description = book.Description,
                      ID = book.ID,
                   
                      Title = book.Title,
                      TotalPages = book.TotalPages,
                      CoverImageUrl = book.CoverImageUrl
                  }).Take(5).ToListAsync();
            //var books = new List<BookModel>();
            //var allbooks = await _context.Books.ToListAsync();
            //if (allbooks?.Any() == true)
            //{
            //    foreach (var book in allbooks)
            //    {

            //        books.Add(new BookModel()
            //        {
            //            Author = book.Author,
            //            Category = book.Category,
            //            Description = book.Description,
            //            ID = book.ID,
            //            Language = book.Language,
            //            Title = book.Title,
            //            TotalPages = book.TotalPages,
            //            CoverImageUrl = book.CoverImageUrl


            //        }).Take(5);


            //    }

            //}
            //return books;
        }
        public async Task< BookModel> GetBookByID(int id)
        {
            return await _context.Books.Where(x => x.ID == id)
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     ID = book.ID,
                    // LanguageId = book.LanguageId,
                    // Language = book.Language.Name,
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverImageUrl = book.CoverImageUrl,
                     
                     Gallery = book.bookGallery.Select(g => new GalleryModel()
                     {
                        ID = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList(),
                     BookPdfUrl=book.BookPdfUrl
                    // BookPdfUrl = book.BookPdfUrl
                 }).FirstOrDefaultAsync();

            //var book = await _context.Books.FindAsync(id);
            //if(book!=null)
            //{
            //    var bookdetils = new BookModel()
            //    {
            //        Author = book.Author,
            //        Category = book.Category,
            //        Description = book.Description,
            //        ID = book.ID,
            //        Language = book.Language,
            //        Title = book.Title,
            //        TotalPages = book.TotalPages,
            //        CoverImageUrl = book.CoverImageUrl,
            //        Gallery = book.bookGallery.Select(g => new GalleryModel()
            //        {
            //            ID = g.Id,
            //            Name=g.Name,
            //            URL=g.URL
            //        }).ToList()

            //    };
            //    return bookdetils;
            //}
            //return null;

        }
        public List<BookModel> SearchBooks (string title,string authorName)
        {
            return DataSource().Where(x => x.Title==title && x.Author == authorName).ToList();
        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){ID=1,Title="MVC",Author="Sandip" ,Description="This is Mvc Book",Category="Programming",Language="English",TotalPages=200},
                new BookModel(){ID=2,Title="Java",Author="Mistry",Description="This is Java Book",Category="Balsal",Language="Bangla",TotalPages=300},
                new BookModel(){ID=3,Title="C#",Author="Meghla",Description="This is C# Book",Category="Don",Language="English",TotalPages=400},
                new BookModel(){ID=4,Title="C++",Author="Mazumder",Description="This is C++ Book",Category="Sandip",Language="Mistry",TotalPages=500},

            };
        }
    }
}
