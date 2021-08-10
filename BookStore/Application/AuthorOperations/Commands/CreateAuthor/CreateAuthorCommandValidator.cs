using System;
using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
  public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
  {
    public CreateAuthorCommandValidator()
    {
      RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(1);
      RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(1);
      RuleFor(command => command.Model.BirthDate).NotEmpty().LessThan(DateTime.Now.Date);
    }
  }

}