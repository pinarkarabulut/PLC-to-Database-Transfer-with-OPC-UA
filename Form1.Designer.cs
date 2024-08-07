using Opc.Ua;
using System.ComponentModel;
namespace OPCUAProject
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
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox4 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            textBox2 = new TextBox();
            textBox5 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label11 = new Label();
            textBox9 = new TextBox();
            label12 = new Label();
            label13 = new Label();
            pictureBox1 = new PictureBox();
            button4 = new Button();
            label14 = new Label();
            label15 = new Label();
            textBox10 = new TextBox();
            textBox11 = new TextBox();
            label16 = new Label();
            textBox12 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(352, 180);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += TextBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Historic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Coral;
            label1.Location = new Point(300, 47);
            label1.Name = "label1";
            label1.Size = new Size(260, 25);
            label1.TabIndex = 3;
            label1.Text = "READ VALUE FROM OPC UA";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(302, 180);
            label2.Name = "label2";
            label2.Size = new Size(0, 19);
            label2.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Tai Le", 10.2F);
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(379, 108);
            label3.Name = "label3";
            label3.Size = new Size(70, 22);
            label3.TabIndex = 5;
            label3.Text = "Deger 2";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(88, 174);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(125, 27);
            textBox3.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Tai Le", 10.2F);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(113, 109);
            label4.Name = "label4";
            label4.Size = new Size(70, 22);
            label4.TabIndex = 7;
            label4.Text = "Deger 1";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(616, 177);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 8;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Tai Le", 10.2F);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(647, 109);
            label5.Name = "label5";
            label5.Size = new Size(70, 22);
            label5.TabIndex = 9;
            label5.Text = "Deger 3";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Tai Le", 10.2F);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(113, 250);
            label6.Name = "label6";
            label6.Size = new Size(75, 22);
            label6.TabIndex = 10;
            label6.Text = "Deger f1";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(88, 317);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 11;
            // 
            // textBox5
            // 
            textBox5.HideSelection = false;
            textBox5.Location = new Point(352, 317);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new Size(125, 27);
            textBox5.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Tai Le", 10.2F);
            label7.ForeColor = SystemColors.ControlText;
            label7.Location = new Point(370, 250);
            label7.Name = "label7";
            label7.Size = new Size(75, 22);
            label7.TabIndex = 13;
            label7.Text = "Deger f2";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Tai Le", 10.2F);
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(118, 585);
            label8.Name = "label8";
            label8.Size = new Size(70, 22);
            label8.TabIndex = 14;
            label8.Text = "Deger 4";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft Tai Le", 10.2F);
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(379, 585);
            label9.Name = "label9";
            label9.Size = new Size(75, 22);
            label9.TabIndex = 15;
            label9.Text = "Deger f3";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft Tai Le", 10.2F);
            label10.ForeColor = SystemColors.ControlText;
            label10.Location = new Point(652, 585);
            label10.Name = "label10";
            label10.Size = new Size(29, 22);
            label10.TabIndex = 16;
            label10.Text = "b2";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(88, 637);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 27);
            textBox6.TabIndex = 17;
            // 
            // textBox7
            // 
            textBox7.Location = new Point(352, 637);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(125, 27);
            textBox7.TabIndex = 18;
            // 
            // textBox8
            // 
            textBox8.Location = new Point(607, 636);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(125, 27);
            textBox8.TabIndex = 19;
            // 
            // button1
            // 
            button1.Font = new Font("Century", 7.8F);
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(104, 670);
            button1.Name = "button1";
            button1.Size = new Size(79, 28);
            button1.TabIndex = 20;
            button1.Text = "GONDER";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Century", 7.8F);
            button2.ForeColor = SystemColors.ActiveCaptionText;
            button2.Location = new Point(379, 670);
            button2.Name = "button2";
            button2.Size = new Size(75, 28);
            button2.TabIndex = 21;
            button2.Text = "GONDER";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Century", 7.8F);
            button3.ForeColor = SystemColors.ActiveCaptionText;
            button3.Location = new Point(652, 669);
            button3.Name = "button3";
            button3.Size = new Size(80, 29);
            button3.TabIndex = 22;
            button3.Text = "GONDER";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Microsoft Tai Le", 10.2F);
            label11.ForeColor = SystemColors.ControlText;
            label11.Location = new Point(652, 250);
            label11.Name = "label11";
            label11.Size = new Size(29, 22);
            label11.TabIndex = 23;
            label11.Text = "b1";
            // 
            // textBox9
            // 
            textBox9.Font = new Font("Microsoft Tai Le", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBox9.Location = new Point(616, 317);
            textBox9.Name = "textBox9";
            textBox9.ReadOnly = true;
            textBox9.Size = new Size(125, 27);
            textBox9.TabIndex = 24;
            // 
            // label12
            // 
            label12.Location = new Point(0, 0);
            label12.Name = "label12";
            label12.Size = new Size(89, 22);
            label12.TabIndex = 0;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI Historic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.Coral;
            label13.Location = new Point(302, 524);
            label13.Name = "label13";
            label13.Size = new Size(238, 25);
            label13.TabIndex = 25;
            label13.Text = "WRITE VALUE TO OPC UA";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Elektroteks_logo_02;
            pictureBox1.Location = new Point(675, 7);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(107, 99);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 26;
            pictureBox1.TabStop = false;
            // 
            // button4
            // 
            button4.Location = new Point(606, 669);
            button4.Name = "button4";
            button4.RightToLeft = RightToLeft.Yes;
            button4.Size = new Size(30, 29);
            button4.TabIndex = 29;
            button4.Text = "~";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Microsoft Tai Le", 10.2F);
            label14.Location = new Point(79, 402);
            label14.Name = "label14";
            label14.Size = new Size(134, 22);
            label14.TabIndex = 30;
            label14.Tag = "";
            label14.Text = "Uretim Suresi (s)";
            label14.Click += label14_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Microsoft Tai Le", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.Location = new Point(330, 400);
            label15.Name = "label15";
            label15.Size = new Size(165, 22);
            label15.TabIndex = 31;
            label15.Text = "Urun Uzunlugu (cm)";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(79, 453);
            textBox10.Name = "textBox10";
            textBox10.ReadOnly = true;
            textBox10.Size = new Size(125, 27);
            textBox10.TabIndex = 32;
            // 
            // textBox11
            // 
            textBox11.Font = new Font("Microsoft Tai Le", 10.2F);
            textBox11.Location = new Point(352, 441);
            textBox11.Name = "textBox11";
            textBox11.ReadOnly = true;
            textBox11.Size = new Size(125, 29);
            textBox11.TabIndex = 33;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Microsoft Tai Le", 10.2F);
            label16.Location = new Point(607, 402);
            label16.Name = "label16";
            label16.Size = new Size(155, 22);
            label16.TabIndex = 34;
            label16.Text = "Urun Genisligi (cm)";
            // 
            // textBox12
            // 
            textBox12.Location = new Point(616, 453);
            textBox12.Name = "textBox12";
            textBox12.ReadOnly = true;
            textBox12.Size = new Size(125, 27);
            textBox12.TabIndex = 35;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(811, 710);
            Controls.Add(textBox12);
            Controls.Add(label16);
            Controls.Add(textBox11);
            Controls.Add(textBox10);
            Controls.Add(label15);
            Controls.Add(label14);
            Controls.Add(button4);
            Controls.Add(pictureBox1);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(textBox9);
            Controls.Add(label11);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox8);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(textBox5);
            Controls.Add(textBox2);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBox4);
            Controls.Add(label4);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Font = new Font("Microsoft Tai Le", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.ControlText;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox4;
        private Label label5;
        private Label label6;
        private TextBox textBox2;
        private TextBox textBox5;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label11;
        private TextBox textBox9;
        private Label label12;
        private Label label13;



        private PictureBox pictureBox1;
        private Button button4;
        private Label label14;
        private Label label15;
        private TextBox textBox10;
        private TextBox textBox11;
        private Label label16;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private TextBox textBox12;

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private Button button6;



    }
}

    

