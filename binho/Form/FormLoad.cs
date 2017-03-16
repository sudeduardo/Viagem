using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace binho.Form
{
    public partial class FormLoad : System.Windows.Forms.Form
    {
        public FormLoad()
        {
            InitializeComponent();
        }

        private void TimerLoad_Tick(object sender, EventArgs e)
        {
            if (pgbLoad.Value < 100)
            {
                lblLoad.Text = pgbLoad.Value + "%";
                pgbLoad.Value = pgbLoad.Value + 10;
            }
            else if(pgbLoad.Value == 100) {
                lblLoad.Text = pgbLoad.Value + "%";
                frmPrincipal fer = new frmPrincipal();
                fer.Show();
                this.Hide();
                TimerLoad.Enabled = false;
            }

            
        }
    }
}
