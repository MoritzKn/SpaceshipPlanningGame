using UnityEngine;
using System.Collections;

public class flight : MonoBehaviour {

    // Use this for initialization
    void startFlight() {
        //Detect center of mass
        Vector3 centerOfMass = new Vector3(0,0,0);
        int entireMass = 0;
        GameObject[] parts = GameObject.FindGameObjectsWithTag("part");
        for (int i = 0; i < parts.Length; i++) {
            int mass = parts[i].GetComponent<part_controler>().mass;
            entireMass += mass;
        }
        entireMass += gameObject.GetComponent<ship_controler>().mass;
        for (int i = 0; i < parts.Length; i++)
        {
            int mass = parts[i].GetComponent<part_controler>().mass;
            centerOfMass += parts[i].GetComponentInChildren<Renderer>().bounds.center * ((float) mass / entireMass);
        }
        centerOfMass += gameObject.GetComponent<Renderer>().bounds.center * ((float) gameObject.GetComponent<ship_controler>().mass / entireMass);

        Debug.DrawLine(new Vector3(0,0,0), centerOfMass, Color.blue, 2f);

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;
        rb.mass = entireMass / 45;
        
		foreach(engine_controler engine in GetComponentsInChildren<engine_controler>()){
			engine.applyForce(rb);
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Backspace)) {
            startFlight();
        }
	}
}
