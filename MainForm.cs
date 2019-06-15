using AnalisisMedicos.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisMedicos
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rAnalisis a = new rAnalisis();
            a.Show();
        }

        private void TipoAnalisisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTiposAnalisis tp = new rTiposAnalisis();
            tp.Show();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rUsuarios us = new rUsuarios();
            us.Show();
        }
    }
}
