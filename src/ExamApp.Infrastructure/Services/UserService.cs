using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExamApp.Core.Domain;
using ExamApp.Core.Repositories;
using ExamApp.Infrastructure.DTO;
using ExamApp.Infrastructure.Extensions;

namespace ExamApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);

            return _mapper.Map<AccountDto>(user);
        }

        public async Task<IEnumerable<AccountDto>> BrowseAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<AccountDto>>(users);
        }

        public async Task RegisterAsync(Guid userId, string email, string name, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if(user != null)
            {
                throw new Exception($"User with email: '{email}' already exists.");
            }
            user = new User(userId, email, password, name, role);
            await _userRepository.AddAsync(user);
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if(user == null)
            {
                throw new Exception("Invalid credentials.");
            }
            if(user.Password != password)
            {
                throw new Exception("Invalid credentials.");
            }
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };
        }
    }
}