using UnityEngine;
using System.Collections;

public class XPworthMonster : MonoBehaviour
{
    private int xPMonster;
    // Use this for initialization
    void Start()
    {
        xPMonster = gameObject.GetComponentInParent<Move_destination>().xPValue;
        gameObject.GetComponent<TextMesh>().text = xPMonster + " XP";
    }


}
