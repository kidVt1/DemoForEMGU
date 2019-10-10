using Emgu.CV;
using Emgu.CV.UI;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DemoForEmgu
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            bool flag = disposing && this.components != null;
            if (flag)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.camara = new ImageBox();
            this.picture = new ImageBox();
            this.TakePhoto = new Button();
            this.textBox1 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.panel1 = new Panel();
            this.label1 = new Label();
            this.imageBox1 = new ImageBox();
            this.button3 = new Button();
            ((ISupportInitialize)this.camara).BeginInit();
            ((ISupportInitialize)this.picture).BeginInit();
            this.panel1.SuspendLayout();
            ((ISupportInitialize)this.imageBox1).BeginInit();
            base.SuspendLayout();
            this.camara.Location = new Point(12, 12);
            this.camara.Name = "camara";
            this.camara.Size = new Size(433, 426);
            this.camara.SizeMode = PictureBoxSizeMode.Zoom;
            this.camara.TabIndex = 2;
            this.camara.TabStop = false;
            this.picture.Location = new Point(481, 13);
            this.picture.Name = "picture";
            this.picture.Size = new Size(307, 160);
            this.picture.SizeMode = PictureBoxSizeMode.Zoom;
            this.picture.TabIndex = 2;
            this.picture.TabStop = false;
            this.TakePhoto.Location = new Point(713, 179);
            this.TakePhoto.Name = "TakePhoto";
            this.TakePhoto.Size = new Size(75, 23);
            this.TakePhoto.TabIndex = 3;
            this.TakePhoto.Text = "拍照";
            this.TakePhoto.UseVisualStyleBackColor = true;
            this.TakePhoto.Click += this.TakePhoto_Click;
            this.textBox1.Location = new Point(481, 179);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(213, 21);
            this.textBox1.TabIndex = 4;
            this.button1.Location = new Point(713, 221);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "训练";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += this.Button1_Click;
            this.button2.Location = new Point(713, 261);
            this.button2.Name = "button2";
            this.button2.Size = new Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "识别";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += this.Button2_Click;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new Point(481, 302);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(307, 136);
            this.panel1.TabIndex = 7;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(24, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0, 12);
            this.label1.TabIndex = 0;
            this.imageBox1.Location = new Point(807, 13);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new Size(433, 426);
            this.imageBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.imageBox1.TabIndex = 8;
            this.imageBox1.TabStop = false;
            this.button3.Location = new Point(608, 221);
            this.button3.Name = "button3";
            this.button3.Size = new Size(75, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "加载图片";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += this.Button3_Click;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(1294, 468);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.imageBox1);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.TakePhoto);
            base.Controls.Add(this.picture);
            base.Controls.Add(this.camara);
            base.Name = "Form1";
            this.Text = "Form1";
            ((ISupportInitialize)this.camara).EndInit();
            ((ISupportInitialize)this.picture).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((ISupportInitialize)this.imageBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        // Token: 0x04000008 RID: 8
        private ImageBox camara;

        // Token: 0x04000009 RID: 9
        private ImageBox picture;

        // Token: 0x0400000A RID: 10
        private Button TakePhoto;

        // Token: 0x0400000B RID: 11
        private TextBox textBox1;

        // Token: 0x0400000C RID: 12
        private Button button1;

        // Token: 0x0400000D RID: 13
        private Button button2;

        // Token: 0x0400000E RID: 14
        private Panel panel1;

        // Token: 0x0400000F RID: 15
        private Label label1;

        // Token: 0x04000010 RID: 16
        private ImageBox imageBox1;

        // Token: 0x04000011 RID: 17
        private Button button3;
    }
}

