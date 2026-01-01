namespace ImageSlider
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_Start = new Button();
            btn_Stop = new Button();
            txt_Count = new TextBox();
            txt_Counter = new TextBox();
            txt_FileName = new TextBox();
            txt_Height = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txt_Folder = new TextBox();
            pictureBox1 = new PictureBox();
            txt_Timer = new TextBox();
            txt_Type = new TextBox();
            dd_Folder = new ComboBox();
            txt_Width = new TextBox();
            txt_OW = new TextBox();
            txt_OH = new TextBox();
            label3 = new Label();
            txtFilter = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtLoop = new TextBox();
            txtFilterMin = new TextBox();
            txtInputURL = new TextBox();
            btnInsertURL = new Button();
            txtUserID = new TextBox();
            txtUserAlias = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btn_Start
            // 
            btn_Start.Location = new Point(14, 70);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(97, 23);
            btn_Start.TabIndex = 0;
            btn_Start.Text = "Start";
            btn_Start.UseMnemonic = false;
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Stop
            // 
            btn_Stop.BackColor = SystemColors.ActiveBorder;
            btn_Stop.ForeColor = SystemColors.ActiveCaptionText;
            btn_Stop.Location = new Point(14, 99);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(97, 23);
            btn_Stop.TabIndex = 1;
            btn_Stop.Text = "Stop";
            btn_Stop.UseVisualStyleBackColor = false;
            btn_Stop.Click += btn_Stop_Click;
            // 
            // txt_Count
            // 
            txt_Count.Location = new Point(27, 128);
            txt_Count.Name = "txt_Count";
            txt_Count.Size = new Size(40, 23);
            txt_Count.TabIndex = 2;
            // 
            // txt_Counter
            // 
            txt_Counter.Location = new Point(71, 128);
            txt_Counter.Name = "txt_Counter";
            txt_Counter.Size = new Size(40, 23);
            txt_Counter.TabIndex = 3;
            // 
            // txt_FileName
            // 
            txt_FileName.Location = new Point(274, 15);
            txt_FileName.Name = "txt_FileName";
            txt_FileName.Size = new Size(306, 23);
            txt_FileName.TabIndex = 4;
            // 
            // txt_Height
            // 
            txt_Height.Location = new Point(27, 157);
            txt_Height.Name = "txt_Height";
            txt_Height.Size = new Size(40, 23);
            txt_Height.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(3, 148);
            label1.Name = "label1";
            label1.Size = new Size(16, 15);
            label1.TabIndex = 7;
            label1.Text = "H";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(3, 180);
            label2.Name = "label2";
            label2.Size = new Size(18, 15);
            label2.TabIndex = 8;
            label2.Text = "W";
            // 
            // txt_Folder
            // 
            txt_Folder.Location = new Point(117, 43);
            txt_Folder.Name = "txt_Folder";
            txt_Folder.Size = new Size(151, 23);
            txt_Folder.TabIndex = 9;
            txt_Folder.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(120, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1006, 426);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // txt_Timer
            // 
            txt_Timer.Location = new Point(50, 215);
            txt_Timer.Name = "txt_Timer";
            txt_Timer.Size = new Size(63, 23);
            txt_Timer.TabIndex = 11;
            // 
            // txt_Type
            // 
            txt_Type.Location = new Point(50, 273);
            txt_Type.Name = "txt_Type";
            txt_Type.Size = new Size(131, 23);
            txt_Type.TabIndex = 12;
            // 
            // dd_Folder
            // 
            dd_Folder.AutoCompleteCustomSource.AddRange(new string[] { "I://GFEOF", "O://GFE//FEAmber/Images", "I://Backgrounds" });
            dd_Folder.FormattingEnabled = true;
            dd_Folder.Location = new Point(115, 14);
            dd_Folder.Name = "dd_Folder";
            dd_Folder.Size = new Size(153, 23);
            dd_Folder.TabIndex = 13;
            // 
            // txt_Width
            // 
            txt_Width.Location = new Point(27, 186);
            txt_Width.Name = "txt_Width";
            txt_Width.Size = new Size(40, 23);
            txt_Width.TabIndex = 5;
            // 
            // txt_OW
            // 
            txt_OW.Location = new Point(73, 186);
            txt_OW.Name = "txt_OW";
            txt_OW.Size = new Size(40, 23);
            txt_OW.TabIndex = 14;
            // 
            // txt_OH
            // 
            txt_OH.Location = new Point(73, 158);
            txt_OH.Name = "txt_OH";
            txt_OH.Size = new Size(40, 23);
            txt_OH.TabIndex = 15;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(4, 122);
            label3.Name = "label3";
            label3.Size = new Size(15, 15);
            label3.TabIndex = 16;
            label3.Text = "C";
            // 
            // txtFilter
            // 
            txtFilter.Location = new Point(50, 244);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(64, 23);
            txtFilter.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(4, 238);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 18;
            label4.Text = "Filter";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(3, 267);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 19;
            label5.Text = "Status";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(4, 214);
            label6.Name = "label6";
            label6.Size = new Size(37, 15);
            label6.TabIndex = 20;
            label6.Text = "Timer";
            // 
            // txtLoop
            // 
            txtLoop.Location = new Point(274, 43);
            txtLoop.Name = "txtLoop";
            txtLoop.Size = new Size(40, 23);
            txtLoop.TabIndex = 21;
            // 
            // txtFilterMin
            // 
            txtFilterMin.Location = new Point(117, 244);
            txtFilterMin.Name = "txtFilterMin";
            txtFilterMin.Size = new Size(64, 23);
            txtFilterMin.TabIndex = 22;
            // 
            // txtInputURL
            // 
            txtInputURL.Location = new Point(12, 415);
            txtInputURL.Name = "txtInputURL";
            txtInputURL.Size = new Size(568, 23);
            txtInputURL.TabIndex = 23;
            // 
            // btnInsertURL
            // 
            btnInsertURL.BackColor = SystemColors.ActiveBorder;
            btnInsertURL.ForeColor = SystemColors.ActiveCaptionText;
            btnInsertURL.Location = new Point(12, 386);
            btnInsertURL.Name = "btnInsertURL";
            btnInsertURL.Size = new Size(97, 23);
            btnInsertURL.TabIndex = 24;
            btnInsertURL.Text = "Insert";
            btnInsertURL.UseVisualStyleBackColor = false;
            btnInsertURL.Click += btnInsertURL_Click_1;
            // 
            // txtUserID
            // 
            txtUserID.Location = new Point(14, 12);
            txtUserID.Name = "txtUserID";
            txtUserID.Size = new Size(97, 23);
            txtUserID.TabIndex = 25;
            // 
            // txtUserAlias
            // 
            txtUserAlias.Location = new Point(14, 41);
            txtUserAlias.Name = "txtUserAlias";
            txtUserAlias.Size = new Size(97, 23);
            txtUserAlias.TabIndex = 26;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1149, 450);
            Controls.Add(txtUserAlias);
            Controls.Add(txtUserID);
            Controls.Add(btnInsertURL);
            Controls.Add(txtInputURL);
            Controls.Add(txtFilterMin);
            Controls.Add(txtLoop);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtFilter);
            Controls.Add(label3);
            Controls.Add(txt_OH);
            Controls.Add(txt_OW);
            Controls.Add(dd_Folder);
            Controls.Add(txt_Type);
            Controls.Add(txt_Timer);
            Controls.Add(pictureBox1);
            Controls.Add(txt_Width);
            Controls.Add(txt_Folder);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_Height);
            Controls.Add(txt_FileName);
            Controls.Add(txt_Counter);
            Controls.Add(txt_Count);
            Controls.Add(btn_Stop);
            Controls.Add(btn_Start);
            Name = "Form1";
            Text = "VeeBro ImageSlider V 260000 .Net8";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Button btn_Start;
        private Button btn_Stop;
        private TextBox txt_Count;
        private TextBox txt_Counter;
        private TextBox txt_FileName;
        private TextBox txt_Height;
        private Label label1;
        private Label label2;
        private TextBox txt_Folder;
        private PictureBox pictureBox1;
        private TextBox txt_Timer;
        private TextBox txt_Type;
        private ComboBox dd_Folder;
        private TextBox txt_Width;
        private TextBox txt_OW;
        private TextBox txt_OH;
        private Label label3;
        private TextBox txtFilter;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtLoop;
        private TextBox txtFilterMin;
        private TextBox txtInputURL;
        private Button btnInsertURL;
        private TextBox txtUserID;
        private TextBox txtUserAlias;
    }
}