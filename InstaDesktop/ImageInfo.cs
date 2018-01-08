using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using ImageMagick;

namespace InstaDesktop
{
    public class ImageInfo
    {
        private readonly string _inputFilePath;
        private BackgroundWorker _backgroundWorker;
        public bool Running;
        public string FileInfoText;

        public ImageInfo(string inputFilePath)
        {
            _inputFilePath = inputFilePath;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            Running = false;
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            Running = true;
            try
            {
                FileInfoText = GetFileInfo();
            }
            finally
            {
                Running = false;
            }
        }

        public void Start()
        {
            Running = true;
            _backgroundWorker.RunWorkerAsync();
        }

        private string GetFileInfo()
        {
            if (File.Exists(_inputFilePath))
            {
                try
                {
                    using (MagickImage magickImage = new MagickImage(_inputFilePath))
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.AppendLine("File path: " + _inputFilePath);
                        stringBuilder.AppendLine("File size: " + magickImage.FileSize.ToString() + " bytes");
                        stringBuilder.AppendLine("Format: " + magickImage.Format.ToString() + " - " + magickImage.FormatInfo.Description);
                        stringBuilder.AppendLine("Bit Depth: " + magickImage.BitDepth().ToString() + " bits");
                        stringBuilder.AppendLine($"Dimensions: {magickImage.Width}x{magickImage.Height}");
                        for (var i = 0; i < magickImage.Channels.ToList().Count; i++)
                        {
                            var magickImageChannel = magickImage.Channels.ToList()[i];
                            stringBuilder.AppendLine($"Channel {i+1}: " + magickImageChannel.ToString());
                        }
                        stringBuilder.AppendLine("Color space: " + magickImage.ColorSpace.ToString());
                        stringBuilder.AppendLine("Compression: " + magickImage.Compression.ToString());
                        stringBuilder.AppendLine("Density: " + magickImage.Density);
                        stringBuilder.AppendLine("Quality: " + magickImage.Quality);
                        stringBuilder.AppendLine("Comment: " + magickImage.Comment);

                        return stringBuilder.ToString();
                    }
                }
                catch (Exception e)
                {
                    return "Exception: " + e.Message;
                }
            }
            else
            {
                return "Unable to find file";
            }
        }
    }
}
