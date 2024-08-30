using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluePanel : MonoBehaviour
{
    public GameObject panelClue;
    public string requiredKeyID; // ID of the key needed to interact with this panel
    private bool isAtPanel = false;

    void Update()
    {
        if (isAtPanel && Input.GetKeyDown(KeyCode.E))
        {
            if (KeyManager.HasKey(requiredKeyID))
            {
                panelClue.SetActive(true); // Activate the clue panel if the player has the required key
            }
            else
            {
                Debug.Log("You need the correct key to view this clue."); // Message if the key is missing
            }
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
        if (collision.tag == "Player")
        {
            isAtPanel = false;
            if (panelClue != null)
            {
                panelClue.SetActive(false); // Hide the clue panel when the player leaves the area
            }
        }
    }

    public void OnDoneRead()
    {
        panelClue.SetActive(false); // Method to close the clue panel when done reading
    }
}
