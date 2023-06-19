using System;
using System.Collections.Generic;

namespace ConnectFour {

    internal class Board {

        /* ------------------------------------------------------------------------------
         * ------------------------------ BOARD PROPERTIES ------------------------------
         * ------------------------------------------------------------------------------ */
        private int Rows { get; set; }
        private int Columns { get; set; }
        public string[,] Matrix { get; set; }
        public List<int> ListValidColumnInputs { get; set; }


        /* ------------------------------------------------------------------------------
         * ----------------------------- BOARD CONSTRUCTORS -----------------------------
         * ------------------------------------------------------------------------------ */
        public Board(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Matrix = new string[Rows, Columns];
            ListValidColumnInputs = new List<int>();

            //Initialize Matrix with default "#" (not filled) symbol in all positions
            ResetBoard();
            //Create list of valid inputs allowed based on Columns property
            PopulateListValidColumnInputs();
        }


        /* ------------------------------------------------------------------------------
         * -------------------------------- BOARD METHODS -------------------------------
         * ------------------------------------------------------------------------------ */
        public void ResetBoard() {
        //Reset Board to initial state

            for(int i=0; i<Rows; i++) {
                for(int j=0; j<Columns; j++) {
                    Matrix[i,j] = "#";
                }
            }
        }


        public void PopulateListValidColumnInputs() {
        //get list of values of valid inputs that the user can type.

            for (int i=0; i<Columns; i++) {
                ListValidColumnInputs.Add(i+1);
            }
        }


        public void UpdateValues(int columnSelected, Player currentPlayer) {
        //Update last row (bottom-most) available (value #) with player icon.
            
            for (int i=Rows-1; i>=0; i--) {
                if (Matrix[i, columnSelected-1]=="#") {
                    Matrix[i, columnSelected-1] = currentPlayer.Icon;
                    break;
                }
            }
        }


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


        public bool CheckIfPlayerWon(Player currentPlayer) {
            //Return true if there is a winner

            //check horizontal win
            for (int i=0; i<Rows; i++) {
                for (int j=0; j<Columns-3; j++) {
                    if (Matrix[i,j]==currentPlayer.Icon && Matrix[i,j+1]==currentPlayer.Icon && Matrix[i,j+2]==currentPlayer.Icon && Matrix[i,j+3]==currentPlayer.Icon) {
                        return true;
                    }
                }
            }

            //check vertical win
            for (int i=0; i<Rows-3; i++) {
                for (int j=0; j<Columns; j++) {
                    if (Matrix[i,j]==currentPlayer.Icon && Matrix[i+1,j]==currentPlayer.Icon && Matrix[i+2,j]==currentPlayer.Icon && Matrix[i+3,j]==currentPlayer.Icon) {
                        return true;
                    }
                }
            }

            //check primary diagonal win
            for (int i=0; i<Rows-3; i++) {
                for (int j=0; j<Columns-3; j++) {
                    if (Matrix[i,j]==currentPlayer.Icon && Matrix[i+1,j+1]==currentPlayer.Icon && Matrix[i+2,j+2]==currentPlayer.Icon && Matrix[i+3,j+3]==currentPlayer.Icon) {
                        return true;
                    }
                }
            }

            //check secondary diagonal win
            for (int i=3; i<Rows; i++) {
                for (int j=0; j<Columns-3; j++) {
                    if (Matrix[i,j]==currentPlayer.Icon && Matrix[i-1,j+1]==currentPlayer.Icon && Matrix[i-2,j+2]==currentPlayer.Icon && Matrix[i-3,j+3]==currentPlayer.Icon) {
                        return true;
                    }
                }
            }

            return false;
        }


        public bool CheckIfBoardFull() {
        // check if there is still any position in the board with value "#" (not filled by a player yet).

            for (int i=0; i<Rows;i++) {
                for (int j=0; j<Columns; j++) {
                    if (Matrix[1,j]=="#") {
                        return false;
                    }
                }
            }

            return true;
        }


        public override string ToString() {
            string result = "";
            result += $"Board Number of Rows: {Rows}\nBoard Number of Columns: {Columns}\n\n";
            result += DisplayCurrentState();
            return result;
        }
    }



    internal class Player {

        /* ------------------------------------------------------------------------------
         * ------------------------------ PLAYER PROPERTIES -----------------------------
         * ------------------------------------------------------------------------------ */
        public string Name { get; set; }
        public string Icon { get; set; }


        /* ------------------------------------------------------------------------------
         * ----------------------------- PLAYER CONSTRUCTORS ----------------------------
         * ------------------------------------------------------------------------------ */
        public Player(string name, string icon) {
            Name = name;
            Icon = icon;
        }


