using UnityEngine;

public class SofaCostumeLogic : CostumeLogic
{
    //IMPLEMENTA LA LÓGICA DE LA CLASE PADRE
    protected override void ChangeVisualCostume()
    {
        //Ajuste de rotación al disfrazarse para que el modelo se vea correctamente. 
        base.ChangeVisualCostume();
        costume.transform.rotation = costumeData.rotation;
    }
}
