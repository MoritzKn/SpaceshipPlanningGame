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
        Quaternion newRot = new Quaternion();
        int count = 0;
        foreach (GameObject port in ports){

			if(count==0){
				port.transform.parent.GetComponent<portGroup_controler>().resetGroup();
			}

            count++;
            newPos += port.transform.position;
            newRot = port.transform.rotation;
            Destroy(port);
		}
        newPos /= count;
        transform.position = newPos;
        transform.rotation = newRot;
	}

	public void groupSelectionHandler(GameObject caller){
		portGroup_controler caller_child = caller.GetComponent<portGroup_controler>();
		for(int i=0;i<transform.childCount;i++){
			portGroup_controler child = transform.GetChild(i).GetComponent<portGroup_controler>();
			if(caller_child != child){
				child.resetGroup ();
			}
		}
	}
}
