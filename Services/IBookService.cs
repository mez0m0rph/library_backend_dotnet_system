namespace project
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync(int pageNumber, int pageSize);

        Task<BookDto?> GetBookByIdAsync(int id);

        Task CreateBookAsync(CreateBookDto dto);
    }
}