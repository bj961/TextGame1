using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{
    internal class Shop
    {
        public List<Item> Items { get; set; }

        public Shop() { new List<Item>(); }

        //isEquipped 이용하여 판매여부 판단 예정. true이면 판매된 상품
        public void PrintShopItems()
        {
            // 헤더 출력
            Console.WriteLine("{0,-4} {1,-20} {2,-20} {3,-30} {4, -10}", "No.", "이름", "스탯", "설명", "가격");
            Console.WriteLine(new string('-', 80));

            // 아이템 출력
            int i = 1;
            foreach (var item in Items)
            {
                string statsString = "";
                foreach (var stat in item.EquipStatus)
                {
                    statsString += $"{stat.Key} {stat.Value} ";
                }

                string costString = item.IsEquipped ? "구매완료" : $"{item.Cost}";


                Console.WriteLine("{0,-4} {1,-20} {2,-20} {3,-30} {4, -10}", i++.ToString().PadRight(5), item.Name.PadRight(20), statsString.PadRight(20), item.Description, costString.PadRight(10));
            }
        }

        public void AddItem(Item item)
        {

        }

        public void BuyItem(int idx)
        {

        }

        public void SellItem()
        {

        }

        public void InitShop()
        {
            //Items.Clear();
            LoadCSV("Items.csv");
        }
        void LoadCSV(string filename)
        {

        }
        /*
        public static void LoadCSV(string filepath)
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
        
        }

        */
    }
}
