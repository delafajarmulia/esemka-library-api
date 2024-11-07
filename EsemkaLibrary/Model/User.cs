using Newtonsoft.Json;

namespace EsemkaLibrary.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
        public string? PhotoPath { get; set; }

        //[JsonIgnore]
        //public List<Borrow>? Borrows { get; set; }

    }
}
