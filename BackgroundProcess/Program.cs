using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;


namespace BackgroundProcess
{


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new MyCustomApplicationContext());
        }
    }

    public class MyCustomApplicationContext : ApplicationContext
    {
        public NotifyIcon trayIcon;
        public Form1 form;
        //public Form2 form2;

        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resource1.mozilla2,
                ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Open", OpenApp),
                new MenuItem("Exit", Exit)
                
            }),
                Visible = true
            };

            //add left clickness
            trayIcon.Click += new System.EventHandler(trayIcon_MouseClick);
            trayIcon.Text = "Mikozilla";

            form = new Form1(this);
            form.ShowInTaskbar = false;
            form.Location = new Point(Screen.GetWorkingArea(new Point(0, 0)).Right, Screen.GetWorkingArea(new Point(0, 0)).Bottom);
            form.Show();
            form.Hide();

        }

        private void trayIcon_MouseClick(object sender, EventArgs e)
        {
            //Do the awesome left clickness
            OpenApp(sender, e);
        }

        public void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }

        //opens form1
        void OpenApp(object sender, EventArgs e)
        {
            form.Location = new Point(Cursor.Position.X - form.Width/2, Screen.GetWorkingArea(new Point(0, 0)).Bottom - form.Height);

            form.Show();
            form.Activate();
        }
    }
}
