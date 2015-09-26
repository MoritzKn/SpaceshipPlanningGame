using UnityEngine;
using System.Collections;

public class portGroup_controler : MonoBehaviour {

	private Material standard;
	private Material selected;
	private Material selectable;

	private ship_controler ship;

	void Start(){
		ship=GameObject.FindGameObjectWithTag ("ship").GetComponent<ship_controler>();
		standard = ship.mat_standard;
		selected = ship.mat_selected;
		selectable = ship.mat_selectable;
	}

	public void selectionHandler(GameObject caller){

		port_controler caller_child = caller.GetComponent<port_controler> ();

		if (!Input.GetKey (KeyCode.LeftControl) || caller_child.selectable) {

			for (int i=0; i< transform.childCount; i++) {

				port_controler child = transform.GetChild (i).GetComponent<port_controler> ();

				if (child == caller_child) {
					child.selected = true;
				}

				if (!Input.GetKey (KeyCode.LeftControl)) {
					if (child != caller_child) {
						child.selected = false;
					}
				}
			}
		}

		bool prev_selected;
		bool next_selected;

		int selectionCount = 0;
		for (int i=0; i< transform.childCount; i++) {
			prev_selected = false;
			next_selected = false;

			if (i > 0) {
				prev_selected = transform.GetChild (i - 1).GetComponent<port_controler> ().selected;
			}
			port_controler child = transform.GetChild (i).GetComponent<port_controler> ();
			if (i < transform.childCount - 1) {
				next_selected = transform.GetChild (i + 1).GetComponent<port_controler> ().selected;
			}

			if (child.selected) {
				child.rend.material = selected;
				selectionCount++;
			} else if (prev_selected || next_selected) {
				child.rend.material = selectable;
				child.selectable = true;
			} else {
				child.rend.material = standard;
				child.selectable = false;
			}
		}
		if (selectionCount > 0) {
			GameObject.Find ("Canvas").GetComponent<gui_controler> ().showParts (selectionCount);
			selectionCount = 0;
		}
		if (!Input.GetKey (KeyCode.LeftControl) || caller_child.selectable) {
			ship.groupSelectionHandler(gameObject);
		}
	}

	public void resetGroup(){
		for (int i=0; i< transform.childCount; i++) {
			port_controler child=transform.GetChild(i).GetComponent<port_controler>();
			child.selected=false;
			child.selectable=false;
			child.rend.material=standard;
		}
	}
}
