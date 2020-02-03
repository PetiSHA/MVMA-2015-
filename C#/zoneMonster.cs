using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class zoneMonster : NetworkBehaviour
{
    [SyncVar]
    Player ply;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player playerScript = other.GetComponent<Player>();

            Vector3 PlayerPosition = playerScript.transform.position;
            gameObject.GetComponentInParent<Move_destination>().goal = PlayerPosition;
            gameObject.GetComponentInParent<Move_destination>().focusOnPlayer = true;
            // gameObject.GetComponentInParent<NavMeshAgent>().SetDestination(PlayerPosition);
            // gameObject.GetComponentInParent<NavMeshAgent>().destination=PlayerPosition;

        }
    }
}
