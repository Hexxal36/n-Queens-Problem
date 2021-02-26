using ChessQueens.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessQueens.Services
{
    public class ChessBoardService
    {
        public int Size { get; set; }

        public List<int[]> Answers { get; set; }

        public ChessBoardService(int size)
        {
            this.Size = size;
            Answers = new();
        }

        public void GetMaxQueenBoards(List<int> board, int row, int[] initialQueens, List<int[]> answers)
        {
            if (row == Size)
            {
                answers.Add(board.ToArray());
            }
            else
            {
                if (initialQueens[row] != -1)
                {
                    board.Add(initialQueens[row]);

                    if (IsValid(board))
                    {
                        GetMaxQueenBoards(board, row + 1, initialQueens, answers);
                    }

                    board.RemoveAt(board.Count - 1);
                }
                else
                {
                    for (int i = 0; i < Size; i++)
                    {
                        board.Add(i);

                        if (IsValid(board))
                        {
                            GetMaxQueenBoards(board, row + 1, initialQueens, answers);
                        }

                        board.RemoveAt(board.Count - 1);
                    }
                }
            }
        }



        public bool IsValid(List<int> board)
        {
            if (board.Count == GetQueenCoords(GetDangerMap(board)).Count)
            {
                return true;
            }

            return false;
        }



        public char[,] GetDangerMap(List<int> board)
        {
            var dangerBoard = new char[Size, Size];

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    dangerBoard[i, j] = '-';
                }
            }

            for (int j = 0; j < board.Count; j++)
            {
                var coord = new Coordinate { X = j, Y = board[j] };


                for (int i = 0; i < Size; i++)
                {
                    dangerBoard[coord.X, i] = '~';
                    dangerBoard[i, coord.Y] = '~';

                    if (coord.X + coord.Y - i < Size && coord.X + coord.Y - i >= 0)
                    {
                        dangerBoard[i, coord.X + coord.Y - i] = '~';
                    }

                    if (coord.Y - coord.X + i < Size && coord.Y - coord.X + i >= 0)
                    {
                        dangerBoard[i, coord.Y - coord.X + i] = '~';
                    }
                }

                dangerBoard[coord.X, coord.Y] = 'Q';
            }


            return dangerBoard;
        }

        private static List<Coordinate> GetQueenCoords(char[,] board)
        {
            var QueenCoords = new List<Coordinate>();

            var size = board.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board[i, j] == 'Q')
                    {
                        QueenCoords.Add(new Coordinate() { X = i, Y = j });
                    }
                }
            }

            return QueenCoords;
        }
    }
}
