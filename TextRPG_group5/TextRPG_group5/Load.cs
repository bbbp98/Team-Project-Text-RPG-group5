using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.Json;
using TextRPG_group5;

namespace Load
{
     internal class LoadData
     {
          //private static string SavePath = @"C:\Users\Public\Downloads";
          private static string SaveFile = Path.Combine("Savedata.json");
          private static readonly JsonSerializerOptions options = new JsonSerializerOptions
          {
               WriteIndented = true
          };

          public Player Load()
          {
               if (!File.Exists(SaveFile))
               {
                    Console.WriteLine($"저장 파일이 경로에 존재하지 않습니다.");
                    return null;
               }

               try
               {
                    var settings = new JsonSerializerSettings
                    {
                         TypeNameHandling = TypeNameHandling.All
                    };
                    string json = File.ReadAllText(SaveFile);
                    Player? loadedPlayer = JsonConvert.DeserializeObject<Player>(json, settings);
                //Player? loadedPlayer = JsonSerializer.Deserialize<Player>(json, settings);
                loadedPlayer.Skill.RestoreActions();
                Console.WriteLine($"데이터를 성공적으로 불러왔습니다.");

                    return loadedPlayer;
               }
               //catch (JsonException ex)
               //{
               //     Console.WriteLine($"JSON 데이터 형식 오류: {ex.Message}");
               //     return null;
               //}
               catch (Exception ex)
               {
                    Console.WriteLine($"파일 불러오는 중 오류 발생: {ex.Message}");
                    return null;
               }
          }
     }
}