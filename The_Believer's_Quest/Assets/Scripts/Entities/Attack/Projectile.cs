using System.Security.Cryptography;
using UnityEngine;
//Nicolas I
public class Projectile : MovingObject
{
    [SerializeField] protected PlayerAsset playerAsset;
    private float speed;
    private int damage;
    private Vector3 direction;
    private Vector3 position;
    private int range;
    
    public void Init(Sprite sprite, float speed, int damage, Vector3 direction)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        range = -1;
    }
    public void Init(Sprite sprite, float speed, int damage, Vector3 direction, int range) //Sarah
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
        this.range = range;
    }
    private void FixedUpdate()
    {
        if (position.x >100 |position.y > 100)
            Destroy(gameObject);
        gameObject.transform.Translate(new Vector3(position.x + direction.x * speed * Time.deltaTime,position.y + direction.y * speed * Time.deltaTime,position.z));
        position.x = position.x + direction.x * speed * Time.deltaTime;
        position.y = position.y + direction.y * speed * Time.deltaTime;
    }
}
