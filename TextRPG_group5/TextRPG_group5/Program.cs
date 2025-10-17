using System.Runtime.CompilerServices;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Managers;
using TextRPG_group5.Scenes;

namespace TextRPG_group5
{
     internal class Program
     {
          static private Scene? currentScene = new NewTitleScene();
          static private Player? player;

          public const int maxStage = 10;

          static void Main(string[] args)
          {
               //Initialize();

               while (true)
               {
                    if (currentScene is NewTitleScene
                         || currentScene is CreatePlayerScene)
                    { currentScene!.Show(); }
                    else
                    {
                         currentScene!.Show();
                         Console.WriteLine();
                         Console.WriteLine("원하시는 행동을 입력해주세요.");
                         Console.Write(">> ");
                         byte input = byte.TryParse(Console.ReadLine(), out byte val) ? val : byte.MaxValue;
                         Console.Clear();
                         currentScene.HandleInput(input);
                    }
               }
          }

          static public void Initialize(Player newPlayer)
          {
               // player initialize
               player = newPlayer;
               player.ReachedStage = 1;

               // test items initialize
               player.Inventory.AddItem(ItemInfo.GetItem("목검"));
               player.Inventory.AddItem(ItemInfo.GetItem("워해머"));
               player.Inventory.AddItem(ItemInfo.GetItem("HP소형포션"));
               player.Inventory.AddItem(ItemInfo.GetItem("나무 활"));
               player.Inventory.AddItem(ItemInfo.GetItem("가죽갑옷"));
               player.Inventory.AddItem(ItemInfo.GetItem("숙련자의 로브"));
               player.Inventory.AddItem(ItemInfo.GetItem("MP소형포션"));

               // scene initialize
               currentScene = new MainScene(player); // startScene
          }

          static public void SetScene(Scene scene)
          {
               currentScene = scene;
          }
     }
}
