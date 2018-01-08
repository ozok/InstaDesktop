using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ImageMagick;

namespace InstaDesktop.Filters
{
    public class Toaster
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private BackgroundWorker _backgroundWorker;
        public bool Running;
        public string ErrorMsg;

        public Toaster(string inputFilePath, string outputFilePath)
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

                        clonedImage.BrightnessContrast(new Percentage(-5), new Percentage(5));
                        clonedImage.Modulate(new Percentage(100), new Percentage(100), new Percentage(100));

                        using (Bitmap bitmap = new Bitmap(srcMagickImage.Width * 2, srcMagickImage.Height * 2))
                        {

                            using (Graphics graphics = Graphics.FromImage(bitmap))
                            {
                                Rectangle rectangle = new Rectangle(0, 0, srcMagickImage.Width * 2, srcMagickImage.Height * 2);
                                GraphicsPath gp = new GraphicsPath();
                                gp.AddEllipse(rectangle);

                                PathGradientBrush pgb = new PathGradientBrush(gp)
                                {
                                    CenterPoint = new PointF(rectangle.Width / 2, rectangle.Height / 2),
                                    CenterColor = new MagickColor(128, 78, 15, 125),
                                    SurroundColors = new Color[] { new MagickColor(59, 0, 59, 255) }
                                };

                                graphics.FillPath(pgb, gp);

                                bitmap.Save("gradientImage.jpg", ImageFormat.Jpeg);

                                pgb.Dispose();
                                gp.Dispose();
                            }
                        }

                        using (MagickImage afterImage = new MagickImage("gradientImage.jpg"))
                        {
                            afterImage.Crop(afterImage.Width / 4, afterImage.Height / 4, afterImage.Width / 2, afterImage.Height / 2);

                            clonedImage.Composite(afterImage, CompositeOperator.Screen);
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
