using System;
using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
  public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
  {
    public UpdateAuthorCommandValidator()
    {
      RuleFor(command => command.Id).GreaterThan(0);
      RuleFor(command => command.Model.FirstName);
      RuleFor(command => command.Model.LastName);
    }
  }
}