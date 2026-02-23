using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace project
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync(int pageNumber, int pageSize, string? searchTerm)
        {
            IQueryable<Book> query = _bookRepository.GetAllQuery();

            if (!string.IsNullOrWhiteSpace(searchTerm))  // фильтр
            {
                var searchLower = searchTerm.ToLower();

                query = query.Where(b => 
                b.Title.ToLower().Contains(searchLower) ||
                b.Author.ToLower().Contains(searchLower));
            }

            var pagedQuery = query
                .OrderBy(b => b.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await pagedQuery
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Author = b.Author,
                    Title = b.Title,
                    IsAvailable = b.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book != null)
            {
                return new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    IsAvailable = book.IsAvailable
                };
            }
            return null;
        }

        public async Task UpdateBookAsync(int id, CreateBookDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book != null)
            {
                book.Title = dto.Title;
                book.Author = dto.Author;

                await _bookRepository.SaveChangesAsync();
            }
        }

        public async Task CreateBookAsync(CreateBookDto dto)
        {
            var bookEntity = new Book
            {
                Title = dto.Title, 
                Author = dto.Author,
                IsAvailable = true
            };

            await _bookRepository.AddAsync(bookEntity);

        }
    }
}