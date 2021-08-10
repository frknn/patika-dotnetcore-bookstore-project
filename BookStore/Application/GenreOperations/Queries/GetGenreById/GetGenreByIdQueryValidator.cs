using FluentValidation;

namespace BookStore.Application.GenreOperations.Queries.GetGenreById
{
  public class GetGenreByIdQueryValidator : AbstractValidator<GetGenreByIdQuery>
  {
    public GetGenreByIdQueryValidator()
    {
      RuleFor(query => query.Id).GreaterThan(0);
    }
  }
}