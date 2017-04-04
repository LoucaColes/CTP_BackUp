using UnityEngine;
using System.Collections;

[System.Serializable]
public class ImportSortingConfig : ScriptableObject {

    [SerializeField] private bool m_useCat, m_useType, m_useSubType;

    public void SetUseCat(bool _value)
    {
        m_useCat = _value;
    }

    public bool GetUseCat() { return m_useCat; }

    public void SetUseType(bool _value)
    {
        m_useType = _value;
    }

    public bool GetUseType() { return m_useType; }

    public void SetUseSubType(bool _value)
    {
        m_useSubType = _value;
    }

    public bool GetUseSubType() { return m_useSubType; }
}
