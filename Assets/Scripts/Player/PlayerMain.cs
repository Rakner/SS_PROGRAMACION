using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private GameObject mortadeloVisual;
    [SerializeField] private int checkCeldaLayer;

    //SUSCRIPCIÓN A EVENTOS Y RESET DE VARIABLES:
    void OnEnable()
    {
        PlayerPrefs.DeleteAll();
        EventManagerSO.Instance.OnPlayerCaptured += DeactivateMovement;
    }

    //Anula todo movimiento y control cuando el jugador es atrapado. 
    private void DeactivateMovement()
    {
        GetComponent<ThirdPersonController>().enabled = false;
        GetComponent<InputController>().enabled = false;
        GetComponent<DisfracesSystem>().enabled = false;
        if (mortadeloVisual.gameObject.activeSelf)
        {
            mortadeloVisual.GetComponent<Animator>().SetTrigger("Captured");
        }
    }

    //Comprueba que el jugador se encuentre dentro de una celda o no para avisar al sistema de detección de los enemigos. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == checkCeldaLayer)
        {
            Debug.Log("Estoy en una celda");
            EventManagerSO.Instance.PlayerNotifiesIsInCelda(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == checkCeldaLayer)
        {
            EventManagerSO.Instance.PlayerNotifiesIsInCelda(false);
        }
    }
    
    //DESUSCRIPCIÓN A EVENTOS
    void OnDisable()
    {
        EventManagerSO.Instance.OnPlayerCaptured -= DeactivateMovement;
    }
}
