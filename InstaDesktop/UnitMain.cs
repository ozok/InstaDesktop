using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using InstaDesktop.Filters;
using InstaDesktop.ImageCombobox;

namespace InstaDesktop
{
    public partial class UnitMain : Form
    {
        public UnitMain()
        {
            InitializeComponent();
        }

        private string _srcPath;
        private string _tmpImgFilePathPattern;
        private List<string> _tmpFilesToClear;
        private string _currentlySelectedTmpFilePath;
        private FilterEnum? _currentlySelectedFilterEnum;

        private void LoadFilters()
        {
            var imgFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\img\\", "*.png");
            foreach (var imgFile in imgFiles)
            {
                imageList1.Images.Add(new Bitmap(imgFile));
            }

            var filterEnums = Enum.GetValues(typeof(FilterEnum)).Cast<FilterEnum>().ToList();

            FiltersList.Items.Clear();
            ImageAndText[] imageAndTexts = new ImageAndText[filterEnums.Count + 1];
            imageAndTexts[0] = new ImageAndText(imageList1.Images[0], "Original", DefaultFont);

            for (var i = 0; i < filterEnums.Count; i++)
            {
                var filterEnum = filterEnums[i];
                imageAndTexts[i + 1] = new ImageAndText(imageList1.Images[i + 1], filterEnum.ToString(), DefaultFont);
            }

            FiltersList.DisplayImagesAndText(imageAndTexts);
            //FiltersList.SelectedIndex = 0;
            if (FiltersList.Items.Count > 0)
            {
                FiltersList.SelectedIndex = 0;
            }
        }

        private static string ApplyToaster(string inputFilePath, string outputFilePath)
        {
            Toaster toaster = new Toaster(inputFilePath, outputFilePath);
            toaster.Start();
            while (toaster.Running)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            return toaster.ErrorMsg;
        }

        private static string ApplyXPro2(string inputFilePath, string outputFilePath)
        {
            XPro2 xPro2 = new XPro2(inputFilePath, outputFilePath);
            xPro2.Start();
            while (xPro2.Running)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            return xPro2.ErrorMsg;
        }

        private static string ApplyNashville(string inputFilePath, string outputFilePath)
        {
            Nashville nashville = new Nashville(inputFilePath, outputFilePath);
            nashville.Start();
            while (nashville.Running)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            return nashville.ErrorMsg;
        }

        private static string ApplyMoon(string inputFilePath, string outputFilePath)
        {
            Moon moon = new Moon(inputFilePath, outputFilePath);
            moon.Start();
            while (moon.Running)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            return moon.ErrorMsg;
        }

        private static string ApplyClarendon(string inputFilePath, string outputFilePath)
        {
            Clarendon clarendon = new Clarendon(inputFilePath, outputFilePath);
            clarendon.Start();
            while (clarendon.Running)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            return clarendon.ErrorMsg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_currentlySelectedFilterEnum.HasValue)
            {
                if (File.Exists(_currentlySelectedTmpFilePath))
                {
                    saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(_srcPath) + "_" + _currentlySelectedFilterEnum.ToString() + Path.GetExtension(_srcPath);
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.Copy(_currentlySelectedTmpFilePath, saveFileDialog1.FileName, false);
                            MessageBox.Show("Saved filtered image successfully.", "Info", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Temp file cannot be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You cannot save the original.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnitMain_Load(object sender, EventArgs e)
        {
            _tmpFilesToClear = new List<string>();
        }

        private void UnitMain_Shown(object sender, EventArgs e)
        {
            LoadFilters();
        }

        private void InputImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                ProgressPanel.Visible = true;
                ProgressPanel.BringToFront();
                Enabled = false;
                try
                {
                    _srcPath = openFileDialog1.FileName;
                    FiltersList.SelectedIndex = 0;
                    _currentlySelectedTmpFilePath = string.Empty;
                    _currentlySelectedFilterEnum = null;
                    _tmpImgFilePathPattern = Path.GetTempPath() + "\\" + Guid.NewGuid().ToString();

                    try
                    {
                        InputImage.Image = null;
                        InputImage.Load(_srcPath);

                        ImageInfo imageInfo = new ImageInfo(_srcPath);
                        imageInfo.Start();
                        while (imageInfo.Running)
                        {
                            Application.DoEvents();
                            Thread.Sleep(50);
                        }
                        FileInfoEdit.Text = imageInfo.FileInfoText;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }
                finally
                {
                    ProgressPanel.Visible = false;
                    Enabled = true;
                }
            }
        }

        private void UnitMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var filePath in _tmpFilesToClear)
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }


        private void FiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = FiltersList.SelectedIndex;
            if (selectedIndex == 0)
            {
                if (File.Exists(_srcPath))
                {
                    InputImage.Load(_srcPath);
                    _currentlySelectedTmpFilePath = _srcPath;
                    _currentlySelectedFilterEnum = null;
                }
            }
            else
            {
                selectedIndex = selectedIndex - 1;
                FilterEnum filterEnum = (FilterEnum)selectedIndex;
                string tmpFilteredImgPath = _tmpImgFilePathPattern + "_" + filterEnum.ToString() + Path.GetExtension(_srcPath);

                if (File.Exists(tmpFilteredImgPath))
                {
                    InputImage.Load(tmpFilteredImgPath);
                    _currentlySelectedFilterEnum = filterEnum;
                    _currentlySelectedTmpFilePath = tmpFilteredImgPath;
                }
                else
                {

                    if (File.Exists(_srcPath))
                    {
                        ProgressPanel.Location = new Point(Width / 2 - (ProgressPanel.Width / 2), (Height / 2) - (ProgressPanel.Height / 2));

                        ProgressPanel.Visible = true;
                        ProgressPanel.BringToFront();
                        Enabled = false;
                        try
                        {

                            string result;

                            switch (filterEnum)
                            {
                                case FilterEnum.Nashville:
                                    result = ApplyNashville(_srcPath, tmpFilteredImgPath);
                                    break;
                                case FilterEnum.Clarendon:
                                    result = ApplyClarendon(_srcPath, tmpFilteredImgPath);
                                    break;
                                case FilterEnum.Moon:
                                    result = ApplyMoon(_srcPath, tmpFilteredImgPath);
                                    break;
                                case FilterEnum.Toaster:
                                    result = ApplyToaster(_srcPath, tmpFilteredImgPath);
                                    break;
                                case FilterEnum.XPro2:
                                    result = ApplyXPro2(_srcPath, tmpFilteredImgPath);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            if (string.IsNullOrEmpty(result))
                            {
                                if (File.Exists(tmpFilteredImgPath))
                                {
                                    _tmpFilesToClear.Add(tmpFilteredImgPath);
                                    InputImage.Load(tmpFilteredImgPath);
                                    _currentlySelectedTmpFilePath = tmpFilteredImgPath;
                                    _currentlySelectedFilterEnum = filterEnum;
                                }
                            }
                            else
                            {
                                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        finally
                        {
                            ProgressPanel.Visible = false;
                            Enabled = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't find input file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
