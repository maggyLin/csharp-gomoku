using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace csharp_gomoku
{
    abstract class Piece : PictureBox
    {
        private static readonly int IMAGE_WIDTH = 50;

        public Piece(int x, int y)
        {
            //使用 System.Drawing.Color 指定背景顏色, 透明
            this.BackColor = Color.Transparent; 
            //產生時位置要以圖片中心,非左上角
            this.Location = new Point(x - IMAGE_WIDTH/2, y - IMAGE_WIDTH / 2);
            this.Size = new Size(IMAGE_WIDTH, IMAGE_WIDTH);
        }

        public abstract PieceType GetPieceType();

    }
}
