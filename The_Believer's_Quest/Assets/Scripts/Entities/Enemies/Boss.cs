using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    private delegate void Attack();
    private List<Attack> attackList;

    [SerializeField] private EnemyAsset bossData;
    private int hpPhase;
    private Animator animator;

    public EnemyAsset BossData { get => bossData; set => bossData = value; }

    private void Awake()
    {
        attackList = new List<Attack>();
        animator = GetComponent<Animator>();
        hpPhase = bossData.Hp / 2;
    }

    public void SetHp(int hp)
    {
        bossData.Hp = hp;
        if (bossData.Hp <= hpPhase)
        {
            ChangePhase();
        }
    }

    protected abstract void ChangePhase();
}