using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gui_controler : MonoBehaviour {
    public GameObject partManager;
    public GameObject partButton;

    private GameObject partsGui;
    private bool isVisible;

    void Start () {
        partsGui = GameObject.Find("parts");
    }

    void Update () {
        if (isVisible && partsGui.GetComponent<RectTransform>().anchoredPosition.x < 100)
        {
            partsGui.GetComponent<RectTransform>().anchoredPosition += new Vector2(260f * Time.deltaTime, 0f);
        } else if (!isVisible && partsGui.GetComponent<RectTransform>().anchoredPosition.x > -120)
        {
            partsGui.GetComponent<RectTransform>().anchoredPosition -= new Vector2(260f * Time.deltaTime, 0f);
        }
    }

    public void showParts(int ports) {
        isVisible = true;
        for (int i = 0; i < partsGui.transform.childCount; i++) {
            Destroy(partsGui.transform.GetChild(i).gameObject);
        }

        GameObject[] parts = partManager.GetComponent<part_manager>().parts;
        for (int i = 0; i < parts.Length; i++) {
            int size = parts[i].GetComponent<part_controler>().getSize();
            if (size <= ports) {
                GameObject newPartButton = Instantiate(partButton);
                newPartButton.transform.SetParent(partsGui.transform, true);
                newPartButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,-30 - (50*i));
                GameObject partPrefab = parts[i];
                string text = partPrefab.GetComponent<part_controler>().getName();
                newPartButton.transform.GetChild(0).GetComponent<Text>().text = text;
                newPartButton.GetComponent<partButton_controler>().usedPart = partPrefab;
                newPartButton.GetComponent<partButton_controler>().init();
            }
        }

    }

    public void hideParts(int ports) {
        isVisible = false;
    }
}
