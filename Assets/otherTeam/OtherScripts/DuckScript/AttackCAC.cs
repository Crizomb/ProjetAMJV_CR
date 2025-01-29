using UnityEngine;
using System.Collections;
using System;

public class AttackCAC : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    private BaseDuckScript baseDuckScript;
    private LayerMask duckLayer; // Layer for ducks
    private LayerMask wallLayer;
    private GameObject targetToAttack;
    private bool CACouDistance; //True for CAC, False for Distance



    private bool canAttack = true;

    static public event Action ATTACK;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CACouDistance = false;
        baseDuckScript = gameObject.GetComponent<BaseDuckScript>();
        duckLayer = LayerMask.GetMask("Duck");
        wallLayer = LayerMask.GetMask("Wall");
    }

    public bool changeCACouDistance(bool whichOne)
    {
        CACouDistance=whichOne;
        return(CACouDistance);
    }
    
    IEnumerator coolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.fightStarted)
        {
            if (canAttack)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, range, duckLayer);
                //GameObject targetToAttack = null;
                float distanceToChosenTarget = range;
                bool targetFound = false;
                foreach (Collider hit in hits)
                {
                    Vector3 directionToTarget = hit.transform.position - transform.position;
                    float distanceToTarget = directionToTarget.magnitude;

                    if (!Physics.Raycast(transform.position, directionToTarget.normalized, distanceToTarget,
                            wallLayer))
                    {
                        if (ArmyManager.getArmy(!baseDuckScript.getTeam()).Contains(hit.GetComponent<AbstractUnit>()))
                        {
                            if (baseDuckScript.getAttackMode() == 1)
                            {
                                if (ArmyManager.getCrownDuck(!baseDuckScript.getTeam()) ==
                                    hit.gameObject)
                                {
                                    targetToAttack = hit.gameObject;
                                    targetFound = true;
                                }
                            }
                        }
                    }

                }

                if (targetFound && CACouDistance)
                {
                    AttackC(targetToAttack);
                }
                if(targetFound && !CACouDistance)
                {
                    AttackD();
                }
            }
        }
    }

    private void AttackC(GameObject targetToAttack)
    {
        StartCoroutine(coolDown());
        targetToAttack.GetComponent<AbstractUnit>().TakeDamage(damage);
        Debug.Log("Corps � corps");
        ATTACK.Invoke();
    }

    private void AttackD()
    {
        Debug.Log("DISTANCE");
        StartCoroutine(coolDown());

        ATTACK.Invoke();
    }

    public GameObject GetTarget()
    {
        return (targetToAttack);
    }

    public float GetDamage()
    {
        return (damage);
    }

}
