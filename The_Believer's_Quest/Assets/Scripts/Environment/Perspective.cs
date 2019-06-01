using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        print(playerBox.bounds.center.y + " =/= " + objectBox.bounds.center.y);
        if (playerBox.bounds.center.y < objectBox.bounds.center.y)
            objectRenderer.sortingOrder = -1;
        else
            objectRenderer.sortingOrder = 2;
    }
}