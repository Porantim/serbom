using System;
using System.Security.Cryptography;
using System.Text;
using Serbom.Domain.Model;

namespace Serbom.Domain;

public class UserService : BaseService
{
    public UserService(string currentUserEmail) : base(currentUserEmail){}
    public UserService() : base(){}

    public User? Get(int id)
    {
        using(var context = new SerbomContext())
        {
            return context.Users.Find(id);
        }
    }

    public User? Get(string email) {
        using(var context = new SerbomContext())
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }
    }

    public List<User> List()
    {
        using(var context = new SerbomContext())
        {
            return context.Users.ToList();
        }
    }

    public int Insert(string? name, string? email, string? password)
    {
        if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Email and password are required");
        }

        var salt = GenerateSalt();
        var hashedPassword = HashPassword(password, salt);

        var user = new User
        {
            Name = name ?? email,
            Email = email,
            Secret = hashedPassword,
            Salt = salt,
            Active = true
        };

        using(var context = new SerbomContext()) {

            var userFound = context.Users.FirstOrDefault(u => u.Email == email);
            if(userFound != null)
            {
                throw new ArgumentException("Email already in use");
            }

            context.Users.Add(user);
            context.SaveChanges();

            context.Histories.Add(new History
            {
                Action = "create",
                EntityType = "User",
                User = _currentUser.Id,
                Date = DateTime.Now
            });
            context.SaveChanges();

            return user.Id;
        }
    }

    public string? Authenticate(string? email, string? password)
    {
        if(String.IsNullOrEmpty(email) || String.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Email and password are required");
        }

        using(var context = new SerbomContext())
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return null;
            }

            var hashedPassword = HashPassword(password, user.Salt);
            if(hashedPassword != user.Secret)
            {
                return null;
            }

            var tokenService = new TokenService(email);

            return tokenService.Generate(user);
        }
    }

    private string HashPassword(string password, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);

        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }
    }

    private string GenerateSalt()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
    }

}
