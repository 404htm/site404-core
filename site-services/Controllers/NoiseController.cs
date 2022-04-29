using Microsoft.AspNetCore.Mvc;
using site_services.Extensions;
using site_services.Services;

namespace site_services.Controllers
{
    [ApiController]
    [Route("Noise")]
    public class NoiseController : ControllerBase
    {
        const double DEFAULT_SCALE = .007;
        const int DEFAULT_SIZE = 200;
        const int INDEX_X = 0;
        const int INDEX_Y = 1;

        private readonly ILogger<NoiseController> _logger;

        public NoiseController(ILogger<NoiseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Generate 2D noise based on the openSimplex Algorithm
        /// </summary>
        /// <param name="scale">Seed for noise generation - Leave null for Random</param>
        /// <returns>2D Array of Simplex Noise</returns>
        [HttpGet(Name = "GetSimplex2D")]
        public float[,] Get(long? seed = null, double scale=.007, int width = DEFAULT_SIZE, int height = DEFAULT_SIZE)
        {
            var seed_value = seed??Random.Shared.NextInt64();
            var result = new float[width, height];

            CoordinateExtensions.Range(width, height)
                .ToList()
                .ForEach(c => result[c[INDEX_X], c[INDEX_Y]] = OpenSimplex2.Noise2(seed_value, scale * c[INDEX_X], scale * c[INDEX_Y]));

            return result;
        }
    }
}