using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    // 장비아이템 중 무기류를 관리할 메서드에 추상클래스 EquipItem 상속 후 재정의
    internal class Weapon : EquipItem
    {
        public int AtkPower { get; protected set; }
        public int MagicPower { get; protected set; }
        public double CriPro { get; protected set; }
        public Class Job { get; protected set; }

        public Weapon(string name, string description, int atkPower, int magicPower, double criPro, int price, int job, bool isEquip)
        {
            string jobLimits;

            Name = name;
            Job = (Class)job;
            AtkPower = atkPower;
            MagicPower = magicPower;
            CriPro = criPro;

            if((int)Job == 1)
            {
                jobLimits = "전사";
            }
            else if((int)Job == 2)
            {
                jobLimits = "마법사";
            }
            else if((int)Job == 3)
            {
                jobLimits = "궁수";
            }
            else if ((int)Job == 4)
            {
                jobLimits = "도적";
            }
            else
            {
                jobLimits = "공용";
            }

            Description = $"스탯증가량 => 공격력 : +{AtkPower}, 마력: {MagicPower} 치명타확률 : +{CriPro}, 장착 가능 직업 : {jobLimits}";
            IsEquip = isEquip;
        }

        public override void YouEquipItem(Player player, Weapon weapon) // ToDo : 매개변수로 플레이어 오브젝트를 할당해야함
        {
            if (weapon.Job.ToString() == player.Job)
            {
                player.AddCritical(CriPro);
                player.Attack += weapon.AtkPower;
                // ToDo : 마법공격력 증가 메서드 필요
            }
            else
            {
                Console.WriteLine($"{player.Job} 직업군은 {weapon.Job} 직업군의 무기를 장착하실 수 없습니다. 다시 선택해주세요");
                return;
            }

        }
    }
}