        /* ------------------------------------------------------------------------------
         * ------------------------------- PLAYER METHODS -------------------------------
         * ------------------------------------------------------------------------------ */
        public override string ToString() {
            return $"Player Name: {Name}, Player Icon: {Icon}";
        }
    }



    internal class GameController {

        /* ------------------------------------------------------------------------------
         * -------------------------- GAMECONTROLLER PROPERTIES -------------------------
         * ------------------------------------------------------------------------------ */
        private Board MyBoard { get; set; }
        private List<Player> ListPlayers { get; set; }
        private int Turn { get; set; }
        private bool IsGameFinished { get; set; }
        public bool KeepPlaying { get; set; }


        /* ------------------------------------------------------------------------------
         * ------------------------- GAMECONTROLLER CONSTRUCTORS ------------------------
         * ------------------------------------------------------------------------------ */
        public GameController() {
            int rows = 6;
            int columns = 7;
            MyBoard = new Board(rows, columns);
            ListPlayers = new List<Player>(2);
            Turn = 0;
            IsGameFinished = false;
            KeepPlaying = true;
        }


        public GameController(int rows, int columns) {
            MyBoard = new Board(rows, columns);
            ListPlayers = new List<Player>(2);
            Turn = 0;
            IsGameFinished = false;
            KeepPlaying = true;
        }


        /* ------------------------------------------------------------------------------
         * --------------------------- GAMECONTROLLER METHODS ---------------------------
         * ------------------------------------------------------------------------------ */
        public void StartupMessage() {
            Console.WriteLine("Connect 4 Game Development Project:");
            Console.WriteLine(MyBoard);
            Console.WriteLine("Start Game...");
        }


        public void ResetGame() {
        //Reset Properties of GameController.
            MyBoard.ResetBoard();
            ListPlayers.Clear();
            Turn = 0;
            IsGameFinished = false;
        }


        public void AddPlayers() {
        //Add players to list of players.

            Console.WriteLine("Please enter a name for player 1: ");
            string namePlayer0 = Console.ReadLine();
            var Player0 = new Player(namePlayer0, "X");
            ListPlayers.Add(Player0);
            Console.WriteLine(Player0);

            Console.WriteLine("Please enter a name for player 2: ");
            string namePlayer1 = Console.ReadLine();
            var Player1 = new Player(namePlayer1, "O");
            ListPlayers.Add(Player1);
            Console.WriteLine(Player1);
        }


        public void Play() {
        //Game Logic.

            Player currentPlayer;
            int selectedColumn;
            bool isBoardFull;

            do {
                currentPlayer = ListPlayers[Turn%2];

                Console.WriteLine($"Board:\n{MyBoard.DisplayCurrentState()}\n");
                
                //Get a valid play
                selectedColumn = PromptValidColumn(currentPlayer);

                //Change board state with user input
                MyBoard.UpdateValues(selectedColumn, currentPlayer);

                //Check for winner (case: there is a winner)
                IsGameFinished = MyBoard.CheckIfPlayerWon(currentPlayer);
                if (IsGameFinished) {
                    Console.WriteLine($"Board:\n{MyBoard.DisplayCurrentState()}\n");
                    Console.WriteLine($"It is a Connect 4. {currentPlayer.Name} Wins!\n\n");
                }

                //check if board is full (case: draw)
                isBoardFull = MyBoard.CheckIfBoardFull();
                if (isBoardFull) {
                    Console.WriteLine($"Board:\n{MyBoard.DisplayCurrentState()}\n");
                    Console.WriteLine($"The Board is Full. The game ended in a draw!\n\n");
                }

                //Pass turn to next player
                Turn++;

            } while ( (! IsGameFinished) && (! isBoardFull) );

        }


        private int PromptValidColumn(Player currentPlayer) {
        //Get valid input from player (must be a valid column number and column must not be full)

            int selectedColumn;

            do {
                Console.WriteLine($">> [Turn {Turn} | Player {(Turn%2)+1} ] Current Player ({currentPlayer.Icon}) : {currentPlayer.Name}.");
                Console.WriteLine($"Please select a column number: ");



                //TODO: Implement a try/catch later (breaking when input not a number)
                selectedColumn = int.Parse(Console.ReadLine());



                //Invalid input value case
                if (! (MyBoard.ListValidColumnInputs.Contains(selectedColumn)) )
                    Console.WriteLine("[Invalid input] Please select a valid column number.");
                //Column is already full case
                else if ( ! (MyBoard.Matrix[0, selectedColumn-1]=="#") ) {
                    Console.WriteLine("[Invalid input] Column is full! Please select another column.");
                }
            } while ( ! ((MyBoard.ListValidColumnInputs.Contains(selectedColumn)) && (MyBoard.Matrix[0, selectedColumn-1]=="#")) );

            return selectedColumn;
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
