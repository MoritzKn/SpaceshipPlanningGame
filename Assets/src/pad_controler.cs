using UnityEngine;
using System.Collections;

public class pad_controler : MonoBehaviour {
    float rot;
    public GameObject shipBody;
    public bool isTested;

    void Awake(){
        Debug.Log("awakle");
		DontDestroyOnLoad (transform.gameObject);
        foreach (GameObject pad in GameObject.FindGameObjectsWithTag("pad")) {
            Debug.Log("pad");
            if (pad.GetComponent<pad_controler>().isTested) {
                Debug.Log("tested");
                isTested = true;
            }
        }

        if (!isTested)
        {
            GameObject newShipBody = Instantiate(shipBody);
            newShipBody.transform.SetParent(transform);
            newShipBody.transform.localPosition = new Vector3(0, 0, 20);
            newShipBody.transform.localEulerAngles = new Vector3(0, 270, 0);
            newShipBody.transform.localScale = new Vector3(3.3f, 3.3f, 3.3f);
        }
        else {
            Debug.Log("destroy");
            Destroy(gameObject);
        }
	}

	// Update is called once per frame
	void Update () {
        rot += Input.GetAxis("Horizontal") * 45 * Time.deltaTime; ;
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            rot,
            transform.eulerAngles.z
        );
    }

    public void turnRight() {
        rot -= 20;
    }

    public void turnLeft()
    {
        rot += 20;
    }
}
