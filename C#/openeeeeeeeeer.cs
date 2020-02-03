using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class openeeeeeeeeer : NetworkBehaviour {

    int gold;
    public NetworkInstanceId idPlayerbox;

    void Start()
    {
        gold = Random.Range(35, 75);
    }

    void OnTriggerEnter(Collider hit)
    {
        Debug.Log("collision0");
        if (hit.gameObject.tag == "Player")
        {
            gameObject.GetComponentInParent<chest_opener>().crossed = true;
            gameObject.GetComponentInParent<ParticleSystem>().enableEmission = false;
            hit.GetComponent<Player>().gold_Adder(gold);
            gold = 0;

        }
    }
}
