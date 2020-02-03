using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour
{

    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform follow;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
        follow = GameObject.FindWithTag("whatCameraFollow").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
       // Debug.DrawRay(follow.position, Vector3.up * distanceUp, Color.red);
        //Debug.DrawRay(follow.position, -1.0f * follow.forward * distanceAway, Color.blue);
        //Debug.DrawLine(follow.position, targetPosition, Color.magenta);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        transform.LookAt(follow);
    }
}
