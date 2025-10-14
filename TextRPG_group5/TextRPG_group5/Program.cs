using TextRPG_group5.Managers;
using TextRPG_group5.Scenes;

namespace TextRPG_group5
{
     internal class Program
     {
          static public Scene? currentScene;

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
               // scene initialize
               currentScene = null; // startScene
          }

          static public void SetScene(Scene scene)
          {
               currentScene = scene;
          }
     }
}
