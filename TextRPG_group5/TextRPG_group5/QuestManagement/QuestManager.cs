using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using TextRPG_group5.ItemManage;
using TextRPG_group5.Managers;


namespace TextRPG_group5.QuestManagement
{
    internal class QuestManager : Exception
    {
        // 퀘스트의 초기정보 데이터의 경로를 명시한 상수
        private static readonly string ORIGIN_FILE_PATH = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,
            "QuestManagement",
            "DefaultQuestsInfo.json");

        // 퀘스트의 초기정보 데이터를 복사할 경로를 명시한 상수
        private static readonly string PERSONAL_FILE_PATH = Path.Combine(
            Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,
            "QuestManagement",
            "QuestsInfo");

        // 복사된 퀘스트정보를 플레이어 이름과 확장자명을 합쳐 완전한 경로를 제공하기 위한 변수
        public string fullPath;

        // 플레이어의 정보를 set할 프로퍼티
        public Player player { get; set; }

        // 불러온 퀘스트정보가 저장될 Quest자료형 리스트
        public static List<Quest>? Quests { get; set; }
        // QuestManager의 Instance
        static private QuestManager? instance;

        // 싱글톤 구현
        public static QuestManager Instance
        {
            get
            {
                // 생성된 인스턴가 없을 때 인스턴스를 생성 후 반환.
                if (instance == null)
                    instance = new QuestManager();
                return instance;
            }
        }

        /// <summary>
        /// 퀘스트 정보를 불러오고, 없을 때는 초기정보를 복사해 새 정보파일을 작성
        /// </summary>
        /// <returns></returns>
        public List<Quest> Load()
        {
            try
            {
                string json;

                // 여러 번 호출될 때, 기존의 리스트와 중복된 정보가 들어가지 않도록, 이미 리스트가 존재할 경우 삭제
                if(Quests != null)
                {
                    Quests = null;
                }

                // 경로를 명시한 상수와 플레이어의 이름정보를 결합해 새파일을 생성할 경로 또는 기존의 정보파일을 탐색할 경로
                fullPath = PERSONAL_FILE_PATH + "_" + player.Name + ".json";

                // json파일 역직렬화시에 사용할 옵션
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true, // 대소문자 무시
                    ReadCommentHandling = JsonCommentHandling.Skip, // 주석 허용
                    AllowTrailingCommas = true, // 마지막 쉼표 허용
                    Converters = { new JsonStringEnumConverter() },

                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.All)
                };


                if (!File.Exists(fullPath)) // 지정된 경로에 파일이 없을 때 
                {
                    File.Copy(ORIGIN_FILE_PATH, fullPath); // 초기 퀘스트정보 복사 후 새경로에 새이름으로 저장
                    json = File.ReadAllText(fullPath, Encoding.UTF8); // UTF-8 형식으로 string 자료형 변수에 저장
                }
                else // 있을 때
                {
                    json = File.ReadAllText(fullPath, Encoding.UTF8); // UTF-8 형식으로 string 자료형 변수에 저장
                }

                Quests = JsonSerializer.Deserialize<List<Quest>>(json, options); // 시리얼화 -> 퀘스트 리스트에 추가 (각 프로퍼티에 자동 적용)

