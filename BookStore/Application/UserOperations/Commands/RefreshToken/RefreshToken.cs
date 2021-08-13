using System;
using System.Linq;
using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;

namespace BookStore.Application.UserOperations.Commands.RefreshToken
{
  public class RefreshTokenCommand
  {
    public string RefreshToken { get; set; }
    private readonly IBookStoreDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
    {
      _dbContext = dbContext;
      _configuration = configuration;
    }

    public Token Handle()
    {
      User user = _dbContext.Users.FirstOrDefault(user => user.RefreshToken == RefreshToken && user.RefreshTokenExpireDate > DateTime.Now);
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
        throw new InvalidOperationException("Geçerli bir Refresh Token bulunamadı.");
      }
    }
  }
}
