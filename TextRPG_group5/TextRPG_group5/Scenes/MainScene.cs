using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TextRPG_group5.Managers;

namespace TextRPG_group5.Scenes
{
     internal class MainScene : Scene
     {
          const string welcomMessage = "스파르타 마을에 오신 여러분 환영합니다.\n" +
               "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n";
          //GameProgress gameProgress = new GameProgress();

          private Player player;

          public MainScene(Player player)
          {
               this.player = player;
          }

          const string savePath = "player.json";

          public override void HandleInput(byte input)
          {
               switch (input)
               {
                    case 0: 
                         //Environment.Exit(0); 
                         //Exit.Show(Program.GetGameProgress());
                         ExitScene exitScene = new ExitScene();
                         exitScene.HandleExitAsync().Wait();
                         break;
                    case 1:
                         Program.SetScene(new PlayerInfoScene(player));
                         break;
                    case 2:
                         Program.SetScene(new InventoryScene(player));
                         break;
                    case 3:
                         Program.SetScene(new QuestScene(player));
                         break;
                    case 4:
                         Program.SetScene(new DungeonEntranceScene(player));
                         break;
                    case 5:
                         //Save();
                         Console.WriteLine("게임을 저장합니다.");
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
               //Console.Clear();
               Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("마을");
               Console.ForegroundColor = ConsoleColor.White;
               Console.WriteLine();
               
               Console.WriteLine(welcomMessage);
               Console.WriteLine("1. 캐릭터 정보 확인");
               Console.WriteLine("2. 인벤토리 확인");
               Console.WriteLine("3. 퀘스트 확인");
               Console.WriteLine("4. 던전 탐험");
               Console.WriteLine("5. 저장하기");
               Console.WriteLine("0. 게임 종료");
          }


          //public void Save()
          //{
          //     string saveJson = JsonSerializer.Serialize(player, new JsonSerializerOptions
          //     {
          //          WriteIndented = true
          //     });
          //     File.WriteAllText(savePath, saveJson);
          //}

          //public Player Load()
          //{
          //     if (File.Exists(savePath))
          //     {
          //          string json = File.ReadAllText(savePath);
          //          return (JsonSerializer.Deserialize<Player>(json)); // 만약 불러온 Json파일이 null이면 캐릭터 생성 메서드 호출할 수 있도록
          //     }
          //     else
          //     {
          //          return new Player("test", "궁수");
          //     }
          //}
     }
}
