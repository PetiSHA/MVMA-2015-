using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDInventoryAndPlayerGOLDTransfer : MonoBehaviour
{
    [SerializeField]
    int GOLDtransfer=0;

    public Button buTTON;

    [SerializeField]
    Text notEnoughGold;
    [SerializeField]
    Text successs;
    [SerializeField]
    Transform putOutt;
    [SerializeField]
    Transform show;


    void Start()
    {
        //GOLDtransfer = 2;
        //SetText();
    }

    public int GetGoldInventory()
    {
        return GOLDtransfer;
    }
    public void SetGoldInventory(int amount)
    {
        GOLDtransfer = amount;
    }

    public void SetText()
    {
        gameObject.GetComponent<Text>().text = GOLDtransfer.ToString();
    }

    public void GetInterrestingButton(Button Buttooon)
    {
        buTTON = Buttooon;
        
        
    }

    public void DoBuy()
    {
        int price = buTTON.GetComponent<itemStat>().gold;
        if (!buTTON.GetComponent<itemStat>().bought)
        {

            if (GOLDtransfer - price >= 0)
            {
                GOLDtransfer -= price;
                buTTON.GetComponent<itemStat>().bought = true;
                SetText();
                successs.text = "SUCCESS !";
                Invoke("ResetSucess", 2.5f);
                putOutt.gameObject.SetActive(false);
                show.gameObject.SetActive(true);
            }
            else
            {
                notEnoughGold.text = "NOT ENOUGH GOLD";
                Invoke("ResetNoGold", 2.5f);
            }
        }
    }

    void ResetNoGold()
    {
        notEnoughGold.text = "";
    }
    void ResetSucess()
    {
        successs.text = "";
    }

    public void GetSceneOut(Transform tr)
    {
        putOutt = tr;
    }

    public void GetSceneIn(Transform tri)
    {
        show = tri;
    }
}
