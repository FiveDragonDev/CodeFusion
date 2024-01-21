using CodeFusion;
using System.Drawing;
using Random = CodeFusion.Random;

namespace Test
{
    internal class Perlin
    {
        public static void Main()
        {
            const int Smoothing = (int)(512 / 3f);
            const int MaxFrames = 6;
            const int Width = 1280;
            const int Height = 720;
            var perlin = Random.GetPerlinNoise();

            List<Frame> frames = new();

            Console.WriteLine($"Resolution: {Width}x{Height}");
            Console.WriteLine($"World Seed: {perlin.Seed}");

            for (int t = 0; t < MaxFrames; t++)
            {
                Canvas canvas = new(Width, Height);
                canvas.Fill(Color.Black);
                for (int i = 0; i < canvas.Width; i++)
                {
                    for (int j = 0; j < canvas.Height; j++)
                    {
                        float x = i + canvas.Width / 2;
                        float y = j + canvas.Height / 2;
                        Color color = GetSurfaceColor(
                            (1 + perlin.NoiseOctaves(
                            (float)(x + t * 2) / Smoothing,
                            (float)(y) / Smoothing,
                            0.5f))
                            / /*Water Level*/ (2.25f + (MathF.Sin(t) / 1000)));
                        canvas.SetPixel(i, j, color);
                    }
                }
                frames.Add(new(canvas));
                Console.WriteLine($"Frame {t + 1}/{MaxFrames}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("== RENDER STARTED ==");
            Console.ResetColor();
            Clip clip = new(frames.ToArray());
            clip.SaveAsMp4("output");
            Console.WriteLine("End. Press any key to close.");
            Console.ReadKey();
        }
        private static Color GetSurfaceColor(float height)
        {
            return height switch
            {
                < 0.32f => Color.FromArgb(56, 117, 207),
                < 0.39f => Color.FromArgb(60, 110, 200),
                < 0.44f => Color.FromArgb(64, 104, 192),
                < 0.48f => Color.FromArgb(208, 207, 130),
                < 0.55f => Color.FromArgb(84, 150, 29),
                < 0.6f => Color.FromArgb(61, 105, 22),
                < 0.7f => Color.FromArgb(91, 68, 61),
                < 0.8f => Color.FromArgb(75, 58, 54),
                _ => Color.FromArgb(255, 255, 255)
            };
        }
    }
}