using Newtonsoft.Json;

namespace EsemkaLibrary.Model
{
    public class Borrow
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public DateTime? BorrowAt { get; set; }
        public DateTime? DueAt { get; set; }
        public DateTime? ReturnAt { get; set; }

        [JsonIgnore]
        public User? EmailUser { get; set; }
        public Book? CodeBook { get; set; }
        public BookInformation? BookInformationIsbn { get; set; }
    }
}
