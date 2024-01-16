using CodeFusion;
using System.Drawing;
using Random = CodeFusion.Random;

namespace Test
{
    internal class Program
    {
        private static void Main()
        {
            const int Smoothing = (int)(512 / 3f);
            const int MaxFrames = 500;
            var perlin = Random.GetPerlinNoise();
            Canvas canvas = new((int)(3440 / 3f), (int)(1440 / 3f));

            Console.WriteLine($"Resolution: {canvas.Width}x{canvas.Height}");
            Console.WriteLine($"World Seed: {perlin.Seed}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("== RENDER STARTED ==");
            Console.ResetColor();

            for (int t = 0; t < MaxFrames; t++)
            {
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
                canvas.Save($"res/frame_{t}.png");
                Console.WriteLine($"Frame {t + 1}/{MaxFrames}");
            }
            Console.WriteLine("End. Press any key to close.");
            Console.ReadKey();
        }
        private static Color GetSurfaceColor(float height)
        {
            if (height < 0.32f)
                return Color.FromArgb(56, 117, 207);
            else if (height < 0.39f)
                return Color.FromArgb(60, 110, 200);
            else if (height < 0.44f)
                return Color.FromArgb(64, 104, 192);
            else if (height < 0.48f)
                return Color.FromArgb(208, 207, 130);
            else if (height < 0.55f)
                return Color.FromArgb(84, 150, 29);
            else if (height < 0.6f)
                return Color.FromArgb(61, 105, 22);
            else if (height < 0.7f)
                return Color.FromArgb(91, 68, 61);
            else if (height < 0.87f)
                return Color.FromArgb(75, 58, 54);
            else if (height > 0.9f)
                return Color.FromArgb(255, 255, 255);
            else
                return Color.FromArgb(255, 255, 255);
        }
    }
}