using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KertasPanel : MonoBehaviour
{
    public GameObject panelKertas; // Assign your panel in the Unity Editor
    private bool isAtPanel = false;

    private string objectID;

    void Start()
    {
        // Create a unique ID for the object based on its name and position
        objectID = gameObject.name + "_" + transform.position.x + "_" + transform.position.y + "_" + transform.position.z;

        // Check if this object has been read before
        if (PlayerPrefs.GetInt(objectID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isAtPanel && Input.GetKeyDown(KeyCode.E))
        {
            panelKertas.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAtPanel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAtPanel = false;
        if (panelKertas != null)
        {
            panelKertas.SetActive(false);
        }
    }

    public void OnClosePanel()
    {
        panelKertas.SetActive(false);

        // Mark this object as read
        PlayerPrefs.SetInt(objectID, 1);
        PlayerPrefs.Save();

        // Destroy the object
        Destroy(gameObject);
    }

    public void OnDoneRead()
    {
        panelKertas.SetActive(false);
    }
}
