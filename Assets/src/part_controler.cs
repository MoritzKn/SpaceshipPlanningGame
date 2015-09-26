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
        Vector3 newPos = new Vector3(0,0,0);
        int count = 0;
        foreach (GameObject port in ports){
            count++;
            newPos += port.transform.position;
            Debug.Log(newPos);
            Destroy(port);
		}
        newPos /= count;
        transform.position = newPos;
	}
}
