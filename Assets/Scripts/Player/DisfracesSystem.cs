using System;
using System.Collections;
using UnityEngine;

public class DisfracesSystem : MonoBehaviour
{
     //DECLARACIÓN DE VARIABLES: 
     [Header("DISFRACES EN USO")]
     [SerializeField] private CostumeDataSO[] costumes;
     [SerializeField] private CostumeDataSO currentCostumeData;
     [SerializeField] private string lastCostumeUsed;
     
     [Header("OBJECTOS CON MAYAS")]
     [SerializeField] private GameObject mortadelo;
     [SerializeField] private GameObject mortadeloDisfraz;
     
     [Header("SONIDOS")]
     [SerializeField] protected AudioClip useCostumeSfx;
     [SerializeField] protected AudioClip changeCostumeSfx;
     [SerializeField] protected AudioClip cantUseCostumeSfx;
     
     [Header("CHECKS INTERNOS")]
     [SerializeField] private int currentIndex;
     [SerializeField] private bool[] canUseCostumes;
     [SerializeField] private bool costumesAreFull;
     public bool isUsingCostume;

     //ASINGACIÓN DE VALORES AL INICIO: 
     void Awake()
     {
          currentIndex = 0;
          currentCostumeData =  costumes[currentIndex];
          canUseCostumes = new bool[costumes.Length];
          
          //En caso de perder y volver a intentarlo se vuelve a permitir el uso de todos los slots de disfraces
          var index = 0;
          foreach (var costume in canUseCostumes)
          {
               canUseCostumes[index] = true;
               index++;
          }
     }

     //SUSCRIPCIÓN A EVENTOS:
     void OnEnable()
     {
          EventManagerSO.Instance.OnPickUpCostume += AsignCostumeData;
          InputController.Instance.OnScroll += ChangeCostumeWithScroll;
          InputController.Instance.OnNewCostume += ChangeCostume;
          InputController.Instance.OnUseCostume += UseCostume;
     }

     //Añade un nuevo disfraz con sus datos
     private void AsignCostumeData(CostumeDataSO costumeData)
     {
          var index = 0;
          foreach (var costume in costumes)
          {
               //Si aún quedan espacios en los slots se asigna el disfraz al slot libre y se selecciona automáticamente dicho disfraz.
               if (costume == null)
               {
                    costumes[index] = costumeData;
                    currentCostumeData = costumeData;
                    currentIndex = index;
                    EventManagerSO.Instance.NotifiesCostumePickedToUI(index, costumeData.costumeIcon);
                    EventManagerSO.Instance.NotifieCostumeSelectedToUI(index);
                    if (index == costumes.Length - 1)
                    {
                         costumesAreFull = true;
                    }
                    break;
               }
               //Si todos los slots de disfraces están llenos, se cambia el disfraz nuevo por el disfraz que este seleccionado actualmente para dejar elegir al jugador que disfraz quiere quitar.
               else
               {
                    if (costumesAreFull && index == currentIndex)
                    {
                         costumes[index] = costumeData;
                         currentCostumeData = costumeData;
                         currentIndex = index;
                         EventManagerSO.Instance.NotifiesCostumePickedToUI(index, costumeData.costumeIcon);
                         EventManagerSO.Instance.NotifieCostumeSelectedToUI(index);
                         break;
                    }
               }
               index++;
          }
     }
     
