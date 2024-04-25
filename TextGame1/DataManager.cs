using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace TextGame1
{
    internal class DataManager
    {

        public static void Test(string filepath)
        {
            StreamReader file = new StreamReader(filepath);
            while(!file.EndOfStream)
            {
                var line = file.ReadLine();
                Console.WriteLine(line);
            }
        }

        public static void LoadItemCSV(string filepath, out List<Item> Items)
        {
            StreamReader file = new StreamReader(filepath);
            Items = new List<Item>();

            var line = file.ReadLine();

            while (!file.EndOfStream)
            {
                line = file.ReadLine();

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

                if (!string.IsNullOrWhiteSpace(fields[5]))
                {
                    stats.Add(eStatus.ATK, int.Parse(fields[5]));
                }

                if (!string.IsNullOrWhiteSpace(fields[6]))
                {
                    stats.Add(eStatus.DEF, int.Parse(fields[6]));
                }

                string description = fields[7];

                var item = new Item(id, name, cost, itemType, stats, description);

                Items.Add(item);
            }

            file.Close();
        }

    }
}
