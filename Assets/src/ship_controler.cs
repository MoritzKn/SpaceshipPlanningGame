using UnityEngine;
using System.Collections;

public class ship_controler : MonoBehaviour {

	[SerializeField] private Material mat_standard;

	public void groupSelectionHandler(GameObject caller){
		
		foreach(GameObject part in GameObject.FindGameObjectsWithTag ("part")){
			part_controler part_ctrl=part.GetComponent<part_controler>();
			part_ctrl.selected=false;
			part_ctrl.rend.material = mat_standard;
		}

		portGroup_controler caller_child = caller.GetComponent<portGroup_controler>();
		foreach (GameObject portGroup in GameObject.FindGameObjectsWithTag("portGroup")) {
			portGroup_controler child = portGroup.GetComponent<portGroup_controler>();
			if(caller_child != child){
				child.resetGroup ();
			}
		}
	}
}
