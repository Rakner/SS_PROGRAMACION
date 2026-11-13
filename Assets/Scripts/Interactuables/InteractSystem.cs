using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    private IInteractable _currentInteractable;
    [SerializeField] private Animator animator;

    //SUSCRIPCIÓN A EVENTOS:
    private void OnEnable()
    {
        InputController.Instance.OnInteract += LaunchInteraction;
    }

    //Lanza la lógica del objeto interactuable cuando se usa la acción de interactuar. También se lanza la animación de Mortadelo para acompañar a la lógica. 
    private void LaunchInteraction()
    {
        animator.SetTrigger("Take");
        _currentInteractable?.Interact();
        _currentInteractable?.OnInteractableDeactivated();
        _currentInteractable = null;
    }

    //Detecta si lo que tenemos en rango es un interactuable o no. De serlo, lo asigna para que en caso de interactuar, se llame al interactuable correcto. 
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (interactable != _currentInteractable)
            {
                _currentInteractable?.OnInteractableDeactivated();
            }
            
            _currentInteractable =  interactable;
            _currentInteractable?.OnInteractableActivated();
        }
    }

    //Resetea los valores al dejar de detectar un interactuable
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (interactable == _currentInteractable)
            {
                _currentInteractable?.OnInteractableDeactivated();
                _currentInteractable =  null;
            }
        }
    }

    //DESUSCRIPCIÓN A EVENTOS:
    private void OnDisable()
    {
        InputController.Instance.OnInteract -= LaunchInteraction;
    }
}
