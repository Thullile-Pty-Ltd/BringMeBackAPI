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
        // Validate email uniqueness (optional)
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new Exception("Email address is already registered.");
        }

        // Hash password (recommended for security)
        user.Password = HashPassword(user.Password); // Implement your hash method

        await _userRepository.AddUserAsync(user);
        return user;
    }

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        // Retrieve user by email (assuming email is unique)
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        // Validate password (compare hashed passwords)
        if (!VerifyPassword(user.Password, password)) // Implement your verification method
        {
            throw new Exception("Incorrect password.");
        }

        return user;
    }

    // Example hash and verify password methods (replace with your implementation)
    private string HashPassword(string password)
    {
        // Implement hashing algorithm (e.g., bcrypt)
        return password; // Placeholder, replace with actual hash
    }

    private bool VerifyPassword(string hashedPassword, string password)
    {
        // Implement password verification logic
        return hashedPassword == password; // Placeholder, replace with actual verification
    }
}
