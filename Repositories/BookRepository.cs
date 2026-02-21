using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace project
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        
        public BookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var result = await _appDbContext.Books.ToListAsync();
            return result;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var res_book = await _appDbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
            return res_book;
        }

        public async Task AddAsync(Book book)
        {
            await _appDbContext.Books.AddAsync(book);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _appDbContext.Books.FindAsync(id);

            if(task != null)
            {
                _appDbContext.Books.Remove(task);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}