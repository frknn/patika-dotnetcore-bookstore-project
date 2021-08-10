using FluentValidation;

namespace BookStore.Application.BookOperations.Queries.GetBookById
{
  public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
  {
    public GetBookByIdQueryValidator()
    {
      RuleFor(query => query.Id).GreaterThan(0);
    }
  }
}