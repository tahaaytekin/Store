using FluentValidation;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace WebApi.Application.Commands.DeleteGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).When(x=> x.Model.Name !=string.Empty);
;        }
    }   
}