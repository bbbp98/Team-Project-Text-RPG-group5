using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_group5.QuestManagement;

namespace TextRPG_group5.Scenes
{
    internal class QuestScene : Scene
    {
        const int WAIT_SEE = 2000; // 메시지 출력후 바로 사라지지 않기위해 출력 후 지연시킬 시간(단위 : ms)을 코드컨벤션에 따라 상수로 지정
        const int REQUIRED = 0; // 퀘스트리스트의 조건의 리스트의 항목은 하나뿐이므로, 참조할 인덱스는 항상 0이기 때문에 코드 컨벤션에 따라 상수로 지정
        private Player player; // 플레이어 정보를 저장할 변수 선언

        /// <summary>
        /// 퀘스트 씬의 생성자. 생성되면 플레이어 정보를 클래스 멤버에 할당
        /// </summary>
        /// <param name="player"></param>
        public QuestScene(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// 사용자의 입력을 매개변수로 받아 입력에 따라 다음행동을 판단 (매개변수는 잘못된 값 입력시 byte자료형의 최대값으로 설정됨)
        /// </summary>
        /// <param name="input"></param>
        public override void HandleInput(byte input)
        {
            if (input != byte.MaxValue) // 입력이 올바를 때
            {
                if (input != 0)
                {
                    QuestManager.Instance.AcceptQuest(input, player);
                    Console.Clear();
                    Program.SetScene(new QuestScene(player));
                }
                else
                {
                    Console.WriteLine("메인으로 돌아갑니다.");
                    Thread.Sleep(WAIT_SEE);
                    Console.Clear();
                    Program.SetScene(new MainScene(player));
                }
            }
            else // 잘못된 입력일 때
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
            }
        }

        /// <summary>
        /// 퀘스트 정보 출력 담당
        /// </summary>
        public override void Show()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J"); // Console.Clear() 후에도 콘솔 상에 남아있는 버퍼를 지우기 위한 이스케이프 시퀀스

            byte questIdx = 0; // 입력받을 퀘스트ID

            List<Quest> quests = QuestManager.Instance.Load(); // 퀘스트의 정보를 저장한 리스트를 퀘스트매니저의 메서드를 통해 수령
            quests = quests.DistinctBy(q => q.QuestID).ToList(); // 리스트 내의 중복 항목제거

            // 퀘스트를 순서대로 출력
            for (questIdx = 0; questIdx < quests.Count; questIdx++)
            {
                if (quests[questIdx].Status != QuestStatus.Done) // 완료 후 보상까지 수령한 퀘스트는 출력하지 않음
                {
                    Console.WriteLine("--------- 퀘스트 내용 ---------");
                    Console.WriteLine($"{quests[questIdx].QuestID} : {quests[questIdx].QuestTitle}");
                    Console.WriteLine($"{quests[questIdx].QuestDescription}");
                    Console.WriteLine($"조건 : {quests[questIdx].objectives[REQUIRED].Target} {quests[questIdx].objectives[REQUIRED].Count}마리 {quests[questIdx].objectives[REQUIRED].Type}");
                    Console.WriteLine($"보상 : {quests[questIdx].Rewards.Exp}EXP, {quests[questIdx].Rewards.Gold}G, {string.Join(", ", (quests[questIdx].Rewards.Items))}");

                    if (quests[questIdx].Status == QuestStatus.NoProgress) // 아직 받지않은 퀘스트일 때
                    {
                        Console.WriteLine("수락 가능");
                    }
                    else if (quests[questIdx].Status == QuestStatus.InProgress) // 진행 중인 퀘스트일 때
                    {
                        Console.WriteLine("진행중");
                        Console.WriteLine($"현재 : {quests[questIdx].objectives[REQUIRED].Target} {quests[questIdx].objectives[REQUIRED].Current}/{quests[questIdx].objectives[REQUIRED].Count}");
                    }
                    else // 완료 후 보상을 수령받지 않은 퀘스트
                    {
                        Console.WriteLine("완료 (보상 수령 가능)");
                    }

                    Console.WriteLine("\n");
                }
            }

            // 0을 입력받을시 마을로 귀환
            Console.WriteLine("\n 마을로 돌아가기 : 숫자 '0' 입력");
        }
    }
}
