using BLEditor.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class FormRunMADS : Form
    {
        public FormRunMADS(MapSet mapset,String MADSFullPath, String asmBaseLineFullPath, bool runEmulator, decimal firstmap=0)
        {
            InitializeComponent();
            this.MADSFullPath = MADSFullPath;
            this.asmBaseLineFullPath = asmBaseLineFullPath;
            this.Mapset = mapset;
            this.runEmulator = runEmulator;
            this.firstmap = firstmap;
            this.Shown += FormRunMADS_ExecAsync;
        }

        private string MADSFullPath;
        private string asmBaseLineFullPath;
        private MapSet Mapset;
        private bool runEmulator;
        private decimal firstmap;

        public string XEXFullPath { get; private set; }

        private async void FormRunMADS_ExecAsync(object sender222, EventArgs ezzzz)
        {
             String mapsetDirectory = Path.Combine(new FileInfo(Mapset.Path).Directory.FullName, "maps.asm");

            ASM.export(mapsetDirectory, Mapset, firstmap);

            XEXFullPath = $"{Path.Combine(Path.GetDirectoryName(mapsetDirectory), Path.GetFileNameWithoutExtension(Mapset.Path)) }.XEX";

            AddLine($"{Environment.NewLine}**Generating {XEXFullPath} **{Environment.NewLine}", Color.Red);

            var arguments = $"-i:\"{Path.GetDirectoryName(mapsetDirectory)}\" \"{asmBaseLineFullPath}\" -o:\"{XEXFullPath}\"";
            AddLine(arguments, Color.Red);
            var processResult = await ProcessAsyncHelper.RunProcessAsync(MADSFullPath, arguments, -1, p_OutputDataReceived, p_ErrorDataReceived);

            // Mads Exit codes
            // 3 = bad parameters, assembling not started
            // 2 = error occured
            // 0 = no errors
            buttonOK.Enabled = true;

            if (processResult.ExitCode == 0 && runEmulator)
            {          
                String EmulatorFullPath = Properties.Settings.Default["EmulatorFullPath"].ToString();
                String EmulatorCommandLine = Properties.Settings.Default["EmulatorCommandLine"].ToString();
                if (File.Exists(EmulatorFullPath))
                {
                    Process.Start(EmulatorFullPath, String.Format(EmulatorCommandLine, XEXFullPath));
                    DialogResult = DialogResult.OK;
                }
            }     
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
            {
                 return;
            }

            if (e.Data != null)
            {
           
                  Invoke(new Action(() => AddErrorLine((String)e.Data.Clone())));
            }
        }



        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
            {
                return;
            }

            if (e.Data != null)
            {
                Invoke(new Action(() => AddLine((String)e.Data.Clone(), Color.Black)));         
            }
        }


        private void AddErrorLine(String output)
        {
             AddLine(output, Color.Blue);
        }

        private void AddLine(String text, Color color)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
  
           textBoxConsole.SuspendLayout();
            textBoxConsole.SelectionColor = color;

            if (!string.IsNullOrWhiteSpace(textBoxConsole.Text))
            {
                textBoxConsole.AppendText($"{Environment.NewLine}{text}");
            }
            else
            {
                textBoxConsole.AppendText(text);
            }

            textBoxConsole.ScrollToCaret();
            textBoxConsole.ResumeLayout();
        }

        public static void Compile(MapSet mapSet, bool runEmulator=true, decimal firstmap = 0)
        {
            String MADSFullPath = Properties.Settings.Default["MADSFullPath"].ToString();
            if (!File.Exists(MADSFullPath))
            {
                MessageBox.Show("Please configure 'MADSFullPath' in Settings");
                return;
            }
            String AssemblyFullPath = Properties.Settings.Default["AssemblyFullPath"].ToString();
            if (!File.Exists(AssemblyFullPath))
            {
                MessageBox.Show("Please configure 'AssemblyFullPath' in Settings");
                return;
            }

            new FormRunMADS(mapSet, MADSFullPath, AssemblyFullPath, runEmulator, firstmap).ShowDialog();
        }
    }
}
