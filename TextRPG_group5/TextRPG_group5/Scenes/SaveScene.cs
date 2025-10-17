using System;
using System.IO;
using System.Text.Json;
using TextRPG_group5;
using TextRPG_group5.ItemManage;

namespace TextRPG_group5.Scenes
{
    internal class SaveScene : Scene
    {
        
        //private const string SavePath = @"C:\Users\Public\Downloads";
        private static string SaveFile = ("Savedata.json");
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private Player player;

        public SaveScene(Player player)
        {
            this.player = player;   
        }

        public static void Save(Player player)
        {

            try
            {
                string json = JsonSerializer.Serialize(player, options);
                File.WriteAllText(SaveFile, json);
                Console.WriteLine($"파일이 경로에 성공적으로 저장되었습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"파일 저장 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        public override void HandleInput(byte input)
        {
            switch (input)
            {
                case 0:
                    Program.SetScene(new MainScene(player));
                    break;
           
            }
        }

        public override void Show()
        {
            Save(player);
            Console.Write("0. 메인으로 돌아갑니다.");
            Console.WriteLine();
           
        }

        //internal class  SaveDddafafda
        //{

        //    private static SaveData scene;
        //    private static GameProgress gameProgress;
        //    private static SaveData saveData1;

        //    public bool ShouldExitGame { get; private set; } = false;
        //    public static SaveData GetInstance(GameProgress gp)
        //    {
        //        if (scene == null)
        //        {
        //            SaveData saveData = saveData1;
        //            scene = saveData;
        //        }
        //        gameProgress = gp;
        //        return scene;
        //    }


            internal static object HandleExitAsync()
            {
                throw new NotImplementedException();
            }

            internal static void PerformSave()
            {
                throw new NotImplementedException();
            }
        }
    }



