using BusinessLogic.Users;
using Entity.Users;
using System.Windows.Forms;

namespace FrmProducto.Principal
{
    public partial class FrmUsuarios : Form
    {
       private CLsUserE objUsuario = null;
        private readonly ClsUserBL objUsuarioBL = new ClsUserBL();
        public FrmUsuarios()
        {
            InitializeComponent();
        }
    }
}
