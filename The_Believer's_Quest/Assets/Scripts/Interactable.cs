using UnityEngine;
//Maxence
public class Interactable : MonoBehaviour
{
    private Transform interactionTransform;
    private bool isChest;
    bool debugTrigger;

    Transform player; 

    private void Start()
    {
        debugTrigger = true;
        isChest = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(debugTrigger)
        {
            if (gameObject.CompareTag("Chest") && collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().IsNearChest();
            }
            else if (gameObject.CompareTag("Shop") && collision.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().IsNearShop();
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
            else if (collision.tag == "Player" && gameObject.tag == "Shop")
            {
                collision.gameObject.GetComponent<Player>().IsNearShop();
            }

            debugTrigger = true;

            ChestUI.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            InventoryUI.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            ShopUI.instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Player").GetComponent<Player>().canAttack = true;
        }
    }

    public bool IsTrigger()
    {
        return isChest;
    }
}
