using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas L
public class Strider : Boss
{
    public GameObject blueSlime;

    bool test;

    void Start()
    {
        base.Start();
        attackList.Add(DashAttack);
        nbAttacks = 1;
        test = true;

    }

    protected override void ChangePhase()
    {
        bossData.Speed *= 1.5f;
        bossData.Damage *= 1.5f;

        attackList.Add(OverAttack);
        nbAttacks += 1;
    }

    private void OverAttack()
    {
        isAttacking = true;
        StartCoroutine(BigAttack());
    }

    private IEnumerator BigAttack()
    {
        for(int i = 0; i < 5; i++)
        {
            StartCoroutine(Throwing(0.5f));
            yield return null;
        }
    }

    private void DashAttack()
    {
        isAttacking = true;
        StartCoroutine(DoingDashAttack());
    }

    private IEnumerator DoingDashAttack()
    {
        Vector3 finalPos = playerAsset.Position;

        float step = 2 * bossData.Speed * Time.deltaTime;
        while ((transform.position.x > finalPos.x + 0.1f || transform.position.x < finalPos.x - 0.1f) && (transform.position.y > finalPos.y + 0.1f || transform.position.y < finalPos.y - 0.1f))
        {
            if (test)
            {
                Throwing(1f);
            }
            
            transform.position = Vector3.MoveTowards(transform.position, finalPos, step);
            attack.BossLauncher(Attack.Trajectory.Cqc);
            yield return null;
        }

        isAttacking = false;
    }

    private IEnumerator Throwing(float sec)
    {
        test = false;
        attack.BossLauncher(Attack.Trajectory.Circle);
        yield return new WaitForSeconds(sec);
        test = true;
    }
}
