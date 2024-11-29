namespace Movies.Dto
{
    public class MovieCreateDto
    {
        public string Title { get; set; }
        public float Rating { get; set; }
        public ICollection<DirecorInputDto> Directors { get; set; }
    }
}
