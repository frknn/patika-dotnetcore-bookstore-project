using FluentValidation;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorById
{
  public class GetAuthorByIdQueryValidator : AbstractValidator<GetAuthorByIdQuery>
  {
    public GetAuthorByIdQueryValidator()
    {
      RuleFor(query => query.Id).GreaterThan(0);
    }
  }
}