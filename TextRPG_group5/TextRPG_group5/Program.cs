using TextRPG_group5.Managers;
using TextRPG_group5.Scenes;

namespace TextRPG_group5
{
     internal class Program
     {
          static private Scene? currentScene;
          static private Player? player;

          public const int maxStage = 10;
          static private GameProgress gameProgress = new GameProgress();

          static void Main(string[] args)
          {
               Initialize();

               while (true)
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

          static private void Initialize()
          {
               // player initialize
               // load player data OR create player data 
               player = new Player("group5", "전사"); // test player
               player.ReachedStage = 1;
               //gameProgress = new GameProgress();

               // scene initialize
               currentScene = new MainScene(player); // startScene
          }

          static public void SetScene(Scene scene)
          {
               currentScene = scene;
          }

          static public GameProgress GetGameProgress()
          {
               return gameProgress;
          }
     }
}
