using Backend.src.Abstraction;
using Backend.src.Entity;
using AutoMapper;
using Backend.src.DTO;
using Backend.src.Shared;
using Backend.src.Service.Impl;
using System.Text;
using System.Text.RegularExpressions;


namespace Backend.src.Service
{
    public class UserService : IUserService
    {
        protected readonly IUserRepo _userRepo;
        private readonly IConfiguration _config;
        protected readonly IMapper _mapper;

        public UserService(IUserRepo UserRepo, IConfiguration config, IMapper mapper)
        {
            _userRepo = UserRepo;
            _config = config;
            _mapper = mapper;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _userRepo.EmailExistsAsync(email);
        }

        // register
        public async Task<UserReadDto> CreateOneAsync(UserCreateDto createDto)
        {
            var isExisted = await _userRepo.EmailExistsAsync(createDto.Email);
            // check if user already exist also password
            if (isExisted)
            {
                throw CustomException.BadRequest("The provided email address is already in use.");
            }

            PasswordUtils.HashPassword(createDto.Password, out string hashedPassword, out byte[] salt);
            var user = _mapper.Map<UserCreateDto, User>(createDto);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Customer;
            var savedUser = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<User, UserReadDto>(savedUser);
        }
        // sign in
        public async Task<string> SignInAsync(UserSignInDto userSignIn)
        {
            var foundByEmail = await _userRepo.FindByEmailAsync(userSignIn.Email);
            if (foundByEmail is null)
            {
                throw CustomException.UnAuthorized();
            }
            var passwordMatched = PasswordUtils.VerifyPassword(userSignIn.Password, foundByEmail.Password, foundByEmail.Salt);
            if (passwordMatched)
            {
                var tokenUtils = new TokenUtils(_config);
                return tokenUtils.GenerateToken(foundByEmail);
            }
            throw CustomException.UnAuthorized();
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

        public async Task<UserReadDto> FindByEmailAsync(string email)
        {
            var foundUser = await _userRepo.FindByEmailAsync(email);
            if (foundUser == null)
            {
                throw CustomException.NotFound($"User with {email} is not found");
            }
            return _mapper.Map<User, UserReadDto>(foundUser);

        }


        // should only run 1 time
        public async Task<UserReadDto> CreateAdminAsync(UserCreateDto createDto)
        {
            PasswordUtils.HashPassword(createDto.Password, out string hashedPassword, out byte[] salt);
            var user = _mapper.Map<UserCreateDto, User>(createDto);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = Role.Admin;
            var savedUser = await _userRepo.CreateOneAsync(user);
            return _mapper.Map<User, UserReadDto>(savedUser);
        }

        // make other become admin
        public async Task<bool> UpdateAdminAsync(Guid userId)
        {
            var foundUser = await _userRepo.GetByIdAsync(userId);
            if (foundUser == null)
            {
                return false;
            }
            foundUser.Role = Role.Admin;

            return await _userRepo.UpdateOneAsync(foundUser);

        }

    }


}