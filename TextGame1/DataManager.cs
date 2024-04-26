using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace TextGame1
{
    internal class DataManager
    {

        public static void LoadItemCSV(string filepath, out List<Item> Items)
        {
            
            Items = new List<Item>();
            using (StreamReader file = new StreamReader(filepath))
            {
                var line = file.ReadLine();

                while (!file.EndOfStream)
                {
                    line = file.ReadLine();

                    var fields = line.Split(',');

                    int id = int.Parse(fields[0]);
                    string name = fields[1];
                    int cost = int.Parse(fields[2]);
                    eItemType itemType = (eItemType)Enum.Parse(typeof(eItemType), fields[3]);
                    Dictionary<eStatus, float> stats = new Dictionary<eStatus, float> { };

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
            }
        }

        public static void Save(Character player, Shop shop)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            DateTime now = DateTime.Now;

            string directoryPath = @"..\..\..\save\";
            string fileName = $"{player.Name} {now.ToString("yyyy-MM-dd HH시mm분ss초")}.dat";
            string filePath = directoryPath + fileName;


            string directoryName = Path.GetDirectoryName(filePath);
            if(!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                List<object> data = new List<object>();

                data.Add(player);
                data.Add(shop);

                formatter.Serialize(stream, data);
            }

            Console.WriteLine($"데이터가 {fileName}에 저장되었습니다.");
        }

        public static void Load(string fileName, out Character player, out Shop shop)
        {
            string directoryPath = @"..\..\..\save\";
            string filePath = directoryPath + fileName;

            BinaryFormatter formatter = new BinaryFormatter();

            List<object> loadedData;

            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                loadedData = (List<object>)formatter.Deserialize(stream);
            }

            player = (Character)loadedData[0];
            shop = (Shop)loadedData[1];
        }

        
    }
}
