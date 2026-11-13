using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class LineOfSightDetector : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] protected LayerMask m_playerLayerMask;
    [SerializeField] protected float m_detectionRange = 10.0f;
    [SerializeField] protected float m_detectionHeight = 3f;
    [SerializeField] protected float angle;
    
    [SerializeField] protected bool showDebugVisuals = true;
    [SerializeField] protected AudioClip targetDetectedSfx;
    [SerializeField] protected bool hasPlaySound;

    
    //Calccula el rango de visión del enemigo en base al ángulo asignado. 
    //Cuando el jugador se encuentra dentro del rango de visión tira un rayo en su dirección 
    //Si lo ve comunica que ha visto a el jugador para que la IA del enemigo cambie de estado. 
    public virtual GameObject PerformDetection(GameObject potentialTarget)
    {
        float signedAngle = Vector3.Angle(transform.forward, potentialTarget.transform.position - transform.position);
        if (Mathf.Abs(signedAngle) < angle / 2)
        {
            Debug.Log("VEO A MORTADELO");
            RaycastHit hit;
            Vector3 direction = (potentialTarget.transform.position - transform.position).normalized;
            Physics.Raycast(transform.position, direction, out hit, 100000, m_playerLayerMask);
        
            Debug.DrawLine(transform.position, direction, Color.deepPink);
        
            //Debug.Log(hit.collider.gameObject.name);
        
            if (hit.collider != null && hit.collider.gameObject == potentialTarget)
            {
                if (showDebugVisuals && this.enabled)
                {
                    Debug.DrawLine(transform.position,
                        potentialTarget.transform.position, Color.red);
                }

                if (!hasPlaySound)
                {
                    AudioManager.Instance.PlaySound(targetDetectedSfx);
                    hasPlaySound = true;
                }
                
                return hit.collider.gameObject;
            }
            else
            {
                if (hasPlaySound)
                {
                    hasPlaySound = false;
                }
                return null;
            }
        }
        else
        {
            if (hasPlaySound)
            {
                hasPlaySound = false;
            }
            return null;
        }
    }

    //VISUALIZACIÓN DEL RANGO DE VISIÓN PARA EL EDITOR: 
    protected void OnDrawGizmos()
    {
        if (showDebugVisuals)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + Vector3.up * m_detectionHeight, 0.3f);
            
            Vector3 angle1 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
            Vector3 angle2 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);
                
             Handles.DrawLine(transform.position, transform.position + angle1 * m_detectionRange);
             Handles.DrawLine(transform.position, transform.position + angle2 * m_detectionRange);
        }
    }

    protected Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees +=  eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
