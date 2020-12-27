using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtmTerminal
{
    public partial class Form2 : Form
    {
        public string card2;
        public Form2(string cardNumber)
        {
            InitializeComponent();

            comboBox1.Items.Add("1111 1111 1111 1111");
            comboBox1.Items.Add("4444 4444 4444 4444");
            comboBox1.Items.Add("2222 2222 2222 2222");
            comboBox1.Items.Add("3333 3333 3333 3333");

            comboBox1.Items.Remove(cardNumber);

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                card2 = comboBox1.Text;
                this.Visible = false;
            }
        }
    }
}
