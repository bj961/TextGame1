using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{
    internal class ItemDB
    {
        //public static List<Item> Items = LoadCSV("Items.csv");


        /*
        public static List<Item> LoadCSV(string filepath)
        {
            List<Item> Items = new List<Item>();

            var lines = File.ReadAllLines(filepath);

            foreach (var line in lines)
            {
                var fields = line.Split(',');

                var item = new Item
                {
                    ID = int.Parse(fields[0]),
                    Name = fields[1],
                    Cost = int.Parse(fields[2]),
                    ItemType = (eItemType)Enum.Parse(typeof(eItemType), fields[3]),
                    EquipStatus = new Dictionary<eStatus, int>
                    {
                        { eStatus.HP, int.Parse(fields[4]) },
                        { eStatus.ATK, int.Parse(fields[5]) },
                        { eStatus.DEF, int.Parse(fields[6]) }
                    },
                    Description = fields[7]
                };

                Items.Add(item);
            }

            return Items;
        }

        */
    }
}
