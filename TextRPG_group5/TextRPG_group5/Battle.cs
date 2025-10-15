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

          public byte userChoice;

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

          public void SetBattleState(BattleState currentState)
          {
               state = currentState;
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

               Console.WriteLine();
               Console.WriteLine("0. 취소");
          }

          public void SelectSkill()
          {
               Console.WriteLine("[1] 스킬 1번");
               Console.WriteLine("[2] 스킬 2번");
               Console.WriteLine("[3] 스킬 3번");

               Console.WriteLine();
               Console.WriteLine("0. 취소");
          }

          public void SelectUsableItem()
          {
               Console.WriteLine("[1] 소비 아이템 1번");
               Console.WriteLine("[2] 소비 아이템 2번");
               Console.WriteLine("[3] 소비 아이템 3번");

               Console.WriteLine();
               Console.WriteLine("0. 취소");
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
