using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TextRPG_group5.EffectManagement;

namespace TextRPG_group5
{
     internal class PlayerSkill
     {
          private Player player;

          public List<SkillData> skillBook { get; set; } = new List<SkillData>();

          public PlayerSkill(Player player)
          {
               this.player = player;
               //InitializeSkills();
          }
          public void SetOwner(Player player)
          { 
            this.player = player;
            foreach (var skill in skillBook)
            {
                switch (skill.Name)
                {
                    case "파워 슬래시":
                        skill.Action = PowerSlash;
                        break;
                    case "파워 스트라이크":
                        skill.Action = PowerStrike;
                        break;
                    case "헤드 샷":
                        skill.Action = HeadShot;
                        break;
                    case "포이즌 애로우":
                        skill.Action = PoisonArrow;
                        break;
                    case "더블 스텝":
                        skill.Action = DoubleStep;
                        break;
                    case "블러드 스텝":
                        skill.Action = BloodStep;
                        break;
                    case "파이어 볼":
                        skill.Action = FireBall;
                        break;
                    case "프리징 브레스":
                        skill.Action = FreezingBreath;
                        break;
                }
            }
        }

        public void InitializeSkills()
          {
               switch (player.Job)
               {
                    case "전사":
                         skillBook.Add(new SkillData("파워 슬래시", 20, PowerSlash));
                    skillBook.Add(new SkillData("파워 스트라이크,", 30, PowerStrike));
                         break;
                    case "궁수":
                         skillBook.Add(new SkillData("헤드 샷", 20, HeadShot));
                    skillBook.Add(new SkillData("포이즌 애로우", 30, PoisonArrow));
                         break;
                    case "도적":
                         skillBook.Add(new SkillData("더블 스텝", 15, DoubleStep));
                    skillBook.Add(new SkillData("블러드 스텝", 30, BloodStep));
                         break;
                    case "마법사":
                         skillBook.Add(new SkillData("파이어 볼", 30, FireBall));
                    skillBook.Add(new SkillData("프리징 브레스", 40, FreezingBreath));
                         break;

               }
          }
          public void ShowSkill()
          {
               Console.WriteLine("====  보유 스킬  ====");
               foreach (var skill in skillBook)
                    Console.WriteLine($"={skill.Name}=");
               Console.WriteLine("====================");
          }
          public void UseSkill(int index, Character target)
          {
               if (index < 0 || index >= skillBook.Count)
               {
                    Console.WriteLine("잘못된 스킬 선택입니다.");
                    return;
               }

               if (target == null)
               {
                    Console.WriteLine("타겟이 존재하지 않습니다.");
                    return;
               }

               skillBook[index].Action(target);
          }
          private void PowerSlash(Character target)
          {
               int mpCost = 20;
               if (player.NowMp < mpCost)
               {
                    Console.WriteLine("MP가 부족합니다");
                    return;
               }
               player.NowMp -= mpCost;
               int damage = player.Attack * 2;
               target.TakeDamage(damage, 0, true);
               Console.WriteLine($"파워 슬래시 사용.{target.Name} 에게 {damage} 의 피해를 입힘");
          }
        private void PowerStrike(Character target)
        {
            int mpCost = 30;
            if (player.NowMp < mpCost)
            {
                Console.WriteLine("MP가 부족합니다");
                return;
            }
            player.NowMp -= mpCost;
            int damage = player.Attack * 3;
            target.TakeDamage(damage, 0, true);
            target.ApplyEffect(new Stun(player, 1));
            Console.WriteLine($"파워 스트라이크 사용.{target.Name} 에게 {damage} 의 피해를 입히고 1턴 동안 스턴 부여");
        }
          private void HeadShot(Character target) // 크리 터짐
          {
               int mpCost = 20;
               if (player.NowMp < mpCost)
               {
                    Console.WriteLine("MP가 부족합니다");
                    return;
               }
               player.NowMp -= mpCost;
               int damage = player.Attack * 3;
               target.TakeDamage(damage, player.Critical, true);
               Console.WriteLine($"헤드 샷 사용.{target.Name} 에게 {damage} 의 피해를 입힘");
          }
        private void PoisonArrow(Character target) // 크리 터짐
        {
            int mpCost = 30;
            if (player.NowMp < mpCost)
            {
                Console.WriteLine("MP가 부족합니다");
                return;
            }
            player.NowMp -= mpCost;
            int damage = player.Attack * 3;
            target.TakeDamage(damage, player.Critical, true);
            target.ApplyEffect(new Poison(player, 3));
            Console.WriteLine($"포이즌 애로우 사용.{target.Name} 에게 {damage} 의 피해를 입히고 3턴 동안 중독 부여");
        }
        private void DoubleStep(Character target) // 2회공격 크리터짐
          {
               int mpCost = 15;
               if (player.NowMp < mpCost)
               {
                    Console.WriteLine("MP가 부족합니다");
                    return;
               }
               player.NowMp -= mpCost;
               int damage = player.Attack * 1;
               target.TakeDamage(damage, player.Critical, true);
               Console.WriteLine($"더블 스텝 사용.");
               Console.WriteLine($"{target.Name}에게 {damage} 의 피해를 입힘");
               target.TakeDamage(damage, player.Critical);
               Console.WriteLine($"{target.Name}에게 {damage} 의 피해를 입힘");
          }
        private void BloodStep(Character target) // 크리 터짐
        {
            int mpCost = 30;
            if (player.NowMp < mpCost)
            {
                Console.WriteLine("MP가 부족합니다");
                return;
            }
            player.NowMp -= mpCost;
            int damage = player.Attack * 3;
            target.TakeDamage(damage, player.Critical, true);
            target.ApplyEffect(new Poison(player, 3));
            Console.WriteLine($"포이즌 애로우 사용.{target.Name} 에게 {damage} 의 피해를 입히고 3턴 동안 출혈 부여");
        }
          private void FireBall(Character target)
          {
               int mpCost = 30;
               if (player.NowMp < mpCost)
               {
                    Console.WriteLine("MP가 부족합니다");
                    return;
               }
               player.NowMp -= mpCost;
               int damage = player.Attack * 4;
               target.TakeDamage(damage, 0, true);
               Console.WriteLine($"파이어 볼 사용. {target.Name}에게 {damage} 의 피해를 입힘");
          }
        private void FreezingBreath(Character target)
        {
            int mpCost = 40;
            if (player.NowMp < mpCost)
            {
                Console.WriteLine("MP가 부족합니다");
                return;
            }
            player.NowMp -= mpCost;
            int damage = player.Attack * 4;
            target.TakeDamage(damage, 0, true);
            target.ApplyEffect(new Freeze(player, 3, 0.5));
            Console.WriteLine($"프리징 브레스 사용. {target.Name}에게 {damage} 의 피해를 입히고 1턴간 빙결 부여");
        }


    }
     internal class SkillData //UseSkill(int index, Character target)로도 가능
     {
          public string Name { get; }
          public int MpCost { get; }
          [JsonIgnore]
          public Action<Character> Action { get; set; }

          public SkillData(string name, int mpCost, Action<Character> action)
          {
               Name = name;
               Action = action;
               MpCost = mpCost;
          }
     }
}
