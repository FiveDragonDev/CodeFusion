using static System.MathF;

namespace CodeFusion
{
    public class Math
    {
        public static float Fade(float t) => t * t * t * (t * (t * 6 - 15) + 10);
        public static float Lerp(float a, float b, float t) => a + (t * (b - a));
        public static float Grad(int hash, float x, float y)
        {
            int h = hash & 3;
            float u = h == 0 ? x : h == 1 ? -x : y;
            float v = h == 0 || h == 1 ? y : -x;
            return (h & 2) == 0 ? u : v;
        }
        public static float Clamp(float value, float min, float max) => Max(Min(value, max), min);
    }
}
