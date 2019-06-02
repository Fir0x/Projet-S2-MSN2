using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities;
//Nicolas I && Nicolas L
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
    protected float HP;
    protected float hpPhase;
    protected Animator animator;

    
    protected Node nextNode;
    private Node precedentNode;

    protected Vector3 startPos;
    protected Vector3 nextPos;
    protected Vector3 direction;

    protected Transform transformPlayer;
    //protected ChoosePathfinding Pathfinding;
    protected RealPathfinding realPathfinding;
    protected Attack attack;
    protected bool shot;
    protected bool testForCoolDown;
    protected bool isFirstPhase;

    public BossAsset BossData { get => bossData; set => bossData = value; }
    public BossLifebar healthBar;

    private BossAnimation animatorController;

    protected void Start()
    {
        animatorController = GetComponent<BossAnimation>();
        animator = GetComponent<Animator>();

        HP = bossData.Hp;
        healthBar = GetComponent<BossLifebar>();
        healthBar.SetMaxValue(bossData.MaxHp);

        roomManager = GetComponentInParent<RoomManager>();
        isFirstPhase = true;
        shot = true;
        testForCoolDown = true;
        isAttacking = false;
        attackList = new List<BossAttack>();

        precedentNode = new Node(true, GameObject.Find("Player").transform.position, 0, 0);

        hpPhase = bossData.Hp / 2;
        realPathfinding = GetComponentInParent<RealPathfinding>();
        attack = GetComponent<Attack>();
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = playerGO.GetComponent<Transform>();

    }

    protected void OnDestroy()
    {
        ChangeDiamonds();
        ChangeGold();
    }

    protected void FixedUpdate()
    {
        if (!isAttacking)
            AStarPathfindingMoving();

        if(testForCoolDown)
        {
            animatorController.Attack();
            StartCoroutine(AttackWithCoolDown());
        }
        if (shot)
        {
            animatorController.Attack();
            attack.BossLauncher(Attack.Trajectory.Cqc);
        }

        direction = nextPos - startPos;
        ChangeDirection();
        startPos = nextPos;

        if (precedentNode != nextNode && nextNode != null)
        {
            precedentNode = nextNode;
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
        if (HP + hp > bossData.MaxHp)
        {
            hp = 0;
        }

        HP += hp;
        healthBar.SetValue(HP);
        if (HP <= 0)
        {
            healthBar.SliderDisappear();
            roomManager.DestroyEnemy(this.gameObject);
        }

        if (HP <= hpPhase && isFirstPhase)
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
            else
            {
                transform.position = Vector2.MoveTowards(startPos, precedentNode.worldPos + new Vector3(0.5f, 0.5f, 0), bossData.Speed * Time.deltaTime);
            }
        }

        if (nextNode != null)
            nextPos = nextNode.worldPos;
        else if (precedentNode != null)
            nextPos = precedentNode.worldPos;
        else
            nextPos = playerAsset.Position;

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

    private void ChangeGold()
    {
        playerAsset.Gold += (int)(Random.Range(10, 30) * playerAsset.Floor * 0.5f);
        UIController.uIController.changeGold.Invoke();
    }
    private void ChangeDiamonds()
    {
        playerAsset.Diamond += Random.Range(1, 4) * playerAsset.Floor;
        UIController.uIController.changeDiamond.Invoke();
    }

    public void ChangeDirection()
    {
        float x = direction.x;
        float y = direction.y;

        if (x < 0)
            animatorController.ChangeDirection(3);
        else if (x > 0)
            animatorController.ChangeDirection(1);
        else if (y < 0)
            animatorController.ChangeDirection(2);
        else
            animatorController.ChangeDirection(0);
    }
}