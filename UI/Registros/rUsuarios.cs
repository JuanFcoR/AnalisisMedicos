using AnalisisMedicos.BLL;
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
    public partial class rUsuarios : Form
    {
        public rUsuarios()
        {
            InitializeComponent();
        }

        private void limpiar()
        {
            UsuarioIdnumericUpDown.Value = 0;
            NombresTextBox.Text = String.Empty;
            EmailTextBox.Text = String.Empty;
            NivelUsuarioTextBox.Text = String.Empty;
            UsuarioTextBox.Text = String.Empty;
            ClaveTextBox.Text = String.Empty;
            FechaIngresoDateTimePicker.Value = DateTime.Now;
            SuperErrorProvider.Clear();
        }

        private bool Validar()
        {
            bool paso = true;
            
                if (String.IsNullOrWhiteSpace(NombresTextBox.Text))
                {
                    SuperErrorProvider.SetError(NombresTextBox, "Este campo no debe estar vacio");
                    paso = false;
                }

                if (String.IsNullOrWhiteSpace(EmailTextBox.Text))
                {
                    SuperErrorProvider.SetError(EmailTextBox, "Este campo no debe estar vacio");
                    EmailTextBox.Focus();
                    paso = false;
                }

                if (String.IsNullOrWhiteSpace(NivelUsuarioTextBox.Text))
                {
                    SuperErrorProvider.SetError(NivelUsuarioTextBox, "Este campo no debe estar vacio");
                    NivelUsuarioTextBox.Focus();
                    paso = false;
                }

                if (String.IsNullOrWhiteSpace(UsuarioTextBox.Text))
                {
                    SuperErrorProvider.SetError(UsuarioTextBox, "Este campo no debe estar vacio");
                    UsuarioTextBox.Focus();
                    paso = false;
                }

                if (String.IsNullOrWhiteSpace(ClaveTextBox.Text))
                {
                    SuperErrorProvider.SetError(ClaveTextBox, "Este campo no debe estar vacio");
                    ClaveTextBox.Focus();
                    paso = false;
                }
                


            
            return paso;
        }
        private Usuarios llenarClase()
        {
            Usuarios Usuario = new Usuarios();
            Usuario.UsuarioId = Convert.ToInt32(UsuarioIdnumericUpDown.Value);
            Usuario.Nombres = NombresTextBox.Text.ToString();
            Usuario.Email = EmailTextBox.Text.ToString();
            Usuario.NivelUsuario = NivelUsuarioTextBox.Text.ToString();
            Usuario.Clave = ClaveTextBox.Text.ToString();
            Usuario.Usuario = UsuarioTextBox.Text.ToString();
            Usuario.FechaIngreso = FechaIngresoDateTimePicker.Value;
            return Usuario;
        }
        private void GuardarButton_Click(object sender, EventArgs e)
        {
            Usuarios Usuario;
            bool paso = false;


            if (!Validar())
                return;
            Usuario = llenarClase();
            limpiar();

            if (UsuarioIdnumericUpDown.Value == 0)
            {
                paso = UsuariosBLL.Guardar(Usuario);
            }
            else
            {
                if (!ExisteEnLaBasedeDatos())
                {
                    MessageBox.Show("No se puede modificar una persona que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = UsuariosBLL.Editar(Usuario);
            }
            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
                MessageBox.Show("No se pudo guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ExisteEnLaBasedeDatos()
        {
            Usuarios Usuario = UsuariosBLL.Buscar((int)UsuarioIdnumericUpDown.Value);
            return (Usuario != null);
        }

        private void NuevoButton_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void LlenarCampos(Usuarios usuario)
        {
            UsuarioIdnumericUpDown.Value = usuario.UsuarioId;
            NombresTextBox.Text = usuario.Nombres;
            EmailTextBox.Text = usuario.Email;
            NivelUsuarioTextBox.Text = usuario.NivelUsuario;
            UsuarioTextBox.Text = usuario.Usuario;
            ClaveTextBox.Text = usuario.Clave;
            FechaIngresoDateTimePicker.Value = usuario.FechaIngreso;
        }

        private void BuscarButton_Click(object sender, EventArgs e)
        {
            int id;
            Usuarios usuario = new Usuarios();
            int.TryParse(UsuarioIdnumericUpDown.Value.ToString(), out id);
            limpiar();

            usuario = UsuariosBLL.Buscar(id);

            if (usuario != null)
            {
                MessageBox.Show("Persona Encontrada");
                LlenarCampos(usuario);
            }
            else
                MessageBox.Show("Persona no encontrada");
        }

        private void EliminarButton_Click(object sender, EventArgs e)
        {
            SuperErrorProvider.Clear();
            int id;
            int.TryParse(Convert.ToString(UsuarioIdnumericUpDown.Value), out id);
            limpiar();
            if (UsuariosBLL.Eliminar(id))
                MessageBox.Show("Eliminado");
            else
                SuperErrorProvider.SetError(UsuarioIdnumericUpDown, "No se puede eliminar una persona que no existe");
        }
    }
}
