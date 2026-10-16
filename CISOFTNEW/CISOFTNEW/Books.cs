using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CISOFTNEW
{
    public partial class Books : UserControl
    {
        public Books()
        {
            InitializeComponent();
            
        }

        public event EventHandler? BackRequested;
        private void btnSalir_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
