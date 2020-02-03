using UnityEngine;
using System.Collections;

public class levelOfMonster : MonoBehaviour
{
    private int level;
    // Use this for initialization
    void Start()
    {
        level = gameObject.GetComponentInParent<Move_destination>().levelMonster;
        gameObject.GetComponent<TextMesh>().text = "level " + level.ToString();
    }



}
