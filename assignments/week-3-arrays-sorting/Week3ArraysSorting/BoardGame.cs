using System;

namespace Week3ArraysSorting
{
    public class BoardGame
    {
        private char[,] board = new char[3, 3];
        private char currentPlayer = 'X';
        private bool gameOver = false;
        private string winner = "";

        public BoardGame()
        {
            Console.WriteLine(
                "TROENDLE P - BoardGame Dev 260 Array - Tic-Tac-Toe initialized. Bellevue College Fall 2025."
            );
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("=== BOARD GAME (Part A) ===");
            Console.WriteLine();
            DisplayInstructions();

            bool playAgain = true;
            while (playAgain)
            {
                InitializeNewGame();
                PlayOneGame();
                playAgain = AskPlayAgain();
            }

            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to return to main menu...");
            Console.ReadKey();
        }

        private void DisplayInstructions()
        {
            Console.WriteLine("TIC-TAC-TOE RULES:");
            Console.WriteLine("- Players take turns placing X and O.");
            Console.WriteLine("- Enter row and column (0-2) when prompted.");
            Console.WriteLine("- First to get 3 in a row wins!");
            Console.WriteLine();
        }

        private void InitializeNewGame()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = ' ';
            currentPlayer = 'X';
            gameOver = false;
            winner = "";
        }

        private void PlayOneGame()
        {
            while (!gameOver)
            {
                RenderBoard();
                GetPlayerMove();
                CheckWinCondition();
                if (!gameOver)
                    SwitchPlayer();
            }

            RenderBoard();
            if (winner != "")
                Console.WriteLine($"Player {winner} wins!");
            else
                Console.WriteLine("It's a draw!");
        }

        private void RenderBoard()
        {
            Console.Clear();
            Console.WriteLine("  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                    if (j < 2)
                        Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2)
                    Console.WriteLine("  -----");
            }
            Console.WriteLine();
        }

        private void GetPlayerMove()
        {
            bool validMove = false;
            while (!validMove)
            {
                Console.Write(
                    $"Player {currentPlayer}, enter row and column (e.g., 1 2) or 'q' to quit: "
                );
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "q" || input == "exit")
                {
                    Console.WriteLine("Game exited early by player.");
                    gameOver = true;
                    return;
                }

                var parts = input?.Split();
                if (
                    parts?.Length == 2
                    && int.TryParse(parts[0], out int row)
                    && int.TryParse(parts[1], out int col)
                    && row >= 0
                    && row < 3
                    && col >= 0
                    && col < 3
                    && board[row, col] == ' '
                )
                {
                    board[row, col] = currentPlayer;
                    validMove = true;
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }
        }

        private void CheckWinCondition()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if (
                    board[i, 0] == currentPlayer
                    && board[i, 1] == currentPlayer
                    && board[i, 2] == currentPlayer
                )
                {
                    gameOver = true;
                    winner = currentPlayer.ToString();
                    return;
                }

                if (
                    board[0, i] == currentPlayer
                    && board[1, i] == currentPlayer
                    && board[2, i] == currentPlayer
                )
                {
                    gameOver = true;
                    winner = currentPlayer.ToString();
                    return;
                }
            }

            // Check diagonals
            if (
                board[0, 0] == currentPlayer
                && board[1, 1] == currentPlayer
                && board[2, 2] == currentPlayer
            )
            {
                gameOver = true;
                winner = currentPlayer.ToString();
                return;
            }

            if (
                board[0, 2] == currentPlayer
                && board[1, 1] == currentPlayer
                && board[2, 0] == currentPlayer
            )
            {
                gameOver = true;
                winner = currentPlayer.ToString();
                return;
            }

            // Check for draw
            bool boardFull = true;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == ' ')
                        boardFull = false;

            if (boardFull)
            {
                gameOver = true;
                winner = "";
            }
        }

        private bool AskPlayAgain()
        {
            Console.Write("Play again? (y/n): ");
            string? input = Console.ReadLine()?.Trim().ToLower();
            return input == "y" || input == "yes";
        }

        private void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}

