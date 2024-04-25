using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{
    internal class DataManager
    {

        public static List<Item> LoadItemCSV(string filepath)
        {
            List<Item> Items = new List<Item>();

            var lines = File.ReadAllLines(filepath);

            foreach (var line in lines)
            {
                var fields = line.Split(',');

                int id = int.Parse(fields[0]);
                string name = fields[1];
                int cost = int.Parse(fields[2]);
                eItemType itemType = (eItemType)Enum.Parse(typeof(eItemType), fields[3]);
                Dictionary<eStatus, int> stats = new Dictionary<eStatus, int> { };

                if (!string.IsNullOrWhiteSpace(fields[4]))
                {
                    stats.Add(eStatus.HP, int.Parse(fields[4]));
                }

                // ATK 필드가 비어있지 않으면 추가
                if (!string.IsNullOrWhiteSpace(fields[5]))
                {
                    stats.Add(eStatus.ATK, int.Parse(fields[5]));
                }

                // DEF 필드가 비어있지 않으면 추가
                if (!string.IsNullOrWhiteSpace(fields[6]))
                {
                    stats.Add(eStatus.DEF, int.Parse(fields[6]));
                }

                string description = fields[7];

                var item = new Item(id, name, cost, itemType, stats, description);

                /*
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
                */
                Items.Add(item);
            }

            return Items;
        }

    }
}
