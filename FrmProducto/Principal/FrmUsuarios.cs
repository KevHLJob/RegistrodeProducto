using BusinessLogic.Users;//referencia negocio
using Entity.Users;//referencia entity
using System;
using System.Windows.Forms;

namespace FrmProducto.Principal
{

    public partial class FrmUsuarios : Form
    {

        private CLsUserE objUsuarios= null;
        private readonly ClsUserBL objUsuariosBL = new ClsUserBL();
        public FrmUsuarios()
        {
            InitializeComponent();
            //llamada de metodo para cargar en data grid view los usuarios
            CargarListaUsuarios();
        }

        //metodo para caragar usuarios
        private void CargarListaUsuarios()
        {
            objUsuarios = new CLsUserE();
            objUsuariosBL.Index(ref objUsuarios);
            if(objUsuarios.MessageError == null)
            {
                DgvUsuarios.DataSource = objUsuarios.DtResults;
            }
            else
            {
                MessageBox.Show(objUsuarios.MessageError,"Mensaje de error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //evento para agregar usuarios
        private void BtnAgregar_Click(object sender, System.EventArgs e)
        {
            objUsuarios = new CLsUserE()
            {
                Nombre1 = txtNombre.Text,
                Primer_Apellido1 = txtPrimerApellido.Text,
                Segundo_Apellido1 = txtSegundoApellido.Text,
                FechaNacimiento1 = DtpFechaNacimiento.Value,
                Estado1=CbEstado.Checked
            };

            objUsuariosBL.Create(ref objUsuarios);
            if(objUsuarios.MessageError == null)
            {
                MessageBox.Show("El ID " + objUsuarios.ValorScalar1 + ", fue agregado adecuadamente");
                CargarListaUsuarios();
            }
            else
            {
               MessageBox.Show(objUsuarios.MessageError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //evento para actualizar usuarios
        private void BtnActualizar_Click(object sender, System.EventArgs e)
        {
            objUsuarios = new CLsUserE()
            {
                Idusuario1=Convert.ToByte(lbIdusuario.Text),
                Nombre1 = txtNombre.Text,
                Primer_Apellido1 = txtPrimerApellido.Text,
                Segundo_Apellido1 = txtSegundoApellido.Text,
                FechaNacimiento1 = DtpFechaNacimiento.Value,
                Estado1 = CbEstado.Checked
            };
            objUsuariosBL.Update(ref objUsuarios);
            if (objUsuarios.MessageError == null)
            {
                MessageBox.Show("El usuario fue modificado adecuadamente");
                CargarListaUsuarios();
            }
            else
            {
                MessageBox.Show(objUsuarios.MessageError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        //metodo de selección de registro
        private void DgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DgvUsuarios.Columns[e.ColumnIndex].Name == "Editar")
                {
                    objUsuarios = new CLsUserE()
                    {
                        Idusuario1 = Convert.ToByte(DgvUsuarios.Rows[e.RowIndex].Cells["Idusuario"].Value.ToString())
                    };
                    lbIdusuario.Text=objUsuarios.Idusuario1.ToString();

                    objUsuariosBL.Read(ref objUsuarios);
                    txtNombre.Text = objUsuarios.Nombre1;
                    txtPrimerApellido.Text = objUsuarios.Primer_Apellido1;
                    txtSegundoApellido.Text = objUsuarios.Segundo_Apellido1;
                    DtpFechaNacimiento.Value= objUsuarios.FechaNacimiento1;
                    CbEstado.Checked = objUsuarios.Estado1;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //evento de eliminar usuarios
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            objUsuarios = new CLsUserE()
            {
                Idusuario1 = Convert.ToByte(lbIdusuario.Text)
            };
            objUsuariosBL.Delete(ref objUsuarios);
            if (objUsuarios.MessageError == null)
            {
                MessageBox.Show("El usuario fue eliminado adecuadamente");
                CargarListaUsuarios();
            }
            else
            {
                MessageBox.Show(objUsuarios.MessageError, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
