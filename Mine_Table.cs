using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Mine_Table : TableLayoutPanel
    {
        private Mine_Grids_Information mine_Grids_Information;
        private Mine_Grid[,] mine_Grids;
        public void Init(Mine_Grids_Information mine_Grids_Information)
        {
            this.mine_Grids_Information = mine_Grids_Information;
            set_Table_row_And_conlum();
            Generate_Mines();
            Random_Flood();
        }
        //TableLayout行列设置
        private void set_Table_row_And_conlum()
        {
            ColumnCount = mine_Grids_Information.column;
            RowCount = mine_Grids_Information.row;
            ColumnStyles.Clear();
            RowStyles.Clear();
            for (int x = 0; x < ColumnCount; x++)
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            for (int y = 0; y < RowCount; y++)
                RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
        }
        //生成地雷Mine_Grid控件
        private void Generate_Mines()
        {
            mine_Grids = new Mine_Grid[mine_Grids_Information.column, mine_Grids_Information.row];
            for (int x = 0; x < mine_Grids_Information.column; x++)
                for (int y = 0; y < mine_Grids_Information.row; y++)
                {
                    Mine_Grid mine_Grid = new Mine_Grid(x, y);
                    mine_Grids[x, y] = mine_Grid;
                    mine_Grids[x, y].MouseDown += new MouseEventHandler(Mine_Grid_MouseDown);
                    Controls.Add(mine_Grids[x, y]);
                }
        }
        //Mine_Grid_MouseDown事件
        private void Mine_Grid_MouseDown(object sender, MouseEventArgs e)
        {
            Mine_Grid mine_Grid = sender as Mine_Grid;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!mine_Grids_Information.isExplored[mine_Grid.X, mine_Grid.Y])
                    {
                        if (mine_Grids_Information.isMine[mine_Grid.X, mine_Grid.Y])
                        {
                            mine_Grids_Information.isExplored[mine_Grid.X, mine_Grid.Y] = true;
                            mine_Grid.BackColor = Color.Red;
                            mine_Grid.Text = "地雷";
                        }
                        else
                            Flood_Fill(mine_Grid);
                    }
                    break;
                case MouseButtons.Right:
                    if (!mine_Grids_Information.isExplored[mine_Grid.X, mine_Grid.Y])
                        switch (mine_Grid.Text)
                        {
                            case "🚩":
                                mine_Grid.Text = "?";
                                break;
                            case "?":
                                mine_Grid.Text = "";
                                break;
                            default:
                                mine_Grid.Text = "🚩";
                                break;
                        }
                    break;
                default:
                    break;
            }
        }
        //泛水算法
        private void Flood_Fill(Mine_Grid mine_Grid)
        {
            if (!mine_Grids_Information.isExplored[mine_Grid.X, mine_Grid.Y])
            {
                mine_Grids_Information.isExplored[mine_Grid.X, mine_Grid.Y] = true;
                mine_Grid.Set_ForeColor(mine_Grids_Information.number[mine_Grid.X, mine_Grid.Y]);
                mine_Grid.Text = mine_Grids_Information.number[mine_Grid.X, mine_Grid.Y].ToString();
                if (mine_Grids_Information.number[mine_Grid.X, mine_Grid.Y] == 0)
                {
                    for (int x = mine_Grid.X - 1; x <= mine_Grid.X + 1; x++)
                        for (int y = mine_Grid.Y - 1; y <= mine_Grid.Y + 1; y++)
                            if (x >= 0 && x < mine_Grids_Information.column && y >= 0 && y < mine_Grids_Information.row)
                            {
                                Flood_Fill(mine_Grids[x, y]);
                            }
                }
            }
        }
        //开局随机洪水
        private void Random_Flood()
        {
            Random rn = new Random();
            int x = rn.Next(mine_Grids_Information.column);
            int y = rn.Next(mine_Grids_Information.row);
            while(mine_Grids_Information.number[x,y]>0)
            {
                x = rn.Next(mine_Grids_Information.column);
                y = rn.Next(mine_Grids_Information.row);
            }
            Flood_Fill(mine_Grids[x, y]);
        }
    }
}
