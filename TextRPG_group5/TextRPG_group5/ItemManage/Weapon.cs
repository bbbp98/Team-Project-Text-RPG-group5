using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManage
{
    // 장비아이템 중 무기류를 관리할 메서드에 추상클래스 EquipItem 상속 후 재정의
    internal class Weapon : EquipItem
    {
        public int AtkPower { get; set; }
        public double CriPro { get; set; }

        /// <summary>
        /// 무기의 공격력, 치명타 확률을 세팅하고, int형 변수를 ClassJob(Enum)형으로 명시적 변환을 실행하여 착용가능 직업 설정
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="atkPower"></param>
        /// <param name="criPro"></param>
        /// <param name="price"></param>
        /// <param name="job"></param>
        /// <param name="isEquip"></param>
        public Weapon(string name, string description, int atkPower, double criPro, int price, int job, bool isEquip)
        {
            string jobLimits;

            Name = name;
            Job = (JobClass)job;
            AtkPower = atkPower;
            CriPro = criPro;

            if (Job == JobClass.Warrior)
            {
                jobLimits = "전사";
            }
            else if (Job == JobClass.Magician)
            {
                jobLimits = "마법사";
            }
            else if (Job == JobClass.Archer)
            {
                jobLimits = "궁수";
            }
            else if (Job == JobClass.Thief)
            {
                jobLimits = "도적";
            }
            else
            {
                jobLimits = "공용";
            }

            Price = price;
            Description = $"스탯 증가량 => 공격력 : +{AtkPower}, 치명타확률 : +{CriPro}, 장착 가능 직업 : {jobLimits}";
            IsEquip = isEquip;
        }
    }
}
