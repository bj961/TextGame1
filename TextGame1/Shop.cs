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
                string costString = item.IsEquipped ? "구매완료" : $"{item.Cost} G";

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

            //PrintShopUI(character);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("보유 골드");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            PrintShopItems();

            Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 3)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            switch (input)
            {
                case 1:
                    while(BuyItemMenu(character) != 0) {;}
                    break;
                case 2:
                    while( SellItemMenu(character) != 0 ) {;}
                    break;
            }

            return input;
        }


        public int BuyItemMenu(Character character)
        {
            int input;

            //PrintShopUI(character);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("보유 골드");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");


            PrintShopItems();

            Console.WriteLine($"\n1~{shop.Count}. 아이템 구매\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > shop.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            if(input == 0)
            {
                return 0;
            }
            else
            {
                int idx = input - 1;
                //BuyItem(character, input);
                if(shop[idx].IsEquipped == true)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.ReadLine();
                }
                else
                {
                    if(character.Gold >= shop[idx].Cost)
                    {
                        character.Gold -= shop[idx].Cost;
                        character.Inventory.AddItem(shop[idx]);
                        shop[idx].IsEquipped = true;
                        Console.WriteLine("구매를 완료했습니다.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        Console.ReadLine();
                    }
                    
                }
                
                return input;
            }


        }


        public int SellItemMenu(Character character)
        {
            int input=1;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다..\n");

            Console.WriteLine("보유 골드");
            Console.WriteLine($"{character.Gold} G\n");
            Console.WriteLine("[아이템 목록]");

            //character.Inventory.PrintItems();
            // 헤더 출력
            Console.WriteLine("{0,-6}  {1,-20}  {2,-15} {3,-30} {4,-7}", " No.", "이름", "스탯", "설명", "판매가격");
            Console.WriteLine(new string('-', 80));

            // 아이템 출력. 출력 포맷은 나중에 수정 후 다른 포맷들과 통일할 예정
            int i = 1;
            foreach (var item in character.Inventory.Items)
            {
                string equippedString = item.IsEquipped ? "[E]" : " ";
                string statsString = "";
                foreach (var stat in item.EquipStatus)
                {
                    statsString += $"{stat.Key} {stat.Value} ";
                }


                Console.WriteLine("- {0,-3}{1,-3}{2,-20}| {3,-15}| {4,-30}| {5, -10}G", i++, equippedString, item.Name.PadRight(20), statsString.PadRight(15), item.Description, (int)(item.Cost*0.85));
            }

            /*
             //출력 포맷 시도한 흔적. 추후 수정하거나 삭제할 예정
            foreach (var item in character.Inventory.Items)
            {
                StringBuilder itemString = new StringBuilder();
                itemString.Append($"- {i++} ");
                itemString.Append(item.IsEquipped ? "[E]" : "  ");
                itemString.Append($"{item.Name}\t");
                if(item.Name.Length > 5)
                {
                    itemString.Append("\t");
                }
                
                StringBuilder statString = new StringBuilder("| ");
                int count = 0;
                foreach (var stat in item.EquipStatus)
                {
                    statString.Append($"{stat.Key} {stat.Value} ");
                    count++;
                }
                itemString.Append(statString.ToString().PadRight(20));
                itemString.Append($"\t| {item.Description}\t| {item.Cost * 0.85}");

                Console.WriteLine(itemString.ToString());
            }
            */


            Console.WriteLine($"\n1~{shop.Count}. 아이템 판매\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > shop.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            if(input == 0)
            {
                return 0;
            }
            else
            {
                int idx = input - 1;
                character.Gold += (int)(character.Inventory.Items[idx].Cost*0.85);
                if (character.Inventory.Items[idx].IsEquipped )
                {
                    character.UnequipItem(idx);
                }
                character.Inventory.Items.RemoveAt(idx);
            }

            return input;
        }

    }
}
