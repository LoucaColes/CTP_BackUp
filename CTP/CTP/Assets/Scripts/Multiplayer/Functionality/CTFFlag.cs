using UnityEngine;
using System.Collections;

public class CTFFlag : MonoBehaviour {

    private GameObject m_flagPoint;

    public void SetFlagPoint(GameObject _flagPoint)
    {
        m_flagPoint = _flagPoint;
    }

    public void ResetFlag()
    {
        gameObject.transform.parent = null;
        gameObject.transform.position = m_flagPoint.transform.position;
        gameObject.transform.parent = m_flagPoint.transform;
    }
}
