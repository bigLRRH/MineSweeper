using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Mine_Grid : Button
    {
        //纯改个名字^_^
        public int X { set; get; }
        public int Y { set; get; }
        public Mine_Grid(int X, int Y) : base()
        {
            this.X = X;
            this.Y = Y;
            BackColor = Color.LightGray;
            Font = new Font("Microsoft YaHei", 10f, FontStyle.Bold);
            Dock = DockStyle.Fill;
        }

        public void Set_ForeColor(sbyte number)
        {
            switch(number)
            {
                case 0:
                    break;
                case 1:
                    ForeColor = Color.Blue;
                    break;
                case 2:
                    ForeColor = Color.Green;
                    break;
                case 3:
                    ForeColor = Color.Red;
                    break;
                case 4:
                    ForeColor = Color.DarkBlue;
                    break;
                case 5:
                    ForeColor = Color.DarkRed;
                    break;
                case 6:
                    ForeColor = Color.DarkSeaGreen;
                    break;
                case 7:
                    ForeColor = Color.Black;
                    break;
                case 8:
                    ForeColor = Color.SlateGray;
                    break;
                default:
                    break;
            }
        }
    }
}
