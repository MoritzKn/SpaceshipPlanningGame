using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class part_controler : MonoBehaviour {
    [SerializeField] private string name;
    [SerializeField] private int size = 1;
    public int mass;

    private Material mat_standard;
	private Material mat_selected;

	public Renderer rend;

	public List<GameObject> assigned_ports;
	public bool selected=false;

	void Start(){
		rend = gameObject.GetComponentInChildren<Renderer> ();
		ship_controler ship=GameObject.FindGameObjectWithTag ("ship").GetComponent<ship_controler>();
		mat_standard = ship.mat_standard;
		mat_selected = ship.mat_selected;
	}

	void Update(){
		if (selected && Input.GetKeyDown (KeyCode.Delete)) {
			remove ();
		}
	}
    
    public string getName(){
        return name;
    }
    public int getSize() {
        return size;
    }

	public void placePart(List<GameObject> ports){

		assigned_ports = ports;

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
            port.SetActive(false);
		}
        newPos /= count;
        transform.position = newPos;
        transform.rotation = newRot;
	}

	void OnMouseDown(){

		foreach (GameObject portGroup in GameObject.FindGameObjectsWithTag ("portGroup")) {
			portGroup.GetComponent<portGroup_controler>().resetGroup();
		}

		foreach(GameObject part in GameObject.FindGameObjectsWithTag ("part")){
			part_controler part_ctrl=part.GetComponent<part_controler>();
			part_ctrl.selected=false;
			part_ctrl.rend.material = mat_standard;
		}

		rend.material = mat_selected;
		selected = true;

		foreach (part_controler child in GetComponentsInChildren <part_controler>()) {
			child.selected = true;
			child.rend.material = mat_selected;
		}
	}

	public void remove(){
		foreach (GameObject port in assigned_ports) {
			port.SetActive(true);
		}
		Destroy (gameObject);
	}
}
