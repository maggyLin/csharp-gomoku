using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace csharp_gomoku
{
    public partial class Form1 : Form
    {

        private Game game = new Game();
        private bool isEnd = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //this.Controls.Add( new BlackPiece( e.X,e.Y ));

            if (isEnd)
            {
                MessageBox.Show("Game over!");
            }
            else
            {
                Piece piece = game.PlaceAPiece(e.X, e.Y);
                if (piece != null)
                {
                    this.Controls.Add(piece);

                    //check winner
                    if (game.Winner == PieceType.BLACK)
                    {
                        MessageBox.Show("black win!");
                        isEnd = true;
                    }
                    else if (game.Winner == PieceType.WHITE)
                    {
                        MessageBox.Show("white win!");
                        isEnd = true;
                    }
                }
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //確認滑鼠是否靠近節點,修改鼠標樣式
            if( game.CanBePlaced(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
