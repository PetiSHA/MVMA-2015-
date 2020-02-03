using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    [SerializeField]
    float mouse_sensibility = 0.4f;

    [SerializeField]
    private float speed = 7.0f;
    private float slow = 1.0f;
    [SerializeField]
    private float rotSpeed = 200.0f;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject arrowPrefab;
    [SerializeField]
    GameObject MonsterPrefab;
    [SyncVar]
    public Animator anim;

    public Transform myTransform;
    private Renderer myRenderer;

    private float inputH;
    private float inputV;
    private bool isRunning;

    [SyncVar]		
	TextMesh DispMyName;		
	[SyncVar]		
	TextMesh DispMyLevel;		
	[SyncVar]


    string myName;
    private Vector3 lastPosition;
    private float lastYRotation;


    public float monsterYOrientation = 0;

    [SyncVar]
    float delay = 0.0f;


    private Transform targetTransform;
    private Transform target2Transform;

    private Text ExpText;
    private Text lifeText;
    private Text manaText;
    private Text weaponText;
    private Text staminaText;
    private Text levelText;
    private Text goldText;
    private float healthBarFloat;


    [SyncVar]
    public Vector3 SyncPosition;
    [SyncVar]
    public float SyncRotationY;
    [SyncVar]
    private Vector3 SyncMonsterPosition;
    [SyncVar]
    private float SyncMonsterRotationY;


    //[SyncVar]
    //private int healthcooldown = 0;
    [SyncVar]
    private int manaCooldown = 0;
    [SyncVar]
    private string nameWeapon = "Ice strike";
    [SyncVar]
    private float speedSprint = 1.6f;



    [SyncVar]
    public int stamina = 100;
    [SyncVar]
    public int staminaMax = 100;
    [SyncVar]
    public int health = 100;
    [SyncVar]
    public int healthMax = 100;
    [SyncVar]
    public int score = 0;
    [SyncVar]
    public int mana = 100;
    [SyncVar]
    public int manaMax = 100;
    [SyncVar]
    public bool died = false;
    [SyncVar]
    public bool invulnerability = false;
    [SyncVar]
    public bool manaNoUse = false;
    [SyncVar]
    private int choiceWeapon = 0;
    [SyncVar]
    private int numberoOfWeapon = 5;
    public Scrollbar healthBar;
    public Scrollbar manaBar;
    public Scrollbar staminaBar;
    public Transform menuInGame;
    //private Image healthColor;
    [SyncVar]
    public int Level = 1;
    [SyncVar]
    public int experience = 0;
    [SyncVar]
    public int experienceRemaining;
    [SyncVar]
    public int gold = 0;

    private int choiceAction;

    private float timeCooldown = 0f;
    private bool spellInUse = false;
    private float mourrant = 0f;

    #region iventoryBOOST
    public int lifeBoost = 0;
    public int manaBoost = 0;
    public int staminaBoost = 0;
    public int damagesBoost = 0;
    public int armorBoost = 0;

    public void setLifeBoost(int lifepoint)
    {
        lifeBoost = lifepoint;
    }
    public void setManaBoost(int manapoint)
    {
        manaBoost = manapoint;
    }
    public void setStaminaBoost(int poin)
    {
        staminaBoost = poin;
    }
    public void setArmorBoost(int poin)
    {
        armorBoost = poin;
    }
    public void setDamageBoost(int poin)
    {
        damagesBoost = poin;
    }
    public int getDamageBoost()
    {
        return damagesBoost;
    }
    public int getArmorBoost()
    {
        return armorBoost;
    }

    #endregion

    #region palyer
    public void Setlife(int a)
    {
        health = a;
    }
    public void Setmana(int a)
    {
        mana = a;
    }
    public void Setstamina(int a)
    {
        stamina = a;
    }
    public void Setscore(int a)
    {
        score = a;
    }
    public void Setexp(int a)
    {
        experience = a;
    }
    public void Setgold(int a)
    {
        gold = a;
    }
    public void TP(Vector3 t, Quaternion a)
    {
        myTransform.position = t;
        myTransform.rotation = a;
    }

    #endregion
    // Use this for initialization
    void Start()
    {

        isRunning = false;
        experienceRemaining = 20;
        myTransform = GetComponent<Transform>();
        targetTransform = myTransform.FindChild("Target").GetComponent<Transform>();
        target2Transform = myTransform.FindChild("Target2").GetComponent<Transform>();
        myRenderer = GetComponent<Renderer>();
        lastPosition = myTransform.position;
        if (isLocalPlayer)
        {
            myName = InfoStorage.namee;
             DispMyName = myTransform.FindChild("NameText").GetComponent<TextMesh>();
             DispMyLevel = myTransform.FindChild("LevelText").GetComponent<TextMesh>();
             DispMyLevel.text = "Level 1";
             DispMyName.text = myName;
            myTransform.FindChild("rotationCamera").FindChild("camera4player").GetComponent<Camera>().enabled = true;// activer camera joueur local uniquement 
            //myRenderer.material.color = new Color(1, 0, 0); // colorer en rouge le joueur local
            TransmitPosition();
            TransmitRotation();
            ExpText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("ExpTxt").GetComponent<Text>();
            lifeText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("HealthTxt").GetComponent<Text>();
            manaText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("ManaTxt").GetComponent<Text>();
            levelText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("LevelText").GetComponent<Text>();
            weaponText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("WeaponTxt").GetComponent<Text>();
            staminaText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("StaminaTxt").GetComponent<Text>();
            goldText = GameObject.Find("HUD").GetComponent<Transform>().FindChild("GoldText").GetComponent<Text>();
            menuInGame = GameObject.Find("Canvas good").GetComponent<Transform>().FindChild("Pause");
            //Debug.Log(GameObject.Find("HealthbarHUD").GetComponent<Transform>().FindChild("Healthbar").GetComponent<Scrollbar>());
            healthBar = GameObject.Find("HealthbarHUD").GetComponent<Transform>().FindChild("Healthbar").GetComponent<Scrollbar>();
            //healthColor = GameObject.Find("HealthbarHUD").GetComponent<Transform>().FindChild("Healthbar").FindChild("Mask").FindChild("Sprite").GetComponent<Image>();
            //Debug.Log(GameObject.Find("HealthbarHUD").GetComponent<Transform>().FindChild("Healthbar").FindChild("Mask").FindChild("Sprite").GetComponent<Image>());
            manaBar = GameObject.Find("ManabarHUD").GetComponent<Transform>().FindChild("Manabar").GetComponent<Scrollbar>();
            staminaBar = GameObject.Find("StaminabarHUD").GetComponent<Transform>().FindChild("Staminabar").GetComponent<Scrollbar>();
            anim = GetComponent<Animator>();

        }


        //idTest = GetComponent<NetworkIdentity> ().netId.Value;
    }



    void FixedUpdate()
    {

        timeDelayerForAnimation(choiceAction);
        //Debug.Log(choiceAction);
        interVAlleDeRelance();

        NetworkIdentity.FindObjectOfType<Player>();
        //Debug.Log(Time.deltaTime);
        if (isLocalPlayer)
        { //verifie si le joueur est local
               TransmitLevelAndName();
            increaseLevel();
            delay += Time.deltaTime;// ajoute temps d'une frame

            if (!died)
            {
                if (health < 0)
                    died = true;
                if (delay > 0.6f && (health < healthMax + lifeBoost || mana < manaMax + manaBoost || stamina < staminaMax + staminaBoost))
                {

                    if (health < healthMax + lifeBoost)
                        health++;
                    if (mana < manaMax + manaBoost)
                        mana++;
                    if (stamina < staminaMax + staminaBoost)
                        stamina++;

                    delay = 0.1f;

                }
                if (Vector3.Distance(lastPosition, myTransform.position) > 0.08f)
                {
                    TransmitPosition(); // transmet la position si mouvement  
                    lastPosition = myTransform.position;
                }
                if (Mathf.Abs(lastYRotation - myTransform.eulerAngles.y) > 0.3f)
                {
                    TransmitRotation();
                    lastYRotation = myTransform.eulerAngles.y;
                }

                if (!spellInUse && (Input.GetKeyDown("space") || Input.GetKey(KeyCode.Joystick1Button0)))
                {
                    choiceAction = 1;
                    anim.Play("jump", -1, 0f);
                    timerDeSort(2.0f);

                }
                if ((Input.GetKeyDown(KeyCode.G) && delay > 0.1))
                {
                    gold += 1000;
                    delay = 0.0f;
                }
                if (!spellInUse && ((Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Joystick1Button1)) && delay > 0.1))
                {
                    //CmdTellMyShootToTheServer();
                    timerDeSort(1.5f);
                    useWeapon();
                    delay = 0.0f;
                }
                if ((Input.GetKeyDown(KeyCode.P)) && delay > 0.1)
                {
                    menuActivator();
                    delay = 0.0f;
                }
                if ((Input.GetKeyDown(KeyCode.R)) && delay > 0.1)
                {
                    gold += 700;
                    delay = 0.0f;
                }

                if (Input.GetMouseButton(1))// voir autour de soi
                {
                    float h = mouse_sensibility * Input.GetAxis("Mouse X");

                    myTransform.FindChild("rotationCamera").Rotate(0, h, 0);

                }

                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)// si mouvement reset orientation camera
                {
                    myTransform.FindChild("rotationCamera").transform.eulerAngles = myTransform.eulerAngles;
                }

                inputH = Input.GetAxis("Horizontal");
                inputV = Input.GetAxis("Vertical");

                anim.SetFloat("inputH", inputH);
                anim.SetFloat("inputV", inputV);
                anim.SetBool("isRunning", isRunning);
                anim.SetBool("diied", died);

                if (manaNoUse)
                {
                    manaCooldown++;
                    if (manaCooldown > 3)
                    {
                        manaNoUse = false;
                        manaCooldown = 0;
                    }
                }
                //if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Joystick1Button0)
                //jumpDirection.y = jumpspeed;

            }
            else
            {
                //Destroy(gameObject);
                //Instantiate(gameObject, new Vector3(23, 201, 91), Quaternion.Euler(0, 10, 0));
                //myTransform = gameObject.transform;


                //animation mort!!

                timerDeSort(2.0f);
                anim.Play("falling_back_death");
                Invoke("revitalisation", 1.9f);


            }
            lifeText.text = "Health: " + health.ToString() + "/" + (healthMax + lifeBoost).ToString();
            manaText.text = "Mana: " + mana.ToString() + "/" + (manaMax + manaBoost).ToString();
            levelText.text = "Level : " + Level.ToString();
            ExpText.text = "Remaining XP : " + (experienceRemaining - experience).ToString();
            weaponText.text = "Weapon: " + nameWeapon;
            staminaText.text = "Stamina: " + stamina.ToString() + "/" + (staminaMax + staminaBoost).ToString();
            goldText.text = "Gold : " + gold.ToString();
            healthBar.size = (float)health / (float)(healthMax + lifeBoost);
            manaBar.size = (float)mana / (float)(manaMax + manaBoost);
            staminaBar.size = (float)stamina / (float)(staminaMax + staminaBoost);
            //healthColor.color = SetLifePointColor();
        }


    }


    // Update is called once per frame
    void Update()
    {

        if (isLocalPlayer) //verifie si le joueur est local
        {
            if (!spellInUse && !died)
            {

                choiceOfWeapon();
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button8)) && stamina > 0)
                {

                    myTransform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * slow * speed * speedSprint, Space.Self);// récupère la position du joueur local
                    if (Input.GetAxis("Vertical") != 0)
                    {
                        stamina--;
                        isRunning = true;
                        //anim.Play("running", -1, 0);
                    }
                    else
                    {
                        isRunning = false;
                    }
                }
                else
                {

                    isRunning = false;
                    //anim.Play("running", -1, 0);


                    myTransform.Translate(Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime * slow * speed, Space.Self);// récupère la position du joueur local



                }

                myTransform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed, 0);// recupere la rotation du joueur local		

            }
        }
        else
        {
            myTransform.position = Vector3.Lerp(myTransform.position, SyncPosition, Time.deltaTime * 15);// recupere la position des autres joueurs
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.Euler(0, SyncRotationY, 0), Time.deltaTime * 15);// recupere la rotation des autres joueurs

        }

    }


    [Command]
    void CmdTellMyArrowToTheServer()
    {
        GameObject Arrow = Instantiate(arrowPrefab, target2Transform.position, Quaternion.identity) as GameObject;
        Arrow arrowScript = Arrow.GetComponent<Arrow>();

        arrowScript.syncYRotation2 = target2Transform.eulerAngles.y;
        arrowScript.idPlayer2 = GetComponent<NetworkIdentity>().netId;

        NetworkServer.Spawn(Arrow);
        Destroy(Arrow, 3.0f);
    }

    [Command]
    void CmdTellMyShootToTheServer()
    {
        GameObject bullet = Instantiate(bulletPrefab, targetTransform.position, Quaternion.identity) as GameObject;
        bullet bulletScript = bullet.GetComponent<bullet>();

        bulletScript.syncYRotation = targetTransform.eulerAngles.y;
        bulletScript.idPlayer = GetComponent<NetworkIdentity>().netId;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 3.0f);
    }

    [Command]
    void CmdSendMyPositionToTheServer(Vector3 positionReceived)
    {
        SyncPosition = positionReceived;
    }

    [Command]
    void CmdSendMyRoationToTheServer(float rotationYReceived)
    {
        SyncRotationY = rotationYReceived;

    }
    [Command]		
	void CmdSendMyLevelAndNameToTheServer(int Levele, string Name_)
	{		
	RpcSetPlayerName(Name_);		
	RpcSetPlayerLevel(Levele);		
	}

    [Client]		
	void TransmitLevelAndName()
	{		
	CmdSendMyLevelAndNameToTheServer(Level, DispMyName.text);		
	}		
			
	[ClientRpc]		
	void RpcSetPlayerName(string namer)
	{		
	gameObject.transform.FindChild("NameText").GetComponent<TextMesh>().text = namer;		
	}		
	[ClientRpc]		
	void RpcSetPlayerLevel(int Leveler)
	{		
	gameObject.transform.FindChild("LevelText").GetComponent<TextMesh>().text = "Level : " + Leveler.ToString();		
	}

