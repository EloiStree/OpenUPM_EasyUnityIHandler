using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CursorMono_FingerTipOnOff : MonoBehaviour
{

    public Transform m_tipOne;
    public Transform m_tipTwo;

    public float m_distanceThreshold = 0.02f;
    public bool m_isClicking;
    public UnityEvent m_onPress;
    public UnityEvent m_onRelease;
    public UnityEvent m_onPressingChanged;


    public void Update()
    {
        if (m_tipOne == null || m_tipTwo == null)
            return;

        float distance = Vector3.Distance(m_tipOne.position, m_tipTwo.position);
        bool previous = m_isClicking;
        bool current= distance < m_distanceThreshold;

        if (previous != current)
        {
            m_isClicking = current;
            if (m_isClicking)
                m_onPress.Invoke();
            else
                m_onRelease.Invoke();
            m_onPressingChanged.Invoke();
        }
    }
}
