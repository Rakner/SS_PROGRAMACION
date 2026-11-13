using UnityEngine;

//IMPLEMENTA LA INTERFAZ DE SER INTERACTUABLE
public class Door : MonoBehaviour, IInteractable
{
    //DECLARACIÓN DE VARIABLES:
    private Animator _animator;
    private static readonly int Open = Animator.StringToHash("open");
    private static readonly int Close = Animator.StringToHash("close");
    
    [SerializeField] private GameObject intetactionIcon;
    [SerializeField] private AudioClip jailDoorSfx;
    
    //ASINGACIÓN DE VALORES: 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
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

    //Lógica de interacción: lanza un sonido, abre o cierra la puerta mediante una animación, dependiendo del estado en que se encuentre. 
    public void Interact()
    {
        AudioManager.Instance.PlaySound(jailDoorSfx);
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _animator.SetTrigger(Close);
        }
        else
        {
            _animator.SetTrigger(Open);
            Debug.Log("Interact Door");
        }
    }
}
