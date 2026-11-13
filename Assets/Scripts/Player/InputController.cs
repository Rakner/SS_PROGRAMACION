using System;
using UnityEngine;
using UnityEngine.InputSystem;

//CONTROLADOR DE INPUTS DEL JUGADOR: 
public class InputController : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES Y EVENTOS
    public static InputController Instance{get; private set;}
    private PlayerInput playerInput;
    public event Action<Vector2> OnMove;
    public event Action<float> OnScroll;
    public event Action<int> OnNewCostume;
    public event Action OnUseCostume;
    public event Action OnInteract;

    //CREACIÓN DEL SINGLETON
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            playerInput = GetComponent<PlayerInput>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //SUSCRIPCIÓN A LOS INPUTS DECLARADOS:
    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMoveUpdated;
        playerInput.actions["Move"].canceled += OnMoveUpdated;
        playerInput.actions["ChangeWeapon"].performed += OnScrollUpdated;
        playerInput.actions["Costume1"].started += ChangeCostume;
        playerInput.actions["Costume2"].started += ChangeCostume2;
        playerInput.actions["Costume3"].started += ChangeCostume3;
        playerInput.actions["Costume4"].started += ChangeCostume4;
        playerInput.actions["Interact"].started += OnInteractStarted;
        playerInput.actions["UseCostume"].started += OnUseCostumeStarted;
    }

    //LLAMADAS DE LOS DIFERENTES INPUTS:
    private void OnUseCostumeStarted(InputAction.CallbackContext obj)
    {
        OnUseCostume?.Invoke();
    }

    private void ChangeCostume(InputAction.CallbackContext obj)
    {
        OnNewCostume?.Invoke(0);
    }
    
    private void ChangeCostume2(InputAction.CallbackContext obj)
    {
        OnNewCostume?.Invoke(1);
    }
    
    private void ChangeCostume3(InputAction.CallbackContext obj)
    {
        OnNewCostume?.Invoke(2);
    }
    
    private void ChangeCostume4(InputAction.CallbackContext obj)
    {
        OnNewCostume?.Invoke(3);
    }

    private void OnScrollUpdated(InputAction.CallbackContext ctx)
    {
        Debug.Log("OnScrollUpdated:   " +  ctx.ReadValue<float>());
        
        OnScroll?.Invoke(ctx.ReadValue<float>());
    }

    private void OnMoveUpdated(InputAction.CallbackContext ctx)
    {
        OnMove?.Invoke(ctx.ReadValue<Vector2>());
    }
    
    private void OnInteractStarted(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke();
    }

    //DESUSCRIPCIÓN A LOS INPUTS DECLARADOS:
    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnMoveUpdated;
        playerInput.actions["Move"].canceled -= OnMoveUpdated;
        playerInput.actions["ChangeWeapon"].performed -= OnScrollUpdated;
        playerInput.actions["Costume1"].started -= ChangeCostume;
        playerInput.actions["Costume2"].started -= ChangeCostume2;
        playerInput.actions["Costume3"].started -= ChangeCostume3;
        playerInput.actions["Costume4"].started -= ChangeCostume4;
    }
}
