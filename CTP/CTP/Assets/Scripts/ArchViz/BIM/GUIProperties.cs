using UnityEngine;
using System.Collections;

[System.Serializable]
public class GUIProperties
{
    [SerializeField]
    public Color colour; 
    [SerializeField]
    public bool enabled;        // Move these into an enum
    [SerializeField]
    public bool transparent;

    public Color Colour
    {
        get
        {
            return colour;
        }
        set
        {
            colour = value;
        }
    }

    public bool Enabled
    {
        get
        {
            return enabled;
        }
        set
        {
            enabled = value;
        }
    }

    public bool Transparent
    {
        get
        {
            return transparent;
        }
        set
        {
            transparent = value;
        }
    }



}
