using UnityEngine;

//DECLARACIÓN DE LA INTERFAZ Y LOS MÉTODOS NECESARIOS:
public interface IInteractable
{
    void OnInteractableActivated();
    void OnInteractableDeactivated();
    void Interact();
}
