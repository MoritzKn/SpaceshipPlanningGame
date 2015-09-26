using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class partButton_controler : MonoBehaviour {
    public GameObject usedPart;
    GameObject shipBody;

    public void init () {
        shipBody = GameObject.Find("shipBody");
        GetComponent<Button>().onClick.AddListener(() => placePart());
    }

    public void placePart(){
        GameObject newPart = Instantiate(usedPart);

        GameObject[] ports =  GameObject.FindGameObjectsWithTag("port");
        List<GameObject> selectedPorts = new List<GameObject>();
        for (int i = 0; i < ports.Length; i++) {
            if (ports[i].GetComponent<port_controler>().selected) {
                selectedPorts.Add(ports[i]);
                if (ports[i].transform.GetComponentInParent<part_controler> () != null)
                {
                    newPart.transform.SetParent(ports[i].transform.GetComponentInParent<part_controler>().transform);
                }
                else {
                    newPart.transform.SetParent(ports[i].transform.GetComponentInParent<ship_controler>().transform);
                }
            }
        }
        newPart.GetComponent<part_controler>().placePart(selectedPorts);
        GameObject.Find("Canvas").GetComponent<gui_controler>().hideParts();
    }
}
