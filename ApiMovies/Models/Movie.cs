namespace ApiMovies.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public virtual Ids Ids { get; set; }

        // 
        public int Watchers { get; set; }
        public bool Popular { get; set; }
    }
}
