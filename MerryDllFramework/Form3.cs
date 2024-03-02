using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlX.XDevAPI;

namespace MerryDllFramework
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }
        Form2 form2 = new Form2();
        public string aa;
        private void Form3_Load(object sender, EventArgs e)
        {
            new1();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        public string  new1()
        {
            if (aa== "02")
            {
                label1.Text = "当前型号为:HDT668-012\n电池型号为:02–ON SEMI AEC\nHeadSetPIDVID:058B 03F0";
            }else if (aa == "01")
            {
                label1.Text = "当前型号为:HDT668-013\n电池型号为:01-ON SEMI EPW\nHeadSetPIDVID:058B 03F0";     
            }     
            return aa;
        }      
        private void label1_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
