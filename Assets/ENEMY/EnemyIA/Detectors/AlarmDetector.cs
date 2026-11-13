using UnityEngine;

public class AlarmDetector : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES
    [SerializeField] private bool alarmIsTriggered;
    
    //SUSCRIPCCIÓN A EVENTOS:
    void OnEnable()
    {
        EventManagerSO.Instance.OnAlarmTriggered += ChangeAlarmState;
    }

    //Cambia  el valor de la variable para el sistema de detección enemigo y su IA. 
    private void ChangeAlarmState(bool state)
    {
        alarmIsTriggered =  state;
    }
    
    //Llamada que realiza la IA enemiga para recoger el valor. 
    public bool IsAlarmTriggered()
    {
        return alarmIsTriggered;
    }

    //DESUSCRIPCCIÓN A EVENTOS:
    void OnDisable()
    {
        EventManagerSO.Instance.OnAlarmTriggered -= ChangeAlarmState;
    }
}
