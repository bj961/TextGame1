using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextGame1
{
    internal class Inventory
    {
        public List<Item> Items { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        //아이템 추가
        public void AddItem(Item item)
        {
            Items.Add(new Item(item));
        }

        //idx 입력받아 해당 아이템 제거
        public void DeleteItem(int idx)
        {
            Items.RemoveAt(idx-1);
        }

        public void PrintItems()
        {
            // 헤더 출력
            Console.WriteLine("{0,-4} {1,-5} {2,-20} {3,-20} {4,-30}", "No.", "장비", "이름", "스탯", "설명");
            Console.WriteLine(new string('-', 80));

            // 아이템 출력
            int i = 1;
            foreach (var item in Items)
            {
                string equippedString = item.IsEquipped ? "[E]" : " ";
                string statsString = "";
                foreach (var stat in item.EquipStatus)
                {
                    statsString += $"{stat.Key} {stat.Value} ";
                }
                

                Console.WriteLine("{0,-4} {1,-5} {2,-20} {3,-20} {4,-30}", i++, equippedString, item.Name.PadRight(20, ' '), statsString.PadRight(20), item.Description);
            }
        }

    }
}
