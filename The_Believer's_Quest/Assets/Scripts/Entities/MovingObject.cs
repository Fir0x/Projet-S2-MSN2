using UnityEngine;
using Vector2 = UnityEngine.Vector2;
//Nicolas L
public class MovingObject : MonoBehaviour
{

    public bool Collision(Vector2 pos, float speed, int direction)     //0 : left, 1 : up, 2 : right, 3 : down
    {
        bool noCollision = true;

        switch (direction)
        {
            case 0:
                RaycastHit2D hitLeft = Physics2D.Raycast(pos + new Vector2(-0.2f, -0.35f), new Vector2(-1, 0), speed * Time.deltaTime, LayerMask.GetMask("Obstacle"));
                Debug.DrawRay(pos + new Vector2(-0.2f, -0.35f), new Vector2(-1, 0) * speed * Time.deltaTime, Color.red);
                
                if (hitLeft.collider != null && hitLeft.collider.gameObject.CompareTag("Pattern"))
                {
                    noCollision = false;
                }
                break;

            case 1:
                RaycastHit2D hitUp1 = Physics2D.Raycast(pos + new Vector2(-0.15f, -0.3f), new Vector2(0, 1), speed * Time.deltaTime, LayerMask.GetMask("Obstacle"));
                RaycastHit2D hitUp2 = Physics2D.Raycast(pos + new Vector2(0.15f, -0.3f), new Vector2(0, 1), speed * Time.deltaTime, LayerMask.GetMask("Obstacle"));

                Debug.DrawRay(pos + new Vector2(-0.15f, -0.3f), new Vector2(0, 1) * speed * Time.deltaTime, Color.red);
                Debug.DrawRay(pos + new Vector2(0.15f, -0.3f), new Vector2(0, 1) * speed * Time.deltaTime, Color.red);

                if (hitUp1.collider != null && hitUp2.collider != null)
                {
                    if (hitUp1.collider.gameObject.CompareTag("Pattern") || hitUp2.collider.gameObject.CompareTag("Pattern"))
                    {
                        noCollision = false;
                    }
                }
                else if (hitUp1.collider != null)
                {
                    if (hitUp1.collider.gameObject.CompareTag("Pattern"))
                    {
                        noCollision = false;
                    }
                }
                else if (hitUp2.collider != null)
                {
                    if (hitUp2.collider.gameObject.CompareTag("Pattern"))
                    {
                        noCollision = false;
                    }
                }
                break;

            case 2:
                RaycastHit2D hitRight = Physics2D.Raycast(pos + new Vector2(0.2f, -0.35f), new Vector2(1, 0), speed * Time.deltaTime, LayerMask.GetMask("Obstacle"));
                Debug.DrawRay(pos + new Vector2(0.2f, -0.35f), new Vector2(1, 0) * speed * Time.deltaTime, Color.red);

                if (hitRight.collider != null && hitRight.collider.gameObject.CompareTag("Pattern"))
                {
                    noCollision = false;
                }
                break;

            default:
                RaycastHit2D hitDown1 = Physics2D.Raycast(pos + new Vector2(-0.15f, -0.4f), new Vector2(0, -1), speed * Time.deltaTime, LayerMask.GetMask("Obstacle"));
                RaycastHit2D hitDown2 = Physics2D.Raycast(pos + new Vector2(0.15f, -0.4f), new Vector2(0, -1), speed * Time.deltaTime, LayerMask.GetMask("Obstacle"));

                Debug.DrawRay(pos + new Vector2(-0.15f, -0.4f), new Vector2(0, -1) * speed * Time.deltaTime, Color.red);
                Debug.DrawRay(pos + new Vector2(0.15f, -0.4f), new Vector2(0, -1) * speed * Time.deltaTime, Color.red);

                if (hitDown1.collider != null && hitDown2.collider != null)
                {
                    if (hitDown1.collider.gameObject.CompareTag("Pattern") || hitDown2.collider.gameObject.CompareTag("Pattern"))
                    {
                        noCollision = false;
                    }
                }
                else if (hitDown1.collider != null)
                {
                    if (hitDown1.collider.gameObject.CompareTag("Pattern"))
                    {
                        noCollision = false;
                    }
                }
                else if (hitDown2.collider != null)
                {
                    if (hitDown2.collider.gameObject.CompareTag("Pattern"))
                    {
                        noCollision = false;
                    }
                }
                break;
        }

        return noCollision;
    }
}