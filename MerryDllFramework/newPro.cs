using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryDllFramework
{
    public partial class newPro : Form
    {
        private bool flag;
        private int time;

        public newPro(string name, bool flag, int timer)
        {
            InitializeComponent();
            this.Text = name;
            this.flag = flag;
            time = timer;
        }

        public newPro()
        {

        }


        int i;
        private void newPro_Load(object sender, EventArgs e)
        {
            i = 0;
            timer1.Interval = time;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i = i + 5;
            progressBar1.Value = i;
            //如果執行時間超過，則this.DialogResult = DialogResult.No;
            if (i >= 100)
            {
                this.DialogResult = DialogResult.No;
                timer1.Enabled = false;
                this.Close();
            }
        }
    }
}
