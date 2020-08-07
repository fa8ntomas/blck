using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace BLEditor
{
    public partial class MultiPagePanel : UserControl
    {
        public interface ISavePanel
        {
            bool Save();
            void Load();
        }

        public MultiPagePanel()
        {
            InitializeComponent();
        }


        private Control _CurrentPage;
        public Control CurrentPage
        {
            get => _CurrentPage;
            set
            {
                if (_CurrentPage is ISavePanel)
                {
                    bool @continue = ((ISavePanel)_CurrentPage).Save();
                    if (!@continue)
                    {
                        SystemSounds.Beep.Play();
                        return;
                    }
                }

                if (!Controls.Contains(value))
                {
                    Controls.Add(value);
                }

                _CurrentPage = value;
                if (_CurrentPage != null)
                {
                    _CurrentPage.Dock = DockStyle.Fill;

                    if (_CurrentPage is ISavePanel)
                    {
                        ((ISavePanel)_CurrentPage).Load();

                    }

                    _CurrentPage.BringToFront();
                }
            }
        }
    }
}
