using UnityEngine;
using System.Collections;

public class flight : MonoBehaviour {
    bool isFlying = false;

	float flytime=0;

	GameObject clone;

    // Use this for initialization
    void startFlight() {

		Application.LoadLevel ("start");

		//clone=Instantiate (gameObject);

		//gameObject.SetActive (false);
		//clone.GetComponent<Renderer>().enabled = false;

		//print ("original");

        isFlying = true;
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


        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.centerOfMass = (rb.centerOfMass*14 + centerOfMass - transform.position)/15;
        rb.mass = entireMass / 20f;
        rb.angularDrag = 30;
        rb.drag = 1;
        
		foreach(engine_controler engine in GetComponentsInChildren<engine_controler>()){
			engine.applyForce(rb);
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Backspace) && !isFlying) {
            startFlight();
        }

		if (isFlying) {
			flytime+=Time.deltaTime;
			print (flytime);
		}

		if(flytime>10){
			//Destroy (clone);
			Application.LoadLevel ("editor");
			isFlying=false;
			flytime=0;
		}
	}
}
