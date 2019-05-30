using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing : Boss
{
    public GameObject blueSlime;
    private RoomManager roomManager;

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
        
    }
}
