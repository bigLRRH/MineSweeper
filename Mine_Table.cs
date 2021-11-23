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
            //TableLayout设置
            Controls.Clear();
            RowCount = mine_Grids_Information.row;
            ColumnCount = mine_Grids_Information.column;
            if (ColumnCount <= RowCount)
                Width = Height * ColumnCount / RowCount;
            else
                Height = Width * RowCount / ColumnCount;
            ColumnStyles.Clear();
            RowStyles.Clear();
            for (int i = 0; i < RowCount; i++)
                RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            for (int j = 0; j < ColumnCount; j++)
                ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            //生成地雷Mine_Grid控件
            mine_Grids = new Mine_Grid[RowCount, ColumnCount];
            for (int i = 0; i < RowCount; i++)
                for (int j = 0; j < ColumnCount; j++)
                {
                    Mine_Grid mine_Grid = new Mine_Grid(i, j);
                    mine_Grids[i, j] = mine_Grid;
                    mine_Grids[i, j].MouseDown += new MouseEventHandler(Mine_Grid_MouseDown);
                    Controls.Add(mine_Grids[i, j]);
                }
            //开局随机洪水
            Random rn = new Random();
            int x = rn.Next(RowCount);
            int y = rn.Next(ColumnCount);
            while (mine_Grids_Information.number[x, y] > 0)
            {
                x = rn.Next(RowCount);
                y = rn.Next(ColumnCount);
            }
            Flood_Fill(mine_Grids[x, y]);
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
                            if (x >= 0 && x < RowCount && y >= 0 && y < ColumnCount)
                            {
                                Flood_Fill(mine_Grids[x, y]);
                            }
                }
            }
        }
    }
}
