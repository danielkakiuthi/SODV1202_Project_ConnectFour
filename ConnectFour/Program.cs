using System;

namespace ConnectFour {

    internal class Board {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string[,] Matrix { get; set; }

        public Board(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Matrix = new string[Rows, Columns];
        }

        public void Display() {
        //Displays current state of the board    
            
            string result = "\n";
            
            //Display Board
            for(int i=1; i<=Rows; i++) {
                result += "| ";
                for(int j=1; j<=Columns; j++) {
                    result += "# ";
                }
                result += "|\n";
            }

            //Display column numbers below board
            result += "  ";
            for(int i=1; i<=Columns; i++) {
                result += $"{i} ";
            }

            //print board
            Console.WriteLine(result);
        }
    }



    internal class Program {
        static void Main(string[] args) {
            
            Console.WriteLine("Connect 4 Game Development Project:");
            var myBoard = new Board(6, 7);

            myBoard.Display();

        }
    }
}
