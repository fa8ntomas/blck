using BLEditor.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLEditor
{
    public partial class FormRunMADS : Form
    {
        public FormRunMADS(MapSet mapset,String MADSFullPath, String AsmBaseLineFullPath, bool runEmulator, int firstmap=0)
        {
            InitializeComponent();
            this.MADSFullPath = MADSFullPath;
            this.AsmBaseLineFullPath = AsmBaseLineFullPath;
            this.Mapset = mapset;
            this.runEmulator = runEmulator;
            this.firstmap = firstmap;
            this.Shown += (s, e) => { CompileAndRun(); };
        }

        public FormRunMADS(MapSet Mapset, string MADSFullPath, string AsmBaseLineFullPath, string ExomizerFullPath)
        {
            InitializeComponent();
            this.Mapset = Mapset;
            this.MADSFullPath = MADSFullPath;
            this.AsmBaseLineFullPath = AsmBaseLineFullPath;
            this.ExomizerFullPath = ExomizerFullPath;
            this.Shown += (s, e) => { GenerateRelease(); };
        }

        private readonly string MADSFullPath;
        private string AsmBaseLineFullPath;
        private MapSet Mapset;
        private bool runEmulator;
        private int firstmap;
        private string ExomizerFullPath;

        public String GetXEXFullPath()
        {
            return $"{Path.Combine(new FileInfo(Mapset.Path).Directory.FullName, Path.GetFileNameWithoutExtension(Mapset.Path)) }.XEX";
        }
        public String GetPackedXEXFullPath()
        {
            return $"{Path.Combine(new FileInfo(Mapset.Path).Directory.FullName, Path.GetFileNameWithoutExtension(Mapset.Path)) }-packed.XEX";
        }
        private async void Compile(Action func)
        {
            String generatedSourceFile = Path.Combine(new FileInfo(Mapset.Path).Directory.FullName, "maps.asm");

            ASM.Export(generatedSourceFile, AsmBaseLineFullPath, Mapset);

            AddLine($"{Environment.NewLine}**Generating {GetXEXFullPath()} **{Environment.NewLine}", Color.Red);

            String arguments = $"-i:\"{Path.GetDirectoryName(generatedSourceFile)}\" \"{generatedSourceFile}\" -o:\"{GetXEXFullPath()}\"";
            
            if (firstmap > 0)
            {
                arguments += $" -d:BLCK_MAPSTART={firstmap}";
            }

            AddLine(arguments, Color.Red);
            
            var processResult = await ProcessAsyncHelper.RunProcessAsync(MADSFullPath, arguments, -1, P_OutputDataReceived, p_ErrorDataReceived);

            // Mads Exit codes
            // 3 = bad parameters, assembling not started
            // 2 = error occured
            // 0 = no errors
            buttonOK.Enabled = true;

            if (processResult.ExitCode == 0)
            {
                func();
            }     
        }

        private  void CompileAndRun()
        {
            Compile( () =>
            {
                if (runEmulator) { 
                    String EmulatorFullPath = Properties.Settings.Default["EmulatorFullPath"].ToString();
                    String EmulatorCommandLine = Properties.Settings.Default["EmulatorCommandLine"].ToString();
                    if (File.Exists(EmulatorFullPath))
                    {
                        Process.Start(EmulatorFullPath, String.Format(EmulatorCommandLine, GetXEXFullPath()));
                        DialogResult = DialogResult.OK;
                    }
                }
            });

        }

        private  void GenerateRelease()
        {
            Compile(async () =>
            {
                String ExomizerFullPath = Properties.Settings.Default["ExomizerFullPath"].ToString();
                String EmulatorCommandLine = Properties.Settings.Default["EmulatorCommandLine"].ToString();
                if (File.Exists(ExomizerFullPath))
                {
                    var maxAdr=XEX.Load(GetXEXFullPath()).GetMaxAdr()+2;

                    var arguments = $"sfx sys -x3 \"{GetXEXFullPath()}\" -Di_table_addr={maxAdr} -t 168 -o \"{GetPackedXEXFullPath()}\"";
                    AddLine($"{Environment.NewLine}** {arguments} **{Environment.NewLine}", Color.Red);

                    var processResult = await ProcessAsyncHelper.RunProcessAsync(ExomizerFullPath, arguments, -1, P_OutputDataReceived, p_ErrorDataReceived);

                  //  DialogResult = DialogResult.OK;
                }
            });

        }
        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!(sender is Process p))
            {
                return;
            }

            if (e.Data != null)
            {
           
                  Invoke(new Action(() => AddErrorLine((String)e.Data.Clone())));
            }
        }



        void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!(sender is Process p))
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

        public static void Compile(MapSet mapSet, bool runEmulator=true, int firstmap = 0)
        {
            String MADSFullPath = "";
            String AssemblyFullPath= "";
            if (!CheckMadsPath(ref MADSFullPath, ref AssemblyFullPath)) return;

            new FormRunMADS(mapSet, MADSFullPath, AssemblyFullPath, runEmulator, firstmap).ShowDialog();
        }

        private static bool CheckMadsPath(ref String MADSFullPath, ref String AssemblyFullPath)
        {
            MADSFullPath = Properties.Settings.Default["MADSFullPath"].ToString();
            if (!File.Exists(MADSFullPath))
            {
                MessageBox.Show("Please configure 'MADSFullPath' in Settings");
                return false;
            }

            AssemblyFullPath = Properties.Settings.Default["AssemblyFullPath"].ToString();
            if (!File.Exists(AssemblyFullPath))
            {
                MessageBox.Show("Please configure 'AssemblyFullPath' in Settings");
                return false;
            }

            return true;
        }

        private static bool CheckExomizerPath(ref String ExomizerFullPath)
        {
            ExomizerFullPath = Properties.Settings.Default["ExomizerFullPath"].ToString();
            if (!File.Exists(ExomizerFullPath))
            {
                MessageBox.Show("Please configure 'ExomizerFullPath' in Settings");
                return false;
            }

            return true;
        }
        internal static void BuidRelease(MapSet mapSet)
        {
            String MADSFullPath = "";
            String AssemblyFullPath = "";
            if (!CheckMadsPath(ref MADSFullPath, ref AssemblyFullPath)) return;

            String ExomizerFullPath = "";
            if (!CheckExomizerPath(ref ExomizerFullPath)) return;

            FormRunMADS formRunMADS = new FormRunMADS(mapSet, MADSFullPath, AssemblyFullPath, ExomizerFullPath);
            _ = formRunMADS.ShowDialog();

        }
    }
}
