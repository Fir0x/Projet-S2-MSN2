using UnityEngine;

public class Interactable : MonoBehaviour
{
    private float radius = 3f;               
    private Transform interactionTransform;
    private bool isChest;
    bool debugTrigger;

    bool isFocus = false;   
    Transform player; 

    bool isOpen = false;

    private void Start()
    {
        debugTrigger = true;
        isChest = false;
    }

    //public virtual void Interact() {}

    void Update()
    {
        if (isFocus && !isOpen)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                //Interact();
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
        if(debugTrigger)
        {
            if (gameObject.CompareTag("Chest") && collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().IsNearChest();
            }
            debugTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!debugTrigger)
        {
            if (collision.tag == "Player" && gameObject.tag == "Chest")
            {
                collision.gameObject.GetComponent<Player>().IsNearChest();

            }
            debugTrigger = true;

            ChestUI.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            InventoryUI.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public bool IsTrigger()
    {
        return isChest;
    }
}
