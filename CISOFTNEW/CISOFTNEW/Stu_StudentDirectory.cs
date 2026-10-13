using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CISOFTNEW
{
    public partial class Stu_StudentDirectory : UserControl
    {
        public Stu_StudentDirectory()
        {
            InitializeComponent();
        }

        private void lblCatAlu_Click(object sender, EventArgs e)
        {

        }

        private void campoTexto1_TextoCambiado(object sender, EventArgs e)
        {

        }

        public event EventHandler BackRequested;

        private void btnSalir_Click(object sender, EventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
