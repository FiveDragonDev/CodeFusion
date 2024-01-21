using NAudio.Wave;

namespace CodeFusion
{
    public static class AudioHelper
    {
        public static int ReadFile(string filename)
        {
            var reader = new WaveFileReader(filename);
            int channelCount = reader.WaveFormat.Channels;
            int sampleCount = (int)reader.SampleCount;
            var samples = new byte[sampleCount * channelCount];
            return reader.Read(samples, 0, sampleCount * channelCount);
        }
        public static void PlayAudioMp3(string filename)
        {
            using var ms = File.OpenRead(filename);
            using Mp3FileReader rdr = new(ms);
            using var wavStream = WaveFormatConversionStream.CreatePcmStream(rdr);
            using BlockAlignReductionStream baStream = new(wavStream);
            using WaveOut waveOut = new(WaveCallbackInfo.FunctionCallback());
            waveOut.Init(baStream);
            waveOut.Play();
            while (waveOut.PlaybackState == PlaybackState.Playing)
                Thread.Sleep(100);
        }
    }
}
