using System; // For DateTime type

namespace MovieDatabaseApp.Models
{
    // The Movie model class represents the movie entity in the database
    public class Movie
    {
        public int MovieId { get; set; } 
        public string Title { get; set; }
        public string Genre { get; set; }   
        public DateTime ReleaseDate { get; set; }   
        public decimal Rating { get; set; } 
    }
}
