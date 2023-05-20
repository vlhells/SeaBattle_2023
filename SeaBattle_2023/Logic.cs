using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle_2023
{
    internal class Logic
    {
        static char[,] _computersField = new char[10, 10];
        static char[,] _playersField = new char[10, 10];
        static int _iteration = 0;
        static List<Ship> ships = new List<Ship>();
        static Random random = new Random();
        static List<int> numOfThisDecksShips = new List<int>() { 4, 3, 2, 1 }; // Сначала -- однопалубные и на уменьшение. Количество кораблей данного типа.

        internal static void LoadGameCycle(out char[,] computersField, out char[,] playersField)
        {
            if (_iteration == 0)
            {
                FillField(_playersField);
                FillField(_computersField);
                SpawnShips(_playersField, ships, numOfThisDecksShips);
                
                //Console.ReadLine();
            }

            if (_iteration > 0)
            {

            }


            computersField = _computersField;
            playersField = _playersField;

            _iteration++;
        }

        private static void FillField(char[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[j, i] = '.';
                }
            }
        }

        //private static void OldSpawnShips(char[,] field, List<Ship> ships)
        //{
        //    //for (int i = 0; i < 4; i++) // Спавн однопалубных кораблей.
        //    //{
        //    //    ships.Add(new Ship());

        //    //    int x;
        //    //    int y;

        //    //    do
        //    //    {
        //    //        x = random.Next(0, 10);
        //    //        y = random.Next(0, 10);
        //    //    } while (!CheckShipBoundary(field, (x, y)));

        //    //    field[x, y] = '*';
        //    //    ships[ships.Count - 1].Decks.Add((x, y));
        //    //}

        //    //for (int i = 0; i < 3; i++) // Спавн двухпалубных кораблей.
        //    //{
        //    //    ships.Add(new Ship());
        //    //    List<(int x, int y)> pre_decks_coords_list = new List<(int x, int y)>();

        //    //    do
        //    //    {
        //    //        int x_null = -1;
        //    //        int y_null = -1;

        //    //        int coin = random.Next(0, 2);
        //    //        switch (coin)
        //    //        {
        //    //            case 0:
        //    //                x_null = random.Next(0, 10); // Закрепляем Ох.   
        //    //                break;

        //    //            case 1:
        //    //                y_null = random.Next(0, 10);
        //    //                break;
        //    //        }

        //    //        pre_decks_coords_list.Clear();
        //    //        switch (x_null)
        //    //        {
        //    //            case -1: // Значит, закреплена ось Оу ==> генерим с разными х, но одним у.
        //    //                x_null = random.Next(0, 10);
        //    //                pre_decks_coords_list.Add((x_null, y_null));

        //    //                for (int z = 0; z < 1; z++) // Двухпалубник (+1 палуба)
        //    //                {
        //    //                    int dx = 1;
        //    //                    if (x_null + dx < field.GetLength(0) && field[x_null + dx, y_null] == '.')
        //    //                        pre_decks_coords_list.Add((x_null + dx, y_null));
        //    //                    else if (x_null - dx >= 0 && field[x_null - dx, y_null] == '.')
        //    //                    {
        //    //                        pre_decks_coords_list.Add((x_null - dx, y_null));
        //    //                    }
        //    //                }
        //    //                break;

        //    //            default:
        //    //                y_null = random.Next(0, 10);
        //    //                pre_decks_coords_list.Add((x_null, y_null));

        //    //                for (int z = 0; z < 1; z++) // Двухпалубник (+1 палуба)
        //    //                {
        //    //                    int dy = 1;
        //    //                    if (y_null + dy < field.GetLength(1) && field[x_null, y_null + dy] == '.')
        //    //                        pre_decks_coords_list.Add((x_null, y_null + dy));
        //    //                    else if (y_null - dy >= 0 && field[x_null, y_null - dy] == '.')
        //    //                    {
        //    //                        pre_decks_coords_list.Add((x_null, y_null - dy));
        //    //                    }
        //    //                }
        //    //                break;
        //    //        }
        //    //        //Program.Draw(_playersField);
        //    //    } while (!CheckShipBoundary(field, pre_decks_coords_list));

        //    //    ships[ships.Count - 1].Decks.AddRange(pre_decks_coords_list);
        //    //    foreach (var deck in ships[ships.Count - 1].Decks)
        //    //    {
        //    //        field[deck.x, deck.y] = '*';
        //    //    }
        //    //}

        //    //SpawnThreedecks(field, ships);
        //    //SpawnMultidecksShips(field, ships, numOfThisDecksShips);
        //}

        private static bool CheckShipBoundary(char[,] field, List<(int x, int y)> pre_coords)
        {
            //for (int n = 0; n < field.GetLength(0); n++)
            //{
            //    for (int m = 0; m < field.GetLength(1); m++)
            //    {
            //        if (field[n, m] != '*')
            //            field[n, m] = '.';
            //    }
            //}

            pre_coords.Sort();
            foreach (var deck in pre_coords)
            {
                for (int n = deck.y - 1; n <= deck.y + 1; n++)
                {
                    for (int m = deck.x - 1; m <= deck.x + 1; m++)
                    {
                        if (n >= 0 && m >= 0 && n < field.GetLength(0) && m < field.GetLength(1))
                        {
                            if (field[m, n] == '*' && !pre_coords.Contains((m, n)))
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        //private static bool CheckShipBoundary(char[,] field, (int x, int y) coords) // Для однопалубника.
        //{
        //    for (int n = coords.x - 1; n <= coords.x + 1; n++)
        //    {
        //        for (int m = coords.y - 1; m <= coords.y + 1; m++)
        //        {
        //            if (n >= 0 && m >= 0 && n < field.GetLength(0) && m < field.GetLength(1))
        //            {
        //                if (field[n, m] != '.')
        //                    return false;
        //            }
        //        }
        //    }

        //    return true;

        //}

        //private static void SpawnThreedecks(char[,] field, List<Ship> ships)
        //{
        //    for (int i = 0; i < 2; i++) // Спавн trehpal кораблей.
        //    {
        //        ships.Add(new Ship());
        //        List<(int x, int y)> pre_decks_coords_list = new List<(int x, int y)>();

        //        do
        //        {
        //            int x_null = -1;
        //            int y_null = -1;

        //            int coin = random.Next(0, 2);
        //            switch (coin)
        //            {
        //                case 0:
        //                    x_null = random.Next(0, 10); // Закрепляем Ох.   
        //                    break;

        //                case 1:
        //                    y_null = random.Next(0, 10);
        //                    break;
        //            }

        //            pre_decks_coords_list.Clear();
        //            switch (x_null)
        //            {
        //                case -1: // Значит, закреплена ось Оу ==> генерим с разными х, но одним у.
        //                    x_null = random.Next(0, 10);
        //                    pre_decks_coords_list.Add((x_null, y_null));

        //                    int dx = 2;
        //                    string vector = "";
        //                    for (int h = 0; h < 2; h++)
        //                    {
        //                        if (x_null + dx < field.GetLength(0) && field[x_null + dx, y_null] == '.' && vector != "left")
        //                        {
        //                            pre_decks_coords_list.Add((x_null + dx, y_null));
        //                            vector = "right";
        //                        }
        //                        else if (x_null - dx >= 0 && field[x_null - dx, y_null] == '.' && vector != "right")
        //                        {
        //                            pre_decks_coords_list.Add((x_null - dx, y_null));
        //                            vector = "left";
        //                        }
        //                        dx--;
        //                    }
        //                    break;

        //                default:
        //                    y_null = random.Next(0, 10);
        //                    pre_decks_coords_list.Add((x_null, y_null));

        //                    int dy = 2;
        //                    for (int h = 0; h < 2; h++)
        //                    {
        //                        if (y_null + dy < field.GetLength(1) && field[x_null, y_null + dy] == '.')
        //                            pre_decks_coords_list.Add((x_null, y_null + dy));
        //                        else if (y_null - dy >= 0 && field[x_null, y_null - dy] == '.')
        //                        {
        //                            pre_decks_coords_list.Add((x_null, y_null - dy));
        //                        }
        //                        dy--;
        //                    }
        //                    break;
        //            }
        //            //Program.Draw(_playersField);
        //        } while (!CheckShipBoundary(field, pre_decks_coords_list));

        //        ships[ships.Count - 1].Decks.AddRange(pre_decks_coords_list);
        //        foreach (var deck in ships[ships.Count - 1].Decks)
        //        {
        //            field[deck.x, deck.y] = '*';
        //        }
        //    }
        //}

        private static void SpawnShips(char[,] field, List<Ship> ships, List<int> numOfThisDecksShips)
        {
            int dd = -6; // Приращение к dx/dy для построения зависимости dd=f(числа кораблей данного типа (из списка numOfThisDecksShips)).
            foreach (var num in numOfThisDecksShips)
            {
                dd += 2;
                for (int i = 0; i < num; i++) // Сколько кораблей заспавнить.
                {
                    ships.Add(new Ship());
                    List<(int x, int y)> pre_decks_coords_list = new List<(int x, int y)>();

                    do
                    {
                        pre_decks_coords_list.Clear();

                        int x_null = -1;
                        int y_null = -1;

                        int coin = random.Next(0, 2);
                        switch (coin)
                        {
                            case 0:
                                x_null = random.Next(0, 10); // Закрепляем Ох.   
                                break;

                            case 1:
                                y_null = random.Next(0, 10);
                                break;
                        }

                         ///
                        switch (x_null)
                        {
                            case -1: // Значит, закреплена ось Оу ==> генерим с разными х, но одним у.
                                do
                                {
                                    x_null = random.Next(0, 10);
                                } while (field[x_null, y_null] != '.');
                                pre_decks_coords_list.Add((x_null, y_null));

                                int dx = num + dd;
                                string vector = "";
                                for (int h = 0; h < num + dd; h++)
                                {
                                    if (x_null + dx < field.GetLength(0) && field[x_null + dx, y_null] == '.' && vector != "left")
                                    {
                                        pre_decks_coords_list.Add((x_null + dx, y_null));
                                        vector = "right";
                                    }
                                    else if (x_null - dx >= 0 && field[x_null - dx, y_null] == '.' && vector != "right")
                                    {
                                        pre_decks_coords_list.Add((x_null - dx, y_null));
                                        vector = "left";
                                    }
                                    dx--;
                                }
                                break;

                            default:
                                do
                                {
                                    y_null = random.Next(0, 10);
                                } while (field[x_null, y_null] != '.');
                                pre_decks_coords_list.Add((x_null, y_null));

                                int dy = num + dd;
                                vector = "";
                                for (int h = 0; h < num + dd; h++)
                                {
                                    if (y_null + dy < field.GetLength(1) && field[x_null, y_null + dy] == '.' && vector != "left")
                                    {
                                        pre_decks_coords_list.Add((x_null, y_null + dy));
                                        vector = "right";
                                    }
                                    else if (y_null - dy >= 0 && field[x_null, y_null - dy] == '.' && vector != "right")
                                    {
                                        pre_decks_coords_list.Add((x_null, y_null - dy));
                                        vector = "left";
                                    }
                                    dy--;
                                }
                                break;
                        }
                        //Program.Draw(_playersField);
                    } while (!CheckShipBoundary(field, pre_decks_coords_list) || pre_decks_coords_list.Count != num + dd + 1);

                    ships[ships.Count - 1].Decks.AddRange(pre_decks_coords_list);
                    foreach (var deck in ships[ships.Count - 1].Decks)
                    {
                        field[deck.x, deck.y] = '*';
                    }
                }
            }
        }
    }
}
