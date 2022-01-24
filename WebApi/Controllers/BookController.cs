using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.DBOperations;

namespace WebApi.AddControllers
{
    [ApiController]

    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //readonly ifadeler sadece contructorlar icinde set edilebilirler
        private readonly BookStoreDbContext _context; // Startup.cs'de servislere ekledigimiz BookStoreDbContext in constructor yardimiyla bir ornegini olusturduk (uygulama icerisinden degistiliemesin diye priavte and readonly)

        public BookController(BookStoreDbContext context)
        {
            _context = context;//yukardaki private context'i artik burda kullanabiliriz. (bir kere burda set ettik ve uygulama icinde bir daha degistirelemez)
        }
        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id=1,
        //         Title="Lean Startup",
        //         GenreId=1, //personal growth et cetera
        //         PageCount=200,
        //         PublishDate=new DateTime(2001,06,12)
        //     },

        //     new Book{
        //         Id=2,
        //         Title="Herland", //science fiction
        //         GenreId=2,
        //         PageCount=202,
        //         PublishDate=new DateTime(2003,02,12)
        //     },
        //     new Book{
        //         Id=3,
        //         Title="Dune",
        //         GenreId=2, //science fiction
        //         PageCount=437,
        //         PublishDate=new DateTime(2008,12,12)
        //     },
        // };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();

            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)//route'tan

        {
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        // [HttpGet]

        // public Book Get([FromQuery] string id)
        // {//query'den
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }


        //---------------POST-----------------
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
            {
                return BadRequest();
            }

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        //---------------PUT-----------------

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
            {
                return BadRequest();
            }

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();
        }

        //---------------DELETE-----------------
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);

            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }

    }
}