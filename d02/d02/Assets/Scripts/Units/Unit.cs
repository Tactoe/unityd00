using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float attackRange;
    public float attackDelay;
    public int strength;
    public int maxHealth;
    public int health;

    protected Vector3 target;
    protected string opponentTag;
    protected Animator animator;
    protected AudioSource yes;

    float attackTimer;
    GameObject attackTarget;
    string ennemyType;

    public void SetNewTargetDirection(Vector3 newTarget)
    {
        target = newTarget;
        target.z = transform.position.z;
        transform.up = -(target - transform.position);
        animator.SetBool("isWalking", true);
    }

    void Attack()
    {
        if (attackTarget.GetComponent<Unit>())
        {
            Unit tmp = attackTarget.GetComponent<Unit>();
            tmp.health -= strength;
            Debug.Log(opponentTag + " Unit [" + tmp.health + "/" + tmp.maxHealth + "]HP has been attacked.");
        }
        else if (attackTarget.GetComponent<Building>())
        {
            Building tmp = attackTarget.GetComponent<Building>();
            tmp.health -= strength;
            Debug.Log(opponentTag + " Building [" + tmp.health + "/" + tmp.maxHealth + "]HP has been attacked.");
        }
        attackTimer = attackDelay;
    }

    protected void UnitBehaviorLoop()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if ((target - transform.position).magnitude < 0.1f)
        {
            animator.SetBool("isWalking", false);
            LookForNearbyStuffToAttack();
        }
        if (attackTarget != null)
        {
            if (attackTimer < 0)
                Attack();
            else
                attackTimer -= Time.deltaTime;
        }
        if (health <= 0)
            Destroy(gameObject);
    }

    void LookForNearbyStuffToAttack()
    {
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D obj in nearbyObjects)
        {
            if (obj.transform == transform)
                continue;
            if (obj.gameObject.CompareTag(opponentTag)
                && Vector2.Distance(transform.position, obj.transform.position) <= attackRange)
            {
                if (obj.gameObject.GetComponent<Unit>() != null)
                {
                    ennemyType = "Unit";
                    attackTarget = obj.gameObject;
                }
                else if (obj.gameObject.GetComponent<Building>() != null)
                {
                    ennemyType = "Building";
                    attackTarget = obj.gameObject;
                }
                break;
            }
        }
    }

    void Start()
    {
        health = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        yes = gameObject.GetComponent<AudioSource>();
        target = transform.position;
    }

    void Update()
    {
        UnitBehaviorLoop();
    }
}
