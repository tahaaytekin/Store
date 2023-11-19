using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests :IClassFixture<CommonTestFixture>
    {

         [Theory]
         [InlineData("asdassdsa",0,0)]
         [InlineData("asdassdsa",0,1)]
         [InlineData("taha",1,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int PageCount,int GenreId)
        {
            CreateBookCommand command =new CreateBookCommand(null,null);
            command.Model =new CreateBookModel()
            {
                Title =title,
                PageCount=PageCount,
                PublishDate=DateTime.Now.AddYears(-1),
                GenreId=GenreId,
            };

            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            var result =validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
