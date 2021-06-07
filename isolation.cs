using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        // declaring global parameters 

        static bool[,] board; // false if tile removed

        // player names
        static string playerA;
        static string playerB;
        
        // player starting positions
        static string startA; 
        static string startB;
        
        // # of rows/columns of gameboard
        static int boardRows; 
        static int boardCols; 
        
        static string[ , ] tileValue; // storing the contents of each tile in this array
        
        static int turn = 1; // the integer, turn, is initially odd to start with Player A
        
        // individual characters from user's input
        static int character1;
        static int character2;
        static int character3;
        static int character4;
        
        // storing player's previous position
        static int prevRowA;
        static int prevColA;
        static int prevRowB;
        static int prevColB;

        // storing player's current position
        static int currentRowA;
        static int currentColA;
        static int currentRowB;
        static int currentColB;

        const string h = "\u2500"; // horizontal line
        const string v = "\u2502"; // vertical line
        const string tl = "\u250c"; // top left corner
        const string tr = "\u2510"; // top right corner
        const string bl = "\u2514"; // bottom left corner
        const string br = "\u2518"; // bottom right corner
        const string vr = "\u251c"; // vertical join from right
        const string vl = "\u2524"; // vertical join from left
        const string hb = "\u252c"; // horizontal join from below
        const string ha = "\u2534"; // horizontal join from above
        const string hv = "\u253c"; // horizontal vertical cross
        const string sp = " ";      // space
        const string bb = "\u25a0"; // block
        const string fb = "\u2588"; // left half block
        const string lh = "\u258c"; // left half block
        const string rh = "\u2590"; // right half block

        static string[] letters = {"a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

        static void Main()
        {
            Welcome();
            WriteLine("");
            
            Initialization();
            WriteLine("");

            UpdateBoard(startA + startB, true, "none");
            DrawGameBoard();
            

            while( true ) // this loop will run indefinitely until a winner is detected in the loop
            {                
                while ( PlayerATurn() == true ) // this portion of the code will run when it is Player A's turn
                {
                    Write("{0}, it's your turn. Enter a move: ", playerA);
                    string move = ReadLine();
                    
                    if( move == "q" || move == "quit" ) // the user may quit at any time when prompted for their turn
                    {
                        return;
                    }
                    

                    while (CheckInput(move, false, "A") == false)
                    {
                        Write("Please enter a valid move: ");
                        move = ReadLine();
                        
                    }

                    UpdateBoard(move, false, "A" );
                    DrawGameBoard();
                    
                    if( CheckWinner( character1, character2 ) )
                    {
                        WriteLine( "{0} wins!", playerB );
                        return;
                    }

                    turn ++ ;
                }
                
                while ( PlayerATurn() == false ) // this portion of the code will run when it is Player B's turn
                {
                    Write("{0}, it's your turn. Enter a move: ", playerB);
                    string move = ReadLine();
                    
                    if( move == "q" || move == "quit" ) // the user may quit at any time when prompted for their turn
                    {
                        return;
                    }

                    while (CheckInput(move, false, "B") == false)
                    {
                        Write("Please enter a valid move: ");
                        move = ReadLine();
                        
                    }

                    UpdateBoard(move, false, "B" );
                    DrawGameBoard();
                    
                    if( CheckWinner( character1, character2) )
                    {
                        WriteLine( "{0} wins!", playerA );
                        return;
                    }

                    turn ++ ;
                
                }
                
            }
        }
                
        // a winner is detected when all 8 of it's opponents surrounding tiles are false
        static bool CheckWinner(int character1, int character2)
        {
            if( board[character1, character2 - 1] == false && board[character1, character2 + 1] == false &&
                board[character1 + 1, character2] == false && board[character1 - 1, character2] == false &&
                board[character1 + 1, character2 + 1] == false && board[character1 + 1, character2 - 1] == false &&
                board[character1 - 1, character2 + 1] == false && board[character1 - 1, character2 - 1] == false )            
            {
                return true;
            }
            
            else
            {
                return false;
            }
        }

        
        // uses modulo to determine who's turn it is
        static bool PlayerATurn()
        {
        
            if( turn % 2 == 1 ) // for player A
            {
                return true;
            }
            
            else // for player B
            {
                return false;
            }
        }
        
        
        // draws the gameboard and each of the tile's values
        static void DrawGameBoard()
        {

            WriteLine();

            // Draw the top board labels
            Write("     ");
            for(int c = 0; c < board.GetLength(1); c++)
            {
                Write("{0}   ", letters[c]);
            }

            WriteLine();

            // Draw the top board boundary.
            Write("   ");
            for (int c = 0; c < board.GetLength(1); c++)
            {
                if (c == 0) Write(tl);
                Write("{0}{0}{0}", h);
                if (c == board.GetLength(1) - 1) Write("{0}", tr);
                else Write("{0}", hb);
            }
            WriteLine();

            // Draw the board rows.
            for (int r = 0; r < board.GetLength(0); r++)
            {
                Write(" {0} ", letters[r]);

                // Draw the row contents.
                for (int c = 0; c < board.GetLength(1); c++)
                {
                    if (c == 0) Write(v);
                    // if the tile is true, display its contents
                    if (board[r, c]) Write("{0}{1}", tileValue[r,c], v);
                    else Write("{0}{1}", "   ", v);
                }
                WriteLine();

                // Draw the boundary after the row.
                if (r != board.GetLength(0) - 1)
                {
                    Write("   ");
                    for (int c = 0; c < board.GetLength(1); c++)
                    {
                        if (c == 0) Write(vr);
                        Write("{0}{0}{0}", h);
                        if (c == board.GetLength(1) - 1) Write("{0}", vl);
                        else Write("{0}", hv);
                    }
                    WriteLine();
                }
                else
                {
                    Write("   ");
                    for (int c = 0; c < board.GetLength(1); c++)
                    {
                        if (c == 0) Write(bl);
                        Write("{0}{0}{0}", h);
                        if (c == board.GetLength(1) - 1) Write("{0}", br);
                        else Write("{0}", ha);
                    }
                    WriteLine();
                }
            }

            WriteLine();
        }

        
        // updates the gameboard for each iteration of the game
        static void UpdateBoard(string characters, bool round1, string playerTurn)
        {
            // taking the index of each characters from user's input
            character1 = Array.IndexOf( letters, characters.Substring(0,1) ); // the row where the player wishes to go to
            character2 = Array.IndexOf( letters, characters.Substring(1,1) ); // the column where the player wishes to go to
            character3 = Array.IndexOf( letters, characters.Substring(2,1) ); // the row which the player wishes to remove
            character4 = Array.IndexOf( letters, characters.Substring(3,1) ); // the column which the player wishes to remove
            

            // this section of code applies only to the first round of play
            if (round1 == true)
            {
                for(int i = 0; i < board.GetLength(0); i++)
                {
                    for(int j = 0; j < board.GetLength(1); j++)
                    {
                        tileValue[i, j] = rh + fb + lh;
                        board[i, j] = true;

                    }
                }

                tileValue[character1, character2] = (sp + "A" + sp); // first two characters are player A's position
                tileValue[character3, character4] = (sp + "B" + sp); // second two characters are player B's position 

                currentRowA = character1;
                prevRowA = character1;
                
                currentColA = character2;
                prevColA = character2;
                
                currentRowB = character3;
                prevRowB = character3;
                
                currentColB = character4;
                prevColB = character4;
            }
            
            // this section applies to all rounds after round 1
            else
            {
                if(playerTurn == "A")
                {
                    tileValue[prevRowA, prevColA] = rh + fb + lh; 
                    tileValue[currentRowA, currentColA] = sp + bb + sp; 
                    tileValue[character1, character2] = sp + "A" + sp;
                    
                    prevRowA = character1;
                    prevColA = character2;
                }
                else
                {
                    tileValue[prevRowB, prevColB] = rh + fb + lh; 
                    tileValue[currentRowB, currentColB] = sp + bb + sp; 
                    tileValue[character1, character2] = sp + "B" + sp;
                    
                    prevRowB = character1;
                    prevColB = character2;
                }

                board[character3, character4] = false; // the last two characters the user inputs will be false (and removed)
            }
        }

        // will check the validity of each player's move
        static bool CheckInput(string characters, bool round1, string playerTurn)
        {
            
            if (characters.Length < 4 || characters.Length > 4) // user must input a move of exactly 4 characters
            {
                return false;
            }
                
            // taking the index of each characters from user's input
            character1 = Array.IndexOf( letters, characters.Substring(0,1) ); // the row where the player wishes to go to
            character2 = Array.IndexOf( letters, characters.Substring(1,1) ); // the column where the player wishes to go to
            character3 = Array.IndexOf( letters, characters.Substring(2,1) ); // the row which the player wished to remove
            character4 = Array.IndexOf( letters, characters.Substring(3,1) ); // the column which the player wishes to remove

            
            
            if( character1 < 0 || character2 < 0 || character3 < 0 || character4 < 0) // if all characters are < 0
            {
                return false;
            }
            
            if((round1 == true) && (tileValue[character3, character4] == sp + "A" + sp )) // cannot remove opponent's piece in the first round
            {
                return false;
            }
            
            if((round1 == true) && (tileValue[character3, character4] == sp + "B" + sp )) // cannot remove opponent's piece in the first round
            {
                return false;
            }
            
            if(( character1 > board.GetLength(0) - 1 || character3 > board.GetLength(0) - 1 )) // row of character 1/character 3 must be less than the total rows
            {
                return false;
            }

            if(( character2 > board.GetLength(1) - 1 || character4 > board.GetLength(1) - 1 )) // column of character 2/character 4 must be less than the total columns
            {
                return false;
            }

            if(character1 == character3 && character2 == character4) // the two coordinates the user inputs cannot be the same
            {
                return false;
            }

            if((board[character1, character2] == false || board[character3, character4] == false) && round1 == false) // cannot move to/remove a tile that's already false
            {    
                return false;
            }

            if(tileValue[character1, character2] == sp + "A" + sp || tileValue[character1, character2] == sp + "B" + sp) // cannot move to a tile that is occupied by a player's piece
            {    
                return false;
            }

            if( tileValue[character3, character4] == sp + bb + sp) // cannot remove starting platform
            {    
                return false;
            }            

            // this loop will check if after round 1, if the user tries to move more than one space for their move OR remove opponents current tile
            if(round1 == false)
            {
                int row;
                int col;

                if(playerTurn == "A")
                {
                    row = prevRowA;
                    col = prevColA;
                    
                    if (tileValue[character3, character4] == sp + "B" + sp) // cannot remove opponent's current position
                        return false;
                }
                else
                {
                    row = prevRowB;
                    col = prevColB;

                    if (tileValue[character3, character4] == sp + "A" + sp) // cannot remove opponent's current position
                        return false;
                }

                // your next row move has to be within 1 tile of your current row
                if (character1 < row - 1 || character1 > row + 1) 
                {
                    return false;
                }
                
                // your next column move has to be within 1 tile of your current column
                if (character2 < col - 1 || character2 > col + 1)
                {
                    return false;
                }
            }

            return true; // if none of these apply, the method will return true
        }
        
        // checks whether the initial starting positions of the board are within board parameters
        static bool StartCheck( string startA, string startB)
        {
            int startRowA = Array.IndexOf( letters, startA.Substring(0,1)); // finds individual index of Player A's row
            int startColA = Array.IndexOf( letters, startA.Substring(1,1)); // finds individual index of Player A's column
            int startRowB = Array.IndexOf( letters, startB.Substring(0,1)); // finds individual index of Player B's row
            int startColB = Array.IndexOf( letters, startB.Substring(1,1)); // finds individual index of Player B's column
       

            if( startRowA < 0 || startRowB < 0 || startColA < 0 || startColB < 0 ) // coordinates cannot be < 0
            {
                return false;
            }
            
            if( startRowA > boardRows || startRowB > boardRows || startColA > boardCols || startColB > boardCols ) // coordinates cannot exceed the length of the row/column
            {
                return false;
            }
            
            if( startRowA == startRowB && startColA == startColB ) // player A and B's starting positions cannot be the same
            {
                return false;
            }
            
            else // if none of these apply, return true
            {
                return true;
            }
        }

        // prints the introduction and instructions for play
        static void Welcome()
        {
            WriteLine( "" );
            WriteLine( "WELCOME TO ISOLATION!");
            WriteLine("");
            WriteLine( "HOW TO PLAY:" );
            WriteLine("");
            WriteLine( "(a) Before playing, you and your opponent will be prompted to configure your names, starting platforms and board size." );
            WriteLine( "(b) The goal of the game will be to isolate your opponent's gamepiece so it would be impossible for them to make any further moves." );
            WriteLine( "(c) For each player's turn, they will be asked to move their own gamepiece to a coordinate on the board and remove a different tile from the board." );
            WriteLine( "(d) The player will be asked to make this move denoted by 4 lowercase letters (ie. 'abcd' )." );
            WriteLine( "(e) In this case, 'a' denotes the row and 'b' denotes the column that the player wishes to move their gamepiece to." );
            WriteLine( "(f) Likewise, 'c' and 'd' respectively represent the row and column of the tile the player wishes to remove from the board." );
            WriteLine( "(g) Keep in mind that you cannot move your gamepiece more than one space in any direction." );
            WriteLine( "(h) However, you are allowed to remove any free spaces as long as a gamepiece is on it or it isn't a starting platform." );
            WriteLine( "" );
            WriteLine( "Once you completely isolate your opponent on all 8 of their surrounding tiles, you win!");
            WriteLine( "If both you and your opponent cannot make any further moves (ie. you are BOTH isolated), the game is a draw." );
            WriteLine( "");
            WriteLine( "NOTE: If you decide to quit the game, simply type 'q' or 'quit' when your turn is prompted." );
            WriteLine( "" );
            
        }
        
        // configures the players, their starting positions and board size
        static void Initialization()
        {

            Write("Player A, enter your name: ");
            playerA = ReadLine();
            if (playerA == "") playerA = "Player A";

            Write("Player B, enter your name: ");
            playerB = ReadLine();
            if (playerB == "") playerB = "Player B";

            
            bool validInput = false; // set initially false and will only become true if the input is within 4 and 26
          
            while( validInput == false )
            {
                Write("Enter the number of rows on the gameboard (4 to 26): ");
                string rows = ReadLine();
                
                if( rows.Length == 0)
                {
                    rows = "6";
                }
                
                boardRows = int.Parse( rows );
                
                if( boardRows < 4 || boardRows > 26 )
                {
                    WriteLine( "Invalid input. Try again." );
                    validInput = false;
                }
                validInput = true;
            }
            validInput = false;
            
            while( validInput == false) 
            {
                Write("Enter the number of columns on the board (4 to 26): ");
                string columns = ReadLine();
                
                if (columns.Length == 0)
                {
                    columns = "8";
    
                }
                boardCols = int.Parse( columns );
                
                if( boardCols < 4 || boardCols > 26 )
                {
                    WriteLine( "Invalid input. Try again." );
                    validInput = false;
                }
                
                validInput = true;
            }
            
            // this variable will store the values of the tiles as false or true (removed or not)
            board = new bool[boardRows, boardCols]; 
            
            // this array will store the contents of each tile to be displayed
            tileValue = new string[boardRows, boardCols]; 

            // draws gameboard so users can pick starting positions
            DrawGameBoard();

            Write(playerA + "'s starting position [default 'cd']: ");
            startA = ReadLine();
            if (startA == "")
            {
                startA = "cd";
            }
            
            Write(playerB + "'s starting position [default 'dh']: ");
            startB = ReadLine();
            if (startB == "")
            {
                startB = "dh";
            }
            
            // checks the validity of starting positons and if false, will prompt this loop
            while( StartCheck(startA, startB) == false )
            {
                WriteLine("Invalid! Please re-enter your input.");

                Write(playerA + "'s starting position [default 'cd']: ");
                startA = ReadLine();
                if (startA == "")
                {
                    startA = "cd";
                }
                
                Write( playerB + "'s starting position [default 'dh']: ");
                startB = ReadLine();
                if (startB == "")
                {
                    startB = "dh";
                }
                
                
            }
        }
        
    }
}
