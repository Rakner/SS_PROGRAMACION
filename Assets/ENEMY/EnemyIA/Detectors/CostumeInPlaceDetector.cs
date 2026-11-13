using UnityEngine;

public class CostumeInPlaceDetector : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES
    [SerializeField] private bool isInPlace;
    
    //SUSCRIPCCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnCheckCostumePlace += ChangeCostumeInPlaceDetector;
    }

    //Cambia  el valor de la variable para el sistema de detección enemigo y su IA. 
    private void ChangeCostumeInPlaceDetector(bool state)
    {
        isInPlace =  state;
    }
    
    //Llamada que realiza la IA enemiga para recoger el valor. 
    public bool IsInPlace()
    {
        return isInPlace;
    }

    //DESUSCRIPCCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnCheckCostumePlace -= ChangeCostumeInPlaceDetector;
    }
}
