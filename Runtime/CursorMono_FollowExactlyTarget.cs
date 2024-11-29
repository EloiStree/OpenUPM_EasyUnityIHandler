using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorMono_FollowExactlyTarget : MonoBehaviour
{

    public Transform m_whatToFollow;
    public Transform m_whatToMove;

    public bool m_usingUpdate=true;
    public bool m_usingLateUpdate;


    public void Reset()
    {
        m_whatToMove = transform;
    }

    public void Update()
    {
        if (m_usingUpdate)
            Follow();
    }

    public void LateUpdate()
    {
        if (m_usingLateUpdate)
            Follow();
    }

    public void Follow()
    {
        if (m_whatToFollow == null || m_whatToMove == null)
            return;

        m_whatToMove.position = m_whatToFollow.position;
        m_whatToMove.rotation = m_whatToFollow.rotation;
    }
}
