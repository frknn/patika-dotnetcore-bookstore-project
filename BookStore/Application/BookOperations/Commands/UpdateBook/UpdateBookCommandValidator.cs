using FluentValidation;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
  public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
  {
    public UpdateBookCommandValidator()
    {
      RuleFor(command => command.Id).GreaterThan(0);
      RuleFor(command => command.Model.GenreId).GreaterThan(-1);
      RuleFor(command => command.Model.AuthorId).GreaterThan(-1);
      RuleFor(command => command.Model.Title).MinimumLength(4).When(command => command.Model.Title != string.Empty);
      RuleFor(command => command.Model.PageCount).GreaterThan(-1);
    }
  }
}