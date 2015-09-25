using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class part_controler : MonoBehaviour {
    [SerializeField] private string name;
    [SerializeField] private int size = 1;
    
    public string getName(){
        return name;
    }
    public int getSize() {
        return size;
    }

	public void placePart(List<GameObject> ports){
		Instantiate (gameObject);
		transform.localToWorldMatrix = ports [0].transform.localToWorldMatrix;
		foreach(GameObject port in ports){
			Destroy (port);
		}
	}
}
