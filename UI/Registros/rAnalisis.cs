﻿using AnalisisMedicos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisMedicos.UI.Registros
{
    public partial class rAnalisis : Form
    {
        public List<AnalisisDetalle> Detalle { get; set; }
        public rAnalisis()
        {
            InitializeComponent();
            this.Detalle = new List<AnalisisDetalle>();
        }

        private void Limpiar()
        {
            IdAnalisisNumericUpDown.Value = 0;
            ResultadoTextBox.Text = string.Empty;
           
            FechaNacimientoDateTimePicker.Value = DateTime.Now;
            MyErrorProvider.Clear();
        }
        private void CargarGrid()
        {
            DetalleDataGridView.DataSource = null;
            DetalleDataGridView.DataSource = Detalle;
        }
        private void NuevoButton_Click(object sender, EventArgs e)
        {

        }
    }
}
