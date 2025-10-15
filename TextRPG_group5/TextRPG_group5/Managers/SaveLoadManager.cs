using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextRPG_group5.Managers
{
     public class GameProgress
     {
          //public PlayerData Player { get; set; } = new PlayerData();
          //public List<Quest> ActiveQuests { get; set; } = new List<Quest>();
          //public Inventory PlayerInventory { get; set; } = new Inventory();
          //public Position PlayerPosition { get; set; } = new Position();
          //public GameTime GameTimeInfo { get; set; } = new GameTime();
          public DateTime LastSaveTime { get; set; } = DateTime.Now;
          public TimeSpan TotalPlayTime { get; set; } = TimeSpan.Zero;
          public int SaveVersion { get; set; } = 1;

          public override string ToString()
          {
               //return $"플레이어: {Player.Name} (Lv.{Player.Level}), 점수: {Player.Score}, 골드: {Player.Gold}\n" +
               //$"퀘스트 {ActiveQuests.Count}개, 마지막 저장: {LastSaveTime:yy-MM-dd HH:mm}, 플레이 시간: {TotalPlayTime:hh\\:mm\\:ss}";
               return "";
          }
          public static class SaveLoadManager
          {
               private static readonly string SaveDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SatsumaGame");
               private static readonly string SaveFile = Path.Combine(SaveDir, "gameprogress.json");
               private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

               public static async Task SaveAsync(GameProgress data)
               {
                    try
                    {
                         data.LastSaveTime = DateTime.Now;
                         if (!Directory.Exists(SaveDir)) Directory.CreateDirectory(SaveDir);
                         string json = JsonSerializer.Serialize(data, Options);
                         string tmpFile = SaveFile + ".tmp";
                         await File.WriteAllTextAsync(tmpFile, json);
                         File.Copy(tmpFile, SaveFile, true);
                         File.Delete(tmpFile);
                         Console.WriteLine("✅ 저장 완료: " + SaveFile);
                    }
                    catch (Exception ex)
                    {
                         Console.WriteLine("❌ 저장 실패: " + ex.Message);
                    }
               }

               public static async Task<GameProgress> LoadAsync()
               {
                    try
                    {
                         if (!File.Exists(SaveFile))
                         {
                              Console.WriteLine("저장된 데이터 없음, 새 게임 시작.");
                              return new GameProgress();
                         }
                         string json = await File.ReadAllTextAsync(SaveFile);
                         var data = JsonSerializer.Deserialize<GameProgress>(json);
                         Console.WriteLine("✅ 불러오기 완료.");
                         return data ?? new GameProgress();
                    }
                    catch (Exception ex)
                    {
                         Console.WriteLine("❌ 불러오기 실패: " + ex.Message);
                         return new GameProgress();
                    }
               }
          }
     }
}
