using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class bullet : NetworkBehaviour
{

    [SerializeField]
    private float speed = 20.0f;

    [SyncVar]
    public float syncYRotation;
    [SyncVar]
    public NetworkInstanceId idPlayer;

    private Transform myTransform;

    [SerializeField]
    private Vector3 SpawnChest;
    [SerializeField]
    private GameObject Chest;

    // Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTransform.eulerAngles = new Vector3(0, syncYRotation, 0);
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
                    Player playe = NetworkServer.objects[idPlayer].GetComponent<Player>();
                    int dmg = 0;
                    if (playe != null)
                        dmg = playe.getDamageBoost();
                    int whatToSubit = 40 + dmg - playerScript.getArmorBoost();
                    if (whatToSubit > 0)
                        playerScript.subit_damages(whatToSubit, 1);


                    if (playerScript.health <= 0)
                    {
                        if (playe != null)
                            NetworkServer.objects[idPlayer].GetComponent<Player>().experience += playerScript.Level * 20;
                    }


                    Destroy(gameObject);
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
                    enemyScript.health_M -= 60;

                    if (enemyScript.health_M <= 0)
                    {
                        SpawnChest = hit.GetComponent<Transform>().position;
                        NetworkServer.objects[idPlayer].GetComponent<Player>().experience += enemyScript.xPValue;
                        if (enemyScript.corpAcorps)
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