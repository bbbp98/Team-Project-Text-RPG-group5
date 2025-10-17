using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    internal class PlayerSkill
    {
        private Player player;
        public Dictionary<string, Action<Character>> skillBook = new Dictionary<string, Action<Character>>();
        public PlayerSkill(Player player)
        {
            this.player = player;
            InitializeSkills();
        }
        private void InitializeSkills()
        {
            switch (player.Job)
            {
                case "전사":
                    skillBook.Add("파워 슬래시", PowerSlash);
                    break;
                case "궁수":
                    skillBook.Add("헤드 샷", HeadShot);
                    break;
                case "도적":
                    skillBook.Add("더블 스텝", DoubleStep);
                    break;
                case "법사":
                    skillBook.Add("파이어 볼", FireBall);
                    break;

            }
        }
        public void ShowSkill()
        {
            Console.WriteLine("====  보유 스킬  ====");
            foreach(var skill in skillBook.Keys)
                Console.WriteLine($"={skill}=");
            Console.WriteLine("====================");
        }
        public void UseSkill(string skillName, Character target)
        {
            if(!skillBook.ContainsKey(skillName))
            {
                Console.WriteLine("존재하지 않는 스킬입니다.");
                return;
            }
            if(target == null)
            {
                Console.WriteLine("타겟이 존재하지 않습니다");
                return;
            }
            skillBook[skillName](target);
            }
        private void PowerSlash(Character target)
        {
            int mpCost = 20;
            if (player.NowMp <  mpCost)
            {
                Console.WriteLine("MP가 부족합니다");
                return;
            }
            player.NowMp -= mpCost;
            int damage = player.Attack * 2;
            target.TakeDamage(damage, 0);
            Console.WriteLine($"파워 슬래시 사용.{target.Name} 에게 {damage} 의 피해를 입힘");
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
            target.TakeDamage(damage, player.Critical);
            Console.WriteLine($"헤드 샷 사용.{target.Name} 에게 {damage} 의 피해를 입힘");
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
            target.TakeDamage(damage, player.Critical);
            Console.WriteLine($"더블 스텝 사용.");
            Console.WriteLine($"{target.Name}에게 {damage} 의 피해를 입힘");
            target.TakeDamage(damage, player.Critical);
            Console.WriteLine($"{target.Name}에게 {damage} 의 피해를 입힘");
        }
        private void FireBall(Character target)
        {
            int mpCost = 30;
                if(player.NowMp < mpCost)
            {
                Console.WriteLine("MP가 부족합니다");
                return;
            }
            player.NowMp -= mpCost;
            int damage = player.Attack * 4;
            target.TakeDamage(damage, 0);
            Console.WriteLine($"파이어 볼 사용. {target.Name}에게 {damage} 의 피해를 입힘");
        }
    }
}
