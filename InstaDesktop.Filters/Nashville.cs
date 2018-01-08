using System;
using System.ComponentModel;
using ImageMagick;

namespace InstaDesktop.Filters
{
    public class Nashville
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private BackgroundWorker _backgroundWorker;
        public bool Running;
        public string ErrorMsg;

        public Nashville(string inputFilePath, string outputFilePath)
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
                    using (MagickImage sepiaImage = new MagickImage(new MagickColor(125, 125, 125, 60), srcMagickImage.Width, srcMagickImage.Height))
                    {
                        sepiaImage.SepiaTone(new Percentage(80));

                        sepiaImage.Composite(srcMagickImage, CompositeOperator.Overlay);

                        sepiaImage.BrightnessContrast(new Percentage(0), new Percentage(10));
                        sepiaImage.Modulate(new Percentage(100), new Percentage(100), new Percentage(100));

                        using (MagickImage beforeImage = new MagickImage(new MagickColor(247, 176, 15, 25), srcMagickImage.Width, srcMagickImage.Height))
                        {
                            using (MagickImage afterImage = new MagickImage(new MagickColor(0, 70, 150, 120), srcMagickImage.Width, srcMagickImage.Height))
                            {
                                sepiaImage.Composite(beforeImage, CompositeOperator.Darken);

                                sepiaImage.Composite(afterImage, CompositeOperator.Lighten);
                                sepiaImage.Write(_outputFilePath);

                                return string.Empty;
                            }
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
