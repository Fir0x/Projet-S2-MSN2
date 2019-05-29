using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKing : Boss
{
    protected override void ChangePhase()
    {
        gameObject.GetComponentInParent<RoomManager>();
    }
}
