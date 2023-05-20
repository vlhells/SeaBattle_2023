using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle_2023
{
    internal interface IHuman
    {
        public abstract void SpawnShips(char[,] field, List<Ship> ships, List<int> numOfThisDecksShips);
    }
}
