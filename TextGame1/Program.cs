using System.Diagnostics;

namespace TextGame1
{
    internal class Program
    {
        static Character player;
        static Shop shop;

        static int StartMenu()
        {
            int input;
            string filepath;
            int isLoadSuccess;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n스 파 르 타 던 전\n");
            Console.ResetColor();
            Console.WriteLine("[0] 새 게임");
            Console.WriteLine("[1] 불러오기");
            Console.Write(" >> ");

            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 1)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write(" >> ");
            }

            switch (input)
            {
                case 0:
                    //상점 초기화
                    filepath = @"..\..\..\ShopItems.csv";
                    shop = new Shop(filepath);
                    Console.Write("플레이어 이름을 입력하세요: ");
                    player = new Character(Console.ReadLine());

                    // 테스트용 초기 아이템. 추후 CSV 파일 활용해 읽어올 예정
                    player.Inventory.AddItem(new Item(0, "무쇠갑옷", 1000, eItemType.Chest, new Dictionary<eStatus, float> { { eStatus.DEF, 5 } }, "무쇠로 만들어져 튼튼한 갑옷입니다."));
                    player.Inventory.AddItem(new Item(1, "스파르타의 창", 1500, eItemType.Weapon, new Dictionary<eStatus, float> { { eStatus.ATK, 7 } }, "스파르타의 전사들이 사용했다는 전설의 창입니다."));
                    player.Inventory.AddItem(new Item(2, "낡은 나무 방패", 500, eItemType.Shield, new Dictionary<eStatus, float> { { eStatus.DEF, 2 } }, "쉽게 볼 수 있는 낡은 방패 입니다."));
                    player.Inventory.AddItem(new Item(3, "낡은 검", 500, eItemType.Weapon, new Dictionary<eStatus, float> { { eStatus.ATK, 2 } }, "쉽게 볼 수 있는 낡은 검 입니다."));
                    player.Inventory.AddItem(new Item(4, "광전사의 목걸이", 1500, eItemType.Amulet, new Dictionary<eStatus, float> { { eStatus.ATK, 7 }, { eStatus.DEF, -3 } }, "공격성을 증폭시키는 광전사의 목걸이입니다."));

                    return 1;

                case 1:
                    isLoadSuccess = LoadMenu();
                    //while((isLoadSuccess = LoadMenu()) != 1){}
                    return isLoadSuccess;
                default:
                    return 0;
            }

        }

        static void Main(string[] args)
        {
            bool isPlaying = true;
            int input;

            while (StartMenu() != 1){}

            //메인 메뉴
            while (isPlaying)
            {

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("메인메뉴");
                Console.ResetColor();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전 입장\n5. 휴식하기\n6. 세이브\n7. 로드\n\n0. 게임 종료\n");
                
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
                while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > 7)
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
                    case 6:
                        SaveMenu();
                        break;
                    case 7:
                        while (LoadMenu()!=1) { }
                        break;
                }
            }
        }

        public static void SaveMenu()
        {
            int input;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("저장하기");
            Console.ResetColor();
            Console.WriteLine($"현재 데이터를 저장할 수 있습니다.");

            Console.Write("데이터를 저장하시겠습니까? 1.예  2.아니오\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 1 || input > 2)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("데이터 저장 1.예  2.아니오\n >> ");
            }
            switch (input)
            {
                case 1:
                    DataManager.Save(player, shop);
                    Console.WriteLine("\n계속하려면 엔터를 누르세요.");
                    Console.ReadLine();
                    break;
                case 2:
                    break;
            }
        }

        public static int LoadMenu()
        {
            //로드 성공 시 1, 로드 실패 시 -1, 플레이어가 나가기 실행 시 0 리턴
            int input;

            Console.Clear();
            

            //세이브 디렉토리 파일 출력
            string directoryPath = @"..\..\..\save\";
            string[] files;


            if (Directory.Exists(directoryPath))
            {
                files = Directory.GetFiles(directoryPath, "*.dat");

                if (files.Length == 0)
                {
                    //해당 폴더에 .dat 파일 없을 경우
                    Console.WriteLine("저장된 데이터가 없습니다.");
                    Console.WriteLine("\n계속하려면 엔터를 누르세요.");
                    return -1;
                }
                else
                {
                    //메뉴 출력
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("로드하기");
                    Console.ResetColor();
                    Console.WriteLine($"세이브 데이터를 불러올 수 있습니다.\n");

                    int i = 1;
                    Console.WriteLine("번호   파일명");
                    foreach (string file in files)
                    {
                        Console.WriteLine($"{i++} : {file}");
                    }
                    Console.WriteLine("0 : 나가기");
                }
            }
            else
            {
                //save 폴더가 존재하지 않음 == 데이터를 저장한 적 없음. 데이터 저장 시 폴더 만들고 저장함
                Console.WriteLine("저장된 데이터가 없습니다.");
                Console.WriteLine("\n계속하려면 엔터를 누르세요.");
                return -1;
            }

            Console.Write("불러올 데이터의 번호를 입력해주세요. \n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > files.Length)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write(" >> ");
            }

            if(input == 0)
            {
                return 0;
            }

            int idx = input - 1;

            Console.WriteLine($"{files[idx]}");
            Console.Write("이 데이터를 불러오시겠습니까? 1.예  2.아니오\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 1 || input > 2)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("데이터 저장 1.예  2.아니오\n >> ");
            }
            switch (input)
            {
                case 1:
                    DataManager.Load(files[idx], out player, out shop);
                    Console.WriteLine("불러오기에 성공했습니다!");
                    Console.WriteLine("\n계속하려면 엔터를 누르세요.");
                    Console.ReadLine();
                    return 1;
                case 2:
                    break;
            }

            return 0;
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
                    Console.WriteLine("\n계속하려면 엔터를 누르세요.");
                    Console.ReadLine();
                    break;
            }

            return input;
        }
    }

}