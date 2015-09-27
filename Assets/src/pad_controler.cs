using UnityEngine;
using System.Collections;

public class pad_controler : MonoBehaviour {
    float rot;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
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
