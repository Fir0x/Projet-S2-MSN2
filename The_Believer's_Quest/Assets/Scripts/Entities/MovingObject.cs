using UnityEngine;
using Vector2 = UnityEngine.Vector2;
//Nicolas L
public class MovingObject : MonoBehaviour
{

    public bool Collision(Vector2 pos, float x, float y, float speed)
    {
        bool noCollision = true;
        RaycastHit2D hit1 = Physics2D.Raycast(pos + new Vector2(0.2f, -0.4f), new Vector2(x, y), speed * Time.deltaTime * 1.5f);
        RaycastHit2D hit2 = Physics2D.Raycast(pos + new Vector2(-0.2f, -0.4f), new Vector2(x, y), speed * Time.deltaTime * 1.5f);
        Debug.DrawRay(pos + new Vector2(0.2f, -0.4f), new Vector2(x, y) * speed * Time.deltaTime, Color.red);
        Debug.DrawRay(pos + new Vector2(-0.2f, -0.4f), new Vector2(x, y) * speed * Time.deltaTime, Color.red);

        if (hit1.collider != null && hit2.collider != null)
        {
            if (hit1.collider.gameObject.CompareTag("Pattern") || hit2.collider.gameObject.CompareTag("Pattern"))
            {
                noCollision = false;
            }
        }
        else if (hit1.collider != null)
        {
            if (hit1.collider.gameObject.CompareTag("Pattern"))
            {
                noCollision = false;
            }
        }
        else if (hit2.collider != null)
        {
            if (hit2.collider.gameObject.CompareTag("Pattern"))
            {
                noCollision = false;
            }
        }

        return noCollision;
    }
}