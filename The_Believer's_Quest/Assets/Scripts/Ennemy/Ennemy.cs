using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    protected int life = 3;
    protected int effectGauge = 0;
    private PlayerAsset player;
    private float moveSpeed = 1.5f;

    public Vector2 GetPos()
    {
        return this.transform.position;
    }
    public int GetLife()
    {
        return life;
    }

    public void SetLife(int life)
    {
        this.life = life;
    }

    protected void Attack()
    {
        player.Hp -= 2;
    }

    protected void Move()
    {
        Vector2 posPlayer = player.Position;
        float moveX = moveSpeed * Time.deltaTime;
        float moveY = moveSpeed * Time.deltaTime;

        this.transform.Translate(posPlayer);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Start()
    {
        Move();
    }
}
