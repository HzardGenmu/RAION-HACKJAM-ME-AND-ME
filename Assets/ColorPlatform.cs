using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlatform : MonoBehaviour
{
    public GameObject[] redPlatforms;
    public GameObject[] greenPlatforms;
    public GameObject[] bluePlatforms;

    public GameObject[] yellowPlatforms;  // Red + Green
    public GameObject[] cyanPlatforms;    // Green + Blue
    public GameObject[] magentaPlatforms; // Red + Blue

    void Start()
    {
        // Retrieve the first color from Scene 1
        float r = PlayerPrefs.GetFloat("ColorR", 0);
        float g = PlayerPrefs.GetFloat("ColorG", 0);
        float b = PlayerPrefs.GetFloat("ColorB", 0);
        Color firstColor = new Color(r, g, b);

        // Retrieve the second color from Scene 1 (if it exists)
        Color secondColor = Color.black;
        if (PlayerPrefs.HasKey("SecondColorR"))
        {
            float sr = PlayerPrefs.GetFloat("SecondColorR", 0);
            float sg = PlayerPrefs.GetFloat("SecondColorG", 0);
            float sb = PlayerPrefs.GetFloat("SecondColorB", 0);
            secondColor = new Color(sr, sg, sb);
        }

        // Deactivate platforms based on the first and second colors
        DeactivatePlatformsBasedOnColor(firstColor);
        if (secondColor != Color.black)
        {
            DeactivatePlatformsBasedOnColor(secondColor);
        }
    }

    private void DeactivatePlatformsBasedOnColor(Color color)
    {
        float r = color.r;
        float g = color.g;
        float b = color.b;

        if (r > 0 && g == 0 && b == 0) // Red only
        {
            DeactivatePlatforms(redPlatforms);
        }
        else if (g > 0 && r == 0 && b == 0) // Green only
        {
            DeactivatePlatforms(greenPlatforms);
        }
        else if (b > 0 && r == 0 && g == 0) // Blue only
        {
            DeactivatePlatforms(bluePlatforms);
        }
        else if (r > 0 && g > 0 && b == 0) // Red + Green = Yellow
        {
            DeactivatePlatforms(yellowPlatforms);
        }
        else if (g > 0 && b > 0 && r == 0) // Green + Blue = Cyan
        {
            DeactivatePlatforms(cyanPlatforms);
        }
        else if (r > 0 && b > 0 && g == 0) // Red + Blue = Magenta
        {
            DeactivatePlatforms(magentaPlatforms);
        }
        else if (r > 0 && g > 0 && b > 0) // Red + Green + Blue = White (all platforms)
        {
            DeactivatePlatforms(redPlatforms);
            DeactivatePlatforms(greenPlatforms);
            DeactivatePlatforms(bluePlatforms);
            DeactivatePlatforms(yellowPlatforms);
            DeactivatePlatforms(cyanPlatforms);
            DeactivatePlatforms(magentaPlatforms);
        }
    }

    private void DeactivatePlatforms(GameObject[] platforms)
    {
        foreach (GameObject platform in platforms)
        {
            platform.SetActive(false);
        }
    }

    private void DeactivateAllPlatforms()
    {
        DeactivatePlatforms(redPlatforms);
        DeactivatePlatforms(greenPlatforms);
        DeactivatePlatforms(bluePlatforms);
        DeactivatePlatforms(yellowPlatforms);
        DeactivatePlatforms(cyanPlatforms);
        DeactivatePlatforms(magentaPlatforms);
    }
}
