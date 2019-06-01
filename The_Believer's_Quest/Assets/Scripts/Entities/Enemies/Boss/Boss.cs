using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities;
//Nicolas I
public abstract class Boss : MonoBehaviour
{
    protected bool isAttacking;
    protected RoomManager roomManager;

    protected delegate void BossAttack();
    //protected delegate void ChoosePathfinding();
    protected List<BossAttack> attackList;
    protected int nbAttacks;
    
    [SerializeField] protected BossAsset bossData;
    [SerializeField] protected PlayerAsset playerAsset;
    protected float hpPhase;
    protected Animator animator;

    protected Vector3 startPos;
    protected Node nextNode;
    protected Transform transformPlayer;
    //protected ChoosePathfinding Pathfinding;
    protected RealPathfinding realPathfinding;
    protected Attack attack;
    protected bool shot;
    protected bool testForCoolDown;
    protected bool isFirstPhase;

    public BossAsset BossData { get => bossData; set => bossData = value; }
    public BossLifebar healthBar;

    protected void Start()
    {
        print("start!");
        healthBar = GetComponent<BossLifebar>();
        healthBar.SetMaxValue(bossData.MaxHp);
        roomManager = GetComponentInParent<RoomManager>();
        isFirstPhase = true;
        shot = true;
        testForCoolDown = true;
        isAttacking = false;
        attackList = new List<BossAttack>();
        animator = GetComponent<Animator>();
        hpPhase = bossData.Hp / 2;
        realPathfinding = GetComponentInParent<RealPathfinding>();
        attack = GetComponent<Attack>();
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = playerGO.GetComponent<Transform>();

        //Pathfinding = AStarPathfindingMoving;

    }

    protected void OnDestroy()
    {
        GetComponentInParent<RoomManager>().DestroyEnemy(gameObject);
    }

    protected void FixedUpdate()
    {
        if(!isAttacking)
            AStarPathfindingMoving();

        if(testForCoolDown)
        {
            StartCoroutine(AttackWithCoolDown());
        }
        if (shot)
        {
            attack.BossLauncher(Attack.Trajectory.Cqc);
        }
    }

    IEnumerator AttackWithCoolDown()
    {
        testForCoolDown = false;
        attackList[Random.Range(0, nbAttacks)]();

        yield return new WaitForSeconds(bossData.Cooldown);
        testForCoolDown = true;
    }

    public void ChangeLife(float hp)
    {
        if (bossData.Hp + hp > bossData.MaxHp)
        {
            hp = 0;
        }

        bossData.Hp += hp;
        healthBar.SetValue(bossData.Hp);
        if (bossData.Hp <= 0)
        {
            roomManager.DestroyEnemy(gameObject);
            healthBar.SliderAppear();
        }

        //healthBar.value = bossData.Hp;
        if (bossData.Hp <= hpPhase && isFirstPhase)
        {
            ChangePhase();
            isFirstPhase = false;
        }
    }

    protected void AStarPathfindingMoving()
    {
        startPos = transform.position;

        nextNode = realPathfinding.FindPath(startPos, transformPlayer.position);

        if (nextNode != null)
        {
            transform.position = Vector2.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.5f, 0), bossData.Speed * Time.deltaTime);
        }
        else
        {
            if (transform.position.magnitude - transformPlayer.position.magnitude < 0.5)
            {
                transform.position = Vector2.MoveTowards(transform.position, transformPlayer.position,
                    bossData.Speed * Time.deltaTime);
            }
        }

        if (CanAttack())
        {
            shot = true;
        }
    }

    private bool CanAttack()
    {
        return (gameObject.transform.position - playerAsset.Position).magnitude < bossData.Range;
    }

    protected abstract void ChangePhase();
}