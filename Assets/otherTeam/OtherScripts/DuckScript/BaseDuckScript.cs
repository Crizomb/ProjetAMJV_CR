using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine.UI;

public class BaseDuckScript : AbstractUnit
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isEnemy = true;
    
    [SerializeField] GameObject crownPrefab;
    [SerializeField] private int attackMode = 0; //0 do Nothing, 1 offense, 2 Neutre, 3 Défense
    
    NavMeshAgent agent;
    private Rigidbody duckRB;
    Vector3 destination;
    private float health;
    [SerializeField] private float baseHealth;
    [SerializeField] private float armor;
    [SerializeField] public GameObject healthBarPrefab;
    [SerializeField] private GameObject healthCanvas;
    private RectTransform healthCanvasRect;
    private GameObject healthBar = null;
    //private RectTransform healthBarRect = null;
    private float DuckHeight;
    private GameObject healthBarInside;
    private Image healthBarGradient;
    private float raycastDistance = 1.0f; //for getting ground material
    private LayerMask allGroundLayers;
    [SerializeField] private float baseSpeed;
    private GameObject crown;
    [SerializeField] public int cost = 5;
    [SerializeField] public List<string> troopStats; 
    
    void Start()
    {
        health = baseHealth;
        
        agent = GetComponent<NavMeshAgent>();
        duckRB = gameObject.GetComponent<Rigidbody>();
        healthCanvasRect = healthCanvas.GetComponent<RectTransform>();
        
        DuckHeight = transform.Find("TigeUI").GetComponent<Renderer>().bounds.size.y;
        allGroundLayers = LayerMask.GetMask("Dirt") | LayerMask.GetMask("Sand");
    }

    // Update is called once per frame
    void Update()
    {
        //Pour que le Duck regarde sa cible constamment !!
        if (GetComponent<AttackCAC>().GetTarget() != null)
        {
            transform.LookAt(GetComponent<AttackCAC>().GetTarget().transform);
        }

        if (GameManager.Instance.fightStarted)
        {
            updateMovement();
        }
        else
        {
            duckRB.isKinematic = true;
        }

        if (health != baseHealth)
        {
            displayHealthBar();
        }
        
        Vector3 rayOrigin = transform.position;
        Ray ray = new Ray(rayOrigin, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, allGroundLayers))
        {
            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Dirt")
            {
                agent.speed = baseSpeed;
            } else if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "Sand")
            {
                agent.speed = baseSpeed/2;
            }
        }
    }

    public void displayHealthBar()
    {
        if (healthBar == null)
        {
            healthBar = Instantiate(healthBarPrefab, healthCanvas.transform);
            healthBar.transform.localScale = new Vector2(0.25f, 0.25f);
            healthBarInside = healthBar.transform.GetChild(1).gameObject;
            healthBarGradient = healthBarInside.GetComponent<Image>();
            healthBarGradient.type = Image.Type.Filled;
            healthBarGradient.fillMethod = Image.FillMethod.Horizontal;
        }

        if (healthBar != null) //safety measure 
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            healthBar.transform.position = screenPoint + new Vector2(0f, DuckHeight*80*Screen.width/3840);
            healthBarGradient.fillAmount = health / baseHealth;
            healthBarGradient.color = Color.Lerp(Color.red, Color.green, health/baseHealth);
            
        }
        
    }
    public void updateMovement()
    {
        duckRB.isKinematic = false;
        if (attackMode == 1 && ArmyManager.getCrownDuck(!isEnemy))
        {
            destination = ArmyManager.getCrownDuck(!isEnemy).transform.position;
            agent.destination = destination;
        }
    }

    public void setTeam(bool isOnEnemyTeam){
        isEnemy = isOnEnemyTeam;
    }

    public void despawn()
    {
      
        Destroy(gameObject);
    }
    
    void OnMouseOver()
    {
        // Check if the right mouse button is clicked
        /*
        if (Input.GetMouseButtonDown(1)) // 1 is for the right mouse button
        {
            if (!armyManagerScript.getCrownDuck(isEnemy) && !isEnemy)
            {
                becomeCrownDuck();
            }
            else if (armyManagerScript.getCrownDuck(isEnemy) == gameObject && !isEnemy)
            {
                loseMyCrown();
            }
        }
        */
    }

    public override void TakeDamage(float damage)
    {
        float damageReallyTaken = Mathf.Max(0, damage - armor);
        health -= damageReallyTaken;
        if (health <= 0)
        {
            Debug.Log("dead");
            die();
        }
    }
    
    public override void Heal(float value)
    {
        health += value;
        health = (health > baseHealth) ? baseHealth : health;
    }

    public override void AddArmor(float armor)
    {
        // Nothing
    }

    public override void RemoveArmor(float armor)
    {
        // Nothing
    }

    public override void StartFight()
    {
        // Nothing
    }
    

    public float getHealth()
    {
        return health;
    }

    public float getBaseHealth()
    {
        return baseHealth;
    }
    private void die()
    {
        Destroy(healthBar);
        Destroy(gameObject);
    }

    public int getAttackMode()
    {
        return (attackMode);
    }

    public void setAttackMode(int mode)
    {
        attackMode = mode;
    }

    public bool getTeam()
    {
        return (isEnemy);
    }

    public float getSpeed()
    {
        return (baseSpeed);
    }

    public void setHealthCanvas(GameObject healthCanvas)
    {
        this.healthCanvas = healthCanvas;
    }

}
