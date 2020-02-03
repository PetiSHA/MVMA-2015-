using UnityEngine;
using System.Collections;

public class DIsplayhealthtext : MonoBehaviour {

    private int health;
    private int healthMax;
	// Use this for initialization
	void Start () {
        health = gameObject.GetComponentInParent<Move_destination>().health_M;
        healthMax = gameObject.GetComponentInParent<Move_destination>().healthMax_M;

    }
	
	// Update is called once per frame
	void Update () {
        health = gameObject.GetComponentInParent<Move_destination>().health_M;
        gameObject.GetComponent<TextMesh>().text = "Health : "+ health.ToString()+"/"+healthMax.ToString();
              
    }
}
