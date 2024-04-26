using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{
    public enum eStatus
    {
        HP,
        ATK,
        DEF
    }

    public enum eCharacterClass1
    {
        Warrior,
        Ranger,
        Wizard,
        Rogue
    }

    public struct EquippedItem
    {
        public int itemID;
        public int itemType;
        public int[] itemStat;
    }

    internal class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int[] Status { get; set; } //캐릭터 스탯
        public int Gold { get; set; } //소지 골드
        public string CharacterClass { get; set; } //캐릭터 직업
       
        public Inventory Inventory { get; set; }
        private Dictionary<eItemType, int> equippedItems = new Dictionary<eItemType, int>(); //<아이템 착용 부위, 해당 아이템 인벤토리 idx>
        
        private int[] equippedItemStat; //아이템으로 변동된 스탯

        public Character(string name)
        {
            Name = name;
            Level = 1;
            Exp = 0;
            Status = new int[Enum.GetNames(typeof(eStatus)).Length];
            Status[(int)eStatus.HP] = 100;
            Status[(int)eStatus.ATK] = 10;
            Status[(int)eStatus.DEF] = 5;
            Gold = 1500;
            CharacterClass = "전사";
            Inventory = new Inventory();
            equippedItemStat = new int[Enum.GetNames(typeof(eStatus)).Length];
            equippedItemStat[(int)eStatus.HP] = 0;
            equippedItemStat[(int)eStatus.ATK] = 0;
            equippedItemStat[(int)eStatus.DEF] = 0;
            Console.WriteLine($"{Enum.GetNames(typeof(eStatus)).Length}");//tmp
        }

        //상태창 메뉴
        public void StatusMenu()
        {
            ViewStatus();

            Console.WriteLine("\n0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n >> ");

            int input;
            while (int.TryParse(Console.ReadLine(), out input) == false || input != 0)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            switch (input)
            {
                case 0:
                    return;
                default:
                    break;
            }
        }



        //캐릭터의 능력치를 출력하는 메소드
        public void ViewStatus()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name} ( {CharacterClass} )");

            string[] equippedItemStatString = Enumerable.Repeat("", Enum.GetNames(typeof(eStatus)).Length).ToArray();

            for (int i = 0 ; i < Enum.GetNames(typeof(eStatus)).Length; i++)
            {
                if (equippedItemStat[i] != 0)
                {
                    equippedItemStatString[i] += "(";
                    if(equippedItemStat[i] > 0)
                    {
                        equippedItemStatString[i] += "+";
                    }
                    equippedItemStatString[i] += $"{equippedItemStat[i]})";
                }
            }

            Console.WriteLine($"공격력 : {Status[(int)eStatus.ATK]} " + equippedItemStatString[(int)eStatus.ATK]);
            Console.WriteLine($"방어력 : {Status[(int)eStatus.DEF]} " + equippedItemStatString[(int)eStatus.DEF]);
            Console.WriteLine($"체  력 : {Status[(int)eStatus.HP]} " + equippedItemStatString[(int)eStatus.HP]);
            Console.WriteLine($"GOLD : {Gold} G");
        }



        public int InventoryMenu()
        {
            int input;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");

            Inventory.PrintItems();

            Console.WriteLine("\n1. 장착 관리\n0. 나가기\n");
            //Console.WriteLine("\n1. 장착 관리\n2. 아이템 버리기\n0. 나가기");
            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 2)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            switch (input)
            {
                case 0:
                    break;
                case 1:
                    while (EquipManagementMenu(0) != 0) {; }
                    break;
                case 2:
                    //while (EquipManagementMenu(1) != 0) {; }
                    break;
            }

            return input;
        }

        public int EquipManagementMenu(int mode)
        {
            int input;
            string modeString = "";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            switch (mode)
            {
                case 0:
                    Console.WriteLine("인벤토리 - 장착 관리");
                    Console.ResetColor();
                    Console.WriteLine("보유 중인 아이템을 장착/해제할 수 있습니다.\n");
                    modeString += "장착/해제";
                    break;
                    /*
                case 1:
                    Console.WriteLine("인벤토리 - 아이템 버리기");
                    Console.ResetColor();
                    Console.WriteLine("보유 중인 아이템을 버릴 수 있습니다.\n");
                    modeString += "버리기";
                    break;
                    */
            }
            Console.WriteLine("[아이템 목록]");
            this.Inventory.PrintItems();

            Console.WriteLine($"\n1~{Inventory.Items.Count}. 아이템 " + modeString + "\n0. 나가기");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > Inventory.Items.Count)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            switch (mode)
            {
                case 0:
                    if (input > 0 && input <= Inventory.Items.Count)
                    {
                        EquipManagement(input);
                    }
                    break;
                case 1:
                    if (input > 0 && input <= Inventory.Items.Count)
                    {
                        if (Inventory.Items[input - 1].IsEquipped)
                        {
                            //아이템 해제
                            UnequipItem(input);
                        }
                        Inventory.DeleteItem(input);
                    }
                    break;
            }

            return input;
        }


        // itemIdx는 인벤토리의 인덱스
        // 인덱스를 받아 해당 아이템의 장착/해제를 수행하는 메소드
        public void EquipManagement(int input)
        {
            int itemIdx = input - 1;
            eItemType TypeOfSelectedItem = Inventory.Items[itemIdx].ItemType; //사용자가 선택한 아이템의 타입
            
            if (equippedItems.ContainsKey(TypeOfSelectedItem)) //사용자가 선택한 장비 슬롯 확인
            {
                int idxOfEquipedItem = equippedItems[TypeOfSelectedItem]; //장착된 아이템의 인덱스

                if (idxOfEquipedItem != (itemIdx))
                {
                    Console.WriteLine($"{TypeOfSelectedItem} 슬롯에는 이미 장착된 아이템이 있습니다.");
                    Console.WriteLine($"장착 중인 아이템 : {Inventory.Items[idxOfEquipedItem].Name}");
                    Console.ReadLine();
                    return;
                }

            }
            

            //선택한 아이템이 착용 중 이라면
            if (Inventory.Items[itemIdx].IsEquipped)
            {
                //아이템 해제
                UnequipItem(itemIdx);

            }
            else
            {
                //아이템 장착
                EquipItem(itemIdx);
            }
        }

        public void UnequipItem(int itemIdx)
        {
            eItemType TypeOfSelectedItem = Inventory.Items[itemIdx].ItemType;

            Inventory.Items[itemIdx].IsEquipped = false; // 아이템 장착중 플래그 해제
            equippedItems.Remove(TypeOfSelectedItem); // 착용 부위 플래그 해제
            foreach (var stat in Inventory.Items[itemIdx].EquipStatus) // 능력치 변동 해제
            {
                Status[(int)stat.Key] -= stat.Value;
                equippedItemStat[(int)stat.Key] -= stat.Value;
            }
        }

        public void EquipItem(int itemIdx)
        {
            eItemType TypeOfSelectedItem = Inventory.Items[itemIdx].ItemType;

            Inventory.Items[itemIdx].IsEquipped = true; // 아이템 장착중 플래그 적용
            equippedItems[TypeOfSelectedItem] = itemIdx; // 착용 부위 플래그 적용
            foreach (var stat in Inventory.Items[itemIdx].EquipStatus) // 능력치 변동 적용
            {
                Status[(int)stat.Key] += stat.Value;
                equippedItemStat[(int)stat.Key] += stat.Value;
            }
        }

    }
}
