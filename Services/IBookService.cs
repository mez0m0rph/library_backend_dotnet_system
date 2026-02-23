namespace project
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetBooksAsync(int pageNumber, int pageSize, string? searchTerm);

        Task<BookDto?> GetBookByIdAsync(int id);

        Task UpdateAsync (int id, CreateBookDto dto);

        Task CreateBookAsync(CreateBookDto dto);
    }
}