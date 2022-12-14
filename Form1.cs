using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace ImageSlider
{
    public partial class Form1 : Form
    {
     
        public string CheckClose = string.Empty;
        /// </summary>
        private int _ZoomFactor;
        public Form1()
        {
            InitializeComponent();
           
            txt_Count.Text = "50";
            txt_Timer.Text = "1500";
            txt_Folder.Text = "I:\\GFEOF";

            List<Item> items = new List<Item>();
            items.Add(new Item() { Text = "GFEOBJ", Value = "I:\\GFEOF" });
            items.Add(new Item() { Text = "Backgrounds", Value = "I:\\Backgrounds" });
            items.Add(new Item() { Text = "HardDisk", Value = "F:\\Images\\XXX" });

            dd_Folder.DataSource = items;
            dd_Folder.DisplayMember = "Text";
            dd_Folder.ValueMember = "Value";
           
        }

      
        private void btn_Start_Click(object sender, EventArgs e)
        {
            Start_Process();    
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Start_Process()
        {
           

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

            List<Image> images = new List<Image>();
            //DirectoryInfo di = new DirectoryInfo(txt_Folder.Text); // give path
            //FileInfo[] finfos = di.GetFiles("*.jpg", SearchOption.AllDirectories);

            

            Random _rnd = new Random();
            int cntr = 0;
            while (cntr < Convert.ToInt32(txt_Count.Text))
            {

                if (this.CheckClose == string.Empty)
                {
                    string fileName = getrandomfile();
                    //fileName = "I:\\Backgrounds\\DSC01142.jpg";
                    var img = Image.FromFile(fileName);

                    int timerval = 1500;
                    if (fileName.Contains("LA") || fileName.Contains("TH") || fileName.Contains("2022") || fileName.Contains("2021"))
                    {
                        timerval = 2000;
                        txt_Timer.Text = timerval.ToString();
                        txt_Timer.BackColor = Color.Yellow;
                        txt_Timer.ForeColor = Color.Black;
                        txt_Timer.Refresh();
                    }
                    else
                    {
                        timerval = 1000;
                        txt_Timer.Text = timerval.ToString();
                        txt_Timer.BackColor = Color.White;
                        txt_Timer.ForeColor = Color.Black;
                        txt_Timer.Refresh();
                    }

                    Logger("--------------" );
                    Logger("New File   : " + fileName);

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
                    txt_Folder.Visible = false;
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
                        Image n_img = img;
                        int mult = 3;

                        

                        if (img.Height < img.Width)
                        {
                            Image l_image = resizeImage(img, new Size(img.Width - 900, img.Height - 700));
                            Logger("Show Pic L : " + fileName);
                            Landscape(l_image);
                            txt_Type.Text = "Landscape";
                            txt_Type.Refresh();
                        }
                        else
                        {
                            
                            if (img.Height > 1900)
                            {
                                mult = 3;
                                if (img.Height > 2000)
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
                                if (img.Height > 1080)
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
                                    decimal value = 3.14m;
                                    int n = (int)value;

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
                                        _type = "> 1080" + wmin4.ToString();
                                        txt_OH.BackColor = Color.LightSeaGreen;
                                        mult = 1;
                                        Image p_image1 = resizeImage(img, new Size((img.Width / mult) - wmin4, (img.Height / mult) - hmin4 ));
                                        pictureBox1.Image = p_image1;
                                        txt_OH.Text = p_image1.Height.ToString();
                                        txt_OW.Text = p_image1.Width.ToString();
                                        txt_Height.BackColor = Color.LightYellow;
                                        Logger("Show Pic X : " + fileName);
                                    }
                                    if (img.Height < 1080)
                                    {
                                        wmin4 = (1080 - img.Width);
                                        hmin4 = (1080 - img.Height);
                                        _type = "< 1080";
                                        mult = 1;
                                        txt_OH.BackColor = Color.LightSalmon;
                                        Image p_image2 = resizeImage(img, new Size((img.Width / mult) + wmin4, (img.Height / mult) + hmin4));
                                        pictureBox1.Image = p_image2;
                                        txt_OH.Text = p_image2.Height.ToString();
                                        txt_OW.Text = p_image2.Width.ToString();
                                        txt_Height.BackColor = Color.LightPink;
                                    }

                                    //  Image p_image = resizeImage(img, new Size((img.Width / mult) , (img.Height / mult) ));
                                  
                                    txt_OH.Refresh();
                                    txt_OW.Refresh();
                                    //  Portrait(p_image);
                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                   
                                    pictureBox1.Refresh();
                                    Logger("Show Pic P : " + fileName);
                                    
                                    txt_Height.ForeColor = Color.Black;
                                    txt_Height.Refresh();
                                    Thread.Sleep(2000);
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

                                    Image p_image = resizeImage(img, new Size((img.Width / mult) + wmin, (img.Height / mult) + hmin));
                                    txt_OH.Text = p_image.Height.ToString();
                                    txt_OW.Text = p_image.Width.ToString();
                                    txt_OH.Refresh();
                                    txt_OW.Refresh();
                                    pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                                    pictureBox1.Image = p_image;
                                    pictureBox1.Refresh();
                                    Logger("Show Pic P  "  + fileName);
                                    txt_Height.BackColor = Color.LightCyan;
                                    txt_Height.ForeColor = Color.Black;
                                    txt_Height.Refresh();
                                    Thread.Sleep(2000);
                                }

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
                        pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
                        pictureBox1.Image = img;
                        txt_OH.Text = img.Height.ToString();
                        txt_OW.Text = img.Width.ToString();
                        txt_OH.Refresh();
                        txt_OW.Refresh();
                        Logger("Show Pic N : " + fileName);
                        pictureBox1.Refresh();
                        txt_Type.Text = "Normal";
                        txt_Type.Refresh();
                        txt_Height.BackColor = Color.LightBlue;
                        txt_Height.ForeColor = Color.Black;
                        txt_Timer.BackColor = Color.White;
                        txt_Timer.ForeColor = Color.Black;
                        txt_Timer.Refresh();
                        txt_Height.Refresh();
                    }

                    int milliseconds = Convert.ToInt32(timerval);
                    Thread.Sleep(milliseconds);
                    pictureBox1.Image = null;
                    pictureBox1.ImageLocation = null;
                    pictureBox1.Update();
                    Thread.Sleep(100);
                    pictureBox1.Image = null;
                    pictureBox1.ImageLocation = null;
                    pictureBox1.Update();

                    cntr++;
                    txt_Counter.Text = cntr.ToString();
                    txt_Counter.Refresh();
                    

                }
                else
                {
                    break;
                }
            }

            
            Logger("Loop " + cntr.ToString());
            this.Close();
        }

        private void Landscape(Image img)
        {
        try { 
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
              //  pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                pictureBox1.Refresh();
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
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                try
                {
                    var di = new DirectoryInfo(_folder);
                    var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                    Random R = new Random();
                    _file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                }
                // probably should only catch specific exceptions
                // throwable by the above methods.
                catch(Exception ex) {
                    Logger("Error in Random File : " + ex.Message);
                }
            }
            return _file;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }


        
        private void UpdateZoomedImage(MouseEventArgs e)
        {
            // Calculate the width and height of the portion of the image we want
            // to show in the pictureBox1 picturebox. This value changes when the zoom
            // factor is changed.
            int zoomWidth = pictureBox1.Width / _ZoomFactor;
            int zoomHeight = pictureBox1.Height / _ZoomFactor;

            // Calculate the horizontal and vertical midpoints for the crosshair
            // cursor and correct centering of the new image
            int halfWidth = zoomWidth / 2;
            int halfHeight = zoomHeight / 2;

            // Create a new temporary bitmap to fit inside the pictureBox1 picturebox
            Bitmap tempBitmap = new Bitmap(zoomWidth, zoomHeight, PixelFormat.Format24bppRgb);

            // Create a temporary Graphics object to work on the bitmap
            Graphics bmGraphics = Graphics.FromImage(tempBitmap);

            // Clear the bitmap with the selected backcolor
            //bmGraphics.Clear(_BackColor);

            // Set the interpolation mode
            bmGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Draw the portion of the main image onto the bitmap
            // The target rectangle is already known now.
            // Here the mouse position of the cursor on the main image is used to
            // cut out a portion of the main image.
            bmGraphics.DrawImage(pictureBox1.Image,
                                 new Rectangle(0, 0, zoomWidth, zoomHeight),
                                 new Rectangle(e.X - halfWidth, e.Y - halfHeight, zoomWidth, zoomHeight),
                                 GraphicsUnit.Pixel);

            // Draw the bitmap on the pictureBox1 picturebox
            pictureBox1.Image = tempBitmap;

            // Draw a crosshair on the bitmap to simulate the cursor position
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight - 4, halfWidth + 1, halfHeight - 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 1, halfHeight + 6, halfWidth + 1, halfHeight + 3);
            bmGraphics.DrawLine(Pens.Black, halfWidth - 4, halfHeight + 1, halfWidth - 1, halfHeight + 1);
            bmGraphics.DrawLine(Pens.Black, halfWidth + 6, halfHeight + 1, halfWidth + 3, halfHeight + 1);

            // Dispose of the Graphics object
            bmGraphics.Dispose();

            // Refresh the pictureBox1 picturebox to reflect the changes
            pictureBox1.Refresh();
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
            string path = "D:/Log/";
            VerifyDir(path);
            string fileName = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_Logs.txt";
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
                file.WriteLine(DateTime.Now.ToString() + ": " + lines);
                file.Close();
            }
            catch (Exception) { }
        }

        private void dd_Folder_SelectedIndexChanged(object sender, EventArgs e)
        {
            
             
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
                pictureBox1.Left = pictureBox1.Left + 10;
            
        }
    }
    public class Item
    {
        public Item() { }
        public string Value { set; get; }
        public string Text { set; get; }
    }


}