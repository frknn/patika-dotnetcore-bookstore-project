using System;
using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
  public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
  {
    public UpdateGenreCommandValidator()
    {
      RuleFor(command => command.Id).GreaterThan(0);
      RuleFor(command => command.Model.Name).MinimumLength(4).When(command => command.Model.Name != string.Empty);
    }
  }
}