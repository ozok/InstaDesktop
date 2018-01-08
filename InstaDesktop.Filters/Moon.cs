using System;
using System.ComponentModel;
using ImageMagick;

namespace InstaDesktop.Filters
{
    public class Moon
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private BackgroundWorker _backgroundWorker;
        public bool Running;
        public string ErrorMsg;

        public Moon(string inputFilePath, string outputFilePath)
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
                    using (MagickImage grayScaleImage = (MagickImage)srcMagickImage.Clone())
                    {
                        using (MagickImage beforeImage = new MagickImage(new MagickColor(160, 160, 160, 255), srcMagickImage.Width, srcMagickImage.Height))
                        {
                            using (MagickImage afterImage = new MagickImage(new MagickColor(56, 56, 56, 255), srcMagickImage.Width, srcMagickImage.Height))
                            {
                                grayScaleImage.Grayscale(PixelIntensityMethod.Undefined);
                                grayScaleImage.BrightnessContrast(new Percentage(10), new Percentage(10));

                                grayScaleImage.Composite(beforeImage, CompositeOperator.SoftLight);
                                grayScaleImage.Composite(afterImage, CompositeOperator.Lighten);
                                grayScaleImage.Write(_outputFilePath);

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
