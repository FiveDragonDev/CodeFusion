using CodeFusion;
using System.Drawing;
using Random = CodeFusion.Random;
using Math = CodeFusion.Math;

namespace Test
{
    internal class Voronoi
    {
        public static void Main()
        {
            const int Smoothing = (int)(512 / 3f);
            const int MaxFrames = 300;
            var perlin = Random.GetVoronoiNoise();
            Canvas canvas = new(1280, 720);

            Console.WriteLine($"Resolution: {canvas.Width}x{canvas.Height}");
            Console.WriteLine($"World Seed: {perlin.Seed}");

            for (int t = 0; t < MaxFrames; t++)
            {
                canvas.Fill(Color.Black);
                for (int i = 0; i < canvas.Width; i++)
                {
                    for (int j = 0; j < canvas.Height; j++)
                    {
                        float height = (1 + perlin.Noise(
                            (float)(i + t * 3) / Smoothing,
                            (float)j / Smoothing, cellularReturnType:
                            CodeFusion.Utils.VoronoiNoise.CellularReturnType.Distance2Div))
                            / /*Water Level*/ 2.25f;
                        int brightness = (int)Math.Clamp((int)(height * 255), 0, 255);
                        Color color = Color.FromArgb(brightness, brightness, brightness);
                        canvas.SetPixel(i, j, color);
                    }
                }
                canvas.Save($"res/voronoi/distance_div/frame_{t}.png");
                Console.WriteLine($"Frame {t + 1}/{MaxFrames}");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("== RENDER STARTED ==");
            Console.ResetColor();
            Console.WriteLine("End. Press any key to close.");
            Console.ReadKey();
        }
    }
}