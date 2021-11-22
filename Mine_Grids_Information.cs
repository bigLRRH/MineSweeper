using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Mine_Grids_Information
    {
        public int column { set; get; }//列数
        public int row { set; get; }//行数
        //public int mines_Count_Number;
        public bool[,] isMine;//是否有地雷
        public bool[,] isExplored;//是否被探索
        public sbyte[,] number;//显示周围地雷数字

        public Mine_Grids_Information(int column,int row,int mine_Count_number)
        {
            this.column = column;
            this.row = row;
            isMine = new bool[column, row];
            isExplored = new bool[column, row];
            number = new sbyte[column, row];
            for(int x=0;x<column;x++)
                for(int y=0;y<row;y++)
                {
                    isMine[x, y] = false;
                    isExplored[x, y] = false;
                    number[x, y] = 0;
                }
            Lay_Mines(mine_Count_number);
        }

        public void Lay_Mines(int mines_Count_Number)
        {
            /*
                Summary:
                    在mined数组中共生成mines_number个地雷
                    在number数组中计算已埋地雷的数量
                bug：
                    1.随机布雷算法在雷区很大，地雷占雷区比例很高的情况下容易死循环
                    解决方案1：当雷数小于一半的时候埋雷，当雷数大于一半的时候挖雷
                    解决方案2：洗牌算法
             */

            Random rn = new Random();
            int x1, y1, max = column * row;
            for (int mined_number = 0; mined_number < mines_Count_Number && mined_number < max;)
            {
                x1 = rn.Next(column);
                y1 = rn.Next(row);
                if (!isMine[x1, y1])
                {
                    isMine[x1, y1] = true;
                    for (int i = x1 - 1; i <= x1 + 1; i++)
                        for (int j = y1 - 1; j <= y1 + 1; j++)
                            if (i >= 0 && i < column && j >= 0 && j < row)
                                number[i, j]++;
                    mined_number++;
                }
            }
        }

    }
}
