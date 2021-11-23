using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Mine_Grids_Information
    {
        public int row { set; get; }//行数
        public int column { set; get; }//列数
        //public int mines_Count_Number;
        public bool[,] isMine;//是否有地雷
        public bool[,] isExplored;//是否被探索
        public sbyte[,] number;//显示周围地雷数字

        public Mine_Grids_Information(int row,int column,int mine_Count_number)
        {
            this.row = row;
            this.column = column;
            isMine = new bool[row, column];
            isExplored = new bool[row, column];
            number = new sbyte[row, column];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                {
                    isMine[i, j] = false;
                    isExplored[i, j] = false;
                    number[i, j] = 0;
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
            int rn_i, rn_j, max = row * column;
            for (int mined_number = 0; mined_number < mines_Count_Number && mined_number < max;)
            {
                rn_i = rn.Next(row);
                rn_j = rn.Next(column);
                if (!isMine[rn_i, rn_j])
                {
                    isMine[rn_i, rn_j] = true;
                    for (int i = rn_i - 1; i <= rn_i + 1; i++)
                        for (int j = rn_j - 1; j <= rn_j + 1; j++)
                            if (i >= 0 && i < row && j >= 0 && j < column)
                                number[i, j]++;
                    mined_number++;
                }
            }
        }

    }
}