     //Hace uso del disfraz seleccionado, creando su lógica y activando los efectos del disfraz si pasa las comprobocaciones de uso. 
     //Las comprobaciones tienen en cuenta si el disfraz esta en CD o no, si hay datos para usarse y si esta esta usando un disfraz o no. 
     private void UseCostume()
     {
          if (currentCostumeData != null && canUseCostumes[currentIndex] && !isUsingCostume)
          {
               //Si se puede usar se comprueba como se ha de crear la lógica del disfraz o usar una ya existente y se pone en CD. 
               AudioManager.Instance.PlaySound(useCostumeSfx);
               StartCoroutine(CostumeCD(currentIndex,  currentCostumeData.cd));
               Debug.Log("Using Costume: " + currentCostumeData);
               Debug.Log("MI TIPO >>>>>>>>>>>>>>>>>>>>>>>" + Type.GetType(currentCostumeData.costumeLogicName));

               if (lastCostumeUsed == currentCostumeData.costumeLogicName)
               {
                    //USO EL MISMO DISFRAZ SI NO HE CAMBIADO
                    Debug.Log("USO EL MISMO DISFRAZ QUE TENGO");
                    EventManagerSO.Instance.GiveCostumeData(currentCostumeData);
                    lastCostumeUsed =  currentCostumeData.costumeLogicName;
               }
               else
               {
                    if (gameObject.GetComponent(lastCostumeUsed) as MonoBehaviour == null)
                    {
                         //CREO EL NUEVO DISFRAZ QUE QUIERO USAR
                         Debug.Log("NO TENGO DISFRAZ Y CREO UNO NUEVO");
                         InstantiateCostumeLogic();
                    }
                    else
                    {
                         //BORRO EL SCRIPT DEL DISFRAZ ANTERIOR Y CREO EL NUEVO
                         Debug.Log("TENGO DISFRAZ Y CAMBIO");
                         Destroy(gameObject.GetComponent(lastCostumeUsed));
                         InstantiateCostumeLogic();
                    }
               } 
          }
          else
          {
               //SONIDO DE NO PODER USARSE
               AudioManager.Instance.PlaySound(cantUseCostumeSfx);
          }
     }
     
     //CD de cada disfraz de forma indepentiente a través de corrutina. 
     IEnumerator CostumeCD(int costumeIndex, float cd)
     {
          var timer = 0f;
          canUseCostumes[costumeIndex] = false;
          
          //Se avisa a la UI de mostrar que esta en CD la primera vez. 
          EventManagerSO.Instance.NotifieShowCostumeCD(costumeIndex, timer, cd);
          
          while (!canUseCostumes[costumeIndex])
          {
               Debug.Log("ME MANTENGO EN CD CON: " + timer + " Y " + cd);
               if (timer < cd)
               {
                    yield return new WaitForSeconds(1);
                    timer += 1;
                    
                    //Se avisa a la UI de mostrar que esta en CD a intervalos regulares que muestran al jugador cuanto CD queda.
                    EventManagerSO.Instance.NotifieShowCostumeCD(costumeIndex, timer, cd);
               }
               else if (timer >= cd)
               {
                    canUseCostumes[costumeIndex] = true;
               }
          }
     }

     //Añade la lógica del disfraz dependiendo de cual se use y los datos que se tengan. Avisa el último disfraz usado para no tener que volver a instanciarlo si se usa el mismo repetidamente.
     private void InstantiateCostumeLogic()
     {
          gameObject.AddComponent(Type.GetType(currentCostumeData.costumeLogicName));
          EventManagerSO.Instance.GiveCostumeData(currentCostumeData);
          lastCostumeUsed =  currentCostumeData.costumeLogicName;
     }

     //Cambia el disfraz seleccionado cuando se recibe la acción por teclado/mando. Se avisa a la UI para mostrar el cambio por pantalla.
     private void ChangeCostume(int index)
     {
          AudioManager.Instance.PlaySound(changeCostumeSfx);
          currentIndex = index;
          currentCostumeData =  costumes[currentIndex];
          EventManagerSO.Instance.NotifieCostumeSelectedToUI(currentIndex);
     }

     //Cambia el disfraz seleccionado cuando se recibe la acción por ratón. Se avisa a la UI para mostrar el cambio por pantalla.
     private void ChangeCostumeWithScroll(float direction)
     {
          AudioManager.Instance.PlaySound(changeCostumeSfx);

          if (direction > 0)
          {
               currentIndex = (currentIndex + 1) % costumes.Length;
          }
          else
          {
               currentIndex =  (currentIndex - 1 + costumes.Length) % costumes.Length; 
          }
          
          currentCostumeData =  costumes[currentIndex];
          EventManagerSO.Instance.NotifieCostumeSelectedToUI(currentIndex);
     }

     //DESUSCRIPCIÓN A EVENTOS:
     void OnDisable()
     {
          InputController.Instance.OnScroll -= ChangeCostumeWithScroll;
          InputController.Instance.OnNewCostume -= ChangeCostume;
          InputController.Instance.OnUseCostume -= UseCostume;
          EventManagerSO.Instance.OnPickUpCostume -= AsignCostumeData;
     }
}
