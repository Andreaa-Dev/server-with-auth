using Backend.src.Entity;

namespace Backend.src.DTO
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public Role Role { get; set; }
    }

    public class UserCreateDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

    }

    public class UserUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserSignInDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}