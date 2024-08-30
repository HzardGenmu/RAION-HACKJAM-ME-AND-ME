using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClueDialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public float lineWaitTime = 1.0f;

    private bool isTextFinished = false;
    private int index;
    private CluePanel panel;

    void Start()
    {
        // Find the CluePanel component in the same GameObject or parent
        panel = GetComponent<CluePanel>();

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

        // Close the panel if the text is finished and player clicks
        if (isTextFinished && Input.GetMouseButtonDown(0))
        {
            EndDialogue();
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
            OnTextFinished();
        }
    }

    public void OnTextFinished()
    {
        isTextFinished = true; // Set the flag when the text is done
    }

    void EndDialogue()
    {
        // Close the panel using CluePanel's method
        if (panel != null)
        {
            panel.OnDoneRead();
        }
    }
}
