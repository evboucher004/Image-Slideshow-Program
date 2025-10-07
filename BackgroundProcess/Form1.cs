using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UITimer = System.Threading.Timer;
using System.Security.Policy;


namespace BackgroundProcess
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, UInt32 wFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }


        static string path = "D:\\Stuff\\Pictures\\All"; // Path to images
        private string[] files = Directory.GetFiles(path);
        private int folderSize = Directory.GetFiles(path).Length;


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private Form2 advOptions;
        private MyCustomApplicationContext thisAppContext;
        public Form1(MyCustomApplicationContext appContext)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0; // Sets default to seconds
            comboBox2.SelectedIndex = 0; // Sets default to monitor 1

            advOptions = new Form2(this);
            advOptions.ShowInTaskbar = false;
            advOptions.Location = new Point(Screen.GetWorkingArea(new Point(0, 0)).Right, Screen.GetWorkingArea(new Point(0, 0)).Bottom);
            advOptions.Show();
            advOptions.Hide();
            thisAppContext = appContext;

            advOptions.setFolderSize(folderSize);
        }

        protected override void OnHandleCreated(EventArgs e)
        {

            //https://ourcodeworld.com/articles/read/573/how-to-register-a-single-or-multiple-global-hotkeys-for-a-single-key-in-winforms
            int UniqueHotkeyId = 1;
            int UniqueHotkeyId2 = 2;
            int UniqueHotkeyId3 = 3;
            int UniqueHotkeyId4 = 4;

            int HotKeyCode = (int)Keys.F8;
            int HotKeyCode2 = (int)Keys.F9;
            int HotKeyCode3 = (int)Keys.F10;
            int HotKeyCode4 = (int)Keys.F11;

            Boolean F12Registered = Form1.RegisterHotKey(
                this.Handle, UniqueHotkeyId, 0x0000, HotKeyCode
            );
            Boolean F9Registered = Form1.RegisterHotKey(
                this.Handle, UniqueHotkeyId2, 0x0000, HotKeyCode2
            );
            Boolean F10Registered = Form1.RegisterHotKey(
                this.Handle, UniqueHotkeyId3, 0x0000, HotKeyCode3
            );
        }

        protected override void WndProc(ref Message m)
        {

            // 5. Catch when a HotKey is pressed !
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                if (id == 1)
                {
                    if (checkBox2.Checked)
                    {
                        checkBox2.Checked = false;
                    }
                    else
                    {
                        checkBox2.Checked = true;
                    }
                }
                else if (id == 2)
                {
                    RandomImage();
                }
                else if (id == 3)
                {
                    CloseWindow(imageHandle);
                }
            }

            base.WndProc(ref m);
        }


        protected override void OnDeactivate(EventArgs e)
        {
            IntPtr handle = GetForegroundWindow();

            if (handle == this.Handle || handle == advOptions.Handle)
            {
                // No need to hide as other form is focused
            }
            else
            {
                this.Hide();
                advOptions.Hide();
            }
        }

        protected override void OnActivated(EventArgs e)
        {
            if (checkBox1.Checked)
            {
                advOptions.Location = new Point(this.Location.X - advOptions.Width + 1, Screen.GetWorkingArea(new Point(0, 0)).Bottom - advOptions.Height);
                advOptions.Show();
                this.Show();
            }
        }
        


        //actual program functions and vars------------------------------------------------------------------------------------------------------------------------------------------------------


        bool enabled = false; // is timer enabled
        bool firstRun = true; // does imageglass need to be opened
        int delay = 2000; // delay for instructions on first run
        string imagePath; // path to open location for current image

        int minTime; // min time for timer
        int maxTime; // max time for timer
        int timeMult; // minutes/seconds multiplier for timer

        static Rectangle monitor; // Monitor to open image on
        IntPtr imageHandle; // handle for imageglass window
        int currMonitor = -1; // monitor the window is currently on
        int monitorNumber; // monitor selected on form

        private bool isLinked = true;
        private string processName;

        int newTime;
        public static UITimer t; // timer
        string time; // pc time

        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int GWL_EXSTYLE = -20;
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOZORDER = 0x0004;
        private const UInt32 NOACTIVE = 0x0010;
        private const UInt32 WM_CLOSE = 0x0010;

        public void setPath(string newPathIn)
        {
            
            if (Directory.Exists(newPathIn))
            {
                path = newPathIn;

                files = Directory.GetFiles(path);
                folderSize = Directory.GetFiles(path).Length;

                advOptions.setFolderSize(folderSize);
            }
            else
            {
                // popup saying path doesnt exist
            }

        }

        private void RandomImage() // Randomly selects in image from the folder
        {
            var rand = new Random();
            int randNum = rand.Next(files.Length);

            imagePath = files[randNum];
            imagePath = "\"" + imagePath + "\"";
            
            advOptions.addToVector(imagePath);
            advOptions.updateFolderCountBox(randNum);
            OpenImage(imagePath);
        }

        public void FindImage(int imageNumber)
        {
            imagePath = files[imageNumber];
            imagePath = "\"" + imagePath + "\"";

            advOptions.addToVector(imagePath);
            advOptions.updateFolderCountBox(imageNumber+1);
            OpenImage(imagePath);
        }


        // gets the time range from the min and max boxes
        private void GetTimes() // Gets time range from the min and max boxes
        {
            Int32.TryParse(textBox1.Text, out minTime);
            Int32.TryParse(textBox2.Text, out maxTime);
            maxTime++;
        }

        Process p;
        public void OpenImage(string image) // Opens image from given location
        {
            advOptions.setFolderSize(folderSize);

            p = new Process();
            /*p.StartInfo = new ProcessStartInfo(@"C:\Program Files\ImageGlass\ImageGlass.exe", image);*/
            p.StartInfo.FileName = image;
            /*p.StartInfo.Arguments = "\"" + path + "\"";*/
            p.StartInfo.UseShellExecute = true; // required to have file open in default program
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; // causes the program to not be focused each time an image is opened
            p.Start();

            processName = p.ProcessName;

            if (firstRun) // instructions for first image to open after application is run/ after image glass is closed
            {
                foreach (Process p in Process.GetProcessesByName(processName))
                {
                    System.Threading.Thread.Sleep(delay);
                    imageHandle = p.MainWindowHandle;

                    int exStyle = GetWindowLong(imageHandle, GWL_EXSTYLE);
                    SetWindowLong(imageHandle, GWL_EXSTYLE, exStyle | WS_EX_NOACTIVATE);

                    this.Show();
                    this.Activate();

                    firstRun = false;
                }
            }

            /*if (currMonitor != monitorNumber)
            {
                MoveMonitor();
                currMonitor = monitorNumber;
            }*/

            p.Dispose();

        }

        private void MoveMonitor() // moves window to set monitor
        {
            RECT rct;

            foreach (Process p in Process.GetProcessesByName(processName))
            {
                imageHandle = p.MainWindowHandle;

                GetWindowRect(imageHandle, out rct);
                Rectangle screen = Screen.AllScreens[monitorNumber].Bounds;
                Point pt = new Point(monitor.Left + monitor.Width / 2 - (rct.Right - rct.Left) / 2, monitor.Top + monitor.Height / 2 - (rct.Bottom - rct.Top) / 2);

                SetWindowPos(imageHandle, 0, pt.X, pt.Y, 0, 0, NOACTIVE | SWP_NOSIZE | SWP_NOZORDER); // sets the windows position and size to the chosen monitor
            }
        }

        // closes open image window
        void CloseWindow(IntPtr hwnd) // Closes open image window
        {

            foreach (Process p in Process.GetProcessesByName(processName))
            {
                // Ask nicely for the process to close.
                p.CloseMainWindow();

                // Wait up to 10 seconds for the process to close.
                p.WaitForExit(500);

                if (!p.HasExited)
                {
                    // The process did not close itself so force it to close.
                    p.Kill();
                }

                // Dispose the Process object, which is different to closing the running process.
                p.Close();

                firstRun = true;
            }

            firstRun = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow(imageHandle);
        }

        void TimerCreation()
        {
            GetTimes();
            newTime = NewTime();

            t = new UITimer(TimerCallback, null, newTime * timeMult, (newTime * timeMult) + 2000);
            time = DateTime.Now.ToString("t");


            //if (comboBox2.SelectedIndex == -1)
            //{
            //    monitor = Screen.AllScreens[monitorNumber].WorkingArea;
            //}
        }

        void TimerCallback(Object o)
        {
            RandomImage();

            newTime = newTime = NewTime();

            if (t != null)
            {
                t.Change(newTime * timeMult, newTime * timeMult + 1000);
                time = DateTime.Now.ToString("t");
            }
        }
        
        int NewTime()
        {
            var rand = new Random();
            return rand.Next(minTime, maxTime);
        }



        //form box functions--------------------------------------------------------------------------------------------------------------------------------------------------------------------


        //advanced options box
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //Program.form2.Show();
                //MyCustomApplicationContext.

                advOptions.Show();
                advOptions.Location = new Point(this.Location.X - advOptions.Width + 1, Screen.GetWorkingArea(new Point(0, 0)).Bottom - advOptions.Height);
            }
            else
            {
                advOptions.Hide();
            }
        }

        //min box
        private void textBox1_TextChanged(object sender, EventArgs e) // Min Box
        {
            int x;
            int x2;

            Int32.TryParse(textBox1.Text, out x);
            Int32.TryParse(textBox2.Text, out x2);

            if (isLinked || x > x2)
            {
                textBox2.Text = textBox1.Text;
            }
        }
        
        //max box
        private void textBox2_TextChanged(object sender, EventArgs e) // Max Box
        {
            if (isLinked)
            {
                textBox1.Text = textBox2.Text;
            }
        }

        //time box
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // Time Box
        {
            
            if (comboBox1.SelectedIndex == 0)
            {
                timeMult = 1000; // seconds
            }
            else
            {
                timeMult = 60000; // minutes
            }
        }

        //monitor box
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) // Monitor Box
        {
            monitorNumber = comboBox2.SelectedIndex;
            monitor = Screen.AllScreens[monitorNumber].WorkingArea;
        }

        //enabled checkbox
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Enabled && enabled == false)
            {
                TimerCreation(); // start timer function
                enabled = true;
            } // ------------------------------------------------enabled variable may not be needed, use checkbox2.enabled instead
            else
            {
                t.Dispose(); // Dispose of timer
                t = null; // Reset timer variable
                enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e) // Exit button
        {
            thisAppContext.trayIcon.Visible = false;

            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e) // Close Window button
        {
            CloseWindow(imageHandle);
        }

        private void button3_Click(object sender, EventArgs e) // Open File Location button
        {
            SetForegroundWindow(imageHandle);
            SendKeys.SendWait("{l}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RandomImage();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void linkedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (linkedBox.Enabled && isLinked == false)
            {
                isLinked = true;
            } 
            else
            {
                isLinked = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string t = textBox6.Text.Replace(@"\", @"\\");

            setPath(t);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
