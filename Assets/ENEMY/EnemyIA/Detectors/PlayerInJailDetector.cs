using UnityEngine;

public class PlayerInJailDetector : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES
    [SerializeField] private bool _isInCelda;
    
    //SUSCRIPCCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnCheckCelda += ChangePlayerInJailDetector;
    }
    
    //Cambia  el valor de la variable para el sistema de detección enemigo y su IA. 
    private void ChangePlayerInJailDetector(bool state)
    {
        Debug.Log("RECIBO EL ESTADO DEL JUGADOR EN CELDA" + state);
        _isInCelda =  state;
    }
    
    //Llamada que realiza la IA enemiga para recoger el valor. 
    public bool IsInCelda()
    {
        return _isInCelda;
    }

    //DESUSCRIPCCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnCheckCelda -= ChangePlayerInJailDetector;
    }
}
