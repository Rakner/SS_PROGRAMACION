using UnityEngine;

//IMPLEMENTA LA INTERFAZ DE SER INTERACTUABLE
public class Lampara : MonoBehaviour, IInteractable
{
    //DECLARACIÓN DE VARIABLES:
    [SerializeField] private GameObject intetactionIcon;
    [SerializeField] private CostumeDataSO lightCostumeData;
    [SerializeField] private AudioClip takeCostumeSfx;
    
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

    //Lógica de interacción: lanza un sonido, se asigna el disfraz, se destruye. 
    public void Interact()
    {
        AudioManager.Instance.PlaySound(takeCostumeSfx);
        EventManagerSO.Instance.CostumeIsPicked(lightCostumeData);
        Destroy(gameObject);
    }
}
