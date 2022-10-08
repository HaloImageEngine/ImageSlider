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
            this.txt_Width = new System.Windows.Forms.TextBox();
            this.txt_Height = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Folder = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_Timer = new System.Windows.Forms.TextBox();
            this.txt_Type = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(12, 12);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
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
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 1;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = false;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // txt_Count
            // 
            this.txt_Count.Location = new System.Drawing.Point(12, 70);
            this.txt_Count.Name = "txt_Count";
            this.txt_Count.Size = new System.Drawing.Size(34, 23);
            this.txt_Count.TabIndex = 2;
            // 
            // txt_Counter
            // 
            this.txt_Counter.Location = new System.Drawing.Point(52, 70);
            this.txt_Counter.Name = "txt_Counter";
            this.txt_Counter.Size = new System.Drawing.Size(35, 23);
            this.txt_Counter.TabIndex = 3;
            // 
            // txt_FileName
            // 
            this.txt_FileName.Location = new System.Drawing.Point(105, 12);
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.Size = new System.Drawing.Size(189, 23);
            this.txt_FileName.TabIndex = 4;
            // 
            // txt_Width
            // 
            this.txt_Width.Location = new System.Drawing.Point(41, 99);
            this.txt_Width.Name = "txt_Width";
            this.txt_Width.Size = new System.Drawing.Size(46, 23);
            this.txt_Width.TabIndex = 5;
            // 
            // txt_Height
            // 
            this.txt_Height.Location = new System.Drawing.Point(41, 127);
            this.txt_Height.Name = "txt_Height";
            this.txt_Height.Size = new System.Drawing.Size(46, 23);
            this.txt_Height.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(12, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "H";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(11, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "W";
            // 
            // txt_Folder
            // 
            this.txt_Folder.Location = new System.Drawing.Point(11, 156);
            this.txt_Folder.Name = "txt_Folder";
            this.txt_Folder.Size = new System.Drawing.Size(76, 23);
            this.txt_Folder.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(105, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(670, 395);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // txt_Timer
            // 
            this.txt_Timer.Location = new System.Drawing.Point(11, 185);
            this.txt_Timer.Name = "txt_Timer";
            this.txt_Timer.Size = new System.Drawing.Size(76, 23);
            this.txt_Timer.TabIndex = 11;
            // 
            // txt_Type
            // 
            this.txt_Type.Location = new System.Drawing.Point(12, 214);
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.Size = new System.Drawing.Size(76, 23);
            this.txt_Type.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Text = "ImageSlider";
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
        private TextBox txt_Width;
        private TextBox txt_Height;
        private Label label1;
        private Label label2;
        private TextBox txt_Folder;
        private PictureBox pictureBox1;
        private TextBox txt_Timer;
        private TextBox txt_Type;
    }
}