using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalisisMedicos.BLL;
using AnalisisMedicos.DAL;
using AnalisisMedicos.Entidades;

namespace AnalisisMedicos.UI.Registros
{
    public partial class rTiposAnalisis : Form
    {
        public rTiposAnalisis()
        {
            InitializeComponent();
        }

        private void limpiar()
        {
            TipoIdNumericUpDown.Value = 0;
            DescripcionTextBox.Text = String.Empty;
        }

        private TiposAnalisis LlenarClase()
        {
            TiposAnalisis Ti = new TiposAnalisis();
            Ti.TipoId = Convert.ToInt32(TipoIdNumericUpDown.Value);
            Ti.Descripcion = DescripcionTextBox.Text;
            return Ti;
        }

        private void LlenarCampos(TiposAnalisis Ti)
        {
            TipoIdNumericUpDown.Value = Ti.TipoId;
            DescripcionTextBox.Text = Ti.Descripcion;
            
        }
        private bool ExisteEnLaBasedeDatos()
        {
            TiposAnalisis Ti = TipoAnalisisBLL.Buscar((int)TipoIdNumericUpDown.Value);
            return (Ti != null);
        }

        private bool Validar()
        {
            bool paso = true;
          
                if (String.IsNullOrWhiteSpace(DescripcionTextBox.Text))
                {
                    SuperErrorProvider.SetError(DescripcionTextBox, "Este campo no debe estar vacio");
                    paso = false;
                }
            return paso;
        }


        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            TiposAnalisis Ti = new TiposAnalisis();
            int.TryParse(TipoIdNumericUpDown.Value.ToString(), out id);
            limpiar();

            Ti = TipoAnalisisBLL.Buscar(id);

            if (Ti != null)
            {
                MessageBox.Show("Tipo de analisis Encontrado");
                LlenarCampos(Ti);
            }
            else
                MessageBox.Show("Tipo de analisis no Encontrado");
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            TiposAnalisis Ti;
            bool paso = false;


            if (!Validar())
                return;
            Ti = LlenarClase();
            limpiar();

            if (TipoIdNumericUpDown.Value == 0)
            {
                paso = TipoAnalisisBLL.Guardar(Ti);
            }
            else
            {
                if (!ExisteEnLaBasedeDatos())
                {
                    MessageBox.Show("No se puede modificar un tipo de analisis que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = TipoAnalisisBLL.Editar(Ti);
            }
            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
                MessageBox.Show("No se pudo guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

   

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            SuperErrorProvider.Clear();
            int id;
            int.TryParse(Convert.ToString(TipoIdNumericUpDown.Value), out id);
            limpiar();
            if (TipoAnalisisBLL.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                SuperErrorProvider.SetError(TipoIdNumericUpDown, "No se puede eliminar un tipo de analisis que no existe");
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            limpiar();
        }
    }
}
