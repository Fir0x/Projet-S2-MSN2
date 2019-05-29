using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;               
    public Transform interactionTransform;  

    bool isFocus = false;   
    Transform player; 

    bool isOpen = false; 

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

}
