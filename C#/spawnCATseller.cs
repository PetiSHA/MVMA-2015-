using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class spawnCATseller : NetworkBehaviour
{
    [SerializeField]
    private GameObject catSeller;
    
    [SerializeField]
    private Vector3 catSpawnpoint = new Vector3(39, 201, 66);
    // Use this for initialization

    void Start()
    {
        Invoke("CmdInstantiateAseller", 1.0f);
        
    }

    [Command]
    void CmdInstantiateAseller()
    {
        GameObject cat = Instantiate(catSeller, catSpawnpoint, Quaternion.Euler(0,180.0f,0)) as GameObject;
        NetworkServer.Spawn(cat);
        
    }

}
