using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    [SerializeField] private ObjectsAsset objectsAsset;

    public ObjectsAsset ObjectsAsset { get => objectsAsset; set => objectsAsset = value; }

    public ObjectsAsset GetAsset()
    {
        return ObjectsAsset;
    }
}
