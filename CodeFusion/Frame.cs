using System.Drawing;

namespace CodeFusion
{
    public class Frame
    {
        private readonly Canvas _canvas;

        public Frame(Bitmap bitmap) => _canvas = new(bitmap);
        public Frame(Canvas canvas) => _canvas = canvas;

        public void Save(string filename) => _canvas.Save(filename);
        public Bitmap ToBitmap()
        {
            Bitmap temp = new(_canvas.Width, _canvas.Height);
            for (int i = 0; i < temp.Width; i++)
                for (int j = 0; j < temp.Height; j++)
                    temp.SetPixel(i, j, _canvas.GetPixel(i, j));
            return temp;
        }
    }
}
