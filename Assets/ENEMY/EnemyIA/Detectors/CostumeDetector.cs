using UnityEngine;

public class CostumeDetector : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES
    [SerializeField] private bool isUsingCostume;
    
    //SUSCRIPCCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnCostumeUsed += ChangePlayerIsUsingCostume;
    }

    //Cambia  el valor de la variable para el sistema de detección enemigo y su IA. 
    private void ChangePlayerIsUsingCostume(bool state)
    {
        isUsingCostume =  state;
    }
    
    //Llamada que realiza la IA enemiga para recoger el valor. 
    public bool IsUsingCostume()
    {
        return isUsingCostume;
    }

    //DESUSCRIPCCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnCheckCelda -= ChangePlayerIsUsingCostume;
    }
}
