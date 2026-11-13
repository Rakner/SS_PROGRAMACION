using UnityEngine;

public class BowlCostumeLogic : CostumeLogic
{
    //IMPLEMENTA LA LÓGICA DE LA CLASE PADRE
    protected override void CancelCostumeAction()
    {
        //Ajuste de posición para evitar salirse de los bordes del mapa por debajo. 
        transform.position += new Vector3(0, 1.5f,0);
        base.CancelCostumeAction();
        
    }

    protected override void ChangeVisualCostume()
    {
        //Ajuste de rotación al disfrazarse para que el modelo se vea correctamente. 
        base.ChangeVisualCostume();
        costume.transform.rotation = costumeData.rotation;
    }
}
