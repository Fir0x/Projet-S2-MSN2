using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float radius = 3f;               
    private Transform interactionTransform;
    private bool isChest;

    bool isFocus = false;   
    Transform player; 

    bool isOpen = false;

    private void Start()
    {
        isChest = false;
    }
    public virtual void Interact() {}

    void Update()
    {
        if (isFocus && !isOpen)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                isOpen = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        isOpen = false;
    }
    
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        isOpen = false;
    }
    
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && gameObject.tag == "Chest")
        {
            isChest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameObject.tag == "Chest")
        {
            isChest = false;
        }
    }

    public bool IsTrigger()
    {
        return isChest;
    }
}
