using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CursorMono_InputButtonOnOff : MonoBehaviour {

    public InputActionReference m_buttonReference;
    public bool m_isOn;
    public float m_threshold = 0.5f;
    public UnityEvent m_onOn;
    public UnityEvent m_onOff;
    public UnityEvent<bool> m_onChanged;

    public void OnEnable()
    {
        m_buttonReference.action.Enable();
        m_buttonReference.action.performed += OnButton;
        m_buttonReference.action.canceled += OnButton;    
    }

    private void OnButton(InputAction.CallbackContext context)
    {
        bool previous = m_isOn;
        m_isOn = context.ReadValue<float>() > m_threshold;

        if (previous != m_isOn)
        {
            m_onChanged.Invoke(m_isOn);
            if (m_isOn)
                m_onOn.Invoke();
            else
                m_onOff.Invoke();
        }

        


    }


    public void OnDisable()
    {
        m_buttonReference.action.Disable();
        m_buttonReference.action.performed -= OnButton;
        m_buttonReference.action.canceled -= OnButton;
    }


}
