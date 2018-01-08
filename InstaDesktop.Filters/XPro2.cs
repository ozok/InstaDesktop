using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ImageMagick;

namespace InstaDesktop.Filters
{
    public class XPro2
    {
        private readonly string _inputFilePath;
        private readonly string _outputFilePath;
        private BackgroundWorker _backgroundWorker;
        public bool Running;
        public string ErrorMsg;

        public XPro2(string inputFilePath, string outputFilePath)
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
                    using (MagickImage sepiaImage = new MagickImage(new MagickColor(125, 125, 125, 55), srcMagickImage.Width, srcMagickImage.Height))
                    {
                        sepiaImage.SepiaTone(new Percentage(92));
                        sepiaImage.Composite(srcMagickImage, CompositeOperator.Overlay);
                        sepiaImage.BrightnessContrast(new Percentage(10), new Percentage(0));

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
                                    CenterColor = Color.White,
                                    SurroundColors = new Color[] { new MagickColor(0, 0, 0, 200) }
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

                            sepiaImage.Composite(afterImage, CompositeOperator.ColorBurn);
                            sepiaImage.Write(_outputFilePath);
                            sepiaImage.Dispose();

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
