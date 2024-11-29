using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Dto;
using Movies.Model;

namespace Movies.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController(AppDbContext context, IMapper mapper) : Controller
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDto>>> GetMovies()
        {
            var movies = await _context.Movies
                .Include(x => x.MovieDirectors)
                    .ThenInclude(x => x.Director)
                    .ToListAsync();

            var moviesDto = _mapper.Map<IEnumerable<MovieReadDto>>(movies);

            return Ok(moviesDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);

            if (movieCreateDto.Directors == null || !movieCreateDto.Directors.Any())
            {
                return BadRequest("Directors list cannot be null or empty.");
            }

            var addingDirectors = movieCreateDto.Directors.ToList();

            for (int i = 0; i < addingDirectors.Count; i++)
            {
                if (addingDirectors[i].Id.HasValue)
                {
                    var existingDirector = _context.Directors.FirstOrDefault(x => x.Id == addingDirectors[i].Id);
                    if (existingDirector == null) {
                        throw new ArgumentException("Used id even though there is not such a director");
                    }
                    else {
                        movie.MovieDirectors.Add(new MovieDirector { DirectorId = existingDirector.Id });
                    }

                }
                else if (!string.IsNullOrWhiteSpace(addingDirectors[i].Name))
                {
                    var newDirector = new Director { Name = addingDirectors[i].Name };
                    _context.Directors.Add(newDirector);
                    await _context.SaveChangesAsync();
                    movie.MovieDirectors.Add(new MovieDirector { DirectorId = newDirector.Id });
                }
                else
                {
                    return BadRequest($"Director at index {i} has neither ID nor Name.");
                }
            }

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return Ok("Nice");
        }
    }
}
