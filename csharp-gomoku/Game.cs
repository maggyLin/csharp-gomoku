using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_gomoku
{
    class Game
    {
        private PieceType currentPlayer = PieceType.BLACK;
        private Board board = new Board();
        private PieceType winner = PieceType.NONE;
        public PieceType Winner { get { return winner; } }

        public bool CanBePlaced(int x,int y)
        {
            return board.CanBePlaced(x, y);
        }

        public Piece PlaceAPiece(int x, int y)
        {

            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                // check is win?
                CheckWinner();

                // change player
                if (currentPlayer == PieceType.BLACK) currentPlayer = PieceType.WHITE;
                else currentPlayer = PieceType.BLACK;

                return piece;
            }
            else return null;
        }

        private void CheckWinner()
        {
            //取得最後下子位置,以此為中心,向8個方位檢查
            int centerX = board.LastPlaceNode.X;
            int centerY = board.LastPlaceNode.Y;

            // 檢查8個方向
            for (int xDir = -1; xDir <= 1; xDir++)
            {
                for(int yDir = -1; yDir <= 1; yDir++)
                {
                    //排除中心點,就是下子的棋子位置
                    if (xDir == 0 && yDir == 0) continue;

                    //check 相同棋子數量
                    int count = 1;
                    int oppositeCount = 0; //反方向

                    while (count < 5)
                    {
                        int targetX = centerX + xDir * count;
                        int targetY = centerY + yDir * count;
                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                            targetY < 0 || targetY >= Board.NODE_COUNT ||
                            board.GetPieceType(targetX, targetY) != currentPlayer) break;

                        count++;
                    }

                    if (count >= 5)  //某一方向有5子相同顏色
                    {
                        winner = currentPlayer;
                        break;
                    }
                    else{
                        //檢查反方向有沒有相同顏色的子
                        while (oppositeCount < 5)
                        {
                            int targetX = centerX + ( xDir * (oppositeCount + 1) * -1 );
                            int targetY = centerY + ( yDir * (oppositeCount + 1) * -1 );
                            if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                                targetY < 0 || targetY >= Board.NODE_COUNT ||
                                board.GetPieceType(targetX, targetY) != currentPlayer) break;

                            oppositeCount++;

                            if (count + oppositeCount >= 5)
                            {
                                winner = currentPlayer;
                                break;
                            }

                        }

                    }


                }
            }

        }

    }
}
