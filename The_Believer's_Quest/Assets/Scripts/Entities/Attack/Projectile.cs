using UnityEngine;
//Sarah
public class Projectile : MovingObject
{
    [SerializeField] protected PlayerAsset playerAsset;
    private float speed;
    private float damage;
    private Vector3 direction;
    private bool player;
    private bool effect;

    private float lifetime;

    public void Init(Sprite sprite, float speed, float damage, Vector3 origin, float angle, bool effect) 
    {
        //projectiles ennemis
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        player = false;
        this.effect = effect;
        lifetime = 0;

        transform.RotateAround(origin, Vector3.forward, angle);
        direction = transform.position - origin;

        direction.Normalize();
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(sprite.rect.size.x / 32, sprite.rect.size.y / 32);
    }
    public void Init(Sprite sprite, float speed, float damage, Vector3 origin, float angle) 
    {
        effect = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        player = true;
        lifetime = 0;
        if (angle != 0)
            transform.RotateAround(origin, Vector3.forward, angle);

    }
    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, 0.1f, LayerMask.GetMask("Aerial", "Ground", "Obstacle", "Default"));
   
        if (hitInfo.collider != null)
        {
            print(hitInfo.collider.tag);
            if (hitInfo.collider.CompareTag("Enemy") && player)
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Boss") && player)
            {
                hitInfo.collider.GetComponent<Boss>().ChangeLife(-damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Player") && !player && !playerAsset.Invicibility && Player.instance.testForDash)
            {
                hitInfo.collider.GetComponent<Player>().SetLife(playerAsset.Hp - damage);
                if (effect)
                    hitInfo.collider.GetComponent<Player>().SetEffect(playerAsset.EffectValue + damage / 2);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Pattern"))
               Destroy(gameObject);
        }
        if (lifetime > 50)
            Destroy(gameObject);

        lifetime += 0.2f;
        if (player)
            direction = transform.up;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
    }
}
