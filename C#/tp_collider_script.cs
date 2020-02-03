using UnityEngine;
using System.Collections;

public class tp_collider_script : MonoBehaviour {

    [SerializeField]
     private Vector3 targetPosition;
    [SerializeField]
    private float SyncRotationY;


    void OnTriggerEnter(Collider other)
    {

        other.transform.position = targetPosition;
        other.transform.rotation = Quaternion.Euler(0, SyncRotationY, 0);
    }
}
