using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    enum BattleState
    {
        None,
        NormalAttack,
        Skill,
        Item
    }
    
    internal class Battle
    {
        Player player;
        List<Monster> monsters;

        private BattleState state = 0;

        bool isPlayerTurn;
        bool isEnemyTurn;

        public Battle(Player player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;

            // Player 선공
            isPlayerTurn = true;
            isEnemyTurn = false;
        }

        public void SetBattleState(byte input)
        {
            switch (input)
            {
                case 1:
                    state = BattleState.NormalAttack;
                    break;
                case 2:
                    state = BattleState.Skill;
                    break;
                case 3:
                    state = BattleState.Item;
                    break;
                default:
                    break;
            }
        }

        public BattleState GetBattleState()
        {
            return state;
        }
        
        public void StartBattle()
        {
            Console.WriteLine("전투 시작!\n");
        }

        public void SelectTarget()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {monsters[i].Name}");
            }
        }

        public void HitNormalAttack()
        {
            Console.WriteLine("일반 공격 사용!");
        }

        public void UseSkill()
        {
            Console.WriteLine("스킬 사용!");
        }

        public void UseItem()
        {
            Console.WriteLine("아이템 사용!");
        }
    }
}
