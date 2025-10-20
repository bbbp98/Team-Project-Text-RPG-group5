using System;
using System.IO;
using System.Text.Json;
using TextRPG_group5.Managers;

namespace TextRPG_group5.Scenes
{
    internal class MainScene : Scene
    {
        const string welcomMessage = "고개를 들면 하늘을 향해 높이 솟아 있는 거대한 탑이 보인다.\n\n" +
          "'꼭대기 층에 도달한 자는 소원을 이룰 수 있다.'\n그런 전설이 전해지는 소원의 탑.\n\n" +
          "전설을 확인하기 위해 도전한 이들은 많았지만\n아직 그 누구도 성공한 자는 없다.\n";

        private Player player;

        public MainScene(Player player)
        {
            this.player = player;
        }

        public override void HandleInput(byte input)
        {
            switch (input)
            {
                case 0: // 게임 종료
                    ExitScene exitScene = new ExitScene();
                    exitScene.HandleExitAsync().Wait();
                    break;
                case 1: // 캐릭터 정보 확인
                    Program.SetScene(new PlayerInfoScene(player));
                    break;
                case 2: // 인벤토리
                    Program.SetScene(new InventoryScene(player));
                    break;
                case 3: // 상점
                    Program.SetScene(new ShopScene(player));
                    break;
                case 4: // 퀘스트
                    Program.SetScene(new QuestScene(player));
                    break;
                case 5: // 던전 입장
                    Program.SetScene(new DungeonEntranceScene(player));
                    break;
                case 6: // 저장
                    Program.SetScene(new SaveScene(player));
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public override void Show()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("마을");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

            Console.WriteLine(welcomMessage);
            Console.WriteLine("탑을 오르기 전 충분한 준비를 하고 가자.\n\n");
            Console.WriteLine("----------------------------------------\n\n") ;
            Console.WriteLine("1. 캐릭터 정보 확인");
            Console.WriteLine("2. 인벤토리 확인");
            Console.WriteLine("3. 상점 확인");
            Console.WriteLine("4. 퀘스트 확인");
            Console.WriteLine("5. 던전 탐험");
            Console.WriteLine("6. 저장하기");
            Console.WriteLine("0. 게임 종료");
        }
    }
}
