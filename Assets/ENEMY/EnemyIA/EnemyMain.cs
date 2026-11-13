using System;
using UnityEngine;

//PARA A LOS ENEMIGOS CUANDO SE TERMINA EL NIVEL
public class EnemyMain : MonoBehaviour
{
    private void OnEnable()
    {
        EventManagerSO.Instance.OnCheckKey += StopIA;
    }

    private void StopIA(bool obj)
    {
        if (obj)
        {
            GetComponent<Behaviour>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
    }

    private void OnDisable()
    {
        EventManagerSO.Instance.OnCheckKey -= StopIA;
    }
}
