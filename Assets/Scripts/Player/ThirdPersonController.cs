using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float gravityScale;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float movementSmoothFactor;
    [SerializeField] private float rotationSmoothFactor;
    [SerializeField] private float rotationVelocity;
    
    [Header("Ground Detection")]
    [SerializeField] private Transform feet;
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool _isGrounded;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineInputAxisController cinemachineInput;
    
    private Vector3 _verticalMovement;
    private Vector3 _hortizontalVelocity, _horizontalVelocityRef;
    private Vector2 _movementInput;
    
    private CharacterController _characterController;
    private Camera _cam;
    
    //ASIGNACIÓN DE VALORES:
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cam = Camera.main;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
    }

    //SUSCRIPCIÓN A EVENTOS. Canecela el movimiento cuando se vuelve a activar, pues hay disfraces que cancelan el movimiento y al no hacer esto mantenía el movimiento previo. 
    void OnEnable()
    {
        _movementInput  = Vector2.zero;
        InputController.Instance.OnMove += UpdateMovement;
    }
    
    //Actualiza el movimiento cada frame
    void Update()
    {
        GroundCheck();
        ApplyGravity();
        MoveAndRotate();
    }
    
    //Actualiza el valor de movimiento según el input del jugador
    private void UpdateMovement(Vector2 input)
    {
        _movementInput = input;
    }
    

    //Comprueba si está tocando el suelo. 
    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(feet.position, detectionRadius, whatIsGround);
    }

    //Actualiza el movimiento y rotación en 3º persona. Esta actualización se realiza tanto de forma interna como visual a través de animaciones. 
    private void MoveAndRotate()
    {
       Vector3 targetVelocity = Vector3.zero;

       if (_movementInput.sqrMagnitude > 0)
       {
           float targetAngle = Mathf.Atan2(_movementInput.x, _movementInput.y) * Mathf.Rad2Deg + _cam.transform.eulerAngles.y;
           
           float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothFactor);
           
           transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
           
           targetVelocity = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * (movementSpeed * _movementInput.magnitude);
       }
       
       _hortizontalVelocity = Vector3.SmoothDamp(_hortizontalVelocity, targetVelocity, ref _horizontalVelocityRef, movementSmoothFactor);
       _characterController.Move((_hortizontalVelocity + _verticalMovement) * Time.deltaTime);
       animator.SetFloat("speed", _hortizontalVelocity.magnitude / movementSpeed);
     }

    //Añade gravedad. 
    private void ApplyGravity()
    {
        if (!_isGrounded && _verticalMovement.y < 0)
        {
            _verticalMovement.y = -1;
        }
        else
        {
            _verticalMovement.y += gravityScale * Time.deltaTime;
        }
        
        _characterController.Move(_verticalMovement * Time.deltaTime);
    }
    
    //DESUSCRIPCIÓN A EVENTOS.
    void OnDisable()
    {
        InputController.Instance.OnMove -= UpdateMovement;
    }
}
