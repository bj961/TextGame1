using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{
    internal class Shop
    {
        public List<Item> shop;


        public Shop()
        {
            shop = new List<Item>();
        }
        public Shop(string filepath)
        {
            DataManager.LoadItemCSV(filepath, out shop); 
            //DataManager.LoadItemCSV(filepath, shop);
        }

        //isEquipped 이용하여 판매여부 판단 예정. true이면 판매된 상품
        public void PrintShopItems()
        {
            // 헤더 출력
            Console.WriteLine("{0,-4} {1,-15} {2,-20} {3,-40} {4, -10}", "No.", "이름", "스탯", "설명", "가격");
            Console.WriteLine(new string('-', 80));

            // 아이템 출력
            int i = 1;
            foreach (var item in shop)
            {
                StringBuilder stringBuilder1 = new StringBuilder();
                
                foreach (var stat in item.EquipStatus)
                {
                    stringBuilder1.Append(stat.Key);
                    stringBuilder1.Append(" ");
                    stringBuilder1.Append(stat.Value);
                    stringBuilder1.Append(" ");

                    //statsString += $"{stat.Key} {stat.Value} ";
                }
                string statsString = stringBuilder1.ToString();
                string costString = item.IsEquipped ? "구매완료" : $"{item.Cost}";

                Console.WriteLine("{0,-4} {1,-15} {2,-20} {3,-40} {4, -7}", i++, item.Name, statsString, item.Description, costString);
            }
        }

        public void PrintShopMenu()
        {

        }

        private void PrintShopUI(Character character)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("보유 골드");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");
        }

        public int ShopMenu(Character character)
        {
            int input;

            PrintShopUI(character);
            PrintShopItems();

            Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기");

            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 3)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            switch (input)
            {
                case 1:
                    //BuyItem(character)
                    break;
                case 2:
                    //SellItem(character)
                    break;
            }

            return input;
        }


        public void BuyItem(Character character)
        {
            int input;

            PrintShopUI(character);
            PrintShopItems();

            Console.WriteLine($"\n1~{character.Inventory.Items.Count}. 아이템 " + modeString + "\n0. 나가기");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > character.Inventory.Items.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

        }

        public void SellItem()
        {

        }

    }
}
