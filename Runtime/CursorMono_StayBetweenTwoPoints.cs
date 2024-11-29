using UnityEngine;

public class CursorMono_StayBetweenTwoPoints : MonoBehaviour {

    public Transform m_whatToMove;
    public Transform m_pointA;
    public Transform m_pointB;
    public Transform m_toFace;

    public bool m_useLerp = true;
    public float m_lerpSpeed = 5;

    public void Update()
    {
        if (m_pointA == null || m_pointB == null)
            return;

        Vector3 middlePoint = (m_pointA.position + m_pointB.position) * 0.5f;
        Vector3 faceDirection = m_toFace.position - middlePoint;

        Vector3 up= Vector3.Cross(faceDirection, m_pointA.position - m_pointB.position);
        m_whatToMove.LookAt(m_toFace, up);
        if (m_useLerp)
            m_whatToMove.position = Vector3.Lerp(m_whatToMove.position, middlePoint, Time.deltaTime * m_lerpSpeed);
        else m_whatToMove.position = middlePoint;





    }
}
