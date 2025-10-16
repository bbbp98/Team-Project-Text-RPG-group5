using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace TextRPG_group5.QuestManagement
{
    internal class QuestManager : Exception
    {
        const string PATH = "C:\\TeamSparta\\GitHub_TP\\Team-Project-Text-RPG-group5\\TextRPG_group5\\TextRPG_group5\\QuestManagement\\QuestsInfo.json";
        public static List<Quest>? Quests { get; private set; }
        

        public static List<Quest> Load()
        {
            try
            {
                string filePath = "C:\\TeamSparta\\GitHub_TP\\Team-Project-Text-RPG-group5\\TextRPG_group5\\TextRPG_group5\\QuestManagement\\QuestsInfo.json";

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true, // 대소문자 무시
                    ReadCommentHandling = JsonCommentHandling.Skip, // 주석 허용
                    AllowTrailingCommas = true, // 마지막 쉼표 허용
                    Converters = { new JsonStringEnumConverter() },

                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All)
                };

                string json = File.ReadAllText(PATH, Encoding.UTF8); // json 파일 전체를 저장
                Quests = JsonSerializer.Deserialize<List<Quest>>(json, options); // 시리얼화 -> 퀘스트 리스트에 추가 (각 프로퍼티에 자동 적용)

                return Quests;
            }
            // 받아온 패스에 파일이 없을 때 메시지 출력
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
            // 파일읽기에 실패했을 때 메시지 출력
            catch (FileLoadException ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        // 퀘스트 수락시 진행상황을 변경하는 메서드
        public static void AcceptQuest(byte id)
        {
            Quest? q = Quests.FirstOrDefault(x => x.QuestID == id);
            if(q != null && q.Status == QuestStatus.NoProgress)
            {
                q.Status = QuestStatus.InProgress;
                Console.WriteLine($"[퀘스트를 수락하셨습니다.] {q.QuestTitle}");
            }

            SaveQuestProgress(Quests);
        }

        // 처치한 몬스터 이름을 받아 진행 중 상태인 퀘스트에 반영하는 메서드
        public void UpdateProgress(string target)
        {
            // 변수 q 에 진행중인 리스트를 순차적으로 저장 
            foreach(var q in Quests.Where(x => x.Status == QuestStatus.InProgress))
            {
                // 저장된 퀘스트의 완료조건 체크, 진행상황 반영, 조건 충족시 완료 상태로 변경
                foreach(var o in q.objectives)
                {
                    o.Current++;
                    Console.WriteLine($"[진행] {o.Target} : {o.Current}/{o.Count}");

                    if(q.CheckCompletion())
                    {
                        q.Status = QuestStatus.Complete;
                        Console.WriteLine($"[완료] {q.QuestTitle}");
                    }
                }
            }

            SaveQuestProgress(Quests);
        }

        /// <summary>
        /// 퀘스트 보상을 player클래스 오브젝트에 반영
        /// </summary>
        internal void GiveReward(Player player)// Todo : 매개변수에 Player오브젝트 추가
        {
            foreach(var q in Quests.Where(x => x.Status == QuestStatus.Complete))
            {
                // 플레이어의 경험치, 돈 변경 로직 추가
                player.GainExp(q.Rewards.Exp);
                player.Gold += q.Rewards.Gold;
                // Todo : 플레이어 인벤토리 추가 필요

                q.Status = QuestStatus.NoProgress; // 보상을 한번만 받을 수 있도록 설정
            }
        }

        public static void SaveQuestProgress(List<Quest> quests)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true, // 대소문자 무시
                ReadCommentHandling = JsonCommentHandling.Skip, // 주석 허용
                AllowTrailingCommas = true, // 마지막 쉼표 허용
                Converters = { new JsonStringEnumConverter() },

                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All)
            };
            string json = JsonSerializer.Serialize(quests, options);
            File.WriteAllText(PATH, json, new UTF8Encoding(false));
        }
    }
}
