using Newtonsoft.Json;

namespace EsemkaLibrary.Model
{
    public class BookInformation
    {
        public int Id { get; set; }
        public string? Isbn { get; set; }
        public string? Isbn13 { get; set; }
        public string? Title { get; set; }
        public string? Publisher { get; set; }
        public string? LateFee { get; set; }
        public string? PublishedAt { get; set; }

        [JsonIgnore]
        //public List<Author>? Authors { get; set; }
        public List<Book>? Books { get; set; }
        //public List<Borrow>? Borrows { get; set; }
    }
}
