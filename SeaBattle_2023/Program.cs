namespace SeaBattle_2023
{
    internal class Program
    {
        static void Main()
        {
            Logic.GameCycle(out char[,] computersField, out char[,] playersField);
            Draw(computersField);
            Draw(playersField);
        }

        public static void Draw(char[,] field)
        {
            //Console.Clear();
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0;  j < field.GetLength(1); j++)
                {
                    switch (field[i, j])
                    {
                        case '*':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        case 'X':
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }
                    Console.Write(field[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}