using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class CostumeLogic : MonoBehaviour
{
    //DECLARACIÓN DE VARIABLES:
    protected Camera cam;
    protected GameObject costume;
    protected GameObject mortadelo;
    protected CostumeDataSO costumeData;
    protected bool takeCostumeAction;
    [SerializeField] protected LayerMask everythingMask;
    
    //SUSCRIPCIÓN A EVENTOS: 
    void OnEnable()
    {
        EventManagerSO.Instance.OnGiveCostumeData += AsignCostumeData;
    }
    
    //Cambios visual al disfraz si corresponde, y comprueba que Mortadelo este disfrazado en el lugar correspondiente: 
    protected virtual void Update()
    {
        if (takeCostumeAction)
        {
            ChangeVisualCostume();
            takeCostumeAction = false;
        }
        CheckPlace();
    }
    
    //Asigna los datos del disfraz, a que objeto tiene que cambiarse y cual quitar. Una vez asignado permite al disfrazar cambiarse correctamente para evitar fallos a priori. 
    private void AsignCostumeData(CostumeDataSO data)
    {
        cam = Camera.main;
        if (GetComponentInChildren<MeshFilter>() != null)
        {
            Debug.Log(GetComponentInChildren<MeshFilter>().gameObject.name);
            costume = GetComponentInChildren<MeshFilter>().gameObject;
            mortadelo = GetComponentInChildren<Animator>().gameObject;
            costumeData = data;
            takeCostumeAction = true;
        }
    }
    
    //Cambio visual del disfraz que se use, con los datos asignados se cambian las mayas y posiciones dle character controller. 
    protected virtual void ChangeVisualCostume()
    {
        //Envía notificación al sistema de detección de los enemigos de que se esta usando un disfraz. 
        EventManagerSO.Instance.PlayerNotifiesCostumeUsed(true);
        
        mortadelo.SetActive(false);
        costume.GetComponent<MeshFilter>().sharedMesh = costumeData.mesh;
        costume.GetComponent<MeshRenderer>().sharedMaterials = costumeData.material;
        costume.transform.localScale = costumeData.escala;
        costume.transform.localPosition = costumeData.position;
        GetComponent<CharacterController>().radius = costumeData.radius;
        GetComponent<CharacterController>().height = costumeData.height;
        GetComponent<DisfracesSystem>().isUsingCostume = true;
        
        StartCoroutine(CancelCostumeEffect());
    }

    //Una vez terminada la duración del disfraz se cancela dicho cambio. 
    IEnumerator CancelCostumeEffect()
    {
        yield return new WaitForSeconds(costumeData.time);
        CancelCostumeAction();
    }

    //Cambio al setting original de Mortadelo y permite volver a usar disfraces. 
    protected virtual void CancelCostumeAction()
    {
        //Envía notificación al sistema de detección de los enemigos de que NO se esta usando un disfraz. 
        EventManagerSO.Instance.PlayerNotifiesCostumeUsed(false);
        
        costume.GetComponent<MeshFilter>().sharedMesh = null;
        costume.transform.rotation = Quaternion.Euler(Vector3.zero);
        mortadelo.SetActive(true);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        GetComponent<ThirdPersonController>().enabled = true;
        GetComponent<InputController>().enabled = true;
        GetComponent<CharacterController>().radius = 0.5f;
        GetComponent<CharacterController>().height = 3f;
        GetComponent<DisfracesSystem>().isUsingCostume = false;
        
    }

    //Comprueba que el disfraz se este usando en el lugar correcto para informar a los enemigos si han de detectar a Mortadelo disfrazado o no. 
    protected virtual void CheckPlace()
    {
        Debug.Log("ESTOY EN CHECK PLACE COMPROBANDO: " + costumeData.mask);
        
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, 100000, costumeData.mask);
        
        Debug.DrawLine(transform.position, Vector3.down, Color.deepSkyBlue);
        Debug.Log("USO MASCARA: " + costumeData.mask.ToString() + " y: " + LayerMask.LayerToName(0));
        Debug.Log(costumeData.mask.ToString() == LayerMask.LayerToName(0));
        
        if (hit.collider !=null || costumeData.mask.ToString() == LayerMask.LayerToName(0))
        {
            Debug.Log("TENGO QUE ESTAR EN LAYER: " + costumeData.mask + " Y ESTOY EN LAYER: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            EventManagerSO.Instance.NotifiesCostumeUsedInplace(true);
        }
        else
        {
            Debug.Log("NO ESTOY EN LA LAYER QUE TOCA");
            EventManagerSO.Instance.NotifiesCostumeUsedInplace(false);
        }
    }
    
    //DESUSCRIPCIÓN A EVENTOS: 
    void OnDisable()
    {
        EventManagerSO.Instance.OnGiveCostumeData -= AsignCostumeData;
    }
}
