using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class flight : MonoBehaviour {
    public bool isFlying = false;
    public bool isStarting = false;
    public bool isCrashed = false;
    
    public GameObject explosion;
	public float maxHeight=0;

    float flytime=-4;

	GameObject clone;

    int stabilization = 0;
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

        transform.position += new Vector3(0f, 3.4f, 0f);
        transform.localEulerAngles = new Vector3(270, 270, 0);


        foreach (part_controler partCon in GetComponentsInChildren<part_controler>()) {
            stabilization += partCon.stabilization;
            Destroy(partCon);
        }

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
        rb.centerOfMass = (rb.centerOfMass*9 + centerOfMass - transform.position)/10;
        rb.mass = entireMass / 32f;
        rb.angularDrag = 20 + stabilization;
        rb.drag = 4;
        
		foreach(engine_controler engine in GetComponentsInChildren<engine_controler>()){
			engine.applyForce(rb);
			gameObject.GetComponent<AudioSource>().Play();
		}

    }

    public void StoppFlight() {
        isFlying = false;
        isStarting = false;
        flytime = 0;
        clone.SetActive(true);
        clone.GetComponent<pad_controler>().destroy = true;
        Application.LoadLevel("editor");
        Destroy(gameObject.transform.parent.gameObject);
    }

// Update is called once per frame
void Update () {
        if (isCrashed) {
            flytime += Time.deltaTime;
            if (flytime > 5)
                StoppFlight();
        }
        else if (isStarting || isFlying) {
			flytime+=Time.deltaTime;
            RaycastHit hit;
            Ray downRay = new Ray(new Vector3(transform.position.x, transform.position.y-8, transform.position.z), -Vector3.up);
            if (Physics.Raycast(downRay, out hit))
            {
                float hight = hit.distance * 3;
				maxHeight=Mathf.Max(maxHeight,hight);
                GameObject.Find("hightCount").GetComponent<Text>().text = Mathf.Round(hight).ToString() + " m";
            }
            if (GameObject.Find("countdown"))
                GameObject.Find("countdown").GetComponent<Text>().text = (flytime + 0.0001).ToString();
		}

		if (flytime > 0 && !isFlying)
        {
            startPhysics();
            isFlying = true;
            GameObject.Find("countdown").transform.parent.GetComponent<Image>().enabled = false;
            GameObject.Find("countdown").GetComponent<Text>().enabled = false;
            GameObject.Find("hightCount").transform.parent.GetComponent<Image>().enabled = true;
            GameObject.Find("hightCount").GetComponent<Text>().enabled = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isFlying && !isCrashed) {
            Instantiate(explosion).transform.position = transform.position;
            foreach (GameObject part in GameObject.FindGameObjectsWithTag("part")) {
                Rigidbody rb = part.AddComponent<Rigidbody>();
            }
            flytime = 0;
            isCrashed = true;
        }
    }
}
