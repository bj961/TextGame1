using System.Diagnostics;

namespace TextGame1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isPlaying = true;
            int input;

            //상점 초기화
            string filepath = @"..\..\..\ShopItems.csv";
            Shop shop = new Shop(filepath);

            Console.Write("플레이어 이름을 입력하세요: ");
            Character player = new Character(Console.ReadLine());


            // 테스트용 초기 아이템. 추후 CSV 파일 활용해 읽어올 예정
            player.Inventory.AddItem(new Item(0, "무쇠갑옷", 1000, eItemType.Chest, new Dictionary<eStatus, int> { { eStatus.DEF, 5 } }, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            player.Inventory.AddItem(new Item(1, "스파르타의 창", 1500, eItemType.Weapon, new Dictionary<eStatus, int> { { eStatus.ATK, 7 } }, "스파르타의 전사들이 사용했다는 전설의 창입니다."));
            player.Inventory.AddItem(new Item(2, "낡은 나무 방패", 500, eItemType.Shield, new Dictionary<eStatus, int> { { eStatus.DEF, 2 } }, "쉽게 볼 수 있는 낡은 방패 입니다."));
            player.Inventory.AddItem(new Item(3, "낡은 검", 500, eItemType.Weapon, new Dictionary<eStatus, int> { { eStatus.ATK, 2 } }, "쉽게 볼 수 있는 낡은 검 입니다."));
            player.Inventory.AddItem(new Item(4, "광전사의 목걸이", 1500, eItemType.Amulet, new Dictionary<eStatus, int> { { eStatus.ATK, 7 }, { eStatus.DEF, -3 } }, "공격성을 증폭시키는 광전사의 목걸이입니다."));


            //메인 메뉴
            while (isPlaying)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("메인메뉴");
                Console.ResetColor();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전 입장\n5. 휴식하기\n\n0. 게임 종료\n");
                
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
                while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 5)
                {
                    Console.WriteLine("\n잘못된 입력입니다.");
                    Console.Write("원하시는 행동을 입력해주세요.\n >> ");
                }

                switch (input)
                {
                    case 0:
                        Console.WriteLine("게임을 종료합니다.");
                        isPlaying = false;
                        break;
                    case 1:
                        player.StatusMenu();
                        break;
                    case 2:
                        while (player.InventoryMenu() != 0) { }
                        break;
                    case 3:
                        while (shop.ShopMenu(player) != 0) { }
                        break;
                    case 4:
                        while (Dungeon.DungeonMenu(player) != 0) { }
                        break;
                    case 5:
                        while(Rest(player) != 0) { }
                        break;
                }
            }
        }

        
        static int Rest(Character character)
        {
            int input;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("휴식하기");
            Console.ResetColor();
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다.");
            Console.WriteLine($"현재 HP : {character.Status[(int)eStatus.HP]}");
            Console.WriteLine($"보유 골드 : {character.Gold} G\n");

            Console.WriteLine("1. 휴식하기\n0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 2)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            switch (input)
            {
                case 0:
                    return 0;
                case 1:
                    if(character.Gold < 500)
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                    else
                    {
                        if(character.Status[(int)eStatus.HP] == 100)
                        {
                            Console.WriteLine("이미 최대 체력입니다.");
                        }
                        else
                        {
                            character.Gold -= 500;
                            character.Status[(int)eStatus.HP] = 100;
                            Console.WriteLine("휴식을 완료했습니다.");
                        }     
                    }
                    Console.ReadLine();
                    break;
            }

            return input;
        }
    }

}