using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests :IClassFixture<CommonTestFixture>
    {
        
        private readonly BookstoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context=testFixture.Context;
            _mapper=testFixture.Mapper;
        }

        [Fact]
        public void WhenAllreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var book=new Book{Title="Test_WhenAllreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",PageCount=100,PublishDate=new System.DateTime(1990,10,11),GenreId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            command.Model=new CreateBookModel() {Title =book.Title};

            FluentActions.Invoking(()=> command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        
        }
        
    }
}