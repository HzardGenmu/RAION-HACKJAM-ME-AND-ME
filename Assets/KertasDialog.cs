using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KertasDialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public float lineWaitTime = 1.0f;

    private int index;
    private KertasPanel kertasPanel;

    void Start()
    {
        // Find the KertasPanel component in the same GameObject or parent
        kertasPanel = GetComponent<KertasPanel>();

        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(lineWaitTime);
        NextLine();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        // Close the panel and destroy the object
        if (kertasPanel != null)
        {
            kertasPanel.OnClosePanel();
        }
    }
}
