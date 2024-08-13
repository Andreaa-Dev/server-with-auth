using Backend.src.Abstraction;
using Backend.src.Entity;
using AutoMapper;
using Backend.src.DTO;
using Backend.src.Shared;
using Backend.src.Service.Impl;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.src.Service
{
    public class UserService : IUserService
    {
        protected readonly IUserRepo _userRepo;
        private IConfiguration _config;
        protected readonly IMapper _mapper;

        public UserService(IUserRepo UserRepo, IConfiguration config, IMapper mapper)
        {
            _userRepo = UserRepo;
            _config = config;
            _mapper = mapper;
        }



        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
            var User = _mapper.Map<UserCreateDto, User>(createDto);
            var UserCreated = await _userRepo.CreateOneAsync(User);
            return _mapper.Map<User, UserReadDto>(UserCreated);
        }
        public async Task<UserReadDto?> SignUp(UserCreateDto newUser)
        {

            User? user = await _userRepo.FindOneByEmailAsync(newUser.Email);
            if (user is not null) throw CustomException.InvalidData("User already exists");

            Regex regex = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            if (!regex.IsMatch(newUser.Email)) throw CustomException.InvalidData("Invalid email format");

            // hashed password
            byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt:Pepper"]!);
            PasswordUtils.HashPassword(newUser.Password, out string hashedPassword, pepper);
            newUser.Password = hashedPassword;

            // Replace SignUp with CreateOneAsync or another appropriate method
            var createdUser = await _userRepo.CreateOneAsync(_mapper.Map<User>(newUser));
            return _mapper.Map<UserReadDto>(createdUser);
        }

        public async Task<string?> SignIn(UserSignInDto userSignIn)
        {
            User? user = await _userRepo.FindOneByEmailAsync(userSignIn.Email);
            if (user is null) throw CustomException.NotFound("User does not exist");

            byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt:Pepper"]!);
            bool isCorrectPass = PasswordUtils.VerifyPassword(userSignIn.Password, user.Password, pepper);

            if (!isCorrectPass) throw CustomException.InvalidData("Invalid data");

            IEnumerable<Claim> claims = [
                new Claim(ClaimTypes.Name, $"{user.Name} {user.Email}"),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            ];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }


        public async Task<bool> DeleteOneASync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser is not null)
            {
                return await _userRepo.DeleteOneAsync(foundUser);
            }
            return false;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllAsync(GetAllOptions getAllOptions)
        {
            var UserList = await _userRepo.GetAllAsync(getAllOptions);
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDto>>(UserList);
        }

        public async Task<UserReadDto> GetByIdAsync(Guid id)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"User with {id} is not found");
            }
            return _mapper.Map<User, UserReadDto>(foundUser);
        }

        public async Task<bool> UpdateOneAsync(Guid id, UserUpdateDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(id);
            if (foundUser == null)
            {
                return false;
            }
            _mapper.Map(updateDto, foundUser);
            return await _userRepo.UpdateOneAsync(foundUser);

        }

        public async Task<User?> FindOneByEmailAsync(string email)
        {
            return await _userRepo.FindOneByEmailAsync(email);
        }
    }


}