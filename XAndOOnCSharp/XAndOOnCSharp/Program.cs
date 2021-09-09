using System;

namespace XAndOOnCSharp
{
    class Program
    {




        static void Main(string[] args)
        {
            int x = 0, y = 0 ;
            char human, AI;
            bool humanFirst;
            bool gameProgress = true;
            int turn = 0;
            bool win;

            int[] playerCoord = new int[2] { x, y};
            char[,] ppField = new char[3, 3];    // заполнения масива пробелами
            for (int i = 0; i < 3; i++)
            {
                
                for (int j = 0; j < 3; j++)
                {
                    ppField[i, j] = '_';
                }
            }





            if (random())           // определяем кто первый ходит
            {
                human = 'X';
                AI = '0';
                humanFirst = true;
                Console.WriteLine("Человек Играет X");
                Console.WriteLine();
                Console.WriteLine("AI Играет 0");

            }
            else
            {
                human = '0';
                AI = 'X';
                humanFirst = false;
                Console.WriteLine("AI играет Х");
                Console.WriteLine();
                Console.WriteLine("Человек Играет 0");
            }


            do
            {
                Console.Clear();
                printGameField(ref ppField);
                if (humanFirst)
                {
                    playerCoord = getHuman(ref ppField, ref human);
                    x = playerCoord[0];
                    y = playerCoord[1];
                    ppField[y, x] = human;
                    humanFirst = false;
                    turn++;
                }
                else
                {
                    playerCoord = getAI(ref ppField);
                    x = playerCoord[0];
                    y = playerCoord[1];
                    ppField[y, x] = AI;
                    humanFirst = true;
                    turn++;
                }

                Console.Clear();
                win = checkWin(ref ppField, ref AI, ref human);
                if (turn > 8 || win)
                {
                    Console.WriteLine();
                    printGameField(ref ppField);
                    gameProgress = false;
                }


            } while (gameProgress);



        }

        private static bool checkWin(ref char[,] arr, ref char AI, ref char human )
        {
            if ((arr[0,0] == arr [1, 1]) && (arr[0,0] == arr[2,2]) && arr[0,0] != '_')
            {
                printWin(ref arr[0, 0], ref AI, ref human);
                return true;
            }

            if ((arr[2, 0] == arr[1, 1]) && (arr[2, 0] == arr[0 , 2]) && arr[2, 0] != '_')
            {
                printWin(ref arr[2, 0], ref AI, ref human);
                return true;
            }

            if ((arr[0, 0] == arr[0, 1]) && (arr[0, 0] == arr[0, 2]) && arr[0, 0] != '_') 
            {
                printWin(ref arr[0, 0], ref AI, ref human);
                return true;
            }

            if ((arr[1, 0] == arr[1, 1]) && (arr[1, 0] == arr[1, 2]) && arr[1, 0] != '_') 
            {
                printWin(ref arr[1, 0], ref AI, ref human);
                return true;
            }

            if ((arr[2, 0] == arr[2, 1]) && (arr[2, 0] == arr[2, 2]) && arr[2, 0] != '_') 
            {
                printWin(ref arr[2, 0], ref AI, ref human);
                return true;
            }

            if ((arr[0, 0] == arr[1, 0]) && (arr[0, 0] == arr[2, 0]) && arr[0, 0] != '_')
            {
                printWin(ref arr[0, 0], ref AI, ref human);
                return true;
            }

            if ((arr[0, 1] == arr[1, 1]) && (arr[0, 1] == arr[2, 1]) && arr[0, 1] != '_')
            {
                printWin(ref arr[0, 1], ref AI, ref human);
                return true;
            }

            if ((arr[0, 2] == arr[1, 2]) && (arr[0, 2] == arr[2, 2]) && arr[0, 2] != '_')
            {
                printWin(ref arr[0, 2], ref AI, ref human);
                return true;
            }

            return false;
        }

        public static void printWin(ref char point, ref char AI, ref char human)
        
        {
            if (point == AI)
            {
                Console.WriteLine("Победил AI!");
            }
            else
            {
                Console.WriteLine("Победил Человек");
            }

        }

        private static int[] getHuman(ref char[,] arr, ref char human)
        {
            Console.WriteLine("Вы ходите: " + human);
            int[] playerCoord = new int[2];
            int  x, y;
            do
            {
                Console.WriteLine("Ведите число по линии X от 1 до 3");
                x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Ведите число по линии Y от 1 до 3");
                y = Convert.ToInt32(Console.ReadLine());
            } while ((x < 1 || x > 3) || (y < 1 || y > 3) || (arr[y - 1,x - 1] != '_'));

            playerCoord[0] = x - 1;
            playerCoord[1] = y - 1;

            return playerCoord;
        } 

        private static int[] getAI(ref char[,] arr)
        {
            int [] playerCoord = new int[2];
            int x, y;
            do
            {
                Random n = new Random();
                x = n.Next(1, 4);
                y = n.Next(1, 4);

            } while ((x < 1 || x > 3) || (y < 1 || y > 3) || (arr[y - 1, x - 1] != '_'));
            playerCoord[0] = x - 1;
            playerCoord[1] = y - 1;

            return playerCoord;

        }


        public static void printGameField(ref char[,] arr)
        {
            for (int i = 0; i < 3; i++)                         // Печать верней линии координат(Х)
                Console.Write("    " + (i + 1));
            Console.WriteLine();



            for (int i = 0; i < 3; i++)             // Печать игрового поля
            {
                Console.Write(i + 1 + " |");        // Печать верней линии координат(У)
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" " + arr[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public static bool random()
        {
            Random rand = new Random();
            if (rand.Next(1, 100) % 2 == 1)
                return true;
            else
                return false;
        }


    }
}
