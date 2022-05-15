using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book_Store.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Components
{
    public class TopBooksViewComponent:ViewComponent
    {
        private readonly BookRepository _bookRepository;
        public TopBooksViewComponent(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _bookRepository.GetTopBooksAsync();
            return View(books);
        }
    }
}
