using UnityEditor.Rendering;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    public enum BillboardType {LookAtCamera, CameraForward}
    [SerializeField] private BillboardType billboardType;

    //HACE QUE EL ICONO DE LOS INTERACTUABLES SIEMPRE MIRE A CÁMARA
    void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.LookAt(Camera.main.transform.position,  Vector3.up);
                break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
        }
    }
}
