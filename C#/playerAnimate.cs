using UnityEngine;
using System.Collections;

public class playerAnimate : MonoBehaviour
{


    public Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("jump",-1,0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.Play("jump", -1, 0f);
        }
    }
}
