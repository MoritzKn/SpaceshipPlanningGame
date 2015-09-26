using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class part_controler : MonoBehaviour {
    [SerializeField] private string name;
    [SerializeField] private int size = 1;

	[SerializeField] private Material mat_standard;
	[SerializeField] private Material mat_selected;
	private Renderer rend;

	private List<GameObject> assigned_ports;
	private bool selected=false;

	void Start(){
		rend = gameObject.GetComponent<Renderer>();
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

	public void groupSelectionHandler(GameObject caller){

		foreach(GameObject part in GameObject.FindGameObjectsWithTag ("part")){
			selected=false;
			rend.material = mat_standard;
		}

		portGroup_controler caller_child = caller.GetComponent<portGroup_controler>();
		for(int i=0;i<transform.childCount;i++){
			portGroup_controler child = transform.GetChild(i).GetComponent<portGroup_controler>();
			if(caller_child != child){
				child.resetGroup ();
			}
		}
	}

	void OnMouseDown(){
		GameObject port_group_parent = assigned_ports [0].transform.parent.GetComponent<GameObject>();
		for (int i=0; i<port_group_parent.transform.childCount; i++) {
			port_group_parent.transform.GetChild(i).GetComponent<portGroup_controler>().resetGroup();
		}
		rend.material = mat_selected;
		selected = true;

		foreach(GameObject part in GameObject.FindGameObjectsWithTag ("part")){
			if(part != gameObject){
				selected=false;
				rend.material = mat_standard;
			}
		}

	}

	public void remove(){
		foreach (GameObject port in assigned_ports) {
			port.SetActive(true);
		}
		Destroy (gameObject);
	}
}
