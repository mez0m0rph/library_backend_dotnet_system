namespace project
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAll([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10, // если в запросе (в URL после "?" не будет ничего стоять, то выберутся значения по дефолту (мы их объявили)) 
            [FromQuery] string? searchTerm = null)
        {
            var books = await _bookService.GetBooksAsync(pageNumber, pageSize, searchTerm);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookDto dto)
        {
            await _bookService.CreateBookAsync(dto);
            return NoContent();
        }
    }
}