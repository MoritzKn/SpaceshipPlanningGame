using UnityEngine;
using System.Collections;

public class port_controler : MonoBehaviour {

	public Material mat_selected;
	public Material standard;
	public bool selected;
	Renderer rend;
	Collider coll;

	// Use this for initialization
	void Start () {
		 rend = gameObject.GetComponent<Renderer>();
		 coll = gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			RaycastHit hitinfo;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray,out hitinfo) && hitinfo.collider==coll ){
				rend.material = mat_selected;
				selected=true;

			}
			else{
				rend.material = standard;
				selected=false;
			}
		}
	}
}
