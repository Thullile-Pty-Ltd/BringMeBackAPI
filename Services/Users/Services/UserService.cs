using BringMeBackAPI.Models.Users;
using BringMeBackAPI.Repository.Users;
using BringMeBackAPI.Services.Users.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }

    public async Task<User> RegisterAsync(User user)
    {
        // Validate email uniqueness
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new Exception("Email address is already registered.");
        }

        // Hash password
        user.Password = HashPassword(user.Password);

        await _userRepository.AddUserAsync(user);
        return user;
    }

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        // Retrieve user by email
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Verify password
        if (!VerifyPassword(user.Password, password))
        {
            throw new Exception("Incorrect password.");
        }

        return user;
    }

    private string HashPassword(string password)
    {
        // Use bcrypt for hashing
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    private bool VerifyPassword(string hashedPassword, string password)
    {
        // Verify password using bcrypt
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
