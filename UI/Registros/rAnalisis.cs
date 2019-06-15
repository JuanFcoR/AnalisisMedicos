using AnalisisMedicos.BLL;
using AnalisisMedicos.DAL;
using AnalisisMedicos.Entidades;
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

        private void cargarCombos()
        {
            Contexto c = new Contexto();
            UsuarioComboBox.DisplayMember = "Nombres";
            UsuarioComboBox.ValueMember = "UsuarioId";
            UsuarioComboBox.DataSource = c.Usuarios.Where(p => true).ToList();
            TipoAnalisisComboBox.DisplayMember = "Descripcion";
            TipoAnalisisComboBox.ValueMember = "TipoId";
            TipoAnalisisComboBox.DataSource = c.TiposAnalisis.Where(p => p.TipoId > 0).ToList();
        }

        private void Limpiar()
        {
            IdAnalisisNumericUpDown.Value = 0;
            ResultadoTextBox.Text = string.Empty;
            FechaDateTimePicker.Value = DateTime.Now;
            SuperErrorProvider.Clear();
        }
        private void CargarGrid()
        {
            DetalleDataGridView.DataSource = null;
            DetalleDataGridView.DataSource = Detalle;
        }
        private Analisis LlenarClase()
        {
            Analisis analisis = new Analisis();
            analisis.AnalisisId = Convert.ToInt32(IdAnalisisNumericUpDown.Value);
            analisis.Fecha = FechaDateTimePicker.Value;
            analisis.UsuarioId =UsuarioComboBox.SelectedIndex;
            analisis.TipoAnalisisId = TipoAnalisisComboBox.SelectedIndex;
            return analisis;
        }

        private bool ExisteEnLaBasedeDatos()
        {
            Analisis analisis = AnalisisBLL.Buscar((int)IdAnalisisNumericUpDown.Value);
            return (analisis != null);
        }

        private void LlenarCampos(Analisis analisis)
        {
            IdAnalisisNumericUpDown.Value = analisis.AnalisisId;
            FechaDateTimePicker.Value = analisis.Fecha;
            UsuarioComboBox.SelectedIndex = analisis.UsuarioId;
            TipoAnalisisComboBox.SelectedIndex = analisis.TipoAnalisisId;
            this.Detalle = analisis.AnalisisDetalles;
            CargarGrid();
            cargarCombos();

        }

        

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            Analisis analisis = new Analisis();
            int.TryParse(IdAnalisisNumericUpDown.Text, out id);

            Limpiar();

            analisis = AnalisisBLL.Buscar(id);

            if (analisis != null)
            {
                MessageBox.Show("Analisis Encontrado");
                LlenarCampos(analisis);
            }
            else
            {
                MessageBox.Show("Analisis no Encontado");
            }

        }

        private bool Validar()
        {

            bool paso = true;
            SuperErrorProvider.Clear();

            if (ResultadoTextBox.Text == string.Empty)
            {
                SuperErrorProvider.SetError(ResultadoTextBox, "El campo Nombre no puede estar vacio");
                ResultadoTextBox.Focus();
                paso = false;
            }
            return paso;
        }
        private void GuardarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            Analisis persona;
           

            if (!Validar())
                return;

            persona = LlenarClase();
            Limpiar();

            //Determinar si es guardar o modificar
            if (IdAnalisisNumericUpDown.Value == 0)
                paso = AnalisisBLL.Guardar(persona);
            else
            {
                if (!ExisteEnLaBasedeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = AnalisisBLL.Modificar(persona);
            }

            //Informar el resultado
            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        
    

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            SuperErrorProvider.Clear();
            int id;
            int.TryParse(IdAnalisisNumericUpDown.Text, out id);
            Limpiar();
            if (AnalisisBLL.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                SuperErrorProvider.SetError(IdAnalisisNumericUpDown, "No se puede eliminar una persona que no existe");
        }

       

         private void RemoverButton_Click(object sender, EventArgs e)
         {
             if (DetalleDataGridView.Rows.Count > 0 && DetalleDataGridView.CurrentRow != null)
             {
                 
                 Detalle.RemoveAt(DetalleDataGridView.CurrentRow.Index);

                 CargarGrid();
             }
         }

         private void AgregarButton_Click(object sender, EventArgs e)
         {
             if (DetalleDataGridView.DataSource != null)
                 this.Detalle = (List<AnalisisDetalle>)DetalleDataGridView.DataSource;
            
             this.Detalle.Add(
                 new AnalisisDetalle(
                     iD: 0,
                     analisisId: (int)IdAnalisisNumericUpDown.Value,
                     tipoId: TipoAnalisisComboBox.SelectedIndex,
                     resultado: ResultadoTextBox.Text
                     )
                ); ;

             CargarGrid();
         }

         private void RAnalisis_Load(object sender, EventArgs e)
         {
            TipoAnalisisComboBox.Items.Clear();
            cargarCombos();
        }

        private void TipoAnalisisComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarCombos();
        }

        private void RAnalisis_Load_1(object sender, EventArgs e)
        {
            TipoAnalisisComboBox.Items.Clear();
            cargarCombos();
        }
    }
}
