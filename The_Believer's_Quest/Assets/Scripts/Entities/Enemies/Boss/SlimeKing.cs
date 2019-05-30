using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas L
public class SlimeKing : Boss
{
    public GameObject blueSlime;
    private RoomManager roomManager;

    void Start()
    {
        base.Start();
        attackList.Add(DashAttack);
        nbAttacks = 1;
    }

    protected override void ChangePhase()
    {
        roomManager = gameObject.GetComponentInParent<RoomManager>();
        InvokeRepeating("SpawnSlimes", 0.1f, 30f);
    }

    private void SpawnSlimes()
    {
        GameObject[] slimes = new GameObject[] { blueSlime, blueSlime, blueSlime, blueSlime};
        roomManager.AddEnemies(slimes);
    }

    private void DashAttack()
    {
        isAttacking = true;
        Vector3 finalPos = playerAsset.Position;
        while (transform.position != finalPos)
        {
            transform.Translate((finalPos - transform.position).normalized);
            attack.Launcher();
        }
        isAttacking = false;
    }
}
