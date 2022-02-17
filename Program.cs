namespace MeucampoMinado
{
    class Program
    {
        public static int Minas = 10;
        public static int Flags = 0;
        public static string[,] Board = {
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
          };
        public static string[,] BoardUser = {
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
            {" "," "," "," "," "," "," "," "," "},
          };

        public static void DrawBoard(string[,] board, int x, int y)
        {
            Console.SetCursorPosition(y, x);
                        
            for (int i = 0; i < 9; i++)
            {
                System.Console.WriteLine("+------------------+");
                                for (int j = 0; j < 9; j++)
                {
                    System.Console.Write($"|{board[i, j]}");
                }
                System.Console.WriteLine("|");
            }
            System.Console.WriteLine("+------------------+");
            System.Console.WriteLine("          CAMPO MINADO          ");
            System.Console.WriteLine(" Somente serão aceitas as teclas: ↓ → ↑ ← e Enter");
                    }
        public static void SetMinas()
        {
            for (int i = 0; i < Minas; i++)
            {
                Random r = new Random();
                int x, y; //Valores declarados aqui para ficar dentro do While//

                // Escolher dois número aleatórios(0,8), ver se é vazio
                // Se não, colocar bomba
                // Se sim, escolher outra posição
                do
                {
                    x = r.Next(0, 8);
                    y = r.Next(0, 8);


                } while (Board[x, y] != " ");


                Board[x, y] = "*";

            }
        }

        public static void SetNumbers()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Board[i, j] == "*")
                    {
                        continue;
                    }
                    int counter = 0;
                    if (Board[i, j] == " ")
                    {
                        // Verificação das bombas superiores
                        if (i - 1 >= 0)
                        {
                            if (Board[i - 1, j] == "*") counter++;
                            if (j - 1 >= 0)
                            {
                                if (Board[i - 1, j - 1] == "*") counter++;
                            }
                            if (j + 1 < 9)
                            {
                                if (Board[i - 1, j + 1] == "*") counter++;
                            }
                        }
                        //Lateral esquerda
                        if (j - 1 >= 0)
                        {
                            if (Board[i, j - 1] == "*") counter++;
                        }
                        //Lateral Direita
                        if (j + 1 < 9)
                        {
                            if (Board[i, j + 1] == "*") counter++;
                        }

                        if (i + 1 < 9)
                        {
                            if (Board[i + 1, j] == "*") counter++;
                            if (j - 1 >= 0)
                            {
                                if (Board[i + 1, j - 1] == "*") counter++;
                            }
                            if (j + 1 < 9)
                            {
                                if (Board[i + 1, j + 1] == "*") counter++;
                            }
                        }
                        Board[i, j] = counter.ToString();
                    }
                }
            }
        }

        public static bool checkVictory()
        {
            if (Flags == Minas)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (BoardUser[i, j] == "!")
                        {
                            if (Board[i, j] != "*") return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            SetMinas();
            SetNumbers();
            DrawBoard(BoardUser, 0, 0);
            // DrawBoard(Board, 22, 0); Debug only
            System.Console.WriteLine();

            Console.SetCursorPosition(1, 1);
            int CursorX = 1, CursorY = 1;

            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey(true);
                int posX = CursorX / 2, posY = CursorY / 2;
                switch (keypress.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (CursorY > 1)
                        {
                            CursorY -= 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (CursorY < 17)
                        {
                            CursorY += 2;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (CursorX < 17)
                        {
                            CursorX += 2;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (CursorX > 1)
                        {
                            CursorX -= 2;
                        }
                        DrawBoard(BoardUser, 0, 0);
                break;

                    case ConsoleKey.Enter:
                        if (BoardUser[posY, posX] == " ")
                        {
                            BoardUser[posY, posX] = Board[posY, posX];
                            if (BoardUser[posY, posX] == "*")
                            {
                                Console.Clear();
                                System.Console.WriteLine("Game Over!");
                                Console.ReadLine();
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                DrawBoard(BoardUser, 0, 0);
                            }
                        }
                        break;
                }
                Console.SetCursorPosition(CursorX, CursorY);
            } while (!checkVictory());

            Console.Clear();
            DrawBoard(Board, 0, 0);
            System.Console.WriteLine("Tabom, você venceu...");
            Console.ReadLine();
            System.Environment.Exit(0);

        }
    }
}
