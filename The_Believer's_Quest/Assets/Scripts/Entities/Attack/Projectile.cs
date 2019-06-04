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

    public void Init(Sprite sprite, float speed, float damage, Vector3 origin, float angle, Vector3 _direction, bool player, bool circle, bool effect) 
    {
        //projectiles ennemis
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.player = player;
        this.effect = effect;

        if (circle || _direction == new Vector3(0, 0, 0))
            _direction = transform.up;
        if (angle != 0)
        {
            transform.RotateAround(origin, Vector3.forward, angle);
        }
        else
            direction = _direction.normalized;
    }
    public void Init(Sprite sprite, float speed, float damage, Vector3 origin, float angle, bool player) 
    {
        effect = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.player = player;
        if (angle != 0)
            transform.RotateAround(origin, Vector3.forward, angle);

    }
    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, 0.1f, LayerMask.GetMask("Aerial", "Ground", "Obstacle", "Default"));
   
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy") && player)
            {
                print("touché");
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Boss") && player)
            {
                hitInfo.collider.GetComponent<Boss>().ChangeLife(-damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Player") && !player)
            {
                hitInfo.collider.GetComponent<Player>().SetLife(playerAsset.Hp - damage);
                if (effect)
                    hitInfo.collider.GetComponent<Player>().SetEffect(playerAsset.EffectValue + damage / 2);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Pattern"))
            {
               Destroy(gameObject);
            }
        }
        
        if (player)
            direction = transform.up;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
    }
}
