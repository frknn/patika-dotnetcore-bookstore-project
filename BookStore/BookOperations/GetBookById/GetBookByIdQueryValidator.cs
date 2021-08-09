using FluentValidation;

namespace BookStore.BookOperations.GetBookById
{
  public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
  {
    public GetBookByIdQueryValidator()
    {
      RuleFor(query => query.Id).GreaterThan(0);
    }
  }
}