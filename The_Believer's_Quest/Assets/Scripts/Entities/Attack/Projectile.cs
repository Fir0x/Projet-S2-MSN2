using UnityEngine;
//Nicolas I
public class Projectile : MovingObject
{
    [SerializeField] protected PlayerAsset playerAsset;
    private float speed;
    private int damage;
    private Vector3 direction;
    private bool player;
   
    
    //Sarah
    public void Init(Sprite sprite, float speed, int damage, Vector3 origin, float angle, Vector3 _direction, bool player) 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.player = player;
        direction = _direction;
        if (angle != 0)
             transform.RotateAround(origin, Vector3.forward, angle);

    }
    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 20, LayerMask.GetMask("Aerial", "Ground", "Default"));
   
        if (hitInfo.collider != null )
        {
            if (hitInfo.collider.CompareTag("Enemy") && player)
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Player") && !player)
            {
                hitInfo.collider.GetComponent<Player>().SetLife(playerAsset.Hp - damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Pattern"))
            {
               Destroy(gameObject);
            }
        }
        gameObject.transform.Translate(new Vector3(direction.x * speed * Time.deltaTime,
            direction.y * speed * Time.deltaTime, 0));
        
    }
}
