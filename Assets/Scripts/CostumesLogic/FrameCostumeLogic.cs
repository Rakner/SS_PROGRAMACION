using System;
using System.Collections;
using UnityEngine;

public class FrameCostumeLogic : CostumeLogic
{
    //DECLARACIÓN DE VARIABLES
    [SerializeField] private bool _canTransform;
    
    //Comprobación de que al usar el disfraz se este usando correctamente. Si es correcta, hace la lógica normal. 
    protected override void Update()
    {
        if (takeCostumeAction)
        {
            DoCostumeLogic();
            if (_canTransform)
            {
                ChangeVisualCostume();
            }
            takeCostumeAction = false;
        }
        CheckPlace();
    }

    //Comprueba mediante raycast que el disfraz se use en una pared dentro del rango peritido y cambia la orientación del modelo para que se pegue a la pared correctamente. 
    public void DoCostumeLogic()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 10f, LayerMask.GetMask("Paredes")))
        {
            //En caso de que se pueda usar el disfraz y el raycast tenga una pared objetiva, se deshabilita el movimiento mientras se esta disfrazado. 
            transform.position = hit.point;
            transform.rotation = Quaternion.Euler(0,0,0);
            GetComponent<InputController>().enabled = false;
            GetComponent<ThirdPersonController>().enabled = false;
            
            Debug.Log("MI NORMAL ES: " + hit.normal);
            Debug.DrawRay(transform.position, hit.normal, Color.green, 10000000, false);

            //Cambio de orientación
            if (hit.normal == new Vector3(1, 0, 0))
            {
                costume.transform.rotation = Quaternion.Euler(-90,0,90);
            }
            
            if (hit.normal == new Vector3(-1, 0, 0))
            {
                costume.transform.rotation = Quaternion.Euler(-90,-90,0);
            }

            if (hit.normal == new Vector3(0, 0, 1))
            {
                costume.transform.rotation = Quaternion.Euler(-90,0,0);
            }
            if (hit.normal == new Vector3(0, 0, -1))
            {
                costume.transform.rotation = Quaternion.Euler(-90,0,0);
            }
            _canTransform = true;
        }
        else
        {
            _canTransform = false;
        }
    }
    
    //Realiza la lógica normal y reajusta las puertas lógicas para que sea necesario hacer la comprobación de nuevo. 
    protected override void CancelCostumeAction()
    {
        base.CancelCostumeAction();
        _canTransform = false;
    }
}
