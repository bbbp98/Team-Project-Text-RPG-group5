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
          public int DefPower { get; protected set; }

          public JobClass Job { get; protected set; }

          public bool isCorrect { get; protected set; }
          public double EvadePro { get; protected set; }
          public Armor(string name, string description, int defPower, double evadePro, int price, int job, bool isEquip)
          {
               string jobLimits;

               Name = name;
               Job = (JobClass)job;
               if ((int)Job == 1)
               {
                    jobLimits = "전사";
               }
               else if ((int)Job == 2)
               {
                    jobLimits = "마법사";
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
