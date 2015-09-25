using UnityEngine;
using System.Collections;

public class part_controler : MonoBehaviour {
    [SerializeField] private string name;
    [SerializeField] private int size = 1;

    public string getName(){
        return name;
    }
    public int getSize() {
        return size;
    }
}
