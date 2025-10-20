using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scenes
{
    internal class DungeonResultScene : Scene
    {
        private bool isClearStage;
        private int stage;
        private Player player;
        private Player preBattlePlayer;
        private bool isRewardGet = false;
        private class Reward
        {
            public int Gold;
            public int Exp;

            public Reward(int stage)
            {
                Gold = 100 * stage;
                Exp = 15 * stage;
            }
        }

        public DungeonResultScene(Player player, Player preBattlePlayer, int stage, bool isClearStage)
        {
            this.player = player;
            this.preBattlePlayer = preBattlePlayer;
            this.stage = stage;
            this.isClearStage = isClearStage;
        }

        public override void HandleInput(byte input)
        {
            if (!isClearStage)
                return;

            switch (input)
            {
                case 0:
                    if (isClearStage)
                        Program.SetScene(new DungeonEntranceScene(player));
                    //else
                    //     Program.SetScene(new MainScene(player));
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        public override void Show()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("던전 결과\n");
            Console.ForegroundColor = ConsoleColor.White;

            // 던전 클리어
            if (isClearStage)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("           던전 클리어 성공!!!");
                Console.WriteLine("===========================================\n");
                Thread.Sleep(800);
                if (stage == Program.maxStage)
                {
                    Console.WriteLine("마침내, 탑 안에 있던 던전을 모두 공략하고 꼭대기 층에 도달했다.\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("오래된 전설처럼… 내 소원이 이뤄질 시간이 다가온다.\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("탑의 꼭대기, 그곳에 ‘진실'이 있었다.\n");
                    Thread.Sleep(1000);
                    Console.WriteLine("------------------------------------------------------------------------\n");
                    Console.WriteLine("탑의 꼭대기에 오른 위대한 모험가여.");
                    Console.WriteLine("탑을 처음 오를 때의 당신은 어땠는가?");
                    Thread.Sleep(1500);
                    Console.WriteLine("분명 약했을 것이다.\n");
                    Console.WriteLine("하지만 지금의 당신은 어떤가?");
                    Thread.Sleep(1500);
                    Console.WriteLine("나는 장담할 수 있다.\n지금의 당신은 분명히 강하다.\n");
                    Thread.Sleep(500);
                    Console.WriteLine("그래, 당신은 여태까지의 여정을 통해서 강해졌다.");
                    Console.WriteLine("그러니 이제 스스로의 힘으로 소원을 이루길 바란다.\n");
                    Console.WriteLine("------------------------------------------------------------------------\n");
                    Thread.Sleep(2000);
                    Console.WriteLine("·\n·\n·\n·\n·");
                }
                else
                {
                    Console.WriteLine("적이 쓰러지고, 문이 열리는 소리가 들린다.\n");
                    Console.WriteLine("이렇게 한 걸음 더, 소원에 가까워졌다.\n\n");
                    Console.WriteLine("다음 층으로 향하는 길이 열렸다.\n\n");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("               던전 클리어 실패...");
                Console.WriteLine("===========================================\n");
                Thread.Sleep(1000);
                Console.WriteLine("·\n·\n·\n·\n·");
                Thread.Sleep(1000);
                Console.WriteLine("……패배했다.\n");
                Thread.Sleep(1000);
                Console.WriteLine("하지만 나는 다시 도전할 거다.\n");
                Thread.Sleep(1000);
                Console.WriteLine("……그게 몇 번이 되더라도.\n");
                Thread.Sleep(1000);
                Program.SetScene(new MainScene(player));
            }

            if (!isRewardGet)
            {
                isRewardGet = true;
                Result();
            }
            if (isClearStage)
                Console.WriteLine("0. 돌아가기");
        }

        private void Result()
        {
            if (isClearStage)
            {
                Reward reward = new Reward(stage);

                player.Gold += reward.Gold; // Player에서 Exp처럼 Gold를 더할 수 있는 메서드 필요
                player.GainExp(reward.Exp);

                Thread.Sleep(1000);
                Console.WriteLine("[캐릭터 정보]");
                string levelStr = preBattlePlayer.Level.ToString();
                string atkStr = preBattlePlayer.Attack.ToString();
                string defStr = preBattlePlayer.Defence.ToString();

                if (preBattlePlayer.Level != player.Level)
                {
                    levelStr = $"{levelStr} -> {player.Level}";
                    atkStr = $"{atkStr} -> {player.Attack}";
                    defStr = $"{defStr} -> {player.Defence}";
                }

                Console.WriteLine($"이름: {player.Name}");
                Console.WriteLine($"Lv: {levelStr}");
                Console.WriteLine($"HP: {preBattlePlayer.NowHp} -> {player.NowHp}");
                Console.WriteLine($"MP: {preBattlePlayer.NowMp} -> {player.NowMp}");
                Console.WriteLine($"공격력: {atkStr}");
                Console.WriteLine($"방어력: {defStr}");

                Console.WriteLine("\n[획득 아이템]");
                Console.WriteLine($"Gold: {preBattlePlayer.Gold} -> {player.Gold}");
                Console.WriteLine($"EXP: {preBattlePlayer.Exp} -> {player.Exp}");

                if (stage == player.ReachedStage) // update player stage
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("\n최고 층이 갱신되었습니다.");
                    if (player.ReachedStage < Program.maxStage)
                        player.ReachedStage++;
                }
                Console.WriteLine();
            }
            else
            {
                player.NowHp = player.MaxHp;
                Program.SetSkipInput(true);
                Console.Clear();
            }
        }
    }
}
