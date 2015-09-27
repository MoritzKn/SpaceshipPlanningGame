using UnityEngine;
using System.Collections;

public class pad_controler : MonoBehaviour {
    float rot;
    public GameObject shipBody;
    public GameObject partManager;
    public bool isTested=false;
    public bool isPartManager=false;
	public bool destroy=false;


    void Awake(){
		DontDestroyOnLoad (transform.gameObject);
        foreach (GameObject pad in GameObject.FindGameObjectsWithTag("pad")) {
            if (pad.GetComponent<pad_controler>().isTested) {
                isTested = true;
            }
			if (pad.GetComponent<pad_controler>().destroy) {
				destroy = true;
			}
            if (pad.GetComponent<pad_controler>().isPartManager)
            {
                isPartManager = true;
                print("partmanager");
            }

        }

        if (GameObject.FindGameObjectsWithTag("partManager").Length >= 1) {
            isPartManager = true;
        }

        if (!isPartManager) {
            print("spawned");
            Instantiate(partManager);
            isPartManager = true;
        }

		if (!isTested)
        {
            GameObject newShipBody = Instantiate(shipBody);
            newShipBody.transform.SetParent(transform);
            newShipBody.transform.localPosition = new Vector3(0, 0, 20);
            newShipBody.transform.localEulerAngles = new Vector3(0, 270, 0);
            newShipBody.transform.localScale = new Vector3(3.3f, 3.3f, 3.3f);
        }
        else if(destroy){
            Destroy(gameObject);
        }
	}

	// Update is called once per frame
	void Update () {
        if (!GetComponentInChildren<flight>().isFlying) {
            rot += Input.GetAxis("Horizontal") * 45 * Time.deltaTime; ;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                rot,
                transform.eulerAngles.z
            );
        }
    }

    public void turnRight() {
        rot -= 20;
    }

    public void turnLeft()
    {
        rot += 20;
    }
}
