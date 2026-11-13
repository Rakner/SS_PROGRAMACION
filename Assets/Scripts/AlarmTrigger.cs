using System;
using System.Collections;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES: 
    [SerializeField] private AudioClip alarmSfx;
    private bool _triggered;
    
    //Al entrar el jugador activa la alarma que avisa a los enemigos para ir hasta la habitación mediante un evento y también activa un sonido y animación de luces. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_triggered)
        {
            AudioManager.Instance.PlaySound(alarmSfx);
            EventManagerSO.Instance.NotifiesAlarmIsTriggered(true);
            StartCoroutine(AlarmTimer());
            _triggered = true;
        }
    }

    IEnumerator AlarmTimer()
    {
       yield return new WaitForSeconds(15f);
       EventManagerSO.Instance.NotifiesAlarmIsTriggered(false);
       enabled = false;
    }
}
