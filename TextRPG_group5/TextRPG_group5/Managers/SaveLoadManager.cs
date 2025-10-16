using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using static TextRPG_group5.SaveScreen;


namespace TextRPG_group5.Managers
{
    internal static class SaveLoadManager
    {
        private static readonly string SaveDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TextRPG_group5");
        private static readonly string SaveFile = Path.Combine(SaveDir, "save.json");

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static async Task<bool> SaveAsync(GameProgress data)
        {
            try
            {
        

                if (!Directory.Exists(SaveDir))
                    Directory.CreateDirectory(SaveDir);

                string json = JsonSerializer.Serialize(data, Options);
                string tmp = SaveFile + ".tmp";

                await File.WriteAllTextAsync(tmp, json);
                File.Copy(tmp, SaveFile, true);
                File.Delete(tmp);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ 저장 실패: " + ex.Message);
                return false;
            }
        }

        public static async Task<GameProgress?> LoadAsync()
        {
            try
            {
                if (!File.Exists(SaveFile))
                {
                    
                    return null;
                }

                string json = await File.ReadAllTextAsync(SaveFile);
                var data = JsonSerializer.Deserialize<GameProgress>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (data == null)
                {
                    
                    return null;
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("불러오기 실패: " + ex.Message);
                return null;
            }
        }
    }
}