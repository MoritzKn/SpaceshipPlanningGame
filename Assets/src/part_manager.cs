using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class part_manager : MonoBehaviour
{
    public List<GameObject> parts;
    void Awake()
    {
        DontDestroyOnLoad(gameObject); 
    }
}
