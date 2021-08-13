using AutoMapper;
using BookStore.Application.UserOperations.Commands.CreateToken;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Application.UserOperations.Commands.RefreshToken;
using BookStore.DBOperations;
using BookStore.TokenOperations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
  [ApiController]
  [Route("[Controller]s")]
  public class UserController : ControllerBase
  {
    private readonly IBookStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
      _context = context;
      _mapper = mapper;
      _configuration = configuration;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserModel newUser)
    {
      CreateUserCommand command = new CreateUserCommand(_context, _mapper);
      command.Model = newUser;
      command.Handle();

      return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] LoginModel loginInfo)
    {
      CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
      command.Model = loginInfo;
      Token token = command.Handle();

      return token;
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
      RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
      command.RefreshToken = token;
      Token resultAccessToken = command.Handle();
      return resultAccessToken;
    }
  }
}