using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserPrefs;

namespace BLEditor
{
    public partial class FormSettings : Form
    {
     
        public FormSettings()
        {
            InitializeComponent();
        }

        public class PropertyBag
        {
            [CategoryAttribute("Assembler Settings")]
            [DisplayName("Mad-Assembler")]
            [Description("Mad-Assembler Full Path")]
            [Editor(typeof(MADSLocationEditor), typeof(UITypeEditor))]
            public string MADSFullPath { get; set; }

            [CategoryAttribute("Assembler Settings")]
            [DisplayName("Assembly Code")]
            [Description("Assembly Code Full Path")]
            [Editor(typeof(AssemblyLocationEditor), typeof(UITypeEditor))]
            public string AssemblyFullPath { get; set; }


            [CategoryAttribute("Emulator Settings")]
            [DisplayName("Emulator Path")]
            [Description("Emulator Full Path")]
            [Editor(typeof(ExeLocationEditor), typeof(UITypeEditor))]
            public string EmulatorFullPath { get; set; }

            [CategoryAttribute("Emulator Settings")]
            [DisplayName("Emulator command line")]
            [Description("Emulator command line. «{0}» = full path of the generated XEX.\nExample of command line for Altirra:« /run \"{0}\" »")]
            public string EmulatorCommandLine { get; set; }

            [CategoryAttribute("Other Settings")]
            [DisplayName("External Palette")]
            [Description("External Palette Full Path.\nCAUTION: You must restart the program for this setting to take effect.")]
            [Editor(typeof(PaletteLocationEditor), typeof(UITypeEditor))]
            public string PaletteFullPath { get; set; }

            public PropertyBag()
            {
                MADSFullPath=Properties.Settings.Default["MADSFullPath"].ToString();
                AssemblyFullPath = Properties.Settings.Default["AssemblyFullPath"].ToString();
                PaletteFullPath = Properties.Settings.Default["PaletteFullPath"].ToString();
                EmulatorFullPath = Properties.Settings.Default["EmulatorFullPath"].ToString();
                EmulatorCommandLine = Properties.Settings.Default["EmulatorCommandLine"].ToString();
            }

            public void Save()
            {
                Properties.Settings.Default["MADSFullPath"]= MADSFullPath;
                Properties.Settings.Default["AssemblyFullPath"] = AssemblyFullPath;
                Properties.Settings.Default["PaletteFullPath"] = PaletteFullPath;
                Properties.Settings.Default["EmulatorFullPath"] = EmulatorFullPath;
                Properties.Settings.Default["EmulatorCommandLine"] = EmulatorCommandLine;
                Properties.Settings.Default.Save();
            }
        }

        public PropertyBag AppSettings = new PropertyBag();

        public class PaletteLocationEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Palette File|*.act";
                    ofd.FilterIndex = 0;

                 
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        return ofd.FileName;
                    }
                }
                return value;
            }
        }
        public class MADSLocationEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Mad-Assembler (MADS)|mads.exe";
                    ofd.FilterIndex = 0;


                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        return ofd.FileName;
                    }
                }
                return value;
            }
        }
        public class ExeLocationEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Windows executable|*.exe";
                    ofd.FilterIndex = 0;


                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        return ofd.FileName;
                    }
                }
                return value;
            }
        }
        public class AssemblyLocationEditor : UITypeEditor
        {
            public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return UITypeEditorEditStyle.Modal;
            }

            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Assembly Main Code|BruceLeeExtended.asm";
                    ofd.FilterIndex = 0;


                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        return ofd.FileName;
                    }
                }
                return value;
            }
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = AppSettings;
        }
    }
}
