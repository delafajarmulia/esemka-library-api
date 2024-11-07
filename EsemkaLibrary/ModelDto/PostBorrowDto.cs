using EsemkaLibrary.Model;

namespace EsemkaLibrary.ModelDto
{
    public class PostBorrowDto
    {
        public string? Status { get; set; }
        public int? IdUser { get; set; }
        public int? IdBook { get; set; }
        public int? IdBookInformation { get; set; }
        public DateTime? BorrowAt { get; set; }

        /*
        public string? EmailUser { get; set; }
        public string? CodeBook { get; set; }
        public string? BookInformationIsbn { get; set; } 
        */

    }
}
