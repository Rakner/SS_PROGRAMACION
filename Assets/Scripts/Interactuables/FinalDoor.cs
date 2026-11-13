using UnityEngine;

//IMPLEMENTA LA INTERFAZ DE SER INTERACTUABLE
public class FinalDoor : MonoBehaviour, IInteractable
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private GameObject intetactionIcon;
    [SerializeField] private AudioClip doorClosedSfx;
    [SerializeField] private AudioClip doorOpenSfx;
    
    //Activa el icono para mostrar que es el interactuable detectado
    public void OnInteractableActivated()
    {
        intetactionIcon.SetActive(true);
    }

    //Desactiva el icono para mostrar que se ha dejado de detectar
    public void OnInteractableDeactivated()
    {
        intetactionIcon.SetActive(false);
    }

    //Lógica de interacción: comprueba si el jugador tiene la llave para terminar el nivel.
    //Si no la tiene lanza un sonido y muestra un mensaje por la UI de que el jugador tiene que buscar la llave. 
    //Si la tiene termina la fase y consigue escapar al interactuar con la puerta. Se envía el evento que avisa al resto de componentes del final de la fase. 
    public void Interact()
    {
        if (PlayerPrefs.GetInt("key") == 1)
        {
            AudioManager.Instance.PlaySound(doorOpenSfx);
            EventManagerSO.Instance.NotifiesHasKey(true);
        }
        else
        {
            AudioManager.Instance.PlaySound(doorClosedSfx);
            EventManagerSO.Instance.NotifiesHasKey(false);
        }
    }
}
