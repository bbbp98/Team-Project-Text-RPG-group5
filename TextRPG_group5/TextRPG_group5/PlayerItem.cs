using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_group5
{
    using TextRPG_group5.ItemManagement;
    public class PlayerItem
    {
        private Player player;
        private bool IsJobMatch(Class job)
        {
            return (player.Job == "전사" && job == Class.Warrior)
            || (player.Job == "마법사" && job == Class.Magician)
            || job == Class.All;
        }
        public Weapon? EquippedWeapon { get; private set; }
        public Armor? EquippedArmor { get; private set; }
        public PlayerItem(Player player)
        { this.player = player; }
        public void EquipItem(EquipItem item)
        {
            if (item is Weapon weapon)
            {
                if (EquippedWeapon != null)
                {
                    Console.WriteLine($"{EquippedWeapon.Name}을(를) 해제합니다.");
                    UnEquipItem(EquippedWeapon);
                }
                if (!IsJobMatch(weapon.Job))
                {
                    Console.WriteLine($"[{weapon.Name}]은(는) {weapon.Job} 전용 무기 입니다.");
                    return;
                }
                EquippedWeapon = weapon;
                player.Attack += weapon.AtkPower;
                player.AddCritical(weapon.CriPro / 100);
                weapon.IsEquip = true;
                Console.WriteLine($"{weapon.Name}을(를) 장착했습니다.");
            }
            else if (item is Armor armor)
            {
                if (EquippedArmor != null)
                {
                    Console.WriteLine($"{EquippedArmor.Name}을(를) 해제합니다.");
                    UnEquipItem(EquippedArmor);
                }
                if (!IsJobMatch(armor.Job))
                {
                    Console.WriteLine($"[{armor.Name}]은(는) {armor.Job} 전용 방어구 입니다.");
                    return;
                }
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
