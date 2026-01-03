using ImageSlider.Models;
using ImageSlider.Services;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace ImageSlider
{
    public partial class Form1 : Form
    {
        public string CheckClose = string.Empty;
        private int _ZoomFactor;

        private CancellationTokenSource? _slideshowCts;
        private bool _isRunning = false;

        public Form1()
        {
            InitializeComponent();

            ToolTip tip = new ToolTip();
            tip.SetToolTip(txtUserID, "Enter your UserID");

            ToolTip tip1 = new ToolTip();
            tip1.SetToolTip(txt_ICategory, "Category");

            ToolTip tip2 = new ToolTip();
            tip2.SetToolTip(txt_ICategory, "Album");


            ToolTip tip3 = new ToolTip();
            tip3.SetToolTip(txt_IComment, "Description");

            ToolTip tip4 = new ToolTip();
            tip4.SetToolTip(txt_IDescription, "Description");


            // Read from App.config instead of hardcoding
            txt_Count.Text = ConfigurationManager.AppSettings["DefaultImageCount"] ?? "250";
            txt_Timer.Text = ConfigurationManager.AppSettings["DefaultTimerInterval"] ?? "3500";
            txt_Folder.Text = ConfigurationManager.AppSettings["DefaultFolder"] ?? "D:\\Backgrounds";
            txtUserID.Text = ConfigurationManager.AppSettings["DefaultUserID"] ?? "1018";
            txtUserAlias.Text = ConfigurationManager.AppSettings["DefaultUserAlias"] ?? "HALOLU19";
            this.txtFilter.Text = ConfigurationManager.AppSettings["DefaultFilter"] ?? "artz";
            this.txtFilterMin.Text = ConfigurationManager.AppSettings["DefaultFilterMin"] ?? "sz";

            // Display actual runtime version
            string runtimeVersion = RuntimeInformation.FrameworkDescription;
            this.Text = $"ImageSlider VeeBro 260121 - {runtimeVersion}";

            this.txtFilter.Text = "artz";
            this.txtFilterMin.Text = "sz";

            List<Item> items = new List<Item>();
            //items.Add(new Item() { Text = "GFEOBJ", Value = "D:\\GFE\\GFEOF" });
            items.Add(new Item() { Text = "Backgrounds", Value = "D:\\Backgrounds" });
            items.Add(new Item() { Text = "BackgroundsX", Value = "D:\\BackgroundsX" });
            items.Add(new Item() { Text = "Amber", Value = "D:\\GFE\\GFEAmber\\Images" });
            items.Add(new Item() { Text = "Kenna", Value = "D:\\GFE\\GFEKenna\\Images" });
            items.Add(new Item() { Text = "Shirly", Value = "D:\\GFE\\GFEShirly\\Images" });
            items.Add(new Item() { Text = "Michelle", Value = "D:\\GFE\\GFEMichelle\\Images" });
            items.Add(new Item() { Text = "Lana", Value = "D:\\GFE\\GFELana\\Images" });
            items.Add(new Item() { Text = "Max", Value = "D:\\GFE\\GFEMax\\Images" });
            items.Add(new Item() { Text = "Mason", Value = "D:\\GFE\\GFEMason\\Images" });
            items.Add(new Item() { Text = "Thumper", Value = "O:\\GFE\\GFEOF\\TH" });
            items.Add(new Item() { Text = "GFEOF", Value = "D:\\GFE\\GFEOF\\Images" });

            //items.Add(new Item() { Text = "HardDisk", Value = "F:\\Images\\XXX" });

            dd_Folder.DataSource = items;
            dd_Folder.DisplayMember = "Text";
            dd_Folder.ValueMember = "Value";

            LoadStartup(); // Call the new method
        }

        private void LoadStartup()
        {
            string? startupFolder = ConfigurationManager.AppSettings["StartUpFolder"];
            string? startupPic = ConfigurationManager.AppSettings["StartUpPic"];

            if (!string.IsNullOrEmpty(startupFolder) && !string.IsNullOrEmpty(startupPic))
            {
                try
                {
                    string imagePath = Path.Combine(startupFolder, startupPic);
                    if (File.Exists(imagePath))
                    {
                        pictureBox1.Image = Image.FromFile(imagePath);
                        pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                    }
                    else
                    {
                        Logger($"Startup image not found at: {imagePath}");
                    }
                }
                catch (Exception ex)
                {
                    Logger($"Error loading startup image: {ex.Message}");
                }
            }
        }

        private async void btn_Start_Click(object sender, EventArgs e)
        {
            // Disable buttons to prevent multiple clicks
            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;

            try
            {
                await Start_ProcessAsync();
            }
            finally
            {
                btn_Start.Enabled = true;
            }
        }

        private async Task Start_ProcessAsync()
        {
            // Test the //ImageLogger immediately
            ImageLogger("ImageSlider Started", "");

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            btn_Start.BringToFront();
            btn_Stop.BringToFront();
            txt_Count.BringToFront();
            txt_Counter.BringToFront();
            txt_Height.BringToFront();
            txt_Width.BringToFront();
            label1.BringToFront();
            label2.BringToFront();

            string _type = string.Empty;

            pictureBox1.SendToBack();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;

            Random _rnd = new Random();
            int cntr = 1;

            Image? previousDisplayImage = null;
            Image? imageToDispose = null;

            try
            {
                while (cntr < Convert.ToInt32(txt_Count.Text))
                {
                    string fileName = string.Empty;
                    if (txtFilter.Text.Length > 0)
                    {
                        if (txtFilterMin.Text.Length > 0)
                        {
                            fileName = getrandomfilefilter(txtFilter.Text.Trim(), txtFilterMin.Text.Trim());
                        }
                        else
                        {
                            fileName = getrandomfilefilter(txtFilter.Text.Trim());
                        }
                    }
                    else
                    {
                        fileName = getrandomfile();
                    }

                    try
                    {
                        using (var img = Image.FromFile(fileName))
                        {
                            cntr = cntr + 1;

                            ImageLogger(fileName, cntr.ToString());
                            Logger($"Start Of Error Watch: ");

                            Image? newDisplayImage = null;

                            if (img.Height < 800)
                            {
                                try
                                {
                                    FileInfo _fileinfo = new FileInfo(fileName);

                                    newDisplayImage = ScaleImage(img, 2400, 1080);
                                    this.pictureBox1.Image = newDisplayImage;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                    this.pictureBox1.Refresh();

                                    // Allow UI to breathe
                                    await Task.Delay(1);
                                }
                                catch (ArgumentException ex)
                                {
                                    Logger($"Error displaying scaled image: {ex.Message} - File: {fileName}");
                                    continue;
                                }

                                txt_Type.Text = "Scale Image Up";
                                txt_Type.Refresh();
                                txt_Counter.Text = cntr.ToString();
                                txt_Counter.Refresh();
                                txt_FileName.Text = fileName;
                                txt_FileName.Refresh();
                                SetColor(img, newDisplayImage, 0);
                                CheckClose = "Done";
                            }
                            else
                            {
                                CheckClose = string.Empty;
                            }

                            if (img.Height > 1400)
                            {
                                FileInfo _fileinfo = new FileInfo(fileName);

                                if (newDisplayImage != null)
                                {
                                    newDisplayImage.Dispose();
                                }

                                newDisplayImage = ScaleImage(img, 2400, 1080);
                                this.pictureBox1.Image = newDisplayImage;
                                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                this.pictureBox1.Refresh();
                                txt_Type.Text = "Scale Image Down";
                                txt_Type.Refresh();
                                txt_Counter.Text = cntr.ToString();
                                txt_Counter.Refresh();
                                txt_FileName.Text = fileName;
                                txt_FileName.Refresh();
                                SetColor(img, newDisplayImage, 1);
                                CheckClose = "Done";
                            }

                            this.Refresh();

                            if (this.CheckClose == string.Empty)
                            {
                                int timerval = Convert.ToInt32(txt_Timer.Text);
                                if (fileName.Contains("2024") || fileName.Contains("2023") || fileName.Contains("2022") ||
                                    fileName.Contains("2021") || fileName.Contains("ART") || fileName.Contains("1") ||
                                    fileName.Contains("2") || fileName.Contains("3") || fileName.Contains("4") ||
                                    fileName.Contains("5") || fileName.Contains("6") || fileName.Contains("7") ||
                                    fileName.Contains("8") || fileName.Contains("9") || fileName.Contains("a"))
                                {
                                    txt_Timer.Text = timerval.ToString();
                                    txt_Timer.BackColor = Color.Yellow;
                                    txt_Timer.ForeColor = Color.Black;
                                    txt_Timer.Refresh();
                                }
                                else
                                {
                                    txt_Timer.Text = timerval.ToString();
                                    txt_Timer.BackColor = Color.White;
                                    txt_Timer.ForeColor = Color.Black;
                                    txt_Timer.Refresh();
                                }

                                btn_Start.Visible = true;
                                btn_Start.Refresh();
                                btn_Stop.Visible = true;
                                btn_Stop.Refresh();
                                label1.Visible = true;
                                label2.Visible = true;
                                label1.Refresh();
                                label2.Refresh();
                                txt_Count.Visible = true;
                                txt_Count.Refresh();
                                txt_Folder.Visible = true;
                                txt_Folder.Refresh();
                                txt_Timer.Visible = true;
                                txt_Timer.Refresh();
                                dd_Folder.Visible = true;
                                dd_Folder.Refresh();
                                txt_FileName.Text = fileName;
                                txt_FileName.Refresh();
                                txt_Folder.Visible = true;

                                if (img.Width > img.Height)
                                {
                                    txt_Width.BackColor = Color.Red;
                                    txt_Width.ForeColor = Color.White;
                                    txt_Height.BackColor = Color.Blue;
                                    txt_Height.ForeColor = Color.White;
                                }
                                else
                                {
                                    txt_Width.BackColor = Color.Blue;
                                    txt_Width.ForeColor = Color.White;
                                    txt_Height.BackColor = Color.Red;
                                    txt_Height.ForeColor = Color.White;
                                }
                                txt_Width.Text = img.Width.ToString();
                                txt_Height.Text = img.Height.ToString();
                                txt_Width.Refresh();
                                txt_Height.Refresh();

                                if ((img.Width > 2000) || (img.Height > 1200))
                                {
                                    int mult = 3;

                                    if (img.Height < img.Width)
                                    {
                                        if (img.Width > 2600)
                                        {
                                            if (newDisplayImage != null) newDisplayImage.Dispose();
                                            newDisplayImage = resizeImage(img, new Size(img.Width / 2, img.Height / 2));
                                            this.pictureBox1.Image = newDisplayImage;
                                            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                            this.pictureBox1.Refresh();
                                            //  Landscape(newDisplayImage);
                                            txt_Type.Text = "Landscape Div";
                                            txt_Type.Refresh();
                                            txt_FileName.Text = fileName;
                                            txt_FileName.Refresh();
                                        }
                                        else
                                        {
                                            int h2 = 0;
                                            if (!fileName.Contains("2023") || !fileName.Contains("SZ"))
                                            {
                                                if (img.Height < 800)
                                                {
                                                    FileInfo _fileinfo = new FileInfo(fileName);
                                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                                    newDisplayImage = ScaleImage(img, 1080, 2500);
                                                    this.pictureBox1.Image = newDisplayImage;
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    this.pictureBox1.Refresh();
                                                }
                                            }

                                            if (newDisplayImage != null) newDisplayImage.Dispose();
                                            newDisplayImage = resizeImage(img, new Size(img.Width + h2, img.Height + h2));
                                            this.pictureBox1.Image = newDisplayImage;
                                            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                            this.pictureBox1.Refresh();
                                            txt_Type.Text = "Landscape Min";

                                            // ⚠️ CRITICAL: Replace Thread.Sleep with await Task.Delay
                                            await Task.Delay(timerval);
                                            txt_Type.Refresh();
                                            txt_FileName.Text = fileName;
                                            txt_FileName.Refresh();
                                        }
                                    }
                                    else
                                    {
                                        if (img.Height > 2100)
                                        {
                                            mult = 3;
                                            if (img.Height > 2100)
                                            {
                                                mult = 4;
                                            }
                                            if (img.Height > 3000)
                                            {
                                                mult = 5;
                                            }
                                            if (img.Height > 4000)
                                            {
                                                mult = 6;
                                            }
                                        }
                                        else
                                        {
                                            if (img.Height > 1280)
                                            {
                                                mult = 2;
                                            }
                                            if (img.Height < 1080)
                                            {
                                                mult = 1;
                                            }
                                        }

                                        try
                                        {
                                            if (mult == 2)
                                            {
                                                int wmin4 = 800;
                                                int hmin4 = (img.Height / 30);

                                                if (img.Height > 1080)
                                                {
                                                    if (img.Height > 2000 && img.Height < 2200)
                                                    {
                                                        wmin4 = 700;
                                                    }
                                                    if (img.Height > 1800 && img.Height < 2000)
                                                    {
                                                        if (img.Width > 1640 && img.Width < 2040)
                                                            wmin4 = 700;
                                                        if (img.Width > 1340 && img.Width < 1640)
                                                            wmin4 = 600;
                                                        if (img.Width > 1100 && img.Width < 1340)
                                                            wmin4 = 500;
                                                    }
                                                    if (img.Height > 1700 && img.Height < 1800)
                                                    {
                                                        if (img.Width > 1540)
                                                            wmin4 = 600;
                                                        else
                                                            wmin4 = 500;
                                                    }
                                                    if (img.Height > 1500 && img.Height < 1700)
                                                    {
                                                        wmin4 = 400;
                                                    }
                                                    if (img.Height > 1300 && img.Height < 1500)
                                                    {
                                                        wmin4 = 300;
                                                    }
                                                    if (img.Height > 1100 && img.Height < 1300)
                                                    {
                                                        wmin4 = 200;
                                                    }

                                                    hmin4 = (img.Height - 1080);
                                                    _type = "> 1080 " + wmin4.ToString();
                                                    txt_OH.BackColor = Color.LightSeaGreen;
                                                    mult = 1;

                                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                                    newDisplayImage = resizeImage(img, new Size((img.Width / mult) - wmin4, (img.Height / mult) - hmin4));
                                                    pictureBox1.Image = newDisplayImage;
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    this.pictureBox1.Refresh();
                                                    txt_OH.Text = newDisplayImage.Height.ToString();
                                                    txt_OW.Text = newDisplayImage.Width.ToString();
                                                    txt_Height.BackColor = Color.LightPink;
                                                    txt_FileName.Text = fileName;
                                                    txt_FileName.Refresh();
                                                }
                                                if (img.Height < 1080)
                                                {
                                                    wmin4 = (1080 - img.Width);
                                                    hmin4 = (1080 - img.Height);
                                                    _type = "< 1080";
                                                    mult = 1;
                                                    txt_OH.BackColor = Color.LightSalmon;

                                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                                    newDisplayImage = resizeImage(img, new Size((img.Width / mult) + wmin4, (img.Height / mult) + hmin4));
                                                    pictureBox1.Image = newDisplayImage;
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    txt_OH.Text = newDisplayImage.Height.ToString();
                                                    txt_OW.Text = newDisplayImage.Width.ToString();
                                                    txt_Height.BackColor = Color.LightPink;
                                                }

                                                txt_FileName.Text = fileName;
                                                txt_FileName.Refresh();

                                                txt_Height.ForeColor = Color.Black;
                                                txt_Height.Refresh();

                                                // ⚠️ CRITICAL: Replace Thread.Sleep with await Task.Delay
                                                await Task.Delay(timerval);
                                            }
                                            else
                                            {
                                                _type = "Plus";
                                                int wmin = (img.Width / 10);
                                                int hmin = (img.Height / 10);
                                                if (img.Height < 1080)
                                                {
                                                    wmin = (img.Height + 1080);
                                                    hmin = (img.Height + 1080);
                                                }

                                                using (Image p_image = resizeImage(img, new Size((img.Width / (mult - 1)) + wmin, (img.Height / (mult - 1)) + hmin)))
                                                {
                                                    txt_OH.Text = p_image.Height.ToString();
                                                    txt_OW.Text = p_image.Width.ToString();
                                                    txt_OH.Refresh();
                                                    txt_OW.Refresh();

                                                    if (mult > 2)
                                                    {
                                                        if (img.Height > 1080)
                                                        {
                                                            if (p_image.Height < 1080)
                                                            {
                                                                // Don't dispose newDisplayImage yet, we need img
                                                                if (newDisplayImage != null) newDisplayImage.Dispose();
                                                                newDisplayImage = (Image)img.Clone();
                                                                pictureBox1.Image = newDisplayImage;
                                                            }
                                                            else
                                                            {
                                                                if (newDisplayImage != null) newDisplayImage.Dispose();
                                                                newDisplayImage = (Image)p_image.Clone();
                                                                pictureBox1.Image = newDisplayImage;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (newDisplayImage != null) newDisplayImage.Dispose();
                                                            newDisplayImage = (Image)img.Clone();
                                                            pictureBox1.Image = newDisplayImage;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (newDisplayImage != null) newDisplayImage.Dispose();
                                                        newDisplayImage = (Image)img.Clone();
                                                        pictureBox1.Image = newDisplayImage;
                                                    }
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    pictureBox1.Refresh();
                                                    txt_Height.BackColor = Color.LightCyan;
                                                    txt_Height.ForeColor = Color.Black;
                                                    txt_Height.Refresh();

                                                    // ⚠️ CRITICAL: Replace Thread.Sleep with await Task.Delay
                                                    await Task.Delay(2000);
                                                }
                                            }

                                            // ⚠️ CRITICAL: Replace Thread.Sleep with await Task.Delay
                                            await Task.Delay(timerval);
                                            txt_Type.Text = "Portrait  " + _type;
                                            txt_Type.Refresh();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger("Error in Landscape : " + ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                    newDisplayImage = (Image)img.Clone();
                                    pictureBox1.Image = newDisplayImage;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                    pictureBox1.Refresh();
                                    txt_OH.Text = img.Height.ToString();
                                    txt_OW.Text = img.Width.ToString();
                                    txt_OH.Refresh();
                                    txt_OW.Refresh();

                                    txt_Type.Text = "Normal";
                                    txt_Type.Refresh();
                                    txt_Height.BackColor = Color.LightBlue;
                                    txt_Height.ForeColor = Color.Black;
                                    txt_Timer.BackColor = Color.White;
                                    txt_Timer.ForeColor = Color.Black;
                                    txt_Timer.Refresh();
                                    txt_Height.Refresh();
                                }

                                // ⚠️ CRITICAL: Replace Thread.Sleep with await Task.Delay
                                await Task.Delay(timerval);
                                cntr++;
                                txt_Counter.Text = cntr.ToString();
                                txt_Counter.Refresh();
                            }
                            else
                            {
                                if (cntr < Convert.ToInt32(txt_Count.Text))
                                {
                                    // Break logic here
                                }
                            }

                            imageToDispose = previousDisplayImage;
                            previousDisplayImage = newDisplayImage;

                            // ⚠️ CRITICAL: Replace Thread.Sleep with await Task.Delay
                            await Task.Delay(2000);

                        }

                        this.Refresh();

                        if (imageToDispose != null && imageToDispose != pictureBox1.Image)
                        {
                            try
                            {
                                imageToDispose.Dispose();
                                imageToDispose = null;
                            }
                            catch (Exception ex)
                            {
                                Logger($"Error disposing image: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger($"Error processing image: {ex.Message} - File: {fileName}");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger($"Fatal error in Start_Process: {ex.Message}");
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    pictureBox1.Image = null;
                    Application.DoEvents();

                    if (previousDisplayImage != null)
                    {
                        previousDisplayImage.Dispose();
                        previousDisplayImage = null;
                    }

                    if (imageToDispose != null)
                    {
                        imageToDispose.Dispose();
                        imageToDispose = null;
                    }
                }
                catch (Exception ex)
                {
                    Logger($"Error in cleanup: {ex.Message}");
                }
            }
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        private void Start_Process()
        {
            // Test the //ImageLogger immediately
            ImageLogger("ImageSlider Started", "");

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            btn_Start.BringToFront();
            btn_Stop.BringToFront();
            txt_Count.BringToFront();
            txt_Counter.BringToFront();
            txt_Height.BringToFront();
            txt_Width.BringToFront();
            label1.BringToFront();
            label2.BringToFront();

            string _type = string.Empty;

            pictureBox1.SendToBack();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;

            Random _rnd = new Random();
            int cntr = 1;

            Image? previousDisplayImage = null;
            Image? imageToDispose = null;

            try  // ← ADD OUTER TRY-CATCH FOR ENTIRE LOOP
            {
                while (cntr < Convert.ToInt32(txt_Count.Text))
                {
                    string fileName = string.Empty;
                    if (txtFilter.Text.Length > 0)
                    {
                        if (txtFilterMin.Text.Length > 0)
                        {
                            fileName = getrandomfilefilter(txtFilter.Text.Trim(), txtFilterMin.Text.Trim());
                        }
                        else
                        {
                            fileName = getrandomfilefilter(txtFilter.Text.Trim());
                        }
                    }
                    else
                    {
                        fileName = getrandomfile();
                    }

                    try  // ← ADD INNER TRY-CATCH FOR EACH IMAGE
                    {
                        using (var img = Image.FromFile(fileName))
                        {
                            cntr = cntr + 1;

                            ImageLogger(fileName, cntr.ToString());
                            Logger($"Start Of Error Watch: ");

                            Image? newDisplayImage = null;

                            if (img.Height < 800)
                            {
                                try
                                {
                                    FileInfo _fileinfo = new FileInfo(fileName);

                                    newDisplayImage = ScaleImage(img, 2400, 1080);
                                    this.pictureBox1.Image = newDisplayImage;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                    this.pictureBox1.Refresh();
                                }
                                catch (ArgumentException ex)
                                {
                                    Logger($"Error displaying scaled image: {ex.Message} - File: {fileName}");
                                    // Continue to next image
                                    continue;
                                }


                                txt_Type.Text = "Scale Image Up";
                                txt_Type.Refresh();
                                txt_Counter.Text = cntr.ToString();
                                txt_Counter.Refresh();
                                txt_FileName.Text = fileName;
                                txt_FileName.Refresh();
                                SetColor(img, newDisplayImage, 0);
                                CheckClose = "Done";
                            }
                            else
                            {
                                CheckClose = string.Empty;
                            }

                            if (img.Height > 1400)
                            {
                                FileInfo _fileinfo = new FileInfo(fileName);

                                // Dispose previous if we're replacing it
                                if (newDisplayImage != null)
                                {
                                    newDisplayImage.Dispose();
                                }

                                newDisplayImage = ScaleImage(img, 2400, 1080);
                                this.pictureBox1.Image = newDisplayImage;
                                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                this.pictureBox1.Refresh();
                                txt_Type.Text = "Scale Image Down";
                                txt_Type.Refresh();
                                txt_Counter.Text = cntr.ToString();
                                txt_Counter.Refresh();
                                txt_FileName.Text = fileName;
                                txt_FileName.Refresh();
                                SetColor(img, newDisplayImage, 1);
                                CheckClose = "Done";
                            }

                            this.Refresh();

                            if (this.CheckClose == string.Empty)
                            {
                                int timerval = Convert.ToInt32(txt_Timer.Text);
                                if (fileName.Contains("2024") || fileName.Contains("2023") || fileName.Contains("2022") ||
                                    fileName.Contains("2021") || fileName.Contains("ART") || fileName.Contains("1") ||
                                    fileName.Contains("2") || fileName.Contains("3") || fileName.Contains("4") ||
                                    fileName.Contains("5") || fileName.Contains("6") || fileName.Contains("7") ||
                                    fileName.Contains("8") || fileName.Contains("9") || fileName.Contains("a"))
                                {
                                    txt_Timer.Text = timerval.ToString();
                                    txt_Timer.BackColor = Color.Yellow;
                                    txt_Timer.ForeColor = Color.Black;
                                    txt_Timer.Refresh();
                                }
                                else
                                {
                                    txt_Timer.Text = timerval.ToString();
                                    txt_Timer.BackColor = Color.White;
                                    txt_Timer.ForeColor = Color.Black;
                                    txt_Timer.Refresh();
                                }

                                btn_Start.Visible = true;
                                btn_Start.Refresh();
                                btn_Stop.Visible = true;
                                btn_Stop.Refresh();
                                label1.Visible = true;
                                label2.Visible = true;
                                label1.Refresh();
                                label2.Refresh();
                                txt_Count.Visible = true;
                                txt_Count.Refresh();
                                txt_Folder.Visible = true;
                                txt_Folder.Refresh();
                                txt_Timer.Visible = true;
                                txt_Timer.Refresh();
                                dd_Folder.Visible = true;
                                dd_Folder.Refresh();
                                txt_FileName.Text = fileName;
                                txt_FileName.Refresh();
                                txt_Folder.Visible = true;

                                if (img.Width > img.Height)
                                {
                                    txt_Width.BackColor = Color.Red;
                                    txt_Width.ForeColor = Color.White;
                                    txt_Height.BackColor = Color.Blue;
                                    txt_Height.ForeColor = Color.White;
                                }
                                else
                                {
                                    txt_Width.BackColor = Color.Blue;
                                    txt_Width.ForeColor = Color.White;
                                    txt_Height.BackColor = Color.Red;
                                    txt_Height.ForeColor = Color.White;
                                }
                                txt_Width.Text = img.Width.ToString();
                                txt_Height.Text = img.Height.ToString();
                                txt_Width.Refresh();
                                txt_Height.Refresh();

                                if ((img.Width > 2000) || (img.Height > 1200))
                                {
                                    int mult = 3;

                                    if (img.Height < img.Width)
                                    {
                                        if (img.Width > 2600)
                                        {
                                            if (newDisplayImage != null) newDisplayImage.Dispose();
                                            newDisplayImage = resizeImage(img, new Size(img.Width / 2, img.Height / 2));
                                            this.pictureBox1.Image = newDisplayImage;
                                            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                            this.pictureBox1.Refresh();
                                            //  Landscape(newDisplayImage);
                                            txt_Type.Text = "Landscape Div";
                                            txt_Type.Refresh();
                                            txt_FileName.Text = fileName;
                                            txt_FileName.Refresh();
                                        }
                                        else
                                        {
                                            int h2 = 0;
                                            if (!fileName.Contains("2023") || !fileName.Contains("SZ"))
                                            {
                                                if (img.Height < 800)
                                                {
                                                    FileInfo _fileinfo = new FileInfo(fileName);
                                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                                    newDisplayImage = ScaleImage(img, 1080, 2500);
                                                    this.pictureBox1.Image = newDisplayImage;
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    this.pictureBox1.Refresh();
                                                }
                                            }

                                            if (newDisplayImage != null) newDisplayImage.Dispose();
                                            newDisplayImage = resizeImage(img, new Size(img.Width + h2, img.Height + h2));
                                            this.pictureBox1.Image = newDisplayImage;
                                            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                            this.pictureBox1.Refresh();
                                            txt_Type.Text = "Landscape Min";

                                            Thread.Sleep(timerval);
                                            txt_Type.Refresh();
                                            txt_FileName.Text = fileName;
                                            txt_FileName.Refresh();
                                        }
                                    }
                                    else
                                    {
                                        if (img.Height > 2100)
                                        {
                                            mult = 3;
                                            if (img.Height > 2100)
                                            {
                                                mult = 4;
                                            }
                                            if (img.Height > 3000)
                                            {
                                                mult = 5;
                                            }
                                            if (img.Height > 4000)
                                            {
                                                mult = 6;
                                            }
                                        }
                                        else
                                        {
                                            if (img.Height > 1280)
                                            {
                                                mult = 2;
                                            }
                                            if (img.Height < 1080)
                                            {
                                                mult = 1;
                                            }
                                        }

                                        try
                                        {
                                            if (mult == 2)
                                            {
                                                int wmin4 = 800;
                                                int hmin4 = (img.Height / 30);

                                                if (img.Height > 1080)
                                                {
                                                    if (img.Height > 2000 && img.Height < 2200)
                                                    {
                                                        wmin4 = 700;
                                                    }
                                                    if (img.Height > 1800 && img.Height < 2000)
                                                    {
                                                        if (img.Width > 1640 && img.Width < 2040)
                                                            wmin4 = 700;
                                                        if (img.Width > 1340 && img.Width < 1640)
                                                            wmin4 = 600;
                                                        if (img.Width > 1100 && img.Width < 1340)
                                                            wmin4 = 500;
                                                    }
                                                    if (img.Height > 1700 && img.Height < 1800)
                                                    {
                                                        if (img.Width > 1540)
                                                            wmin4 = 600;
                                                        else
                                                            wmin4 = 500;
                                                    }
                                                    if (img.Height > 1500 && img.Height < 1700)
                                                    {
                                                        wmin4 = 400;
                                                    }
                                                    if (img.Height > 1300 && img.Height < 1500)
                                                    {
                                                        wmin4 = 300;
                                                    }
                                                    if (img.Height > 1100 && img.Height < 1300)
                                                    {
                                                        wmin4 = 200;
                                                    }

                                                    hmin4 = (img.Height - 1080);
                                                    _type = "> 1080 " + wmin4.ToString();
                                                    txt_OH.BackColor = Color.LightSeaGreen;
                                                    mult = 1;

                                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                                    newDisplayImage = resizeImage(img, new Size((img.Width / mult) - wmin4, (img.Height / mult) - hmin4));
                                                    pictureBox1.Image = newDisplayImage;
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    this.pictureBox1.Refresh();
                                                    txt_OH.Text = newDisplayImage.Height.ToString();
                                                    txt_OW.Text = newDisplayImage.Width.ToString();
                                                    txt_Height.BackColor = Color.LightPink;
                                                    txt_FileName.Text = fileName;
                                                    txt_FileName.Refresh();
                                                }
                                                if (img.Height < 1080)
                                                {
                                                    wmin4 = (1080 - img.Width);
                                                    hmin4 = (1080 - img.Height);
                                                    _type = "< 1080";
                                                    mult = 1;
                                                    txt_OH.BackColor = Color.LightSalmon;

                                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                                    newDisplayImage = resizeImage(img, new Size((img.Width / mult) + wmin4, (img.Height / mult) + hmin4));
                                                    pictureBox1.Image = newDisplayImage;
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    txt_OH.Text = newDisplayImage.Height.ToString();
                                                    txt_OW.Text = newDisplayImage.Width.ToString();
                                                    txt_Height.BackColor = Color.LightPink;
                                                }

                                                txt_FileName.Text = fileName;
                                                txt_FileName.Refresh();

                                                txt_Height.ForeColor = Color.Black;
                                                txt_Height.Refresh();

                                                Thread.Sleep(timerval);
                                            }
                                            else
                                            {
                                                _type = "Plus";
                                                int wmin = (img.Width / 10);
                                                int hmin = (img.Height / 10);
                                                if (img.Height < 1080)
                                                {
                                                    wmin = (img.Height + 1080);
                                                    hmin = (img.Height + 1080);
                                                }

                                                using (Image p_image = resizeImage(img, new Size((img.Width / (mult - 1)) + wmin, (img.Height / (mult - 1)) + hmin)))
                                                {
                                                    txt_OH.Text = p_image.Height.ToString();
                                                    txt_OW.Text = p_image.Width.ToString();
                                                    txt_OH.Refresh();
                                                    txt_OW.Refresh();

                                                    if (mult > 2)
                                                    {
                                                        if (img.Height > 1080)
                                                        {
                                                            if (p_image.Height < 1080)
                                                            {
                                                                // Don't dispose newDisplayImage yet, we need img
                                                                if (newDisplayImage != null) newDisplayImage.Dispose();
                                                                newDisplayImage = (Image)img.Clone();
                                                                pictureBox1.Image = newDisplayImage;
                                                            }
                                                            else
                                                            {
                                                                if (newDisplayImage != null) newDisplayImage.Dispose();
                                                                newDisplayImage = (Image)p_image.Clone();
                                                                pictureBox1.Image = newDisplayImage;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (newDisplayImage != null) newDisplayImage.Dispose();
                                                            newDisplayImage = (Image)img.Clone();
                                                            pictureBox1.Image = newDisplayImage;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (newDisplayImage != null) newDisplayImage.Dispose();
                                                        newDisplayImage = (Image)img.Clone();
                                                        pictureBox1.Image = newDisplayImage;
                                                    }
                                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                                    pictureBox1.Refresh();
                                                    txt_Height.BackColor = Color.LightCyan;
                                                    txt_Height.ForeColor = Color.Black;
                                                    txt_Height.Refresh();
                                                    Thread.Sleep(2000);
                                                }
                                            }

                                            Thread.Sleep(timerval);
                                            txt_Type.Text = "Portrait  " + _type;
                                            txt_Type.Refresh();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger("Error in Landscape : " + ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    if (newDisplayImage != null) newDisplayImage.Dispose();
                                    newDisplayImage = (Image)img.Clone();
                                    pictureBox1.Image = newDisplayImage;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                    pictureBox1.Refresh();
                                    txt_OH.Text = img.Height.ToString();
                                    txt_OW.Text = img.Width.ToString();
                                    txt_OH.Refresh();
                                    txt_OW.Refresh();

                                    txt_Type.Text = "Normal";
                                    txt_Type.Refresh();
                                    txt_Height.BackColor = Color.LightBlue;
                                    txt_Height.ForeColor = Color.Black;
                                    txt_Timer.BackColor = Color.White;
                                    txt_Timer.ForeColor = Color.Black;
                                    txt_Timer.Refresh();
                                    txt_Height.Refresh();
                                }

                                Thread.Sleep(timerval);
                                cntr++;
                                txt_Counter.Text = cntr.ToString();
                                txt_Counter.Refresh();
                            }
                            else
                            {
                                if (cntr < Convert.ToInt32(txt_Count.Text))
                                {
                                    // Break logic here
                                }
                            }

                            // DON'T dispose here - save for later!
                            // Track which image to dispose AFTER the refresh
                            imageToDispose = previousDisplayImage;
                            previousDisplayImage = newDisplayImage;

                            Thread.Sleep(2000);

                        } // img is automatically disposed here

                        // Refresh the form
                        try
                        {
                            this.Refresh();
                        }
                        catch (ArgumentException ex)
                        {
                            Logger($"Error refreshing form: {ex.Message}");
                        }

                        // NOW it's safe to dispose the old image
                        if (imageToDispose != null && imageToDispose != pictureBox1.Image)
                        {
                            try
                            {
                                imageToDispose.Dispose();
                                imageToDispose = null;
                            }
                            catch (Exception ex)
                            {
                                Logger($"Error disposing image: {ex.Message}");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger($"Error processing image: {ex.Message} - File: {fileName}");
                        // Continue to next image
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger($"Fatal error in Start_Process: {ex.Message}");
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Clean up at the very end
                try
                {
                    pictureBox1.Image = null;
                    Application.DoEvents();

                    if (previousDisplayImage != null)
                    {
                        previousDisplayImage.Dispose();
                        previousDisplayImage = null;
                    }

                    if (imageToDispose != null)
                    {
                        imageToDispose.Dispose();
                        imageToDispose = null;
                    }
                }
                catch (Exception ex)
                {
                    Logger($"Error in cleanup: {ex.Message}");
                }
            }
        }
        private void SetColor(Image img, Image oh, int itype)
        {
            if (itype == 0)
            {
                txt_Width.BackColor = Color.Blue;
                txt_Width.ForeColor = Color.White;
                txt_Height.BackColor = Color.Blue;
                txt_Height.ForeColor = Color.White;
                txt_OW.BackColor = Color.Green;
                txt_OW.ForeColor = Color.White;
                txt_OH.BackColor = Color.Green;
                txt_OH.ForeColor = Color.White;

            }
            else
            {
                txt_Width.BackColor = Color.Blue;
                txt_Width.ForeColor = Color.White;
                txt_Height.BackColor = Color.Blue;
                txt_Height.ForeColor = Color.White;
                txt_OW.BackColor = Color.Red;
                txt_OW.ForeColor = Color.White;
                txt_OH.BackColor = Color.Red;
                txt_OH.ForeColor = Color.White;
            }
            txt_Width.Text = img.Width.ToString();
            txt_Height.Text = img.Height.ToString();
            txt_OW.Text = oh.Width.ToString();
            txt_OH.Text = oh.Height.ToString();
            txt_OH.Refresh();
            txt_OW.Refresh();
            txt_Width.Refresh();
            txt_Height.Refresh();
        }

        private void Landscape(Image img)
        {
            try
            {
                // sourceWidth and sourceHeight store the original image's width and height
                // targetWidth and targetHeight are calculated to fit into the picImage picturebox.
                int sourceWidth = img.Width;
                int sourceHeight = img.Height;
                int targetWidth;
                int targetHeight;
                double ratio;

                // Calculate targetWidth and targetHeight, so that the image will fit into
                // the picImage picturebox without changing the proportions of the image.
                if (sourceWidth > sourceHeight)
                {
                    // Set the new width
                    targetWidth = img.Width;
                    // Calculate the ratio of the new width against the original width
                    ratio = (double)targetWidth / sourceWidth;
                    // Calculate a new height that is in proportion with the original image
                    targetHeight = (int)(ratio * sourceHeight);
                }
                else if (sourceWidth < sourceHeight)
                {
                    // Set the new height
                    targetHeight = img.Height;
                    // Calculate the ratio of the new height against the original height
                    ratio = (double)targetHeight / sourceHeight;
                    // Calculate a new width that is in proportion with the original image
                    targetWidth = (int)(ratio * sourceWidth);
                }
                else
                {
                    // In this case, the image is square and resizing is easy
                    targetHeight = img.Height;
                    targetWidth = img.Width;
                }

                // Calculate the targetTop and targetLeft values, to center the image
                // horizontally or vertically if needed
                int targetTop = (img.Height - targetHeight) / 2;
                int targetLeft = (img.Width - targetWidth) / 2;

                // Create a new temporary bitmap to resize the original image
                // The size of this bitmap is the size of the img picturebox.
                Bitmap tempBitmap = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
                //Bitmap tempBitmap = new Bitmap(2000, 1080, PixelFormat.Format24bppRgb);

                // Set the resolution of the bitmap to match the original resolution.
                tempBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);

                // Create a Graphics object to further edit the temporary bitmap
                Graphics bmGraphics = Graphics.FromImage(tempBitmap);

                // First clear the image with the current backcolor


                // Set the interpolationmode since we are resizing an image here
                bmGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Draw the original image on the temporary bitmap, resizing it using
                // the calculated values of targetWidth and targetHeight.
                bmGraphics.DrawImage(img,
                                     new Rectangle(targetLeft, targetTop, targetWidth, targetHeight),
                                     new Rectangle(0, 0, sourceWidth, sourceHeight),
                                     GraphicsUnit.Pixel);

                // Dispose of the bmGraphics object
                bmGraphics.Dispose();

                // Set the image of the picImage picturebox to the temporary bitmap
                pictureBox1.Image = tempBitmap;
                pictureBox1.Refresh();

            }

            catch (Exception ex)
            {
                Logger("Error in Landscape : " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the pictureBox1 image to show the portion of the main image
        /// the mouse is currently over.
        /// </summary>
        /// 
        private void dd_Folder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dd_Folder.SelectedValue.ToString().Contains("X"))
            {
                this.txtFilter.Text = "SZ";
                this.txtFilterMin.Text = "";
            }
            if (!dd_Folder.SelectedValue.ToString().Contains("Backgrounds"))
            {
                this.txtFilter.Text = "";
                this.txtFilterMin.Text = "";
            }


        }

        private void dd_Folder_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Item obj = dd_Folder.SelectedItem as Item;
            if (obj != null)
            {
                txt_Folder.Text = obj.Value.ToString();
                dd_Folder.Text = obj.Value.ToString();
            }


        }

        public async Task CheckDelayAsync(string fileName, CancellationToken token)
        {
            int delay = 2000;

            if (txtFilter.Text.Length == 0)
            {
                if (fileName.Contains("3"))
                {
                    await Task.Delay(1000, token);
                }
                if (fileName.Contains("DSC"))
                {
                    await Task.Delay(1000, token);
                }
            }
            else
            {
                if (fileName.ToLower().Contains("artz666"))
                {
                    await Task.Delay(2000, token);
                }
                if (fileName.Contains("DSC"))
                {
                    await Task.Delay(1000, token);
                }
            }

            await Task.Delay(delay, token);
        }
        public void Portrait(Image img)
        {
            try
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
                bool source_is_wider = (float)img.Width / img.Height > (float)pictureBox1.Width / pictureBox1.Height;

                var resized = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                var g = Graphics.FromImage(resized);
                var dest_rect = new Rectangle(0, 0, 900, 1080);
                Rectangle src_rect;

                if (source_is_wider)
                {
                    float size_ratio = (float)img.Height / img.Height;
                    int sample_width = (int)(pictureBox1.Width / size_ratio);
                    src_rect = new Rectangle((img.Width - sample_width) / 2, 0, sample_width, img.Height);
                }
                else
                {
                    float size_ratio = (float)pictureBox1.Width / img.Width;
                    int sample_height = (int)(pictureBox1.Height / size_ratio);
                    src_rect = new Rectangle(0, (img.Height - sample_height) / 2, img.Width, sample_height);
                }

                g.DrawImage(img, dest_rect, src_rect, GraphicsUnit.Pixel);
                g.Dispose();
                pictureBox1.Image = resized;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                pictureBox1.Refresh();
                txt_Type.Text = "Portrait Tall";
                txt_Type.Refresh();
            }
            catch (Exception ex)
            {
                Logger("Error in Portrait : " + ex.Message);
            }
        }

        private string getrandomfile()
        {
            string _folder = txt_Folder.Text;
            string _file = "";

            if (!string.IsNullOrEmpty(_folder))
            {
                int lcount = 0;
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                try
                {
                    lcount++;
                    var di = new DirectoryInfo(_folder);
                    var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                    this.txtLoop.Text = lcount.ToString();
                    this.txtLoop.Refresh();
                    Random R = new Random();
                    _file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                }
                // probably should only catch specific exceptions
                // throwable by the above methods.
                catch (Exception ex)
                {
                    Logger("Error in Random File : " + ex.Message);
                }
            }

            return _file;
        }


        private string getrandomfilefilter(string filter)
        {
            string _folder = txt_Folder.Text;
            string _checkval = string.Empty;
            string _file = "";
            if (!string.IsNullOrEmpty(_folder))
            {
                int lcount = 0;
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                while (_checkval.Length == 0)
                {
                    try
                    {
                        lcount++;
                        var di = new DirectoryInfo(_folder);
                        var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                        Random R = new Random();
                        _file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                        this.txtLoop.Text = lcount.ToString();
                        this.txtLoop.Refresh();
                        if (_file.ToLower().Contains(filter.ToLower()))
                        {
                            _checkval = _file;
                        }
                    }

                    // probably should only catch specific exceptions
                    // throwable by the above methods.
                    catch (Exception ex)
                    {
                        Logger("Error in Random File : " + ex.Message);
                    }
                }
            }

            return _file;
        }


        private string getrandomfilefilter(string filter, string filtermin)
        {
            string _folder = txt_Folder.Text;
            string _checkval = string.Empty;
            string _file = "";
            if (!string.IsNullOrEmpty(_folder))
            {
                int lcount = 0;
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                while (_checkval.Length == 0)
                {
                    try
                    {
                        lcount++;
                        var di = new DirectoryInfo(_folder);
                        var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                        Random R = new Random();
                        _file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                        this.txtLoop.Text = lcount.ToString();
                        this.txtLoop.Refresh();
                        if (_file.ToLower().Contains(filter.ToLower()) && !_file.ToLower().Contains(filtermin.ToLower()))
                        {
                            _checkval = _file;
                        }

                    }

                    // probably should only catch specific exceptions
                    // throwable by the above methods.
                    catch (Exception ex)
                    {
                        Logger("Error in Random File : " + ex.Message);
                    }
                }
            }
            //ImageLogger
            return _file;
        }

        public class Item
        {
            public Item() { }
            public string Value { set; get; }
            public string Text { set; get; }
        }

        public static void VerifyDir(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (!dir.Exists)
                {
                    dir.Create();
                }
            }
            catch { }
        }

        public static void Logger(string lines)
        {
            string path = "D:/ImageLog/";
            VerifyDir(path);
            string fileName = "LOG" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".txt";
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }
        }

        public static void ImageLogger(string lines, string lines2)
        {
            string path = "D:/ImageLog/";
            VerifyDir(path);
            string fileName = "IMG" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".txt";
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines + " " + lines2);
                file.Close();
            }
            catch (Exception) { }
        }


        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static async Task<(Image? Image, long Size)?> GetImageInfo(string url)
        {
            string savePathDir = "D:\\ImageSave";
            Image? img = null;
            long streamLength = 0;

            try
            {
                VerifyDir(savePathDir);
                string fileName = Path.GetFileName(new Uri(url).AbsolutePath);
                string savePath = Path.Combine(savePathDir, fileName);

                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync(url))
                    {
                        response.EnsureSuccessStatusCode();
                        using (var stream = await response.Content.ReadAsStreamAsync())
                        {
                            // The stream from HttpClient must be copied to a MemoryStream
                            // because the Image.FromStream method keeps a lock on the stream.
                            using (var memoryStream = new MemoryStream())
                            {
                                await stream.CopyToAsync(memoryStream);
                                streamLength = memoryStream.Length; // Capture the stream length here
                                memoryStream.Position = 0; // Reset stream position
                                img = Image.FromStream(memoryStream);

                                // Save the image to the specified directory
                                img.Save(savePath);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger($"Error in GetImageInfo: {ex.Message}");
                // Return null if any error occurs
                return null;
            }

            return (img, streamLength);
        }


        private async void btnInsertURL_Click(object sender, EventArgs e)
        {
            DataAccess da = new DataAccess();
            string userid = txtUserID.Text;
            string useralias = txtUserAlias.Text;
            string imageurl = txtInputURL.Text.Trim();

            var imageResult = await GetImageInfo(imageurl);

            if (imageResult == null)
            {
                MessageBox.Show("Failed to download or process the image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var (imginfo, imageSize) = imageResult.Value;

            string rotation = string.Empty;

            if (imginfo.Height > imginfo.Width)
            { rotation = "P"; }
            else
            { rotation = "L"; }

            ImageModel imgmod = new ImageModel
            {
                // Data from imginfo
                Image_Width = imginfo.Width,
                Image_Height = imginfo.Height,
                Image_Dimentions = $"{imginfo.Width}x{imginfo.Height}",
                Image_Type = new ImageFormatConverter().ConvertToString(imginfo.RawFormat),
                Image_Size = (int)imageSize, // Use the captured stream length

                // Data from UI/URL
                UserID = Convert.ToInt32(userid),
                UserAlias = useralias,
                Image_Location = imageurl,
                Image_Location_Orig = imageurl,

                // Test Data for other fields
                Image_Location_Small = "pic01_sm.jpg",
                Image_Comment = txt_IComment.Text,
                Image_Description = txt_IDescription.Text,
                Image_Date = DateTime.Now,
                Image_Rotation = rotation,
                Image_Category_ID = 1,
                Image_Category = txt_ICategory.Text,
                Image_Album_ID = 1,
                Image_Album_Name = txt_IAlbum.Text,
                Image_Reference = "TestRef123",
                ProfileCover = 0,
                Random = 1,
                Showcase = 0,
                MediaPacketID = null,
                Image_Media_Id = null,
                Image_Media_Name = null
            };

            int imageid = await da.InsertIMGURL(userid, useralias, imgmod);
        }

    }
}