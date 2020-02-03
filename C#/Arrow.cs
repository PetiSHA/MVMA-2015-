using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Arrow : NetworkBehaviour
{
    [SerializeField]
    private float speed = 20.0f;

    [SyncVar]
    public float syncYRotation2;
    [SyncVar]
    public NetworkInstanceId idPlayer2;

    private Transform myTransform;

    [SerializeField]
    private Vector3 SpawnChest;
    [SerializeField]
    private GameObject Chest;
    // Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.eulerAngles = new Vector3(0, syncYRotation2, 0);

    }

    // Update is called once per frame
    void Update()
    {
        myTransform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }


    void OnTriggerEnter(Collider hit)
    {

        if (hit.gameObject.tag == "Player")
        {
            if (isServer)
            {
                Player playerScript = hit.GetComponent<Player>();

                if (playerScript != null)
                {
                    if (!playerScript.died)
                    {
                        Player playe = NetworkServer.objects[idPlayer2].GetComponent<Player>();
                        int dmg = 0;
                        if (playe != null)
                             dmg = playe.getDamageBoost();
                        
                        int whatToSubit = 30 + dmg - playerScript.getArmorBoost();
                        if (whatToSubit > 0)
                            playerScript.subit_damages(whatToSubit, 0);

                        if (playerScript.health <= 0)
                        {
                            if (playe != null)
                                NetworkServer.objects[idPlayer2].GetComponent<Player>().experience += playerScript.Level * 20;
                        }


                        Destroy(gameObject);

                    }
                }
            }
        }
        if (hit.gameObject.tag == "Monster")
        {
            if (isServer)
            {
                Move_destination enemyScript = hit.GetComponent<Move_destination>();

                if (enemyScript != null)
                {
                    int damageplus = NetworkServer.objects[idPlayer2].GetComponent<Player>().getDamageBoost();
                    enemyScript.health_M -= 60 + damageplus;

                    if (enemyScript.health_M <= 0)
                    {
                        SpawnChest = hit.GetComponent<Transform>().position;
                        NetworkServer.objects[idPlayer2].GetComponent<Player>().experience += enemyScript.xPValue;
                        enemyScript.animi.Play("dying_backwards");
                        enemyScript.health_M = 0;
                        enemyScript.aliveSpeed = 0;
                        killMonster(hit);
                        CmdInstantiateAMonster1();
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
    void killMonster(Collider mo)
    {
        Destroy(mo.gameObject);
    }

    [Command]
    void CmdInstantiateAMonster1()
    {
        GameObject Monster = Instantiate(Chest, SpawnChest, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(Monster);
        Destroy(Monster, 180.0f);
    }
}
