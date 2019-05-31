using UnityEngine;
using System.Collections;
using Entities;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;
//Nicolas L
public class Enemy : MovingObject
{
    private bool testForCoolDown;

    [SerializeField] private EnemyAsset enemyAsset;
    [SerializeField] private PlayerAsset playerAsset;

    private Vector3 startPos;
    private float HP;

    private LayerMask mask;
    
    private Node nextNode;
    private Transform transformPlayer;

    private ChoosePathfinding Pathfinding;
    private RealPathfinding realPathfinding;

    private Attack attack;
    public bool shot;

    public EnemyAsset EnemyAsset { get => enemyAsset; set => enemyAsset = value; }

    void Start()
    {
        testForCoolDown = true;
        HP = enemyAsset.Hp;
        
        realPathfinding = GetComponentInParent<RealPathfinding>();
        attack = GetComponent<Attack>();
        
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = playerGO.GetComponent<Transform>();
        mask = gameObject.layer;

        Pathfinding = AStarPathfindingMoving;

    }

    private void FixedUpdate()
    {
        Pathfinding();
        
        if(shot)
        {
            StartCoroutine(AttackWithCoolDown());
        }
    }

    IEnumerator AttackWithCoolDown()
    {
        if (testForCoolDown)
        {
            testForCoolDown = false;
            attack.Launcher();
            shot = false;

            yield return new WaitForSeconds(enemyAsset.Cooldown);
            testForCoolDown = true;
        }
    }

    delegate void ChoosePathfinding();

    private void AStarPathfindingMoving()
    {
        startPos = transform.position;

        nextNode = realPathfinding.FindPath(startPos, transformPlayer.position);

        if (nextNode != null)
        {
            transform.position = Vector2.MoveTowards(startPos, nextNode.worldPos + new Vector3(0.5f, 0.5f, 0), EnemyAsset.Speed * Time.deltaTime);
        }
        else
        {
            if (transform.position.magnitude - transformPlayer.position.magnitude < 0.5)
            {
                transform.position = Vector2.MoveTowards(transform.position, transformPlayer.position,
                    enemyAsset.Speed * Time.deltaTime);
            }
        }

        if (CanAttack())
        {
            print("can attack");
            shot = true;
        }
    }

    private bool CanAttack()
    {
        return (gameObject.transform.position - playerAsset.Position).magnitude < enemyAsset.Range;
    }

    public int GetWeight()
    {
        return EnemyAsset.Weight;
    }

    public void OnDestroy()
    {
        GetComponentInParent<RoomManager>().DestroyEnemy(gameObject);
    }

    public void SetHP(int hp)
    {
        HP = hp;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0 )
            Destroy(gameObject);
    }
}