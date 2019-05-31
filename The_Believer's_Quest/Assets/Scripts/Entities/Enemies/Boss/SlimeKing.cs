using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas L
public class SlimeKing : Boss
{
    public GameObject blueSlime;

    void Start()
    {
        base.Start();
        attackList.Add(DashAttack);
        nbAttacks = 1;

    }

    protected override void ChangePhase()
    {
        bossData.Speed *= 1.5f;
        bossData.Damage *= 1.5f;

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
        while ((transform.position.x > finalPos.x + 0.1f || transform.position.x < finalPos.x - 0.1f) && (transform.position.y > finalPos.y + 0.1f || transform.position.y < finalPos.y - 0.1f))
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
            attack.BossLauncher(Attack.Trajectory.Cqc);
            yield return null;
        }

        isAttacking = false;
    }
}
