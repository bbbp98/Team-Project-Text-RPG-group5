using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG_group5.ItemManage
{
    public class Armor : EquipItem
    {
        public int DefPower { get; set; } // 방어구의 방어력수치
        public double EvadePro { get; set; } // 방어구의 회피율수치

        /// <summary>
        /// 입력된 값에 따라 방어구 아이템의 정보 생성
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="defPower"></param>
        /// <param name="evadePro"></param>
        /// <param name="price"></param>
        /// <param name="job"></param>
        /// <param name="isEquip"></param>
        public Armor(string name, string description, int defPower, double evadePro, int price, int job, bool isEquip)
        {
            string jobLimits;

            Name = name;
            Job = (JobClass)job;
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
            DefPower = defPower;
            EvadePro = evadePro;
            Price = price;
            Description = $"스탯 증가량 => 방어력 : +{defPower}, 장착 가능 직업 : {jobLimits}";
            IsEquip = isEquip;
        }
    }
}
