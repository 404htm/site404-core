using Microsoft.AspNetCore.Mvc;
using site_services.Extensions;
using site_services.Models;
using site_services.Services;

namespace site_services.Controllers
{
    [ApiController]
    [Route("Noise")]
    public class NoiseController : ControllerBase
    {
        const double DEFAULT_SCALE = .007;
        const int DEFAULT_SIZE = 1000;
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
        [HttpGet("/noise")]
        public short[][] Get(long? seed = null, int width = DEFAULT_SIZE, int height = DEFAULT_SIZE, double scale=.01, int range = 1000)
        {
            var seed_value = seed ?? Random.Shared.NextInt64();
            var result = new int[width, height];


            return Enumerable.Range(0, height)
                .Select(x => Enumerable.Range(0, width)
                    .Select(y => (short)calculate(x, y, seed_value, scale, range)).ToArray())
                .ToArray();
        }

        /// <summary>
        /// Generate 2D noise based on the openSimplex Algorithm
        /// </summary>
        /// <param name="scale">Seed for noise generation - Leave null for Random</param>
        /// <returns>2D Array of Simplex Noise</returns>
        [HttpGet("/noise/repeating")]
        public short[][] Get(int width = DEFAULT_SIZE, int height = DEFAULT_SIZE)
        {
            var layers = new LayerParameter[]{
                new LayerParameter { Scale = DEFAULT_SCALE, Magnitude = 1000, Seed = Random.Shared.NextInt64()},
                new LayerParameter { Scale = DEFAULT_SCALE * 5d, Magnitude = 200, Seed = Random.Shared.NextInt64()},
                new LayerParameter { Scale = DEFAULT_SCALE * 10d, Magnitude = 100, Seed = Random.Shared.NextInt64()},
            };

            var seed = Random.Shared.NextInt64();

            return Enumerable.Range(0, height)
                .Select(x => Enumerable.Range(0, width)
                    .Select(y =>
                         (short)layers
                         .Select(l => l.Magnitude * OpenSimplex2.Noise2(l.Seed??seed, l.Scale * x, l.Scale * y))
                         .Sum())
                    .ToArray())
                .ToArray();
        }

        private int calculate(int x, int y, long seed, double scale, int range)
        {
            var value = OpenSimplex2.Noise2(seed, scale * x, scale * y);
            return (int)(range + value * range);
        }
    }
}