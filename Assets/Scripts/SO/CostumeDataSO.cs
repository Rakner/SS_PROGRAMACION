using UnityEngine;

//CREACIÓN DE LA PLANTILLA DEL SCRIPTABLE OBJECT QUE FUNCIONA COMO CAJA DE DATOS DE CADA DISFRAZ
[CreateAssetMenu(fileName = "CostumeDataSO", menuName = "Scriptable Objects/CostumeDataSO")]
public class CostumeDataSO : ScriptableObject
{
    //VISUAL
    public float time;
    public Mesh mesh;
    public Material[] material;
    public Vector3 escala;
    public Quaternion rotation;
    
    //VISUAL UI

    public Sprite costumeIcon;
    
    
    //TECNICO DEL CHARACTER CONTROLLER

    public float height;
    public float radius;
    public Vector3 position;
    
    //LOGICA DEL DISFRAZ 
    
    public string costumeLogicName;
    public LayerMask mask;
    public float cd;
}
