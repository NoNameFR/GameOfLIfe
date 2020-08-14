using System;

namespace GameOfLife
{
    public class GameOfLife
    {
        
        const int size = 100;
        public int Size { get; set; } = size;
        public bool[,] board = new bool[size, size];
        
        public void Init()
        {
            CreateClignotant(0, 1);
            CreateClignotant(4, 4);
            CreateRandomCells(1, 1);
        }
        public void CreateRandomCells(int x, int y)
        {
            

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Random random = new Random();
                    int randomNumber = random.Next(0, 100);
                    if (randomNumber <= 10)
                    {
                        board[i, j] = true;
                    }
                    else
                    {
                        board[i, j] = false;
                    }
                }
            }
        }
        public void ComputeNextTurn()
        {
            bool[,] nextBoard = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    nextBoard[i, j] = board[i, j];
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bool isCellAlive = board[i, j];
                    int numberOfNeighbors = CountNeighbors(i, j);
                    if (isCellAlive)
                    {
                        if (numberOfNeighbors == 2 | numberOfNeighbors == 3)
                        {
                            //Stay alive
                        }
                        else
                        {
                            nextBoard[i, j] = false;
                        }
                    }
                    else
                    {
                        if (numberOfNeighbors == 3)
                        {
                            nextBoard[i, j] = true;
                        }
                    }
                }
            }
             board = nextBoard;
        }

        public int CountNeighbors(int x, int y)
        {
            var count = 0;
            for (int i = -1 + x; i <= 1 + x; i++)
            {
                for (int j = -1 + y; j <= 1 + y; j++)
                {
                    if (CheckIsOnBoard(i, j) && board[i, j])
                    {
                        if (!(i == x && j == y))
                            count++;
                    }
                }
            }
            return count;
            //int count = 0;
            //if (CheckIsOnBoard(x - 1, y - 1) && board[x - 1, y - 1])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x, y - 1) && board[x, y - 1])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x - 1, y) && board[x - 1, y])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x + 1, y - 1) && board[x + 1, y - 1])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x + 1, y) && board[x + 1, y])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x, y + 1) && board[x, y + 1])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x + 1, y + 1) && board[x + 1, y + 1])
            //{
            //    count += 1;
            //}
            //if (CheckIsOnBoard(x - 1, y + 1) && board[x - 1, y + 1])
            //{
            //    count += 1;
            //}
            return count;
        }
        public bool CheckIsOnBoard(int x, int y)
        {
            if (x < 0 | x >= size)
            {
                return false;
            }
            if (y < 0 | y >= size)
            {
                return false;
            }
            return true;
        }
        public void CreateClignotant(int x, int y)
        {

            for (int i = x; i < x + 5; i++)
            {
                for (int j = y; j < y + 5; j++)
                {
                    board[i, j] = false;
                }
            }

            board[x + 1, y + 2] = true;
            board[x + 2, y + 2] = true;
            board[x + 3, y + 2] = true;



            //board[0, 0] = false;
            //board[1, 0] = false;
            //board[2, 0] = false;
            //board[3, 0] = false;
            //board[4, 0] = false;

            //board[0, 1] = false;
            //board[0, 2] = false;
            //board[0, 3] = false;
            //board[0, 4] = false;

            //board[1, 4] = false;
            //board[2, 4] = false;
            //board[3, 0] = false;
            //board[4, 0] = false;

            //board[4, 1] = false;
            //board[4, 2] = false;
            //board[4, 3] = false;
        }
    }
}
