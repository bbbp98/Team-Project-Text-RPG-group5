using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;

namespace TextRPG_group5.QuestManagement
{
    public class LoadQuest : Exception
    {
        public static List<QuestManager> LoadQuests(string filePath)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // 대소문자 무시
                    ReadCommentHandling = JsonCommentHandling.Skip, // 주석 허용
                    AllowTrailingCommas = true // 마지막 쉼표 허용
                };

                string json = File.ReadAllText(filePath, Encoding.UTF8);
                QuestDataRoot data = JsonSerializer.Deserialize<QuestDataRoot>(json, options);
                return data.quests;
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch(FileLoadException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
