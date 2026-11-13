using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private bool showDebugVisuals = true;
    
    [Header("Sound")]
    [SerializeField] private AudioClip targetInRangeSfx;
    [SerializeField]private bool hasPlaySound;

    public GameObject DetectedTarget
    {
        get;
        set;
    }

    //Comprueba que el jugador se encuentre dentro del rango de detección. Cuando el jugador entra por primera, o cada vez despues de salir del rango, lanza un sonido. 
    public GameObject UpdateDetector()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);

        if (colliders.Length > 0)
        {
            DetectedTarget = colliders[0].gameObject;
            if (!hasPlaySound)
            {
                hasPlaySound = true;
                AudioManager.Instance.PlaySound(targetInRangeSfx);
                
            }
        }
        else
        {
            
            if (hasPlaySound)
            {
                hasPlaySound = false;
            }
            
            DetectedTarget = null;
        }
        return DetectedTarget;
    }

    //Muestra el rango por el editor.
    private void OnDrawGizmos()
    {
        if (!showDebugVisuals || this.enabled == false) return;

        Gizmos.color = DetectedTarget ? Color.green : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

    }
}
