using System.Drawing;

namespace CodeFusion
{
    public class Canvas
    {
        public int Width => _bitmap.Width;
        public int Height => _bitmap.Height;

        protected Bitmap _bitmap;
        protected Graphics _graphics;

        public Canvas(int width, int height)
        {
            _bitmap = new(width, height);
            _graphics = Graphics.FromImage(_bitmap);
            Fill(Color.Black);
        }
        public Canvas(string filename)
        {
            _bitmap = new(filename);
            _graphics = Graphics.FromImage(_bitmap);
        }

        public void Fill(Color color) => _graphics.Clear(color);

        public void SetPixel(int x, int y, Color color)
        {
            if (CheckBounds(x, y)) _bitmap.SetPixel(x, y, color);
        }
        public Color GetPixel(int x, int y)
        {
            if (CheckBounds(x, y)) return _bitmap.GetPixel(x, y);
            return new();
        }

        public bool CheckBounds(int x, int y) => (x >= 0 && x < Width) && (y >= 0 && y < Height);
        public void Save(string filename) => _bitmap.Save(filename);
    }
}
