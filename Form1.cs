using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace DemoForEmgu
{

    partial class Form1 : Form
    {
        private ICapture capture;

        private CascadeClassifier cascadeClassifierForEye;


        private CascadeClassifier cascadeClassifierForFace;

        private FaceRecognizer faceRecognizer;
        public Form1()
        {
            this.InitializeComponent();
            this.faceRecognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
            this.faceRecognizer.Read("face.xml");
            string eyeFile = "haarcascades/haarcascade_eye.xml";
            this.cascadeClassifierForEye = new CascadeClassifier(eyeFile);
            string faceFile = "haarcascades/haarcascade_frontalface_default.xml";
            this.cascadeClassifierForFace = new CascadeClassifier(faceFile);
            CvInvoke.UseOpenCL = true;
            this.capture = new VideoCapture(0, VideoCapture.API.Any);
            Task.Run(delegate ()
            {
                while (true)
                {
                    using (Image<Bgr, byte> frameImage = this.capture.QueryFrame().ToImage<Bgr, byte>(false))
                    {
                        this.camara.Image = this.DetectFace(frameImage);
                        Thread.Sleep(100);
                    }
                }
            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
        }

        private Image<Bgr, byte> DetectFace(Image<Bgr, byte> frameImage)
        {
            bool flag = frameImage != null;
            if (flag)
            {
                Image<Gray, byte> grayImage = frameImage.Convert<Gray, byte>();
                Rectangle[] eyes = this.cascadeClassifierForEye.DetectMultiScale(grayImage, 1.1, 12, Size.Empty, default(Size));
                foreach (Rectangle eye in eyes)
                {
                    frameImage.GrabCut(eye, 1);
                    frameImage.Draw(eye, new Bgr(Color.Red), 3, LineType.EightConnected, 0);
                }
                Rectangle[] faces = this.cascadeClassifierForFace.DetectMultiScale(grayImage, 1.1, 12, Size.Empty, default(Size));
                Rectangle[] array2 = faces;
                for (int j = 0; j < array2.Length; j++)
                {
                    Rectangle face = array2[j];
                    Image<Gray, byte> face2 = grayImage.GetSubRect(face);
                    frameImage.GrabCut(face, 1);
                    frameImage.Draw(face, new Bgr(Color.Red), 3, LineType.EightConnected, 0);
                    face2._EqualizeHist();
                    FaceRecognizer.PredictionResult result = this.faceRecognizer.Predict(face2.Resize(100, 100, Inter.Cubic));
                    Console.WriteLine(result.Label);
                    string path = Directory.GetDirectories("img")[result.Label];
                    base.Invoke(new Action(delegate ()
                    {
                        this.label1.Text = string.Format("当前用户是:{0},相似度：{1}", path.Split(new char[]
                        {
                            '\\'
                        }).Last<string>(), result.Distance);
                    }));
                    string txt = string.Format("{0},{1}", path.Split(new char[]
                    {
                        '\\'
                    }).Last<string>(), result.Distance);
                    Font font = new Font("宋体", 60f, GraphicsUnit.Pixel);
                    SolidBrush fontLine = new SolidBrush(Color.Blue);
                 
                    float xPos = (float)(face.X + (face.Width / 2 - txt.Length * 14 / 2));
                    float yPos = (float)(face.Y - 21);
                    frameImage.Draw(txt, new Point((int)xPos, (int)yPos), FontFace.HersheyComplex, 1.0, new Bgr(Color.AntiqueWhite), 1, LineType.AntiAlias);
                }
            }
            return frameImage;
        }

        private void TakePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                using (Image<Gray, byte> faceToSave = new Image<Gray, byte>(this.capture.QueryFrame().Bitmap))
                {
                    Rectangle[] faces = this.cascadeClassifierForFace.DetectMultiScale(faceToSave, 1.1, 12, Size.Empty, default(Size));
                    bool flag = faces.Length != 1;
                    if (!flag)
                    {
                        Image<Gray, byte> face = faceToSave.GetSubRect(faces[0]);
                        string username = this.textBox1.Text.Trim();
                        DirectoryInfo dir = new DirectoryInfo("img/" + username);
                        bool flag2 = !dir.Exists;
                        if (flag2)
                        {
                            dir.Create();
                        }
                        string fileName = dir.GetFiles().Length.ToString();
                        string filePath = string.Format("img/{0}/{1}.bmp", username, fileName);
                        face._EqualizeHist();
                        face.ToBitmap().Save(filePath);
                        this.picture.Image = face;
                        this.Button1_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<Mat> faceImages = new List<Mat>();
            List<int> faceLabels = new List<int>();
            int i = 0;
            foreach (string dirPath in Directory.GetDirectories("img"))
            {
                foreach (string filePath in Directory.GetFiles(dirPath))
                {
                    FileInfo file = new FileInfo(filePath);
                    FileStream memory = file.OpenRead();
                    Image<Gray, byte> faceImage = new Image<Gray, byte>(new Bitmap(memory));
                    faceImages.Add(faceImage.Resize(100, 100, Inter.Cubic).Mat);
                    faceLabels.Add(i);
                }
                i++;
            }
            this.faceRecognizer.Train(faceImages.ToArray(), faceLabels.ToArray());
            this.faceRecognizer.Write("face.xml");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            using (Image<Gray, byte> frameImage = new Image<Gray, byte>(this.capture.QueryFrame().Bitmap))
            {
                bool flag = frameImage != null;
                if (flag)
                {
                    Rectangle[] faces = this.cascadeClassifierForFace.DetectMultiScale(frameImage, 1.1, 12, Size.Empty, default(Size));
                    bool flag2 = faces.Length != 1;
                    if (flag2)
                    {
                        this.label1.Text = "未检出人脸";
                    }
                    else
                    {
                        Image<Gray, byte> face = frameImage.GetSubRect(faces[0]);
                        this.faceRecognizer.Read("face.xml");
                        face._EqualizeHist();
                        FaceRecognizer.PredictionResult result = this.faceRecognizer.Predict(face.Resize(100, 100, Inter.Cubic));
                        Console.WriteLine(result.Label);
                        string path = Directory.GetDirectories("img")[result.Label];
                        this.label1.Text = string.Format("当前用户是:{0},差异度：{1}", path.Split(new char[]
                        {
                            '\\'
                        }).Last<string>(), result.Distance);
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "jpg|*.jpg|png|*.png|bmp|*.bmp";
            bool flag = fileDialog.ShowDialog() == DialogResult.OK;
            if (flag)
            {
                Image<Bgr, byte> modelImage = new Image<Bgr, byte>(fileDialog.FileName);
                modelImage = this.DetectFace(modelImage);
                this.imageBox1.Height = modelImage.Height;
                this.imageBox1.Width = modelImage.Width;
                this.imageBox1.Image = modelImage;
            }
        }

      
    }
}
