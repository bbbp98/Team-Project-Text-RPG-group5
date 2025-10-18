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
        const int WAIT_SEE = 2000;
        const int REQUIRED = 0;
        private Player player;

        public QuestScene(Player player)
        {
            this.player = player;
        }
        public override void HandleInput(byte input)
        {
            if (input != byte.MaxValue)
            {
                if (input != 0)
                {
                    QuestManager.AcceptQuest(input, player);
                    Console.Clear();
                    Program.SetScene(new MainScene(player));
                }
                else
                {
                    Console.WriteLine("메인으로 돌아갑니다.");
                    Thread.Sleep(WAIT_SEE);
                    Console.Clear();
                    Program.SetScene(new MainScene(player));
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요");
            }
        }

        public override void Show()
        {
            bool isActive = true;
            byte questIdx = 0;
            byte id = 0;

            List<Quest> quests = new List<Quest>();
            quests = QuestManager.Load();


            for (questIdx = 0; questIdx < quests.Count; questIdx++)
            {
                if (quests[questIdx].Status != QuestStatus.Done)
                {
                    Console.WriteLine("--------- 퀘스트 내용 ---------");
                    Console.WriteLine($"{quests[questIdx].QuestID} : {quests[questIdx].QuestTitle}");
                    Console.WriteLine($"{quests[questIdx].QuestDescription}");
                    Console.WriteLine($"조건 : {quests[questIdx].objectives[REQUIRED].Target} {quests[questIdx].objectives[REQUIRED].Count}마리 {quests[questIdx].objectives[REQUIRED].Type}");
                    Console.WriteLine($"보상 : {quests[questIdx].Rewards.Exp}EXP, {quests[questIdx].Rewards.Gold}G, {string.Join(", ", (quests[questIdx].Rewards.Items))}");
                    if (quests[questIdx].Status == QuestStatus.NoProgress)
                    {
                        Console.WriteLine("수락 가능");
                    }
                    else if (quests[questIdx].Status == QuestStatus.InProgress)
                    {
                        Console.WriteLine("진행중");
                        Console.WriteLine($"현재 : {quests[questIdx].objectives[REQUIRED].Target} {quests[questIdx].objectives[REQUIRED].Current}/{quests[questIdx].objectives[REQUIRED].Count}");
                    }
                    else
                    {
                        Console.WriteLine("완료 (보상 수령 가능)");
                    }
                }
            }

            Console.WriteLine("\n 마을로 돌아가기 : 숫자 '0' 입력");
        }
    }
}
