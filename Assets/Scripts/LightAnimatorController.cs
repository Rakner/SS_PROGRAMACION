using System;
using System.Collections;
using UnityEngine;

//HACE BRILLAR UNA LUZ CUANDO SE ACTIVA LA ALARMA 
public class LightAnimatorController : MonoBehaviour
{
    [SerializeField] private bool _canPlay = true;
    private void OnEnable()
    {
        EventManagerSO.Instance.OnAlarmTriggered += PlayAnim;
    }

    private void PlayAnim(bool obj)
    {
        if (obj && _canPlay)
        {
            GetComponent<Animator>().SetTrigger("Alarm");
            StartCoroutine(Stop());
        }
       
    }

    IEnumerator Stop()
    {
       yield return new WaitForSeconds(10f);
        _canPlay = false;
    }

    private void OnDisable()
    {
        EventManagerSO.Instance.OnAlarmTriggered -= PlayAnim;
    }
}
