﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectFour {

    internal class Board {

        /* ------------------------------------------------------------------------------
         * ------------------------------ BOARD PROPERTIES ------------------------------
         * ------------------------------------------------------------------------------ */
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public string[,] Matrix { get; private set; }
        public List<int> ListValidColumnInputs { get; private set; }
        public bool IsBoardFull { get; private set; }


        /* ------------------------------------------------------------------------------
         * ----------------------------- BOARD CONSTRUCTORS -----------------------------
         * ------------------------------------------------------------------------------ */
        public Board(int rows, int columns) {
            Rows = rows;
            Columns = columns;
            Matrix = new string[Rows, Columns];
            ListValidColumnInputs = new List<int>();
            IsBoardFull = false;

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

            //check primary diagonal win (top-left to bottom-right)
            for (int i=0; i<Rows-3; i++) {
                for (int j=0; j<Columns-3; j++) {
                    if (Matrix[i,j]==currentPlayer.Icon && Matrix[i+1,j+1]==currentPlayer.Icon && Matrix[i+2,j+2]==currentPlayer.Icon && Matrix[i+3,j+3]==currentPlayer.Icon) {
                        return true;
                    }
                }
            }

            //check secondary diagonal win (bottom-left to top-right)
            for (int i=3; i<Rows; i++) {
                for (int j=0; j<Columns-3; j++) {
                    if (Matrix[i,j]==currentPlayer.Icon && Matrix[i-1,j+1]==currentPlayer.Icon && Matrix[i-2,j+2]==currentPlayer.Icon && Matrix[i-3,j+3]==currentPlayer.Icon) {
                        return true;
                    }
                }
            }

            return false;
        }


        public void CheckIfBoardFull() {
        // check if there is still any position in the board with value "#" (not filled by a player yet).
            for (int i=0; i<Rows;i++) {
                for (int j=0; j<Columns; j++) {
                    if (Matrix[i,j]=="#") {
                        IsBoardFull = false;
                        return;
                    }
                }
            }
            IsBoardFull = true;
        }


        public bool ValidateSelectedColumn(int selectedColumn) {
        //Check Case: Column is already full.
            if ( ! ListValidColumnInputs.Contains(selectedColumn)) {
                throw new ColumnOutOfRangeException();
            }
            else if (Matrix[0, selectedColumn-1]!="#") {
                throw new ColumnAlreadyFullException();
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



    internal abstract class Player : IComparable<Player> {

        /* ------------------------------------------------------------------------------
         * ------------------------------ PLAYER PROPERTIES -----------------------------
         * ------------------------------------------------------------------------------ */
        public string Name { get; protected set; }
        public string Icon { get; protected set; }
        public int ScoreMatches { get; protected set; }


        /* ------------------------------------------------------------------------------
         * ----------------------------- PLAYER CONSTRUCTORS ----------------------------
         * ------------------------------------------------------------------------------ */
        public Player(string name, string icon) {
            Name = name;
            Icon = icon;
            ScoreMatches = 0;
        }


        /* ------------------------------------------------------------------------------
         * ------------------------------- PLAYER METHODS -------------------------------
         * ------------------------------------------------------------------------------ */
        public virtual void IncreaseScore() {
        //Increase Player score by 1.
            ScoreMatches++;
        }

        public abstract int MakeAMove(List<int> ListValidInputs);

        public int CompareTo(Player other) {
        //Compare Player objects by their ScoreMatches property.
            return ScoreMatches.CompareTo(other.ScoreMatches);
        }

        
        public override string ToString() {
            return $"Player Name: {Name}, Player Icon: {Icon}";
        }
    }


    internal class HumanPlayer : Player {

        /* ------------------------------------------------------------------------------
         * --------------------------- HUMAN PLAYER PROPERTIES --------------------------
         * ------------------------------------------------------------------------------ */



        /* ------------------------------------------------------------------------------
         * --------------------------- HUMAN PLAYER CONSTRUCTORS ------------------------
         * ------------------------------------------------------------------------------ */
        public HumanPlayer(string name, string icon) : base(name, icon) {

        }


        /* ------------------------------------------------------------------------------
         * ----------------------------- HUMAN PLAYER METHODS ---------------------------
         * ------------------------------------------------------------------------------ */
        public override int MakeAMove(List<int> ListValidInputs) {
            return int.Parse(Console.ReadLine());
        }


        public override string ToString() {
            return $"Human Player Name: {Name}, Player Icon: {Icon}";
        }
    }



    internal class ComputerPlayer : Player {

        /* ------------------------------------------------------------------------------
         * ----------------------COMPUTER PLAYER PROPERTIES -----------------------------
         * ------------------------------------------------------------------------------ */



        /* ------------------------------------------------------------------------------
         * -------------------- COMPUTER PLAYER CONSTRUCTORS ----------------------------
         * ------------------------------------------------------------------------------ */
        public ComputerPlayer(string name, string icon) : base(name, icon) {

        }


        /* ------------------------------------------------------------------------------
         * ---------------------- COMPUTER PLAYER METHODS -------------------------------
         * ------------------------------------------------------------------------------ */
        public override void IncreaseScore() {
        //Increase Computer Player score by 10 if it wins.
            ScoreMatches = ScoreMatches + 10;
        }

        public override int MakeAMove(List<int> ListValidInputs) {
            Random r = new Random();

            return r.Next(ListValidInputs.Min(), ListValidInputs.Max()+1);
        }


        public override string ToString() {
            return $"Human Player Name: {Name}, Player Icon: {Icon}";
        }
    }



    internal class GameController {

        /* ------------------------------------------------------------------------------
         * -------------------------- GAMECONTROLLER PROPERTIES -------------------------
         * ------------------------------------------------------------------------------ */
        public Board MyBoard { get; private set; }
        public List<Player> ListPlayers { get; private set; }
        public string GameMode { get; private set; }
        public int WhoPlaysFirst { get; set; }
        public int TurnCounter { get; private set; }
        public int MatchCounter { get; private set; }
        public bool IsMatchFinished { get; private set; }
        public bool IsGameFinished { get; private set; }


        /* ------------------------------------------------------------------------------
         * ------------------------- GAMECONTROLLER CONSTRUCTORS ------------------------
         * ------------------------------------------------------------------------------ */
        public GameController() {
            int rows = 6;
            int columns = 7;
            MyBoard = new Board(rows, columns);
            ListPlayers = new List<Player>(2);
            TurnCounter = 1;
            MatchCounter = 1;
            IsMatchFinished = false;
            IsGameFinished = false;

            MyBoard.ResetBoard();
        }


        public GameController(int rows, int columns) {
            MyBoard = new Board(rows, columns);
            ListPlayers = new List<Player>(2);
            TurnCounter = 1;
            MatchCounter = 1;
            IsMatchFinished = false;
            IsGameFinished = false;

            MyBoard.ResetBoard();
        }


        /* ------------------------------------------------------------------------------
         * --------------------------- GAMECONTROLLER METHODS ---------------------------
         * ------------------------------------------------------------------------------ */
        private int PromptValidColumn(Player currentPlayer) {
        //Get valid input from player (must be a valid column number and column must not be full)

            int selectedColumn = -1;
            bool isValidSelectedColumn = false;

            do {
                Console.WriteLine($">> [ Match {MatchCounter} | Turn {TurnCounter} | Player {((WhoPlaysFirst+TurnCounter+1)%2)+1} ] Current Player ({currentPlayer.Icon}) : {currentPlayer.Name}.");
                Console.WriteLine($"Please select a column number: ");

                try {
                    selectedColumn = currentPlayer.MakeAMove(MyBoard.ListValidColumnInputs);
                    isValidSelectedColumn = MyBoard.ValidateSelectedColumn(selectedColumn);
                }
                catch (FormatException e) {
                    Console.WriteLine($"[Invalid Text Input] Please select a valid column number ({MyBoard.ListValidColumnInputs.Min()}..{MyBoard.ListValidColumnInputs.Max()}). Error: {e.Message}");
                }
                catch (ColumnOutOfRangeException e) {
                    Console.WriteLine($"[Invalid Column Selection] Please select a valid column number ({MyBoard.ListValidColumnInputs.Min()}..{MyBoard.ListValidColumnInputs.Max()}). Error: {e.Message}");
                }
                catch (ColumnAlreadyFullException e) {
                    Console.WriteLine($"[Invalid Column Selection] Column is full! Please select another column. Error: {e.Message}");
                }
                catch (Exception e) {
                    Console.WriteLine($"Error: {e.Message}");
                }
                
            } while ( ! isValidSelectedColumn);

            return selectedColumn;
        }
        

        private void DisplayPlayersScores() {

            string result = "";
            for (int i=0; i<ListPlayers.Count; i++) {
                result += $"[ Player {i+1} | {ListPlayers[i].Icon} | {ListPlayers[i].Name} ]: {ListPlayers[i].ScoreMatches} matches won.\n";
            }
            Console.WriteLine(result);
        }


        public void StartupMessage() {
        //Initial message to display at start of the game.
            Console.WriteLine("Connect 4 Game Development Project:");
            Console.WriteLine(MyBoard);
            Console.WriteLine("Start Game...");
        }


        public void ResetMatch() {
        //Reset Properties of GameController.
            MyBoard.ResetBoard();
            TurnCounter = 1;
            IsMatchFinished = false;
            MyBoard.CheckIfBoardFull();
        }


        public void PromptGameMode() {
        //Ask user for Game Mode (Player vs Player | Player vs Computer) 

            //keep asking until correct value is provided
            do {
                Console.WriteLine("\nSelect Game Mode:\nPlayer vs. Player (1)\nPlayer vs. Computer (2):");
                GameMode = Console.ReadLine();

                Console.Clear();
            } while ( ! (GameMode=="1" || GameMode=="2"));
        }
        

        public void AddPlayers() {
        //Add players to list of players.

            //Case Player vs Player
            if (GameMode == "1") {
                Console.WriteLine("Please enter a name for player 1: ");
                string namePlayer0 = Console.ReadLine();
                Player Player0 = new HumanPlayer(namePlayer0, "X");
                ListPlayers.Add(Player0);
                Console.WriteLine(Player0);

                Console.WriteLine("Please enter a name for player 2: ");
                string namePlayer1 = Console.ReadLine();
                Player Player1 = new HumanPlayer(namePlayer1, "O");
                ListPlayers.Add(Player1);
                Console.WriteLine(Player1);
            }
            //Case Player vs Computer
            else if (GameMode == "2") {
                Console.WriteLine("Please enter a name for player 1: ");
                string namePlayer0 = Console.ReadLine();
                Player Player0 = new HumanPlayer(namePlayer0, "X");
                ListPlayers.Add(Player0);
                Console.WriteLine(Player0);

                
                Player Player1 = new ComputerPlayer("Computer#1", "O");
                ListPlayers.Add(Player1);
                Console.WriteLine(Player1);
            }
            
            Console.Clear();
        }


        public void RandomlySelectWhoPlaysFirst() {
        //Randomly select at beginning of match who goes first in the Match (Player[0] or Player[1]).
            Random r = new Random();
            WhoPlaysFirst =  r.Next(0,2);
            Console.WriteLine($"It was randomly decided that Player {WhoPlaysFirst+1} will start!");
        }


        public void PlayMatch() {
        //Match Logic.

            Player currentPlayer;
            int selectedColumn;

            do {
                currentPlayer = ListPlayers[(WhoPlaysFirst+TurnCounter+1)%2];

                DisplayPlayersScores();

                Console.WriteLine($"Board:\n{MyBoard.DisplayCurrentState()}\n");
                
                //Get a valid play
                selectedColumn = PromptValidColumn(currentPlayer);

                Console.Clear();

                //Change board state with user input
                MyBoard.UpdateValues(selectedColumn, currentPlayer);

                //Check for winner (case: there is a winner)
                IsMatchFinished = MyBoard.CheckIfPlayerWon(currentPlayer);
                if (IsMatchFinished) {
                    currentPlayer.IncreaseScore();
                    DisplayPlayersScores();
                    Console.WriteLine($"Board:\n{MyBoard.DisplayCurrentState()}\n");
                    Console.WriteLine($"It is a Connect 4. {currentPlayer.Name} Wins!\n\n");
                    MatchCounter++;
                }

                //check if board is full (case: draw)
                MyBoard.CheckIfBoardFull();
                if (MyBoard.IsBoardFull) {
                    DisplayPlayersScores();
                    Console.WriteLine($"Board:\n{MyBoard.DisplayCurrentState()}\n");
                    Console.WriteLine($"The Board is Full. The match ended in a draw!\n\n");
                    MatchCounter++;
                }

                //Pass turn to next player
                TurnCounter++;

            } while ( (! IsMatchFinished) && (! MyBoard.IsBoardFull) );

        }



        public void PromptKeepPlaying() {
        //Ask Player if game will be played again after ending a match.

            string input;

            //keep asking until correct value is provided
            do {
                Console.WriteLine("Restart? Yes(1) No(0):");
                input = Console.ReadLine();

                Console.Clear();
            } while ( ! (input=="1" || input=="0"));
            
            if (input=="1") {
                IsGameFinished = false;
            }
            else if (input=="0") {
                IsGameFinished= true;
            }
        }


        public void FinalScoreMessage() {
        //Final message to display at and of the game with total score of players.
            string result = "";
            result += $"Final Score of the matches:\n";

            for (int i=0; i<ListPlayers.Count; i++) {
                result += $"Player {i+1} ({ListPlayers[i].Name}): {ListPlayers[i].ScoreMatches} matches won.\n";
            }

            result += "-----------------------------------------------------------------------------\n";

            if (ListPlayers[0].CompareTo(ListPlayers[1]) == 1) {
                result += $"CONGRATULATIONS {ListPlayers[0].Name.ToUpper()}, YOU HAVE WON THE GAME!!!\n";
            }
            else if (ListPlayers[0].CompareTo(ListPlayers[1]) == -1) {
                result += $"CONGRATULATIONS {ListPlayers[1].Name.ToUpper()}, YOU HAVE WON THE GAME!!!\n";
            }
            else {
                result += $"THE GAME ENDED IN A DRAW!!!\n";
            }

            result += "-----------------------------------------------------------------------------\n";

            result += $"\nHave a nice day! =)\n\n\n\n";

            Console.WriteLine(result);
        }

    }



    internal class ColumnAlreadyFullException : ApplicationException {
        public ColumnAlreadyFullException() {
        }
    }



    internal class ColumnOutOfRangeException : ApplicationException {
        public ColumnOutOfRangeException() {

        }
    }



    internal class Program {
        static void Main(string[] args) {

            var myGameController = new GameController();

            myGameController.StartupMessage();
            myGameController.PromptGameMode();
            myGameController.AddPlayers();


            while ( ! myGameController.IsGameFinished) {
                myGameController.RandomlySelectWhoPlaysFirst();
                myGameController.ResetMatch();
                myGameController.PlayMatch();
                myGameController.PromptKeepPlaying();
            }

            myGameController.FinalScoreMessage();
        }
    }
}