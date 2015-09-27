using UnityEngine;
using System.Collections;

public class engine_controler : MonoBehaviour {

	[SerializeField] private int thrust;
    Rigidbody rb;
    bool isActive = false;
    public GameObject fire;

	// Update is called once per frame
	void Update () {
        if (isActive) {
            rb.AddForce(gameObject.transform.forward * -thrust * (Time.deltaTime * 120));
            rb.AddForceAtPosition(gameObject.transform.forward * -thrust/4 * (Time.deltaTime * 120), gameObject.GetComponentInChildren<Renderer>().bounds.center);
        }
    }

	public void applyForce(Rigidbody body){
        isActive = true;
        rb = body;
        GameObject newFire = Instantiate(fire);
        newFire.transform.SetParent(transform);
        newFire.transform.position = transform.position - new Vector3(0,2,0);
    }
}
