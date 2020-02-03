using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;


public class Move_destination : NetworkBehaviour
{

    [SyncVar]
    public Animator animi;

    [SerializeField] public
    bool corpAcorps = true;
    public int aliveSpeed = 1;
    private Vector3[] pos;
    private Vector3 distance_base_enmy;
    private NavMeshAgent agent;
    private GameObject pos_zone;
    //private int DistancePermiseMonstreBase = 4;
    //private int AggroCirculaireEnnemy = 8;
    private int i;

    public Vector3 goal;
    public bool focusOnPlayer = false;

    private float TimeChangeDestination = 0.0f;
    private float slowSpeed = 3.0f;
    private float quickSpeed = 8.0f;
    public Transform Transform_M;
    private Renderer Renderer_M;

    private Transform Transform_Player;

    public int levelMonster = 1;
    [SyncVar]
    public int health_M = 100;
    public int healthMax_M = 100;
    public int damages_M = 10;
    public int xPValue = 10;

    [SerializeField]
    private int posXMin = 0;
    [SerializeField]
    private int posXMax = 0;
    [SerializeField]
    private int posYMin = 0;
    [SerializeField]
    private int posYMax = 0;

    void Start()
    {
        animi = GetComponent<Animator>();
        pos_zone = new GameObject("former position");
        pos_zone.transform.position = patrol();
        healthMax_M = (levelMonster - 1) * 50 + health_M;
        health_M = healthMax_M;
        slowSpeed = slowSpeed + 0.15f * (levelMonster - 1.0f);
        quickSpeed = quickSpeed + 0.25f * (levelMonster - 1.0f);
        damages_M = damages_M + 5 * levelMonster;
        xPValue = System.Convert.ToInt32(120 * Mathf.Pow(1.2f, (levelMonster - 1)) - 110);

    }


    // Update is called once per frame
    void Update()
    {
        TimeChangeDestination += Time.deltaTime;

        distance_base_enmy = gameObject.transform.position - pos_zone.transform.position;
        agent = GetComponent<NavMeshAgent>();

        //if (Mathf.Abs((pos_zone.transform.position - goal).x)<= AggroCirculaireEnnemy || Mathf.Abs((pos_zone.transform.position - goal).z) <= AggroCirculaireEnnemy) //zone de detection autour de la zone appartennant a l'entitee
        if (focusOnPlayer)
        {
            if (corpAcorps)
                animi.SetBool("isRunning", true);
            agent.SetDestination(goal); // definition de la destination de l'entitee au player
            gameObject.GetComponent<NavMeshAgent>().speed = quickSpeed * aliveSpeed;
            if (Mathf.Abs(gameObject.GetComponent<NavMeshAgent>().destination.x - gameObject.GetComponent<Transform>().position.x) < 1.5f
                || Mathf.Abs(gameObject.GetComponent<NavMeshAgent>().destination.z - gameObject.GetComponent<Transform>().position.z) < 1.5f)
            {
                aliveSpeed = 0;
                gameObject.GetComponent<NavMeshAgent>().speed = 0;
                if (corpAcorps)
                    animi.Play("shake_gesture");
                Invoke("SpeedToNormal", 1.2f);

            }
            if (TimeChangeDestination > 2.0f)
            {
                focusOnPlayer = false;
                TimeChangeDestination = 0.0f;
            }
        }
        else
        {
            if (corpAcorps)
                animi.SetBool("isRunning", false);
            agent.SetDestination(pos_zone.transform.position); //retour a la position de patrouille
            gameObject.GetComponent<NavMeshAgent>().speed = slowSpeed * aliveSpeed;
            if (Mathf.Abs(distance_base_enmy.x) < 0.5f || Mathf.Abs(distance_base_enmy.z) < 0.5f || TimeChangeDestination > 20.0f) //si la destination est presque atteinte // distance minimale permise monstre destination
            {

                pos_zone.transform.position = patrol(); // Changer la destination de patrouille
                TimeChangeDestination = 0.0f;
            }
        }
    }

    private Vector3 patrol() //le path de patrouille est definie par cette fonction.
    {


        int xRange = Random.Range(posXMin, posXMin);
        int zRange = Random.Range(posYMin, posYMax);
        return new Vector3(xRange, 201, zRange);

    }

    void SpeedToNormal()
    {
        aliveSpeed = 1;
        gameObject.GetComponent<NavMeshAgent>().speed = quickSpeed * aliveSpeed;
    }

}