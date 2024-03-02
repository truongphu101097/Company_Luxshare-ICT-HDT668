using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerryDllFramework
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(400, 230);
        }
        public string i;
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            i = "02";
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            i = "01";
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            i = "03";
            this.Close();
        }
    }
}
