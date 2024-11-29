using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class CursorMono_SphereCastingToUnityInterface : MonoBehaviour
{

    public Transform m_centerPoint;
    public Transform m_radiusPoint;
    public LayerMask m_layerMask = 32;
    public bool m_useUpdate = true;


    [Header("Debug")]
    public float m_currentDistance;
    public List<MonoBehaviour> m_interactingScriptsCurrent;
    private List<MonoBehaviour> m_interactingScriptsPrevious;
    public int m_encounterCount;


    [Header("Event")]
    public UnityEvent m_onPress;
    public UnityEvent m_onRelease;
    public UnityEvent m_onClick;
    public UnityEvent m_onEncounterChanged;
    public UnityEvent m_onEncounterStart;
    public UnityEvent m_onEncounterEnd;



    public void DebugColorPress() { DebugColor(Color.green); }
    public void DebugColorRelease() { DebugColor(Color.red); }
    public void DebugColorClick() { DebugColor(Color.blue); }
    public void DebugColorEncounter() { DebugColor(Color.yellow); }
    public void DebugColorEncounterStart() { DebugColor(Color.cyan); }
    public void DebugColorEncounterEnd() { DebugColor(Color.magenta); }


    public void DebugColor(Color color) { 
    
        Debug.DrawLine(m_centerPoint.position, m_radiusPoint.position, color,1);
    }

    public void Update()
    {
        if (m_useUpdate)
            UpdateInteractingScripts();
    }

    public bool m_isClicking;

    [ContextMenu("Click and press release")]
    public void ClickAndPressRelease()
    {
       
            UpdateInteractingScripts();
            m_onPress.Invoke();
            DebugColorPress();
            m_onClick.Invoke();
            DebugColorClick();

            m_onRelease.Invoke();
            DebugColorRelease();
            foreach (MonoBehaviour script in m_interactingScriptsCurrent)
            {
                if (script is IPointerDownHandler)
                    (script as IPointerDownHandler).OnPointerDown(null);
                if (script is IPointerClickHandler)
                    (script as IPointerClickHandler).OnPointerClick(null);
                if (script is IPointerUpHandler)
                    (script as IPointerUpHandler).OnPointerUp(null);
            }

            m_isClicking = false;
        
    }
    [ContextMenu("Click")]
    public void Click()
    {
            m_onClick.Invoke();
            DebugColorClick();
            UpdateInteractingScripts();
            foreach (MonoBehaviour script in m_interactingScriptsCurrent)
            {
                if (script is IPointerClickHandler)
                    (script as IPointerClickHandler).OnPointerClick(null);
            }
    }

    [ContextMenu("Press")]
    public void Press()
    {
        bool previous = m_isClicking;
        m_isClicking = true;
        if (previous != m_isClicking)
        {
            UpdateInteractingScripts();
            m_onPress.Invoke();
            DebugColorPress();
            foreach (MonoBehaviour script in m_interactingScriptsCurrent)
            {
                if (script is IPointerDownHandler)
                    (script as IPointerDownHandler).OnPointerDown(null);
            }

        }
    }

    [ContextMenu("Release")]
    public void Release() {
        bool previous = m_isClicking;
        m_isClicking = false;
        if (previous != m_isClicking)
        {
            UpdateInteractingScripts();
            m_onRelease.Invoke();
            DebugColorRelease();
            foreach (MonoBehaviour script in m_interactingScriptsCurrent)
            {
                if (script is IPointerUpHandler)
                    (script as IPointerUpHandler).OnPointerUp(null);
            }

        }
    }




    [ContextMenu("Scroll Up")]
    public void ScrollVerticalUp() { 
        Scroll(0,1);
    }
    [ContextMenu("Scroll Down")]
    public void ScrollVerticalDown() {
        Scroll(0,-1);
    }

    [ContextMenu("Scroll Left")]
    public void ScrollHorizontalLeft()
    {
        Scroll(-1,0);
    }

    [ContextMenu("Scroll Right")]
    public void ScrollHorizontalRight()
    {
        Scroll(1,0);
    }


    public void Scroll(int xLeftRight,int yDownUp)
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.scrollDelta = new Vector2(xLeftRight, yDownUp);
        foreach (MonoBehaviour script in m_interactingScriptsCurrent)
        {
            if (script is IScrollHandler)
            {
                (script as IScrollHandler).OnScroll(data);
            }
        }
    }
    





    private void UpdateInteractingScripts()
    {
        m_currentDistance = Vector3.Distance(m_centerPoint.position, m_radiusPoint.position);
        Collider[] hit = Physics.OverlapSphere(m_centerPoint.position, m_currentDistance, m_layerMask);
        m_interactingScriptsPrevious = m_interactingScriptsCurrent.ToList();
        m_interactingScriptsCurrent.Clear();
        for (int i = 0; i < hit.Length; i++)
        {
            MonoBehaviour[] scripts = hit[i].GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {

                if (script != null && (
                    script is IPointerEnterHandler ||
                    script is IPointerExitHandler ||
                    script is IPointerClickHandler ||
                    script is IPointerDownHandler ||
                    script is IPointerUpHandler ||
                    script is IScrollHandler))
                { 
                    m_interactingScriptsCurrent.Add(script);
                }
                
            }
        }

        List<MonoBehaviour> newItem = m_interactingScriptsCurrent.Except(m_interactingScriptsPrevious).ToList();
        List<MonoBehaviour> removedItem = m_interactingScriptsPrevious.Except(m_interactingScriptsCurrent).ToList();

        
        foreach (MonoBehaviour script in newItem)
        {
            if (script is IPointerEnterHandler)
                (script as IPointerEnterHandler).OnPointerEnter(null);
        }

        foreach (MonoBehaviour script in removedItem)
        {
            if (script is IPointerExitHandler)
                (script as IPointerExitHandler).OnPointerExit(null);
        }

        int previous = m_encounterCount;
        m_encounterCount = m_interactingScriptsCurrent.Count;
        if (previous != m_encounterCount)
        {
            m_onEncounterChanged.Invoke();
            DebugColorEncounter();
            if (previous == 0)
            {
                m_onEncounterStart.Invoke();
                DebugColorEncounterStart();
            }
            else if (m_encounterCount == 0) { 
                m_onEncounterEnd.Invoke();
                DebugColorEncounterStart();
            }
        }


    }
}
