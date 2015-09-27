using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class dialogue : MonoBehaviour {
    public bool isAndersUndSchlimmer;

    void Awake()
    {
        if (isAndersUndSchlimmer) {
            List<GameObject> parts = GameObject.FindGameObjectWithTag("partManager").GetComponent<part_manager>().parts;
            parts.Reverse();
            List<GameObject> newParts = new List<GameObject>();
            bool isFirst = true;

            foreach (GameObject part in parts) {
                if (!isFirst)
                {
                    newParts.Add(part);
                }
                else {
                    isFirst = false;
                    GameObject.Find("Text_m").GetComponent<Text>().text = "You show the finished plans to the commander. But he has bad news for you: The "+
                        part.GetComponent<part_controler>().getName() +" part won't be able to run so you have to find an alternative";
                }
            }
            newParts.Reverse();
            GameObject.FindGameObjectWithTag("partManager").GetComponent<part_manager>().parts = newParts;
        }
    }

    public void startEditor () {
        Application.LoadLevel("editor");
	}


}
