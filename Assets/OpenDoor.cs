using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    private bool isAtCode;

    [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string doorCode;
    public GameObject codePanel;
    public GameObject doorPanel;
    public GameObject doorCollider;

    // Update is called once per frame
    void Update()
    {
        CodeText.text = codeTextValue;

        if (Input.GetKeyDown(KeyCode.E) && isAtCode == true)
        {
            if (Key.keyCount >= 3)
            {
                codePanel.SetActive(true);
            }
            else
            {
                Debug.Log("Collect all keys before opening the door!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isAtCode = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAtCode = false;
        codePanel.SetActive(false);
    }

    public void AddDigit(string digit)
    {
        if (codeTextValue.Length < 3)
        {
            codeTextValue += digit;
        }
    }

    public void RemoveLastDigit()
    {
        if (codeTextValue.Length > 0)
        {
            codeTextValue = codeTextValue.Substring(0, codeTextValue.Length - 1);
        }
    }

    public void ConfirmCode()
    {
        if (codeTextValue == doorCode)
        {
            codePanel.SetActive(false);
            codeTextValue = "";
            doorPanel.SetActive(false);
            doorCollider.SetActive(true);
        }
        else
        {
            codeTextValue = "";
        }
    }

    public void ExitCodePanel()
    {
        if (codeTextValue.Length > 0)
        {
            RemoveLastDigit();
        }
        else
        {
            codePanel.SetActive(false);
        }
    }
}
