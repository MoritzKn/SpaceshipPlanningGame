using UnityEngine;
using System.Collections;

public class launchpadCamera : MonoBehaviour {

	void Update () {
        GameObject ship = GameObject.FindGameObjectWithTag("pad").GetComponentInChildren<ship_controler>().gameObject;
        transform.LookAt(ship.transform);
        float zoom = Vector3.Distance(transform.position, ship.transform.position) - 150;
        zoom /= 3;
        Debug.Log(zoom);
        if (zoom > 28) {
            zoom = 28;
        }
        GetComponent<Camera>().fieldOfView = 16 + zoom;      
	}
}
