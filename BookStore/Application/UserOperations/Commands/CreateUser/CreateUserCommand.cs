using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Application.UserOperations.Commands.CreateUser
{
  public class CreateUserCommand
  {
    public CreateUserModel Model { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
    {
      _dbContext = context;
      _mapper = mapper;
    }

    public void Handle()
    {
      User user = _dbContext.Users.SingleOrDefault(user => user.Email == Model.Email);
      if (user is not null)
      {
        throw new InvalidOperationException("Kullanıcı zaten mevcut.");
      }

      user = _mapper.Map<User>(Model);

      _dbContext.Users.Add(user);
      _dbContext.SaveChanges();
    }
  }

  public class CreateUserModel
  {
    private string firstName;
    public string FirstName
    {
      get { return firstName; }
      set { firstName = value.Trim(); }
    }
    private string lastName;

    public string LastName
    {
      get { return lastName; }
      set { lastName = value.Trim(); }
    }

    private string email;
    public string Email
    {
      get { return email; }
      set { email = value.Trim(); }
    }
    public string Password { get; set; }
  }

}