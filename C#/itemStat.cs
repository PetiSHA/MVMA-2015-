using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class itemStat : MonoBehaviour {

    
    [SerializeField]
    int stamina;
    [SerializeField]
    int life;
    [SerializeField]
    int mana;
    [SerializeField]
    int armor;
    [SerializeField]
    int damages;
    public int gold;
    public int moneyPlayer;
    public Text textToRemplace;
    public bool bought = false;
    public Transform toShow;
    public Transform toHide;
    public Button toChange;
    public Button Original;

    public void setTextOfItem()
    {
        textToRemplace.text = "Do you want to buy this item for " + gold + 
            " golds ? \n \n(" + life + ") life \n(" + mana + ") mana \n(" +
            stamina + ") stamina \n(" + damages + ") damage(s) \n(" + armor +
            ") armor ";
    }

    public void DisplayOrNo()
    {
        if (bought)
        {
            toChange.GetComponent<Image>().sprite = Original.GetComponent<Image>().sprite;
            toChange.spriteState = Original.spriteState;
            toChange.GetComponent<itemStat>().stamina = Original.GetComponent<itemStat>().stamina;
            toChange.GetComponent<itemStat>().mana = Original.GetComponent<itemStat>().mana;
            toChange.GetComponent<itemStat>().life = Original.GetComponent<itemStat>().life;
            toChange.GetComponent<itemStat>().damages = Original.GetComponent<itemStat>().damages;
            toChange.GetComponent<itemStat>().armor = Original.GetComponent<itemStat>().armor;

        }
        else
        {
            toHide.gameObject.SetActive(false);
            toShow.gameObject.SetActive(true);

        }
    }

    public int GetLife()
    {
        return life;
    }
    public int GetMana()
    {
        return mana;
    }
    public int GetStamina()
    {
        return stamina;
    }
    public int GetArmor()
    {
        return armor;
    }
    public int GetDamages()
    {
        return damages;
    }

    public void DoBuy()
    {
        
    }

}
