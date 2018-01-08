using System;
using System.ComponentModel;
using ImageMagick;

namespace InstaDesktop.Filters
{
    public class Clarendon
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private BackgroundWorker _backgroundWorker;
        public bool Running;
        public string ErrorMsg;

        public Clarendon(string inputFilePath, string outputFilePath)
        {
            _inputFilePath = inputFilePath;
            _outputFilePath = outputFilePath;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorkerOnDoWork;
            Running = false;
        }

        private void BackgroundWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            Running = true;
            try
            {
                ErrorMsg = Process();
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

        private string Process()
        {
            try
            {
                using (MagickImage srcMagickImage = new MagickImage(_inputFilePath))
                {
                    using (MagickImage clonedImage = (MagickImage)srcMagickImage.Clone())
                    {
                        using (MagickImage beforeImage = new MagickImage(new MagickColor(127, 187, 227, 51), srcMagickImage.Width, srcMagickImage.Height))
                        {
                            clonedImage.BrightnessContrast(new Percentage(0), new Percentage(10));
                            clonedImage.Modulate(new Percentage(100), new Percentage(130), new Percentage(100));

                            clonedImage.Composite(beforeImage, CompositeOperator.Overlay);
                            clonedImage.Write(_outputFilePath);

                            return string.Empty;
                        }   
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
