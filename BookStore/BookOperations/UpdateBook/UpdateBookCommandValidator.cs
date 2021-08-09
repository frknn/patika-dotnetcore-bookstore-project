using System;
using FluentValidation;

namespace BookStore.BookOperations.UpdateBook
{
  public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
  {
    public UpdateBookCommandValidator()
    {
      RuleFor(command => command.Id).GreaterThan(0);
      RuleFor(command => command.Model.GenreId).GreaterThan(0);
      RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4);
      RuleFor(command => command.Model.PageCount).GreaterThan(0);
      RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
    }
  }
}