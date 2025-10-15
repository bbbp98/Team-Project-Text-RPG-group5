using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.Scene
{
    internal class BattleScene : Scene
    {
        Battle bt = new Battle();

        Character player;
        Character monster1;
        Character monster2;
        Character monster3;
        
        public BattleScene(Character player, Character mon1, Character mon2, Character mon3)
        {
            this.player = player;
            monster1 = mon1;
            monster2 = mon2;
            monster3 = mon3;
        }

        // 플레이어 선택별 출력 분기를 위한 변수
        byte choice = 0;
        
        // 화면에 보여줄 텍스트들(Console.Write관련)
        public override void Show()
        {
            Console.Clear();
            
            // 전투 로직 호출 테스트
            bt.StartBattle();
            
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            monster1.ShowStatus();
            monster2.ShowStatus();
            monster3.ShowStatus();
            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level}\t{player.Name} ( 캐릭터직업 )");
            Console.WriteLine($"HP {player.NowHp} / {player.MaxHp}");
            Console.WriteLine();

            switch (choice)
            {
                case 0:
                    Console.WriteLine("1. 일반 공격");
                    Console.WriteLine("2. 스킬 사용");
                    Console.WriteLine("3. 아이템 사용");
                    break;
                case 1:
                    bt.HitNormalAttack();
                    break;
                case 2:
                    bt.UseSkill();
                    break;
                case 3:
                    bt.UseItem();
                    break;
                default:
                    break;
            }
        }

        // 입력 값 처리 메서드
        public override void HandleInput(byte input)
        {
            switch (input)
            {
                case 1:
                    choice = 1;
                    break;
                case 2:
                    choice = 2;
                    break;
                case 3:
                    choice = 3;
                    break;
                default:
                    choice = 0;
                    break;
            }
        }
    }
}
