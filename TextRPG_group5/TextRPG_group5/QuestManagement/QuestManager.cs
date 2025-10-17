using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;


namespace TextRPG_group5.QuestManagement
{
    internal class QuestManager : Exception
    {
        private static readonly string PATH = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,
            "QuestManagement",
            "QuestsInfo.json");

        public static List<Quest>? Quests { get; set; }


        public static List<Quest> Load()
        {
            try
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
            int idx = 0;
            string[] menu = ["수락", "거절"];
            bool isActive = true;
            bool isSelect = false;
            bool isNoProgress = false;

            ConsoleKeyInfo key = new ConsoleKeyInfo();
            Quest? q = Quests.FirstOrDefault(x => x.QuestID == id);

            while (isActive)
            {
                if (idx < 0) idx = 1;
                else if (idx == menu.Length) idx = 0;

                Console.WriteLine($"선택한 퀘스트 : {q.QuestTitle}");

                Console.WriteLine(" 퀘스트를 수락하시겠습니까? ");
                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == idx)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(menu[i]);
                        Console.ResetColor();
                    }
                    else Console.Write(menu[i]);

                    Console.Write("   ");
                }
                Console.WriteLine("\n * 좌우방향키로 포인터이동, 엔터 입력시 선택");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        idx++;
                        Console.Clear();
                        break;
                    case ConsoleKey.LeftArrow:
                        idx--;
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        isActive = false;
                        isSelect = true;
                        Console.Clear();
                        break;
                }
            }

            isNoProgress = QuestStatusCheck(q, menu, idx);

            Thread.Sleep(2000);

            if (isNoProgress) SaveQuestProgress(Quests);
            else return;
        }
        public static bool QuestStatusCheck(Quest q, string[] menu, int idx)
        {
            if (q != null && q.Status == QuestStatus.NoProgress && menu[idx] == "수락")
            {
                q.Status = QuestStatus.InProgress;
                Console.WriteLine($"[퀘스트를 수락하셨습니다.] {q.QuestTitle}");
                Console.WriteLine("메인으로 돌아갑니다.");

                return true;
            }
            else if (q != null && q.Status == QuestStatus.InProgress && menu[idx] == "수락")
            {
                Console.WriteLine($"[해당 퀘스트는 이미 수행 중입니다.] {q.QuestTitle}");
                Console.WriteLine("메인으로 돌아갑니다.");

                return false;
            }
            else if (q != null && q.Status == QuestStatus.Complete && menu[idx] == "수락")
            {
                Console.WriteLine($"[해당 퀘스트는 이미 완료된 퀘스트입니다.] {q.QuestTitle}");
                Console.WriteLine("메인으로 돌아갑니다.");

                return false;
            }
            else
            {
                Console.WriteLine($"{q.QuestTitle} \n [퀘스트를 거절하셨습니다.]");
                Console.WriteLine("메인으로 돌아갑니다.");

                return false;
            } 
        }

        // 처치한 몬스터 이름을 받아 진행 중 상태인 퀘스트에 반영하는 메서드
        // ToDo : 몬스터가 죽을 때 이 메서드를 호출시켜야 함
        public void UpdateProgress(string target)
        {
            // 변수 q 에 진행중인 리스트를 순차적으로 저장 
            foreach (var q in Quests.Where(x => x.Status == QuestStatus.InProgress))
            {
                // 저장된 퀘스트의 완료조건 체크, 진행상황 반영, 조건 충족시 완료 상태로 변경
                foreach (var o in q.objectives)
                {
                    o.Current++;
                    Console.WriteLine($"[진행] {o.Target} : {o.Current}/{o.Count}");

                    if (q.CheckCompletion())
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
            foreach (var q in Quests.Where(x => x.Status == QuestStatus.Complete))
            {
                // 플레이어의 경험치, 돈 변경 로직 추가
                player.GainExp(q.Rewards.Exp);
                player.Gold += q.Rewards.Gold;
                // 보상 아이템을 순차적으로 인벤토리에 추가
                ItemManagement item; // string배열 Rewards.Items의 멤버를 아이템형식으로 변환후 넣을 ItemManagement자료형
                for(int i = 0; i < q.Rewards.Items.Count; i++)
                {
                    item = ItemInfo.GetItem(q.Rewards.Items[i]);
                    player.Inventory.AddItem(item);
                }
                
                q.Status = QuestStatus.Done; // 보상을 한번만 받을 수 있도록 설정
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
                Converters = { new JsonStringEnumConverter() }, // enum허용

                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All) // 영어 이외에도 출력할 수 있게
            };
            string json = JsonSerializer.Serialize(quests, options); // 퀘스트 리스트를 시리얼화
            File.WriteAllText(PATH, json, new UTF8Encoding(false)); // json파일작성 (순수 UTF8 형식)
        }
    }
}
