using System;
using System.IO;
using System.Text.Json;
using TextRPG_group5;

namespace Save
{

    internal class SaveData
    {
        private static string SavePath = @"C:\Users\Public\Downloads";
        private static string SaveFile = Path.Combine(SavePath, "Savedata.json");
        private Player player;
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public SaveData(Player player)
        {
            this.player = player;
        }

        public static void Save(Character player)
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

    }
}



