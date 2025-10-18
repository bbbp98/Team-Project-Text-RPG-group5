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
        //public JobClass Job { get; set; }

        public Weapon(string name, string description, int atkPower, double criPro, int price, int job, bool isEquip)
        {
            string jobLimits;

            Name = name;
            Job = (JobClass)job;
            AtkPower = atkPower;
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

               Price = price;
            Description = $"스탯 증가량 => 공격력 : +{AtkPower}, 치명타확률 : +{CriPro}, 장착 가능 직업 : {jobLimits}";
            IsEquip = isEquip;
        }
    }
}
