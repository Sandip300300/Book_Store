using Book_Store.Models;
using Book_Store.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository=null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookController(BookRepository bookRepository,IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
       public async Task<ViewResult> GetAllBooks()
        {
            var data= await _bookRepository.GetallBook();
            return View(data);
        }
        public async Task<ViewResult> GetBook(int id )
        {
            var data=await _bookRepository.GetBookByID(id);
            return View(data);
        }
        public List<BookModel> SearchBooks(string bookName,string authorName)
        {
            return _bookRepository.SearchBooks(bookName, authorName);
        }
        public ViewResult AddNewBook(bool isSuccess=false, int BookId=0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.bookId = BookId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid)
            {
                if(bookModel.CoverPhoto!=null)
                {
                    string folder = "books/cover/";
                  bookModel.CoverImageUrl=  await UpoladImage(folder,bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";
                    bookModel.Gallery = new List<GalleryModel>();
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UpoladImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                       
                       
                    }
                  
                }
            if (bookModel.BookPdf != null)
            {
                string folder = "books/pdf/";
                bookModel.BookPdfUrl = await UpoladImage(folder, bookModel.BookPdf);
            }
            int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, BookId = id });
                }
            return View();
        }
        private async Task<string> UpoladImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + " " + file.FileName;
            string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            await file.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            return "/" + folderPath;
            //bookModel.CoverImageUrl = "/" + folder;
            //string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
            //await bookModel.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
        }


    }

       
    
}
