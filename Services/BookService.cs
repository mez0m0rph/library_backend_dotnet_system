using System.Collections;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;

namespace project
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetBooksAsync(int pageNumber, int pageSize)
        {
            var query = _bookRepository.GetAllQuery();

            query = query.OrderBy(b => b.Id); // сорт для правильности

            var pagedQuery = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return await pagedQuery (b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
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