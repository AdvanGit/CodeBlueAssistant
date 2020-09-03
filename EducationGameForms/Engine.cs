using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationGameForms
{
    public class Engine
    {
        public uint CurrentGen { get; private set; }
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;
        

        public Engine(int cols, int rows, int density)
        {
            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];
            Random rnd = new Random();
            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                    field[x, y] = rnd.Next(density) == 0;
        }

        

        private int CountNeighbours(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)

                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + cols) % cols;
                    int row = (y + j + rows) % rows;
                    bool isSelf = col == x && row == y;
                    bool isLife = field[col, row];

                    if (isLife && !isSelf)
                        count++;
                }
            return count;
        }

        private void NextGeneration()
        {
            var newField = new bool[cols, rows];

            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                {
                    int neighborsCount = CountNeighbours(x, y);

                    bool isLife = field[x, y];

                    if (neighborsCount == 3 && !isLife)
                        newField[x, y] = true;
                    else if (isLife && (neighborsCount < 2 || neighborsCount > 3))
                        newField[x, y] = false;
                    else
                        newField[x, y] = field[x, y];
                }
            field = newField;
            CurrentGen++;
        }

        public bool[,] GetCells()
        {
            NextGeneration();
            bool[,] result = field;
            return result;
        }

        private bool CellValidator(int x, int y)
        {
            return x > 0 && y > 0 && x < cols && y < rows;
        }
        private void UpdateCell(int x, int y, bool state)
        {
            if (CellValidator(x, y))
                field[x, y] = state;

        }
               
        public void AddCell(int x, int y)
        {
            UpdateCell(x, y, state: true);
        }

        public void RemoveCell(int x, int y)
        {
            UpdateCell(x, y, state: false);
        }




    }
}
