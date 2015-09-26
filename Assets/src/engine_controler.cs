using UnityEngine;
using System.Collections;

public class engine_controler : MonoBehaviour {

	[SerializeField] private int thrust;

	// Use this for initialization
	void startEngine () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void applyForce(Rigidbody body){
		body.AddForceAtPosition (gameObject.transform.forward*thrust, gameObject.GetComponent<Renderer>().bounds.center);
	}
}
