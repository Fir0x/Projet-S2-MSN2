using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicolas L
public class Perspective : MonoBehaviour
{

    private GameObject player;

    private BoxCollider2D playerBox;
    private BoxCollider2D objectBox;
    private SpriteRenderer objectRenderer;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerBox = player.GetComponent<BoxCollider2D>();
        objectBox = GetComponent<BoxCollider2D>();
        objectRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerBox.bounds.center.y < objectBox.bounds.center.y)
        {
            objectRenderer.sortingOrder = -1;
            print("épepeé");
            if (gameObject.name == "Shopper")
            {
                print("avant:" + gameObject.transform.Find("Gandulf").GetComponent<SpriteRenderer>().sortingOrder);
                for (int i = 0; i < gameObject.transform.childCount; i ++)
                {
                    if (gameObject.transform.GetChild(i).name == "Gandulf" || gameObject.transform.GetChild(i).name == "Bag")
                    {
                        gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = -1;
                    }
                    else
                    {
                        gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = -2;
                    }
                }
                print("après:" + gameObject.transform.Find("Gandulf").GetComponent<SpriteRenderer>().sortingOrder);
            }
        }
        else
        {
            objectRenderer.sortingOrder = 2;
            if (gameObject.name == "Shopper")
            {
                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    if (gameObject.transform.GetChild(i).name == "Gandulf" || gameObject.transform.GetChild(i).name == "Bag")
                    {
                        gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 3;
                    }
                    else
                    {
                        gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                }

            }
        }
    }
}