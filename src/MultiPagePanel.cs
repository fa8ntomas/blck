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
        public interface IStatusTipsProvider
        {
            void AddHandler(EventHandler StatusTipsProvided);
            void RemoveHandler(EventHandler StatusTipsProvided);
        }
        public class StatusTipsUpdateEventArgs : EventArgs
        {
            public String Tips { get; private set; }

            public StatusTipsUpdateEventArgs(String Tips)
              : base()
            {
                this.Tips = Tips;
            }
        }
        public MultiPagePanel()
        {
            InitializeComponent();
        }

        private void updateSatusTips(object sender, EventArgs e)
        {
            UpdateStatusToolTips?.Invoke(sender, e);
        }

        public event EventHandler UpdateStatusToolTips;

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

                if (_CurrentPage is IStatusTipsProvider)
                {
                    ((IStatusTipsProvider)_CurrentPage).RemoveHandler(updateSatusTips);
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

                    if (_CurrentPage is IStatusTipsProvider)
                    {
                        ((IStatusTipsProvider)_CurrentPage).AddHandler(updateSatusTips);
                    }

                    _CurrentPage.BringToFront();
                }
            }
        }
    }
}
