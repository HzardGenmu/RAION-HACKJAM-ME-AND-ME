using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorManager : MonoBehaviour
{
    public GameObject colorPanel;
    public GameObject red;
    public GameObject green;
    public GameObject blue;
    private bool isAtPanel = false;

    private Color firstColor = Color.black;
    private Color secondColor = Color.black;
    private bool isFirstColorSelected = false;
    private bool isMixing = false; // Track if colors are being mixed

    private bool isRedSelected;
    private bool isGreenSelected;
    private bool isBlueSelected;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isAtPanel)
        {
            colorPanel.SetActive(true);
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
        if (colorPanel != null)
        {
            colorPanel.SetActive(false);
        }
    }

    public void OnRedButtonPressed()
    {
        HandleColorSelection(Color.red, ref isRedSelected, red);
    }

    public void OnGreenButtonPressed()
    {
        HandleColorSelection(Color.green, ref isGreenSelected, green);
    }

    public void OnBlueButtonPressed()
    {
        HandleColorSelection(Color.blue, ref isBlueSelected, blue);
    }

    private void HandleColorSelection(Color color, ref bool isSelected, GameObject colorObject)
    {
        if (isSelected)
        {
            colorObject.SetActive(false);
            RemoveColor(color);
            isSelected = false;
        }
        else
        {
            // If switching from mixed state to single color, reset second color
            if (isMixing)
            {
                secondColor = Color.black;
                isMixing = false;
            }

            colorObject.SetActive(true);
            AddColor(color);
            isSelected = true;
        }
    }

    private void AddColor(Color color)
    {
        if (!isFirstColorSelected)
        {
            firstColor = color;
            isFirstColorSelected = true;
        }
        else
        {
            secondColor = color;
            isMixing = true; // Mark as mixing when a second color is selected
        }
    }

    private void RemoveColor(Color color)
    {
        if (firstColor == color)
        {
            firstColor = secondColor;
            secondColor = Color.black;
            isFirstColorSelected = false;
        }
        else if (secondColor == color)
        {
            secondColor = Color.black;
        }
    }

    public void OnMixColors()
    {
        if (firstColor != Color.black && secondColor != Color.black)
        {
            Color mixedColor = firstColor + secondColor;
            mixedColor = ClampColor(mixedColor);

            // Set the mixed color as the first color
            firstColor = mixedColor;

            // Clear the second color after mixing
            secondColor = Color.black;

            // Reset selection flags since we are now working with a mixed color
            isRedSelected = false;
            isGreenSelected = false;
            isBlueSelected = false;
            isFirstColorSelected = true; // Keep this true since we still have a first color
            isMixing = false; // Reset mixing state

            // Deactivate the red, green, and blue GameObjects
            red.SetActive(false);
            green.SetActive(false);
            blue.SetActive(false);

            // Provide visual feedback that the mixing was successful
            Debug.Log("MIX");

            // Optional: Update the panel color to the mixed color
            // GetComponent<Image>().color = mixedColor;
        }
    }


    private Color ClampColor(Color color)
    {
        color.r = Mathf.Clamp(color.r, 0, 1);
        color.g = Mathf.Clamp(color.g, 0, 1);
        color.b = Mathf.Clamp(color.b, 0, 1);
        return color;
    }

    public void OnConfirmColor()
    {
        // Save the first color (this could be a single or mixed color)
        PlayerPrefs.SetFloat("ColorR", firstColor.r);
        PlayerPrefs.SetFloat("ColorG", firstColor.g);
        PlayerPrefs.SetFloat("ColorB", firstColor.b);

        // Save the second color if it exists (could be a mixed color or a single color)
        if (secondColor != Color.black)
        {
            PlayerPrefs.SetFloat("SecondColorR", secondColor.r);
            PlayerPrefs.SetFloat("SecondColorG", secondColor.g);
            PlayerPrefs.SetFloat("SecondColorB", secondColor.b);
        }
        else
        {
            // If there's no second color, clear the previous second color data
            PlayerPrefs.DeleteKey("SecondColorR");
            PlayerPrefs.DeleteKey("SecondColorG");
            PlayerPrefs.DeleteKey("SecondColorB");
        }

        colorPanel.SetActive(false);
        //SceneManager.LoadScene("Scene2"); // Load the next scene
    }

    public void ExitColorPanel()
    {
        colorPanel.SetActive(false);
    }
}
