using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;

namespace BookStore.Application.UserOperations.Commands.CreateToken
{
  public class CreateTokenCommand
  {
    public LoginModel Model { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
      _dbContext = dbContext;
      _mapper = mapper;
      _configuration = configuration;
    }

    public Token Handle()
    {
      User user = _dbContext.Users.FirstOrDefault(user => user.Email == Model.Email && user.Password == Model.Password);
      if (user is not null)
      {
        TokenHandler handler = new TokenHandler(_configuration);
        Token token = handler.CreateAccessToken(user);
        user.RefreshToken = token.RefreshToken;
        user.RefreshTokenExpireDate = token.ExpirationDate.AddMinutes(5);

        _dbContext.SaveChanges();
        return token;
      }
      else
      {
        throw new InvalidOperationException("Kulanıcı adı veya şifre yanlış.");
      }
    }

  }

  public class LoginModel
  {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}