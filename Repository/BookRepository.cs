﻿using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        //Get all books
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,    
                Description = x.Description
            }).ToListAsync();

            return records;
        }
        // GET: api/Books/5
        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var records = await _context.Books.Where(x=>x.Id==bookId).Select(x => new BookModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            }).FirstOrDefaultAsync();

            return records;
        }
        // POST: api/Books
        public async Task<int> AddBooksAsync(BookModel bookModel)
        {
            var book = new Books()
            {
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book.Id;
        }
        // PUT: api/Books/5
        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            //var book = await _context.Books.FindAsync(bookId);
            //if (book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;

            //    await _context.SaveChangesAsync();
            //}

            var book = new Books()
            {
                Id=bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        //Delete api/books/1
        public async Task DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

    }
}
