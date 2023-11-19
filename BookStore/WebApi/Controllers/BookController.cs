
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;

namespace WebApi.Controllers{
   
    [ApiController]
    [Route("[controller]s")]
    public class BookController :ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region  CommandSatirlari
        // private static List<Book> BookList =new List<Book>()
        // {
        //         new Book
        //         {
        //             Id=1,
        //             Title="Lean Startup",
        //             GenreId=1, //Personal Growth
        //             PageCount=200,
        //             PublishDate=new DateTime(2001,06,12)
        //         },
        //          new Book
        //         {
        //             Id=2,
        //             Title="Herland",
        //             GenreId=2, //Science Fiction
        //             PageCount=250,
        //             PublishDate=new DateTime(2010,05,23)
        //         },
        //         new Book
        //         {
        //             Id=3,
        //             Title="Dune",
        //             GenreId=2, //Science Fiction
        //             PageCount=540,
        //             PublishDate=new DateTime(2001,12,21)
        //         }

        // };
        #endregion
        #region GET ISLEMI
        [HttpGet]
        public IActionResult GetBooks()
        {
           GetBooksQuery query =new GetBooksQuery(_context,_mapper);
           var result=query.Handle();
           return Ok(result);
        }

         [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
           
                GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
                query.BookId=id;
                GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
           
                return Ok(result);
           
        }
        #endregion
        #region POST ISLEMI
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
           
            command.Model=newBook;
            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
           
            return Ok();
        }
        #endregion
        #region PUT ISLEMI
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
        {
           
                UpdateBookCommand command = new UpdateBookCommand(_context);
                        command.BookId=id;
                        command.Model=updatedBook; 
                        UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
                        validator.ValidateAndThrow(command);

                        command.Handle();       
              
                
           
            
            return Ok();
        }
        #endregion
        #region DELETE ISLEMI
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id){

           
               DeleteBook command =new DeleteBook(_context);
                 command.BookId=id;  
                 DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                 validator.ValidateAndThrow(command);
                    command.Handle();  
            
                    return Ok();    
        }
        #endregion
    }
}