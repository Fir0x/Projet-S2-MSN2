using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities;

public abstract class Boss : MonoBehaviour
{
    protected bool isAttacking;

    protected delegate void BossAttack();
    protected delegate void ChoosePathfinding();
    protected List<Attack> attackList;

    [SerializeField] protected BossAsset bossData;
    [SerializeField] protected PlayerAsset playerAsset;
    protected int hpPhase;
    protected Animator animator;
    [SerializeField] protected Slider healthBar;

    protected Vector3 startPos;
    protected Node nextNode;
    protected Transform transformPlayer;
    protected ChoosePathfinding Pathfinding;
    protected RealPathfinding realPathfinding;
    protected Attack attack;
    protected bool shot;

    public BossAsset BossData { get => bossData; set => bossData = value; }
    public Slider HealthBar { get => healthBar; set => healthBar = value; }

    protected void Start()
    {
        attackList = new List<Attack>();
        animator = GetComponent<Animator>();
        hpPhase = bossData.Hp / 2;

        realPathfinding = GetComponentInParent<RealPathfinding>();
        attack = GetComponent<Attack>();
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        transformPlayer = playerGO.GetComponent<Transform>();
    }

    protected void OnDestroy()
    {
        GetComponentInParent<RoomManager>().DestroyEnemy(gameObject);
    }

    protected void FixedUpdate()
    {
        print("It works !");
        Pathfinding();
        if (shot)
        {
            attack.Launcher();
            shot = false;
        }
    }

    public void ChangeLife(int hp)
    {
        if (bossData.Hp + hp > bossData.MaxHp)
            hp = 0;

        bossData.Hp += hp;
        if (bossData.Hp < 0)
            bossData.Hp = 0;

        healthBar.value = bossData.Hp;
        if (bossData.Hp <= hpPhase)
        {
            ChangePhase();
        }
    }

    /*IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(enemyAsset.Cooldown);
    }*/

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
            shot = true;
        }
    }

    protected abstract void ChangePhase();
}