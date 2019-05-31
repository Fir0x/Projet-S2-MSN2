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
        GameObject[] slimes = new GameObject[] {blueSlime, blueSlime, blueSlime, blueSlime};
        roomManager.AddEnemies(slimes);
    }

    private void DashAttack()
    {
        isAttacking = true;
        StartCoroutine(DoingDashAttack());
    }

    private IEnumerator DoingDashAttack()
    {
        Vector3 finalPos = playerAsset.Position;

        float step = 4* bossData.Speed * Time.deltaTime;
        while ((transform.position.x > finalPos.x + 0.5f || transform.position.x < finalPos.x - 0.5f) && (transform.position.y > finalPos.y + 0.5f || transform.position.y < finalPos.y - 0.5f))
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
            attack.BossLauncher(Attack.Trajectory.Cqc);
            yield return null;
        }

        isAttacking = false;
    }
}
