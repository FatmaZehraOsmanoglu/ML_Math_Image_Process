using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageProcessingWinForms
{
    public partial class Form1 : Form
    {
        Bitmap originalImage;   // Orijinal resim
        Bitmap editedImage;     // İşlenen resim

        public Form1()
        {
            InitializeComponent();
        }

        // ===================== RESİM YÜKLE =====================
        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları|*.jpg;*.png;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(ofd.FileName);
                editedImage = (Bitmap)originalImage.Clone();

                pictureBox1.Image = originalImage;
                pictureBox2.Image = editedImage;
            }
        }

        // ===================== GRİYE ÇEVİR =====================
        private void btnGray_Click(object sender, EventArgs e)
        {
            if (editedImage == null) return;

            Bitmap temp = new Bitmap(editedImage.Width, editedImage.Height);

            for (int x = 0; x < editedImage.Width; x++)
            {
                for (int y = 0; y < editedImage.Height; y++)
                {
                    Color c = editedImage.GetPixel(x, y);
                    int gri = (c.R + c.G + c.B) / 3;
                    temp.SetPixel(x, y, Color.FromArgb(gri, gri, gri));
                }
            }

            editedImage = temp;
            pictureBox2.Image = editedImage;
        }

        // ===================== TERS RENK =====================
        private void btnTers_Click(object sender, EventArgs e)
        {
            if (editedImage == null) return;

            Bitmap temp = new Bitmap(editedImage.Width, editedImage.Height);

            for (int x = 0; x < editedImage.Width; x++)
            {
                for (int y = 0; y < editedImage.Height; y++)
                {
                    Color c = editedImage.GetPixel(x, y);
                    temp.SetPixel(x, y, Color.FromArgb(
                        255 - c.R,
                        255 - c.G,
                        255 - c.B
                    ));
                }
            }

            editedImage = temp;
            pictureBox2.Image = editedImage;
        }


        // ===================== 🔥 SEPYA FİLTRESİ =====================
        private void btnSepya_Click_1(object sender, EventArgs e)
        {
            if (editedImage == null) return;

            Bitmap temp = new Bitmap(editedImage.Width, editedImage.Height);

            for (int x = 0; x < editedImage.Width; x++)
            {
                for (int y = 0; y < editedImage.Height; y++)
                {
                    Color c = editedImage.GetPixel(x, y);

                    int r = (int)(c.R * 0.393 + c.G * 0.769 + c.B * 0.189);
                    int g = (int)(c.R * 0.349 + c.G * 0.686 + c.B * 0.168);
                    int b = (int)(c.R * 0.272 + c.G * 0.534 + c.B * 0.131);

                    // Değerler 255'ten büyük olursa 255 yap (renk taşmasını engeller)
                    r = (r > 255) ? 255 : r;
                    g = (g > 255) ? 255 : g;
                    b = (b > 255) ? 255 : b;

                    temp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            editedImage = temp;
            pictureBox2.Image = editedImage;
        }


        // ===================== RESET =====================
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (originalImage == null) return;

            editedImage = (Bitmap)originalImage.Clone();
            pictureBox2.Image = editedImage;
        }

        // ===================== KAYDET =====================
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG|*.png|JPEG|*.jpg|Bitmap|*.bmp";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(sfd.FileName);
                MessageBox.Show("Resim kaydedildi.", "Başarılı");
            }
        }
    }
}
