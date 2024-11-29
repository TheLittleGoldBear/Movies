namespace Movies.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public ICollection<MovieDirector> MovieDirectors { get; set; } = new List<MovieDirector>();
    }
}
