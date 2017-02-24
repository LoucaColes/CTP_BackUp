using UnityEngine;
using System.Collections;

[System.Serializable]
public class BIMMetaData // : ScriptableObject 
{
    [SerializeField]
    public string name;
    public GUIProperties renderProperties; 

    public void Initialize(string name) 
    {
        this.name = name;
    }

    public GUIProperties RenderProperties
    {
        get
        {
            return renderProperties;
        }
        set
        {
            renderProperties = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    /*
    public BIMMetaData[] Children
    {
        get
        {
            return children;
        }
        set
        {
            children = value;
        }
    }
     * */

   

}
