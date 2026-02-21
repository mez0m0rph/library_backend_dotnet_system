namespace project
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync();

        Task<BookDto?> GetBookByIdAsync(int id);

        Task CreateBookAsync(CreateBookDto dto);
    }
}