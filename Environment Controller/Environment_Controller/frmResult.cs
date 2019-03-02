using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Environment_Controller
{
    public partial class frmResult : MaterialForm
    {
        public frmResult()
        {
            InitializeComponent();
            var material = MaterialSkinManager.Instance;
            material.AddFormToManage(this);
            material.Theme = MaterialSkinManager.Themes.LIGHT;
            material.ColorScheme = new ColorScheme(Primary.Yellow900, Primary.Orange800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            
        }

        private void frmResult_Load(object sender, EventArgs e)
        {
            textBox1.Text = MyProcess.CMDResult;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult dr = folder.ShowDialog();
            string folderPath = folder.SelectedPath;
            if (File.Exists(folderPath + @"\log.txt"))            
                File.Delete(folderPath + @"\log.txt");
            
            FileStream file = new FileStream(folderPath + @"\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(file);

            sw.Write(MyProcess.CMDResult);
            sw.Close();
        }
    }
}
