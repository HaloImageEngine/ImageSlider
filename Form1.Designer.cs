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
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.txt_Count = new System.Windows.Forms.TextBox();
            this.txt_Counter = new System.Windows.Forms.TextBox();
            this.txt_FileName = new System.Windows.Forms.TextBox();
            this.txt_Height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Folder = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_Timer = new System.Windows.Forms.TextBox();
            this.txt_Type = new System.Windows.Forms.TextBox();
            this.dd_Folder = new System.Windows.Forms.ComboBox();
            this.txt_Width = new System.Windows.Forms.TextBox();
            this.txt_OW = new System.Windows.Forms.TextBox();
            this.txt_OH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLoop = new System.Windows.Forms.TextBox();
            this.txtFilterMin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(12, 12);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(97, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseMnemonic = false;
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_Stop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Stop.Location = new System.Drawing.Point(12, 41);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(97, 23);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // txt_Count
            // 
            this.txt_Count.Location = new System.Drawing.Point(25, 70);
            this.txt_Count.Name = "txt_Count";
            this.txt_Count.Size = new System.Drawing.Size(40, 23);
            this.txt_Count.TabIndex = 2;
            // 
            // txt_Counter
            // 
            this.txt_Counter.Location = new System.Drawing.Point(69, 70);
            this.txt_Counter.Name = "txt_Counter";
            this.txt_Counter.Size = new System.Drawing.Size(40, 23);
            this.txt_Counter.TabIndex = 3;
            // 
            // txt_FileName
            // 
            this.txt_FileName.Location = new System.Drawing.Point(274, 14);
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.Size = new System.Drawing.Size(306, 23);
            this.txt_FileName.TabIndex = 4;
            // 
            // txt_Height
            // 
            this.txt_Height.Location = new System.Drawing.Point(25, 99);
            this.txt_Height.Name = "txt_Height";
            this.txt_Height.Size = new System.Drawing.Size(40, 23);
            this.txt_Height.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(8, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "H";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(7, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "W";
            // 
            // txt_Folder
            // 
            this.txt_Folder.Location = new System.Drawing.Point(115, 43);
            this.txt_Folder.Name = "txt_Folder";
            this.txt_Folder.Size = new System.Drawing.Size(153, 23);
            this.txt_Folder.TabIndex = 9;
            this.txt_Folder.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(128, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(965, 395);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txt_Timer
            // 
            this.txt_Timer.Location = new System.Drawing.Point(48, 157);
            this.txt_Timer.Name = "txt_Timer";
            this.txt_Timer.Size = new System.Drawing.Size(63, 23);
            this.txt_Timer.TabIndex = 11;
            // 
            // txt_Type
            // 
            this.txt_Type.Location = new System.Drawing.Point(48, 215);
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.Size = new System.Drawing.Size(131, 23);
            this.txt_Type.TabIndex = 12;
            // 
            // dd_Folder
            // 
            this.dd_Folder.AutoCompleteCustomSource.AddRange(new string[] {
            "I://GFEOF",
            "I://Backgrounds"});
            this.dd_Folder.FormattingEnabled = true;
            this.dd_Folder.Location = new System.Drawing.Point(115, 14);
            this.dd_Folder.Name = "dd_Folder";
            this.dd_Folder.Size = new System.Drawing.Size(153, 23);
            this.dd_Folder.TabIndex = 13;
            this.dd_Folder.SelectedIndexChanged += new System.EventHandler(this.dd_Folder_SelectedIndexChanged);
            this.dd_Folder.SelectionChangeCommitted += new System.EventHandler(this.dd_Folder_SelectionChangeCommitted);
            // 
            // txt_Width
            // 
            this.txt_Width.Location = new System.Drawing.Point(25, 128);
            this.txt_Width.Name = "txt_Width";
            this.txt_Width.Size = new System.Drawing.Size(40, 23);
            this.txt_Width.TabIndex = 5;
            // 
            // txt_OW
            // 
            this.txt_OW.Location = new System.Drawing.Point(71, 128);
            this.txt_OW.Name = "txt_OW";
            this.txt_OW.Size = new System.Drawing.Size(40, 23);
            this.txt_OW.TabIndex = 14;
            // 
            // txt_OH
            // 
            this.txt_OH.Location = new System.Drawing.Point(71, 100);
            this.txt_OH.Name = "txt_OH";
            this.txt_OH.Size = new System.Drawing.Size(40, 23);
            this.txt_OH.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(5, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "C";
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(48, 186);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(64, 23);
            this.txtFilter.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(7, 195);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Filter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(3, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 19;
            this.label5.Text = "Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(5, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 20;
            this.label6.Text = "Timer";
            // 
            // txtLoop
            // 
            this.txtLoop.Location = new System.Drawing.Point(274, 41);
            this.txtLoop.Name = "txtLoop";
            this.txtLoop.Size = new System.Drawing.Size(40, 23);
            this.txtLoop.TabIndex = 21;
            // 
            // txtFilterMin
            // 
            this.txtFilterMin.Location = new System.Drawing.Point(115, 187);
            this.txtFilterMin.Name = "txtFilterMin";
            this.txtFilterMin.Size = new System.Drawing.Size(64, 23);
            this.txtFilterMin.TabIndex = 22;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1149, 450);
            this.Controls.Add(this.txtFilterMin);
            this.Controls.Add(this.txtLoop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_OH);
            this.Controls.Add(this.txt_OW);
            this.Controls.Add(this.dd_Folder);
            this.Controls.Add(this.txt_Type);
            this.Controls.Add(this.txt_Timer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txt_Width);
            this.Controls.Add(this.txt_Folder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Height);
            this.Controls.Add(this.txt_FileName);
            this.Controls.Add(this.txt_Counter);
            this.Controls.Add(this.txt_Count);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Name = "Form1";
            this.Text = "ImageSlider V 231021";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}