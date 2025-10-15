using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5.ItemManagement
{
    // 장비아이템 중 무기류를 관리할 메서드에 추상클래스 EquipItem 상속 후 재정의
    public class Weapon : EquipItem
    {
        public int AtkPower { get; protected set; }
        public int CriPro { get; protected set; }
        public Class Job { get; protected set; }

        public Weapon(string name, string description, int atkPower, int criPro, int price, int job, bool isEquip)
        {
            string jobLimits;

            Name = name;
            Job = (Class)job;
            AtkPower = atkPower;
            CriPro = criPro;

            if((int)Job == 1)
            {
                jobLimits = "전사";
            }
            else
            {
                jobLimits = "마법사";
            }

            Description = $"스탯증가량 => 공격력 : +{AtkPower}, 치명타확률 : +{CriPro}, 장착 가능 직업 : {jobLimits}";
            IsEquip = isEquip;
        }

        public override void YouEquipItem() // ToDo : 매개변수로 플레이어 오브젝트를 할당해야함
        {
            // ToDo : 아이템 장착 로직 추가 필요
        }
    }
}
