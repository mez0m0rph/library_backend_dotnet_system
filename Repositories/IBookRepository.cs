using Microsoft.EntityFrameworkRepository;

namespace project
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);

        Task AddAsync(Book book);

        Task DeleteAsync(int id);
    }
}