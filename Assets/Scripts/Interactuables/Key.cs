using UnityEngine;

//IMPLEMENTA LA INTERFAZ DE SER INTERACTUABLE
public class Key : MonoBehaviour, IInteractable
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private GameObject intetactionIcon;
    [SerializeField] private AudioClip takeKeySfx;
    
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

    //Lógica de interacción: lanza un audio, asigna la llave al jugador y se notifica, luego se destruye.
    public void Interact()
    {
        AudioManager.Instance.PlaySound(takeKeySfx);
        PlayerPrefs.SetInt("key", 1);
        EventManagerSO.Instance.NotifiesKeyObtained();
        Destroy(gameObject);
    }
}
