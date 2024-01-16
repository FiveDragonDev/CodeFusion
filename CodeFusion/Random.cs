using CodeFusion.Utils;

namespace CodeFusion
{
    public static class Random
    {
        public static PerlinNoise GetPerlinNoise() => new((int)(DateTime.Now.Ticks *
            DateTime.Now.Year * DateTime.Now.DayOfYear));
        public static PerlinNoise GetPerlinNoise(int seed) => new(seed);
        private static System.Random GetRandom() => new((int)(DateTime.Now.Ticks *
            DateTime.Now.Year * DateTime.Now.DayOfYear));

        public static int RandomInt() => GetRandom().Next() * 2 - int.MaxValue;
        public static int RandomInt(int min, int max) => GetRandom().Next(min, max);
    }
}