[Client]
    void TransmitPosition()
    {
        CmdSendMyPositionToTheServer(myTransform.position);

    }

    [Client]
    void TransmitRotation()
    {
        CmdSendMyRoationToTheServer(myTransform.eulerAngles.y);

    }

    void choiceOfWeapon()
    {

        if (!spellInUse && (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetAxisRaw("Mouse ScrollWheel") > 0))
        {
            choiceWeapon++;
            choiceWeapon %= numberoOfWeapon;
            setWeaponName();
        }
        if (!spellInUse && (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetAxisRaw("Mouse ScrollWheel") < 0))
        {
            choiceWeapon--;
            if (choiceWeapon < 0)
                choiceWeapon += numberoOfWeapon;
            setWeaponName();
        }
    }

    void setWeaponName()
    {
        switch (choiceWeapon)
        {
            case 0:
                nameWeapon = "Ice Strike";
                break;
            case 1:
                nameWeapon = "Fire Strike";
                break;
            case 2:
                nameWeapon = "Life Potion";
                break;
            case 3:
                nameWeapon = "Mana Potion";
                break;
            case 4:
                nameWeapon = "Stamina Potion";
                break;
            default:
                break;
        }
    }

    delegate void InvokedFunction();
    IEnumerator WaitAndInvoke(float secondsToWait, InvokedFunction func)
    {
        yield return new WaitForSeconds(secondsToWait);
        func();
    }

    void useWeapon()
    {
        switch (choiceWeapon)
        {
            case 0:
                if (!manaNoUse && mana > 4)
                {
                    mana -= 5;
                    anim.Play("Standing_1H_Magic_Attack_01", -1, 0f);
                    StartCoroutine(WaitAndInvoke(1, CmdTellMyShootToTheServer));
                    //Invoke("CmdTellMyShootToTheServer", 1.0f);
                    //Debug.Log(anim["Standing_1H_Magic_Attack_01"].time);
                    //anim.animation
                    manaNoUse = true;
                }
                break;
            case 1:
                if (!manaNoUse && mana > 5)
                {
                    mana -= 6;
                    anim.Play("Standing_1H_Magic_Attack_01", -1, 0f); //standing_1H_cast_spell_01
                    manaNoUse = true;
                    StartCoroutine(WaitAndInvoke(1, CmdTellMyArrowToTheServer));
                    //Invoke("CmdTellMyArrowToTheServer", 1.0f);
                }
                if (mana < 0)
                    mana = 0;
                break;
            case 2:
                health += (10 * Level);
                if (health > healthMax + lifeBoost)
                    health = healthMax + lifeBoost;
                else
                    anim.Play("standing_1H_cast_spell_01", -1, 0f);
                break;
            case 3:
                mana += (10 * Level);
                if (mana > manaMax + manaBoost)
                    mana = manaMax + manaBoost;
                else
                    anim.Play("standing_1H_cast_spell_01", -1, 0f);
                break;
            case 4:
                stamina += (10 * Level);
                if (stamina > staminaMax + staminaBoost)
                    stamina = staminaMax + staminaBoost;
                else
                    anim.Play("standing_1H_cast_spell_01", -1, 0f);
                break;
            default:
                break;

        }


    }

    public Color SetLifePointColor()
    {
        float life = (float)health / (float)healthMax;
        if (life > 0.3f)
            return new Color(211, 0, 0);

        else
            return new Color(94, 0, 0);
    }

    private void increaseLevel()
    {
        if (experience >= experienceRemaining)
        {
            experience = experience - experienceRemaining;
            Level++;
            DispMyLevel.text = "Level " + Level;
             //SyncLevel = Level;
            healthMax += 50;
            manaMax += 50;
            staminaMax += 50;
            experienceRemaining = System.Convert.ToInt32(120 * Mathf.Pow(1.5f, (Level - 1)) - 110);
        }

    }

    public void subit_damages(int damages, int idSpell)
    {
        if (!died)
            switch (idSpell)
            {
                case 0:
                    health -= damages;
                    if (health < 1)
                        died = true;
                    break;
                case 1:
                    health -= damages;
                    stamina -= 15;
                    if (health < 1)
                        died = true;
                    if (stamina < 0)
                        stamina = 0;
                    slow /= 2;
                    Invoke("normalSpeed", 6.0f);
                    break;
                default:
                    health -= 1;
                    break;
            }
    }

    public void gold_Adder(int money)
    {
        gold += money;
    }

    void normalSpeed()
    {
        slow *= 2;
    }

    public void menuActivator()
    {
        menuInGame.gameObject.active = true;
    }
    void interVAlleDeRelance()
    {
        if (spellInUse)
            timeCooldown -= Time.deltaTime;
        if (timeCooldown < 0)
            spellInUse = false;
    }
    void timerDeSort(float delai)
    {
        spellInUse = true;
        timeCooldown = delai;
    }
    /// <summary>
    /// fait mourir l'individu puis autorise sa réaparition
    /// </summary>
    void revitalisation()
    {
        if (died)
        {

            myTransform.position = new Vector3(23, 201, 91);
            myTransform.transform.rotation = Quaternion.Euler(0, 10, 0);
            health = healthMax;
            mana = manaMax;
            stamina = staminaMax;
            died = false;
        }


    }
    /// <summary>
    /// repertorie les differences action qui peuvent etre effectuees
    /// </summary>
    /// <param name="action"></param>
    void actionToDo(int action)
    {
        switch (action)
        {
            case 1:
                myTransform.Translate(0, 0.05f, 0);
                break;
            case 2:

                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;

        }

    }
    /// <summary>
    /// attend une seconde avant d'effectuer l'action dont le numero est donne
    /// </summary>
    /// <param name="action"></param>
    void timeDelayerForAnimation(int action)
    {
        if (action == 1 /*&& spellInUse */&& timeCooldown < 1.2f && timeCooldown > 0.80f)
        {

            actionToDo(action);
            choiceAction = 2;
        }

        if (action == 2 /*&& spellInUse && timeCooldown < 0.2f && timeCooldown > 0.11f*/)
            actionToDo(action);
        //spellInUse = false;


    }
}
