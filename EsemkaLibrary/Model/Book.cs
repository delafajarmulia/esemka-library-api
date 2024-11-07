using System.Text.Json.Serialization;

namespace EsemkaLibrary.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Isbn { get; set; }
        public string? IsTadon { get; set; }
        public string? ShelfCode { get; set; }
        public string? Source { get; set; }
        public string? SourceDesc { get; set; }
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore] 
        public BookInformation? BookInformationIsbn { get; set; }
        //public List<Book>? CodeBooks { get; set; }
    }
}
