using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
     using TextRPG_group5.ItemManage;
     internal class PlayerItem
     {
          private Player player;

          public Weapon? EquippedWeapon { get; private set; }

          public Armor? EquippedArmor { get; private set; }

          public PlayerItem(Player player)
          { this.player = player; }

          private bool IsJobMatch(JobClass job)
          {
               return (player.Job == "전사" && job == JobClass.Warrior)
               || (player.Job == "마법사" && job == JobClass.Magician)
               || (player.Job == "궁수" && job == JobClass.Archer)
               || (player.Job == "도적" && job == JobClass.Thief);
               //|| job == Class.All;
          }

          public void EquipItem(EquipItem item)
          {
               if (item is Weapon weapon)
               {
                    // 직업 확인 먼저
                    if (!IsJobMatch(weapon.Job))
                    {
                         Console.WriteLine($"[{weapon.Name}]은(는) {weapon.Job} 전용 무기 입니다.");
                         return;
                    }

                    // 만약 장착된 장비가 있으면 기존의 장비 해제 -> 선택 장비 장착
                    if (EquippedWeapon != null)
                         UnEquipItem(EquippedWeapon);

                    EquippedWeapon = weapon;
                    player.Attack += weapon.AtkPower;
                    player.AddCritical(weapon.CriPro);
                    weapon.IsEquip = true;
                    Console.WriteLine($"{weapon.Name}을(를) 장착했습니다.");
               }
               else if (item is Armor armor)
               {
                    if (!IsJobMatch(armor.Job))
                    {
                         Console.WriteLine($"[{armor.Name}]은(는) {armor.Job} 전용 방어구 입니다.");
                         return;
                    }

                    // 만약 장착된 장비가 있으면 기존의 장비 해제 -> 선택 장비 장착
                    if (EquippedArmor != null)
                         UnEquipItem(EquippedArmor);

                    EquippedArmor = armor;
                    player.Defence += armor.DefPower;
                    armor.IsEquip = true;
                    Console.WriteLine($"{armor.Name}을(를) 장착했습니다.");
               }
          }

          public void UnEquipItem(EquipItem item)
          {
               if (item is Weapon weapon && EquippedWeapon == weapon)
               {
                    player.Attack -= weapon.AtkPower;
                    player.AddCritical(-weapon.CriPro / 100);
                    weapon.IsEquip = false;
                    EquippedWeapon = null;
                    Console.WriteLine($"{weapon.Name}을(를) 해제했습니다.");
               }
               else if (item is Armor armor && EquippedArmor == armor)
               {
                    player.Defence -= armor.DefPower;
                    armor.IsEquip = false;
                    EquippedArmor = null;
                    Console.WriteLine($"{armor.Name}을(를) 해제했습니다");
               }
          }

          public void ShowEquipStatus()
          {
               Console.WriteLine("\n=======Equipment=======");
               Console.WriteLine($"Weapon:{(EquippedWeapon != null ? "[E]" + EquippedWeapon.Name : "없음")}");
               Console.WriteLine($"Armor: {(EquippedArmor != null ? "[E]" + EquippedArmor.Name : "없음")}");
               Console.WriteLine("=======================\n");
          }
     }
}
