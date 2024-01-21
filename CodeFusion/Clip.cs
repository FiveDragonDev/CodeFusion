using System.Drawing;
using AForge.Video.FFMPEG;
using AForge.Video.VFW;

namespace CodeFusion
{
    public class Clip
    {
        public enum OutputFormat { png, webp, jpeg }

        protected Frame[] _frames;

        public Clip(params Frame[] frames) => _frames = frames;
        public Clip(params Bitmap[] frames)
        {
            _frames = new Frame[frames.Length];
            for (int i = 0; i < frames.Length; i++)
                _frames[i] = new(frames[i]);
        }
        public Clip(params Canvas[] frames)
        {
            _frames = new Frame[frames.Length];
            for (int i = 0; i < frames.Length; i++)
                _frames[i] = new(frames[i]);
        }

        public void SaveAsFrames(string outputPath, OutputFormat format = OutputFormat.png)
        {
            for (int i = 0; i < _frames.Length; i++)
                _frames[i].Save($"{outputPath}/frame_{i}.{format}");
        }
        public void SaveAsMp4(string filename, int frameRate = 24)
        {
            VideoFileWriter writer = new();
            int width = _frames[0].ToBitmap().Width;
            int height = _frames[0].ToBitmap().Height;
            foreach (var frame in _frames)
            {
                width = (int)MathF.Max(width, frame.ToBitmap().Width);
                height = (int)MathF.Max(height, frame.ToBitmap().Height);
            }
            writer.Open(filename + ".avi", width, height, frameRate, VideoCodec.MPEG4);
            foreach (var frame in _frames)
                writer.WriteVideoFrame(frame.ToBitmap());
            writer.Close();
        }
        public void SaveAsAvi(string filename, int frameRate = 24)
        {
            AVIWriter writer = new("wmv3")
            { FrameRate = frameRate };

            int width = _frames[0].ToBitmap().Width;
            int height = _frames[0].ToBitmap().Height;
            foreach (var frame in _frames)
            {
                width = (int)MathF.Max(width, frame.ToBitmap().Width);
                height = (int)MathF.Max(height, frame.ToBitmap().Height);
            }
            writer.Open(filename + ".avi", width, height);
            foreach (var frame in _frames)
                writer.AddFrame(frame.ToBitmap());
            writer.Close();
        }
    }
}
