using UnityEngine;
//Nicolas I
public class Projectile : MovingObject
{
    [SerializeField] protected PlayerAsset playerAsset;
    private float speed;
    private int damage;
    private Vector3 direction;
    private int range;
   
    
    //Sarah
    public void Init(Sprite sprite, float speed, int damage, Vector3 origin, float angle, Vector3 _direction) 
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        this.speed = speed;
        this.damage = damage;
        this.range = 0;
        direction = _direction;
        if (angle != 0)
             transform.RotateAround(origin, Vector3.forward, angle);

    }
    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 20, LayerMask.GetMask("Aerial", "Ground", "Obstacle"));
        Vector3 transf = transform.position;
        Vector3 trGp = gameObject.transform.position;
        if (hitInfo.collider != null )
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider.CompareTag("Patterns"))
            {
               Destroy(gameObject);
            }
        }
        
        gameObject.transform.Translate(new Vector3(direction.x * speed * Time.deltaTime,
            direction.y * speed * Time.deltaTime, 0));
        
    }
}
