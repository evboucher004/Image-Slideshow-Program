using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace BackgroundProcess
{
    public partial class Form2 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; // x position of upper-left corner
            public int Top; // y position of upper-left corner
            public int Right; // x position of lower-right corner
            public int Bottom; // y position of lower-right corner
        }

        public Form1 form1;
        private List<string> prevImages = new List<string>();
        private List<int> prevImagesNum = new List<int>();
        private int currImage = 0;
        private string prevSize;
        private int folderSize = 0;

        public Form2(Form1 form)
        {
            InitializeComponent();
            form1 = form;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == Keys.Enter)
            {
                this.Hide();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        protected override void OnDeactivate(EventArgs e)
        {
            IntPtr handle = GetForegroundWindow();

            if (handle == this.Handle || handle == form1.Handle)
            {
                // No need to hide as other form is focused
            }
            else
            {
                form1.Hide();
                this.Hide();
            }

        }

        public void setFolderSize(int recFolderSize)
        {
            folderSize = recFolderSize;

            if (folderCountBox.Text == "")
            {
                folderCountLabel.Text = "0 / " + folderSize.ToString();
            }
        }

        // Adds opened images to list
        public void addToVector(string imageLocation)
        {
            prevImages.Add(imageLocation);
            currImage = prevImages.Count;

            prevSize = prevImages.Count.ToString();

            imgCountLabel.Text = currImage.ToString() + " / " + prevSize;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void imgCountBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (imgCountBox.Text != "")
                {
                    int x;
                    Int32.TryParse(imgCountBox.Text, out x);

                    if (x <= prevImages.Count && x > 0)
                    {
                        form1.OpenImage(prevImages[x - 1]);
                        imgCountLabel.Text = (x).ToString() + " / " + prevSize;
                    }
                }
            }
        }

        // Opens previous images from list
        private void backButton_Click(object sender, EventArgs e)
        {

            if (currImage > 1)
            {
                if (currImage == prevImages.Count)
                {
                    currImage -= 1;
                }

                currImage -= 1;
                form1.OpenImage(prevImages[currImage]);

                imgCountLabel.Text = (currImage + 1).ToString() + " / " + prevSize;
                updateFolderCountBoxBF(prevImagesNum[currImage]+1);
            }
        }

        // Opens recent images from list
        private void forwardButton_Click(object sender, EventArgs e)
        {
            if (currImage != prevImages.Count && currImage + 1 != prevImages.Count)
            {
                currImage += 1;
                form1.OpenImage(prevImages[currImage]);

                imgCountLabel.Text = (currImage + 1).ToString() + " / " + prevSize;
                updateFolderCountBoxBF(prevImagesNum[currImage]+1);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void folderCountBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (folderCountBox.Text != "")
                {
                    int x;
                    Int32.TryParse(folderCountBox.Text, out x);

                    if (x > 0 && x <= folderSize)
                    {
                        form1.FindImage(x - 1);
                        folderCountLabel.Text = x.ToString() + " / " + folderSize.ToString();
                    }
                }
            }
        }

        public void updateFolderCountBox(int imageNumberIn)
        {
            prevImagesNum.Add(imageNumberIn);
            
            if (folderCountBox.Text == "")
            {
                folderCountBox.Text = " ";
            }
            folderCountLabel.Text = imageNumberIn.ToString() + " / " + folderSize.ToString();
        }

        private void updateFolderCountBoxBF(int imageNumberIn)
        {
            folderCountLabel.Text = imageNumberIn.ToString() + " / " + folderSize.ToString();
        }

        private void folderCountBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
