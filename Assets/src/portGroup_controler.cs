using UnityEngine;
using System.Collections;

public class portGroup_controler : MonoBehaviour {

	public Material standard;
	public Material selected;
	public Material selectable;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void selectionHandler(GameObject caller){
		port_controler caller_child = caller.GetComponent<port_controler> ();
		for (int i=0; i< transform.childCount; i++) {
			port_controler child=transform.GetChild(i).GetComponent<port_controler>();

			if(child==caller_child){
				child.selected=true;
				child.rend.material=selected;
			}


			if(!Input.GetKey(KeyCode.LeftControl)){
				if(child!=caller_child){
					child.selected=false;
					child.rend.material=selectable;
				}
			}
		}
	}
}
