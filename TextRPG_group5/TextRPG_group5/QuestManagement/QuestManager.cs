using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TextRPG_group5.QuestManagement
{
    public class QuestManager : Exception
    {
        public List<Quest> Quests { get; private set; }

        public void Load(string path)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true, // 대소문자 무시
                    ReadCommentHandling = JsonCommentHandling.Skip, // 주석 허용
                    AllowTrailingCommas = true // 마지막 쉼표 허용
                };

                string json = File.ReadAllText(path, Encoding.UTF8);
                Quests = JsonSerializer.Deserialize<List<Quest>>(json, options);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FileLoadException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AcceptQuest(int id)
        {
            Quest q = Quests.FirstOrDefault(x => x.QuestID == id);
            if(q != null && q.Status == QuestStatus.NoProgress)
            {
                q.Status = QuestStatus.InProgress;
                Console.WriteLine($"[퀘스트를 수락하셨습니다.] {q.QuestTitle}");
            }
        }

        public void UpdateProgress(string target)
        {
            foreach(var q in Quests.Where(x => x.Status == QuestStatus.InProgress))
            {
                foreach(var o in q.objectives)
                {
                    o.current++;
                    Console.WriteLine($"[진행] {o.Target} : {o.current}/{o.Count}");

                    if(q.CheckCompletion())
                    {
                        q.Status = QuestStatus.Complete;
                        Console.WriteLine($"[완료] {q.QuestTitle}");
                    }
                }
            }
        }

        public void GiveReward()// Todo : 매개변수에 Player오브젝트 추가
        {
            foreach(var q in Quests.Where(x => x.Status == QuestStatus.Complete))
            {
                // Todo : 플레이어의 경험치, 돈, 인벤토리 조작 로직 추가
            }
        }


    }
}
