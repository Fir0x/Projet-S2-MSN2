using UnityEngine;

public class Projectile : MovingObject
{
    private float speed;
    private int damage;
    private Vector2 direction;
    private Vector3 position;

    public void Init(Sprite sprite, float speed, int damage, Vector2 direction)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.direction = direction;
    }

    private void FixedUpdate()
    {
        position = gameObject.transform.position;
        if (Collision(new Vector2(position.x, position.y), direction.x, direction.y))
            Destroy(gameObject);
        
        gameObject.transform.position = new Vector3(position.x + direction.x * speed * Time.deltaTime,
                                                    position.y + direction.y * speed * Time.deltaTime,
                                                    position.y);
    }
}
