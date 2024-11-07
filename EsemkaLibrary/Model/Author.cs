using Newtonsoft.Json;

namespace EsemkaLibrary.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LasttName { get; set; }
        public string? MiddleName { get; set; }

        //[JsonIgnore]
        //public BookInformation? BookInformationIsbn { get; set; }
    }
}
