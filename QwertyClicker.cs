using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Reflection;
using System.Configuration;
using System.Threading;
namespace Qwerty_Clicker
{
    public partial class QwertyClicker : Form
    {
        public QwertyClicker()
        {
            InitializeComponent();
            AutoCheckForUpdate();
            delete_old_file(); 
        }
        public void Start()
        {
            notimplemented_dialog.Show();
        }
        public void Stop()
        {
            notimplemented_dialog.Show();
        }
        public void DiscordStart()
        {

        }
        public void DiscordStop()
        {

        }
        public void delete_old_file()
        {
            try
            {
                File.Delete("QwertyClicker.old");
            }
            catch
            {
                return;
            }
        }
        public void Exit()
        {
            Application.Exit();
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void qc_menubar_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = MousePosition.X;
                var y = MousePosition.Y;
                menu_rightclick.Show(x, y);
            }
        }

        private void rightclickstripmenu(object sender, MouseEventArgs e) // the right click form action, and yeah its simple
        {
            if (e.Button == MouseButtons.Right)
            {
                var x = MousePosition.X;
                var y = MousePosition.Y;
                rightclick_action.Show(x, y);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();
        private void discordServerToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://qwertydev.ml/discord"); // sure it will start as default browser.

        private void githubOrganizationToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://github.com/QwertyDev"); // well also github xD

        private void supportUs3ToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://qwertydev.ml/support-us"); // i hope people support lmao

        private void updateToolStripMenuItem_Click(object sender, EventArgs e) => CheckForUpdate();

        private void siticoneButton3_Click(object sender, EventArgs e) => CheckForUpdate();

        private void siticoneButton2_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (siticoneButton2.Text == "Disable")
                {
                    Stop();
                    //siticoneButton2.FillColor = Color.Lime;
                    //siticoneButton2.Text = "Enable";
                    return;
                }
                if (siticoneButton2.Text == "Enable")
                {
                    Start();
                    //siticoneButton2.FillColor = Color.Red;
                    //siticoneButton2.Text = "Disable";
                    return;
                }
            }
        }

        private void siticoneButton1_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (siticoneButton1.Text == "Disable")
                {
                    Stop();
                    //siticoneButton1.FillColor = Color.Lime;
                    siticoneButton1.Text = "Enable";
                    return;
                }
                if (siticoneButton1.Text == "Enable")
                {
                    Start();
                    //siticoneButton1.FillColor = Color.Red;
                    //siticoneButton1.Text = "Disable";
                    return;
                }
            }
        }

        private void r_val(object sender, EventArgs e) => right_cps_label.Text = r_cps.Value + " CPS";
        private void l_val(object sender, EventArgs e) => left_cps_label.Text = l_cps.Value + " CPS";

        private void minimizedToolStripMenuItem_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized; //Hide(); <-- anyone will suck this method

        private void not_implemented(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // oh yeah its not implemented yet. idk when its implemented. you can contribute us!
                notimplemented_dialog.Show();
                //MessageBox.Show("Not implemented yet.\nCheck our features on github for more info.", "Cant toggle!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void siticoneToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
           if (siticoneToggleSwitch3.Checked)
            {
                DiscordStart();
            }
        }
        public void autocheckupdate_failed() => autoupdate_failed.Show();
        public void checkupdate_failed() => updater_failed.Show();
        public void noupdates() => updater_noupdates.Show();
        public void update_failed_dl() => updater_faileddownload.Show();
        public void newupdate_detected() => update_detected.Show();
        public void update_finish() => update_finished.Show();
        public void stopped_clicker() => stopclicker_dialog.Show();
        public static WebClient webClient = new WebClient();
        public static Version GetVersion() => Assembly.GetExecutingAssembly().GetName().Version;
        public void AutoCheckForUpdate()
        {
            try
            {
                webClient.DownloadString("https://raw.githubusercontent.com/xqwtxon/QwertyClicker/main/raw/version.txt");
            }
            catch
            {
                autocheckupdate_failed();
                //MessageBox.Show("Unable to check update at the moment.\nPlease check your connection and try again!", "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Version.Parse(webClient.DownloadString("https://raw.githubusercontent.com/xqwtxon/QwertyClicker/main/raw/version.txt")) > GetVersion())
            {
                if (MessageBox.Show("New Update is available!\nDo you want to update now?", "Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) UpdateClicker();
                return;
            }
        }
        public void CheckForUpdate()
        {
            try
            {
                webClient.DownloadString("https://raw.githubusercontent.com/xqwtxon/QwertyClicker/main/raw/version.txt");
            }
            catch
            {
                checkupdate_failed();
                //MessageBox.Show("Unable to update at the moment.\nPlease check your connection and try again!", "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Version.Parse(webClient.DownloadString("https://raw.githubusercontent.com/xqwtxon/QwertyClicker/main/raw/version.txt")) > GetVersion())
            {
                var Dialog = MessageBox.Show("New Update is available!\nDo you want to update now?", "Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (Dialog == DialogResult.Yes)
                {
                    UpdateClicker();
                }
                /*if (update_detected.Buttons == as now idk)
                {
                    Update();
                }*/
            }
            else
            {
                noupdates();
                //MessageBox.Show("No Updates are available!\nCheck back soon at the mean time...", "Updater", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void UpdateClicker()
        {
            var path = Assembly.GetExecutingAssembly().Location;

            try
            {
                Directory.GetAccessControl(Path.GetDirectoryName(path));
            }
            catch
            {
                MessageBox.Show("The updater has no permission to access the qwerty directory!", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            File.Move(path, Path.ChangeExtension(path, "old"));
            File.Delete("DiscordRPC.dll");
            File.Delete("Newtonsoft.Json.dll");
            File.Delete("Siticone.Desktop.UI.dll");
            try
            {
                new WebClient().DownloadFile("https://github.com/xqwtxon/QwertyInjector/raw/main/download/latest/QwertyClicker.exe", path);
                new WebClient().DownloadFile("https://github.com/xqwtxon/QwertyInjector/raw/main/download/latest/Siticone.Desktop.UI.dll", path);
            }
            catch
            {
                update_failed_dl();
                //MessageBox.Show("Unable to download files!\nPlease check your connection and try again!", "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //MessageBox.Show("Update is finished!", "Updater", MessageBoxButtons.OK, MessageBoxIcon.Information);
            update_finish();
            Process.Start(path);
            //File.Delete("QwertyClicker.old");
            Application.Exit();
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
            if (siticoneButton2.Text == "Disable")
            {
                siticoneButton1.FillColor = Color.Lime;
                siticoneButton1.Text = "Enable";
                siticoneButton2.FillColor = Color.Lime;
                siticoneButton2.Text = "Enable";
                //Stop();
                stopped_clicker();
                return;
            }
            if (siticoneButton1.Text == "Disable")
            {
                siticoneButton1.FillColor = Color.Lime;
                siticoneButton1.Text = "Enable";
                siticoneButton2.FillColor = Color.Lime;
                siticoneButton2.Text = "Enable";
                //Start();
                //stopped_clicker();
                return;
            }
        }  
    }
}
