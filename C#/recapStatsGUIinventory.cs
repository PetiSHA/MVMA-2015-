using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class recapStatsGUIinventory : MonoBehaviour
{
    [SerializeField]
    itemStat hat;
    [SerializeField]
    itemStat armor;
    [SerializeField]
    itemStat weapon;
    [SerializeField]
    itemStat boot;
    [SerializeField]
    itemStat pet;
    [SerializeField]
    itemStat scroll;
    [SerializeField]
    Text tochangee;

    public int lifeSum;
    public int manaSum;
    public int staminaSum;
    public int armorSum;
    public int damageSum;

    public int getlifesum()
    {
        return lifeSum;
    }
    public int getmanasum()
    {
        return manaSum;
    }
    public int getstaminasum()
    {
        return staminaSum;
    }
    public int getarmorsum()
    {
        return armorSum;
    }
    public int getdamagessum()
    {
        return damageSum;
    }

    void DoTheSumm()
    {
        lifeSum = hat.GetLife() + armor.GetLife() + weapon.GetLife() + boot.GetLife() + pet.GetLife() + scroll.GetLife();
        manaSum = hat.GetMana() + armor.GetMana() + weapon.GetMana() + boot.GetMana() + pet.GetMana() + scroll.GetMana();
        staminaSum = hat.GetStamina() + armor.GetStamina() + weapon.GetStamina() + boot.GetStamina() + pet.GetStamina() + scroll.GetStamina();
        armorSum = hat.GetArmor() + armor.GetArmor() + weapon.GetArmor() + boot.GetArmor() + pet.GetArmor() + scroll.GetArmor();
        damageSum = hat.GetDamages() + armor.GetDamages() + weapon.GetDamages() + boot.GetDamages() + pet.GetDamages() + scroll.GetDamages();
    }

    public void SSreturnStringSS()
    {
        DoTheSumm();
        tochangee.text = "Recap: \n\n(" + lifeSum + ") life \n(" + manaSum + ") mana \n(" + staminaSum +
            ") stamina \n(" + armorSum + ") armor \n(" + damageSum+ ") damage(s)";
    }



}
