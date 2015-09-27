using UnityEngine;
using System.Collections;

public class flight : MonoBehaviour {
    public bool isFlying = false;
    public bool isStarting = false;

	float flytime=0;

	GameObject clone;

    Vector3 centerOfMass;
    int entireMass;

    // Use this for initialization
    public void startFlight() {
        centerOfMass = new Vector3(0, 0, 0);
        entireMass = 0;


        Application.LoadLevel ("start");
        
		gameObject.GetComponentInParent<pad_controler>().isTested = true;
		gameObject.GetComponentInParent<pad_controler>().destroy = false;

		clone = Instantiate (transform.parent.gameObject);
		clone.SetActive (false);

		transform.parent.GetComponent<Renderer> ().enabled = false;

        transform.position += new Vector3(0,3,0);
        transform.localEulerAngles = new Vector3(270, 270, 0);

        foreach (part_controler partCon in GetComponentsInChildren<part_controler>())
            Destroy(partCon);

        isStarting = true;
        //Detect center of mass
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
    }

   void startPhysics() {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.centerOfMass = (rb.centerOfMass*14 + centerOfMass - transform.position)/15;
        rb.mass = entireMass / 20f;
        rb.angularDrag = 30;
        rb.drag = 3;
        
		foreach(engine_controler engine in GetComponentsInChildren<engine_controler>()){
			engine.applyForce(rb);
		}
    }

    void StoppFlight() {
        Application.LoadLevel("editor");
        isFlying = false;
        isStarting = false;
        flytime = 0;
        clone.SetActive(true);
        clone.GetComponent<pad_controler>().destroy = true;
        Destroy(gameObject.transform.parent.gameObject);
    }

// Update is called once per frame
void Update () {
        if (isStarting) {
			flytime+=Time.deltaTime;
		}

		if(flytime>4 && !isFlying)
        {
            startPhysics();
            isFlying = true;
        }
    }
}
