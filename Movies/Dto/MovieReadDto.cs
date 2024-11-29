namespace Movies.Dto
{
    public class MovieReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rating { get; set; }
        public ICollection<DirectorWithoutMoviesDto> Directors { get; set; }
    }
}
