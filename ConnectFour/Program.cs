using System;
using System.Collections.Generic;

namespace ConnectFour {

    internal class Board {

        //Board Properties
        private int Rows { get; set; }
        private int Columns { get; set; }
        private string[,] Matrix { get; set; }


        //Board Constructors
        public Board(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Matrix = new string[Rows, Columns];

            //Initialize Matrix with default "#" (not filled) symbol in all positions
            for (int i=0; i<Rows; i++) {
                for (int j=0; j<Columns; j++) {
                    Matrix[i,j] = "#";
                }
            }
        }


        //Board Methods
        public string DisplayCurrentState() {
        //Displays current state of the board    
            
            string result = "";
            
            //Display Board
            for(int i=0; i<Rows; i++) {
                result += "| ";
                for(int j=0; j<Columns; j++) {
                    result += $"{Matrix[i,j]} ";
                }
                result += "|\n";
            }

            //Display column numbers below board
            result += "  ";
            for(int i=0; i<Columns; i++) {
                result += $"{i+1} ";
            }

            return result;
        }


        public override string ToString() {
            string result = "";
            result += $"Board Number of Rows: {Rows}\nBoard Number of Columns: {Columns}\n\n";
            
            result += DisplayCurrentState();
            return result;
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
        private int Turn { get; set; }
        public bool KeepPlaying { get; set; }
        private List<Player> ListPlayers { get; set; }
        private Board myBoard { get; set; }


        //GameController constructors
        public GameController() {
            Turn = 0;
            KeepPlaying = true;
            ListPlayers = new List<Player>(2);
            myBoard = new Board(6, 7);
        }


        //GameController Methods
        public void StartupMessage() {
            Console.WriteLine("Connect 4 Game Development Project:");
            Console.WriteLine(myBoard);
            Console.WriteLine("Start Game...");
        }


        public void ResetGame() {
        //Reset Properties of GameController.

            Turn = 0;
            ListPlayers.Clear();
        }


        public void AddPlayers() {
        //Add players to list of players.
            
            Console.WriteLine("Please enter a name for player 1: ");
            string namePlayer1 = Console.ReadLine();
            var Player1 = new Player(namePlayer1, "X");
            ListPlayers.Add(Player1);
            Console.WriteLine(Player1);

            Console.WriteLine("Please enter a name for player 2: ");
            string namePlayer2 = Console.ReadLine();
            var Player2 = new Player(namePlayer2, "O");
            ListPlayers.Add(Player2);
            Console.WriteLine(Player2);
        }


        public void Play() {
        //Game Logic.


        }


        public void PromptKeepPlaying() {
        //Ask Player if game will be played again after ending a game.
            
            string input;
            
            //keep asking until correct value is provided
            do {
                Console.WriteLine("Restart? Yes(1) No(0):");
                input = Console.ReadLine();
            } while ( ! (input=="1" || input=="0"));
            
            if (input=="1") {
                KeepPlaying = true;
            }
            else if (input=="0") {
                KeepPlaying= false;
            }
        }
        


    }



    internal class Program {
        static void Main(string[] args) {
 
            var myGameController = new GameController();
 
            myGameController.StartupMessage();

            while (myGameController.KeepPlaying) {
                myGameController.ResetGame();
                myGameController.AddPlayers();
                myGameController.Play();
                myGameController.PromptKeepPlaying();
            }

        }
    }
}
