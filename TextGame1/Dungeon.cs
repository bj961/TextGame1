using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGame1
{
    internal class Dungeon
    {
        private static int difficulty;
        private static int numOfDungeons = 3;
        private static string[] dungeonName = { "쉬운 던전", "일반 던전", "어려운 던전" };
        private static float[] recommendDEF = { 5, 11, 17 };
        private static int[] defaultReward = { 1000, 1700, 2500 };

        public static int DungeonMenu(Character character)
        {
            int input;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("던전 입장");
            Console.ResetColor();
            Console.WriteLine("들어갈 던전을 선택할 수 있습니다.");
            Console.WriteLine($"현재 HP가 0 이하라면 던전에 입장할 수 없습니다. (현재 HP:{character.Status[(int)eStatus.HP]})\n");

            for(int i = 0; i < numOfDungeons; i++)
            {
                Console.WriteLine($"{i+1}. {dungeonName[i]}\t| 방어력 {recommendDEF[i]} 이상 권장\t| 기본 보상 {defaultReward[i]} G");
            }
            Console.WriteLine("0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input < 0 || input > numOfDungeons)
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
                if (character.Status[(int)eStatus.HP] <= 0)
                {
                    Console.WriteLine("HP가 없어서 던전에 입장할 수 없습니다");
                    Console.ReadLine();
                    return input;
                }
                difficulty = input - 1;
                DungeonPlay(character);
            }

            return input;
        }

        private static void DungeonPlay(Character character)
        {
            Random random = new Random();

            if (character.Status[(int)eStatus.DEF] < recommendDEF[difficulty])
            {
                int FailureRate = 40;

                if(random.Next(0, 100) < FailureRate)
                {
                    DungeonFail(character);
                    return;
                }
            }

            DungeonClear(character);
        }

        private static void DungeonClear(Character character)
        {
            int input;
            Random random = new Random();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("던전 클리어");
            Console.ResetColor();
            Console.WriteLine("축하합니다!");
            Console.WriteLine($"{dungeonName[difficulty]}을 클리어 하였습니다.\n");

            Console.WriteLine("[탐험 결과]");

            //체력 손실
            Console.Write($"체력 {character.Status[(int)eStatus.HP]} -> ");
            float diffDEF = character.Status[(int)eStatus.DEF] - recommendDEF[difficulty]; // 방어력 - 던전 권장 방어력
            character.Status[(int)eStatus.HP] -= random.Next(20 - (int)diffDEF, 36 - (int)diffDEF);
            if(character.Status[(int)eStatus.HP] < 0) // 체력 0 미만으로 떨어지지 않음
            {
                character.Status[(int)eStatus.HP] = 0;
            }
            Console.WriteLine($"{character.Status[(int)eStatus.HP]}");

            //클리어 골드 보상
            //Range(공격력~공격력*2) % 만큼의 추가 보상
            Console.Write($"Gold {character.Gold} G -> ");
            float min = character.Status[(int)eStatus.ATK] * 0.01f;
            float max = character.Status[(int)eStatus.ATK] * 2 * 0.01f;
            float bonusRewardRate = 1 + (float)((random.NextDouble() * (max - min)) + min) * 0.01f;
            int reward = (int)(defaultReward[difficulty] * bonusRewardRate);
            character.Gold += reward;
            Console.WriteLine($"{character.Gold} G");

            character.Exp++;
            if (character.CheckLevelUp())
            {
                Console.WriteLine($"레벨 업! Lv.{character.Level} 이 되었습니다.");
            }

            Console.WriteLine("\n0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input != 0)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            return;
        }

        private static void DungeonFail(Character character)
        {
            int input;
            Random random = new Random();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("던전 실패");
            Console.ResetColor();
            Console.WriteLine("아쉽네요..");
            Console.WriteLine($"{dungeonName[difficulty]}을 클리어 실패하였습니다.\n");

            Console.WriteLine("[탐험 결과]");

            //체력 손실
            Console.Write($"체력 {character.Status[(int)eStatus.HP]} -> ");
            character.Status[(int)eStatus.HP] -= 50;
            if(character.Status[(int)eStatus.HP] < 0) // 체력 0 미만으로 떨어지지 않음
            { 
                character.Status[(int)eStatus.HP] = 0;
            }
            Console.WriteLine($"{character.Status[(int)eStatus.HP]}");

            Console.WriteLine("\n0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            while (int.TryParse(Console.ReadLine(), out input) == false || input != 0)
            {
                Console.WriteLine("\n잘못된 입력입니다.");
                Console.Write("원하시는 행동을 입력해주세요.\n >> ");
            }

            return;
        }

    }
}
