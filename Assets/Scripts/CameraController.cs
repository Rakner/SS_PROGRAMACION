using System;
using Unity.Cinemachine;
using UnityEngine;

//DESACTIVA EL MOVIMIENTO DE LA CAMARA CUANDO EL JUGADOR ES CAPTURADO O HA ESCAPADO
public class CameraController : MonoBehaviour
{
    void OnEnable()
    {
        EventManagerSO.Instance.OnPlayerCaptured += DeactivateCameraRotation;
    }

    private void DeactivateCameraRotation()
    {
        GetComponent<CinemachineOrbitalFollow>().enabled = false;
    }

    void OnDisable()
    {
        EventManagerSO.Instance.OnPlayerCaptured -= DeactivateCameraRotation;
    }
}
