using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Captcha
{
    public partial class CambiarDatosPortal : Form
    {
        public CambiarDatosPortal()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(650, 50);
        }

        private void CmdCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