                return Quests; // 생성한 리스트를 반환
            }
            // 받아온 패스에 파일이 없을 때 메시지 출력
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message); // 에러메시지 출력

                return null;
            }
            // 파일읽기에 실패했을 때 메시지 출력
            catch (FileLoadException ex)
            {
                Console.WriteLine(ex.Message); // 에러메시지 출력

                return null;
            }
        }

        /// <summary>
        /// 전달받은 퀘스트의 아이디정보를 이용해 선택된 퀘스트의 수락여부를 입력받음 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="player"></param>
        public void AcceptQuest(byte id, Player player)
        {
            int idx = 0;
            string[] menu = ["수락", "거절"];
            bool isActive = true;
            bool isComplete = false;
            bool isNoProgress = false;

            ConsoleKeyInfo key = new ConsoleKeyInfo(); // 방향키를 입력받을 ConsolKey정보
            Quest? q = Quests.FirstOrDefault(x => x.QuestID == id); // 퀘스트id로 선택된 퀘스트 정보저장


            if (q.Status != QuestStatus.Complete)
            {
                while (isActive)
                {
                    if (q.Status != QuestStatus.Complete)
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
                                Console.Clear();
                                break;
                        }
                    }
                }
                isNoProgress = QuestStatusCheck(q, menu, idx, player, isComplete);
            }
            else
            {
                isComplete = true;
                isNoProgress = QuestStatusCheck(q, menu, idx, player, isComplete);
            }

            Thread.Sleep(2000);
            if (isNoProgress) SaveQuestProgress(Quests);
            return;
        }

        /// <summary>
        /// 입력받은 정보와 메뉴의 텍스트배열, 선택된 퀘스트내용, 플레이어 정보, 완료여부 정보를 참조하여 수락가능여부를 판단후 메시지출력. 완료된 퀘스트는 보상 관련 메서드를 호출하여 보상지급
        /// </summary>
        /// <param name="q"></param>
        /// <param name="menu"></param>
        /// <param name="idx"></param>
        /// <param name="player"></param>
        /// <param name="isComplete"></param>
        /// <returns></returns>
        public static bool QuestStatusCheck(Quest q, string[] menu, int idx, Player player, bool isComplete)
        {
            if (!isComplete)
            {
                if (q != null && q.Status == QuestStatus.NoProgress && menu[idx] == "수락")
                {
                    q.Status = QuestStatus.InProgress;
                    Console.WriteLine($"[퀘스트를 수락하셨습니다.] {q.QuestTitle}");
                    Console.WriteLine("퀘스트 확인창으로 돌아갑니다.");

                    return true;
                }
                else if (q != null && q.Status == QuestStatus.InProgress && menu[idx] == "수락")
                {
                    Console.WriteLine($"[해당 퀘스트는 이미 수행 중입니다.] {q.QuestTitle}");
                    Console.WriteLine("퀘스트 확인창으로 돌아갑니다.");

                    return false;
                }
                else
                {
                    Console.WriteLine($"{q.QuestTitle} \n [퀘스트를 거절하셨습니다.]");
                    Console.WriteLine("퀘스트 확인창으로 돌아갑니다.");

                    return false;
                }
            }
            else
            {
                Console.WriteLine($"[완료된 퀘스트입니다.] {q.QuestTitle}");
                Console.WriteLine($"[플레이어에게 보상이 지급됩니다.]");
                QuestManager.Instance.GiveReward(player);
                Console.WriteLine("퀘스트 확인창으로 돌아갑니다.");

                return true;
            }
        }

        /// <summary>
        /// 처치한 몬스터의 이름정보를 매개변수로 전달받아 퀘스트 조건과 대조하여 일치시 진행상황을 업데이트 후 데이터 저장 메서드호출
        /// </summary>
        /// <param name="target"></param>
        public void UpdateProgress(string target)
        {

            if (Quests == null)
            {
                Quests = Load();
            }
            // 변수 q 에 진행중인 리스트를 순차적으로 저장 
            foreach (var q in Quests.Where(x => x.Status == QuestStatus.InProgress))
            {
                // 저장된 퀘스트의 완료조건 체크, 진행상황 반영, 조건 충족시 완료 상태로 변경
                foreach (var o in q.objectives)
                {
                    if (o.Target == target)
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
            }

            SaveQuestProgress(Quests);
        }

        /// <summary>
        /// 완료된 퀘스트의 보상을 플레이어에게 지급
        /// </summary>
        internal void GiveReward(Player player)
        {
            foreach (var q in Quests.Where(x => x.Status == QuestStatus.Complete))
            {
                // 플레이어의 경험치, 돈 변경 로직 추가
                player.GainExp(q.Rewards.Exp);
                Console.WriteLine($"{q.Rewards.Gold}G 획득.");
                player.Gold += q.Rewards.Gold;
                // 보상 아이템을 순차적으로 인벤토리에 추가
                ItemManagement item; // string배열 Rewards.Items의 멤버를 아이템형식으로 변환후 넣을 ItemManagement자료형
                for (int i = 0; i < q.Rewards.Items.Count; i++)
                {

                    item = ItemInfo.GetItem(q.Rewards.Items[i]);
                    if (item != null)
                    {
                        if (item is Potion)
                        {
                            Console.WriteLine($"{item.Name}이 인벤토리에 추가됩니다.");
                            player.Inventory.AddItem(item, 1);
                        }
                        else
                        {
                            Console.WriteLine($"{item.Name}이 인벤토리에 추가됩니다.");
                            player.Inventory.AddItem(item);
                        }
                    }
                }

                q.Status = QuestStatus.Done; // 보상을 한번만 받을 수 있도록 설정하고 같은 퀘스트를 반복할 수 없게 하기 위해 enum의 값
            }

        }

        /// <summary>
        /// 변경된 퀘스트정보의 리스트를 매개변수로 입력받아 기존 데이터를 덮어씌워 저장
        /// </summary>
        /// <param name="quests"></param>
        public void SaveQuestProgress(List<Quest> quests)
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
            File.WriteAllText(fullPath, json, new UTF8Encoding(false)); // json파일작성 (순수 UTF8 형식)
        }

    }
}
