using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class spawnScript : NetworkBehaviour
{

    [SerializeField]
    private GameObject low_level_monster;
    [SerializeField]
    private GameObject mid_level_monster;
    [SerializeField]
    private GameObject high_level_monster;
    [SerializeField]
    private Vector3 spawn1Monster = new Vector3(30, 201, 471);
    [SerializeField]
    private Vector3 spawn2Monster = new Vector3(33, 201, 475);
    [SerializeField]
    private Vector3 spawn3Monster = new Vector3(22, 201, 473);
    [SyncVar]
    private float timering;
    // Use this for initialization
    void Start()
    {
        timering = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
        timering += Time.deltaTime;

        if ( timering > 30.0f)
        {
            int rdn = Random.Range(0, 3);
            choose_Monster(rdn);
            timering = 0.0f;
        }


    }

    [Command]
    void CmdInstantiateAMonster1()
    {
        GameObject Monster = Instantiate(low_level_monster, spawn1Monster, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(Monster);
        Destroy(Monster, 180.0f);
    }
    [Command]
    void CmdInstantiateAMonster2()
    {
        GameObject Monster = Instantiate(mid_level_monster, spawn2Monster, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(Monster);
        Destroy(Monster, 180.0f);
    }
    [Command]
    void CmdInstantiateAMonster3()
    {
        GameObject Monster = Instantiate(high_level_monster, spawn3Monster, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(Monster);
        Destroy(Monster, 180.0f);
    }

    private void choose_Monster(int choice)
    {
        switch(choice)
        {
            case 0:
                CmdInstantiateAMonster1();
                break;
            case 1:
                CmdInstantiateAMonster2();
                break;
            case 2:
                CmdInstantiateAMonster3();
                break;
            default:
                CmdInstantiateAMonster1();
                break;
        }
    }
}
