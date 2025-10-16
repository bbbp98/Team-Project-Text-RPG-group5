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
        const int REQUIRED = 0;
        private Player player;

        public QuestScene(Player player)
        {
            this.player = player;
        }
        public override void HandleInput(byte input)
        {
            QuestManager.AcceptQuest(input);

            Console.ReadKey();
        }

        public override void Show()
        {
            bool isActive = true;
            byte questIdx = 0;
            byte id = 0;

            List<Quest> quests = new List<Quest>();
            quests = QuestManager.Load();


            while (isActive)
            {
                if (questIdx == quests.Count) questIdx = 0;
                else if (questIdx < 0) questIdx = 0;

                Console.WriteLine($"{quests[questIdx].QuestID} : {quests[questIdx].QuestTitle}");
                Console.WriteLine($"{quests[questIdx].QuestDescription}");
                Console.WriteLine($"조건 : {quests[questIdx].objectives[REQUIRED].Target} {quests[questIdx].objectives[REQUIRED].Count}마리 {quests[questIdx].objectives[REQUIRED].Type}");
                Console.WriteLine($"보상 : {quests[questIdx].Rewards.Exp}EXP, {quests[questIdx].Rewards.Gold}G, {string.Join(", ",(quests[questIdx].Rewards.Items))}");
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
                    Console.WriteLine("이미 완료한 퀘스트입니다.");
                }


                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.RightArrow:
                        questIdx++;
                        Console.Clear();
                        break;
                    case ConsoleKey.LeftArrow:
                        if (questIdx == 0)
                        {
                            Console.Clear();
                            break;
                        }
                        questIdx--;
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        HandleInput((byte)(questIdx + 1));
                        isActive = false;
                        Console.Clear();
                        break;
                    default:
                        questIdx = 0;
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
