using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_gomoku
{
    class Board
    {
        public static readonly int NODE_COUNT = 9; //棋盤大小9*9

        // 沒有符合棋盤任何一個交叉點
        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        private static readonly int OFFSET = 75;  //棋盤邊框
        private static readonly int NODE_RADIUS = 10;  //自定義節點半徑
        private static readonly int NODE_DISTRANCE = 75; //節點與節點距離

        private Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT]; //存放已經有棋子的位置

        private Point lastPlaceNode = NO_MATCH_NODE;  //最後下子位置
        public Point LastPlaceNode { get { return lastPlaceNode; } }

        public bool CanBePlaced(int x, int y)
        {
            // 找出最近節點(Node)
            Point nodeId = FindTheClosetNode(x, y);

            // 如果沒有回傳false
            if (nodeId == NO_MATCH_NODE) return false;

            // 如果有，檢查是否已經有棋子
            if (pieces[nodeId.X, nodeId.Y] != null) return false;

            return true;
        }

        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            // 找出最近節點(Node)
            Point nodeId = FindTheClosetNode(x, y);

            // 如果沒有回傳false
            if (nodeId == NO_MATCH_NODE) return null;

            // 如果有，檢查是否已經有棋子
            if(pieces[nodeId.X,nodeId.Y]!=null) return null;

            // 產生對應的棋子
            //將座標調整到正確交叉點位置 (滑鼠點擊時可能會偏移)
            Point formPos = ConverToFormPosition(nodeId);
            if (type == PieceType.BLACK)
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y);
            else if(type == PieceType.WHITE)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y);

            //紀錄最後下子位置
            lastPlaceNode = nodeId;

            return pieces[nodeId.X, nodeId.Y];
        }

        //將座標調整到正確交叉點位置 (滑鼠點擊時可能會偏移)
        private Point ConverToFormPosition(Point nodeId)
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTRANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTRANCE + OFFSET;

            return formPosition;
        }

        private Point FindTheClosetNode(int x, int y)
        {
            // 找出最近節點(Node)

            int nodeIdX = FindTheClosetPos(x);
            if (nodeIdX == -1 || nodeIdX >= NODE_COUNT) return NO_MATCH_NODE;

            int nodeIdY = FindTheClosetPos(y);
            if (nodeIdY == -1 || nodeIdY >= NODE_COUNT) return NO_MATCH_NODE;

            return new Point(nodeIdX, nodeIdY);
        }

        private int FindTheClosetPos(int pos)
        {
            if (pos < (OFFSET - NODE_RADIUS))
                return -1;

            pos -= OFFSET;

            //位置 / 每格子距離 = 找到左邊x位置
            int quotient = pos / NODE_DISTRANCE;
            //距離左邊x差距
            int remainder = pos % NODE_DISTRANCE;

            if (remainder <= NODE_RADIUS) //靠進左邊節點半徑內
                return quotient;
            else if (remainder >= (NODE_DISTRANCE - NODE_RADIUS)) //靠進右邊節點半徑內
                return quotient + 1;
            else 
                return -1;  //不在任何節點

        }

        public PieceType GetPieceType(int nodeIdX, int nodeIdY)
        {
            if (pieces[nodeIdX, nodeIdY] == null) return PieceType.NONE;
            else return pieces[nodeIdX, nodeIdY].GetPieceType();
        }


    }
}
