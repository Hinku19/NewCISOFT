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
            dgvAlumnos.Rows.Add("Mariana López Ruiz", "A-26-0148", "Regular", "Grupo A", "662 123 4567");
            dgvAlumnos.Rows.Add("José Luis Peralta Soto", "A-26-0147", "Becado", "Grupo B", "637 555 0192");
            dgvAlumnos.Rows.Add("Daniela Ochoa Villa", "A-26-0145", "Regular", "Grupo A", "631 208 3345");
        }

        private void lblCatAlu_Click(object sender, EventArgs e)
        {

        }

        private void campoTexto1_TextoCambiado(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgvAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Stu_StudentDirectory_Load(object sender, EventArgs e)
        {

        }
    }
}
