using UnityEngine;
using System.Collections;

public class damagescac : MonoBehaviour
{
    private float counter = 0.0f;
    // Use this for initialization
    private int damages;
    void Start()
    {
        damages = gameObject.GetComponentInParent<Move_destination>().damages_M;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player playerScript = other.GetComponent<Player>();
            if (counter>1.0f)
            {
                playerScript.health -= damages;
                counter = 0.0f;
                if (playerScript.health <= 0)
                    playerScript.died = true;

            }
            
            

        }
    }
}
