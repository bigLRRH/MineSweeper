using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MainForm : Form
    {
        Mine_Grids_Information mine_Grids_Information;
        Mine_Table mine_Table;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mine_Grids_Information = new Mine_Grids_Information(10, 20, 10);
            mine_Table_main.Init(mine_Grids_Information);
        }

        private void button_Face_Click(object sender, EventArgs e)
        {
            mine_Grids_Information = new Mine_Grids_Information(10, 20, 10);
            mine_Table_main.Init(mine_Grids_Information);
        }

    }
}
