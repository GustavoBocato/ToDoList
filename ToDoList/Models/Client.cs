using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListApp.Models
{
    [Table("clients")]
    public class Client
    {
        private string _passwordHash = string.Empty;

        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password
        {
            get => _passwordHash;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _passwordHash = HashPassword(value);
                }
            }
        }

        private string HashPassword(string value)
        {
            var hasher = new PasswordHasher<Client>();
            return hasher.HashPassword(this, value);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Client other)
                return false;

            return Id == other.Id;
        }   

        public override int GetHashCode() => Id.GetHashCode();
    }
}
