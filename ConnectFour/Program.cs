using System;
using System.Collections.Generic;

namespace ConnectFour {

    internal class Board {

        //Board Properties
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string[,] Matrix { get; set; }


        //Board Constructors
        public Board(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Matrix = new string[Rows, Columns];
        }


        //Board Methods
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



    internal class Player {

        //Player Properties
        private string Name { get; set; }
        private string Icon { get; set; }

        //Player Constructors
        public Player(string name, string icon) {
            Name = name;
            Icon = icon;
        }

        //PLayer Methods


        public override string ToString() {
            return $"Player Name: {Name}, Player Icon: {Icon}";
        }
    }



    internal class GameController {

        //GameController Properties
        private int turn = 0;
        private List<Player> listPlayers = new List<Player>(2);

        //GameController constructors
        public GameController() {
        
        }

        //GameController Methods
        public void addPlayers() {
            
            Console.WriteLine("Please enter a name for player 1: ");
            string namePlayer1 = Console.ReadLine();
            var Player1 = new Player(namePlayer1, "X");
            listPlayers.Add(Player1);
            Console.WriteLine(Player1);

            Console.WriteLine("Please enter a name for player 2: ");
            string namePlayer2 = Console.ReadLine();
            var Player2 = new Player(namePlayer2, "O");
            listPlayers.Add(Player2);
            Console.WriteLine(Player2);
            
        }


    }

    internal class Program {
        static void Main(string[] args) {
            
            Console.WriteLine("Connect 4 Game Development Project:");
            var myBoard = new Board(6, 7);

            myBoard.Display();

            Console.WriteLine("Start Game...");

            var myGameController = new GameController();

            myGameController.addPlayers();
            



        }
    }
}
