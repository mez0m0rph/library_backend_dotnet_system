using Microsoft.EntityFrameworkRepository;

namespace project
{
    public interface IBookRepository
    {
        IQueryable<Book> GetAllQuery();

        Task<Book?> GetByIdAsync(int id);

        Task AddAsync(Book book);

        Task DeleteAsync(int id);
    }
}