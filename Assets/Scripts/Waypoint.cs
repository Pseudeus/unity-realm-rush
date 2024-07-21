using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const byte Grid = 10; //Tamaño constante de grilla para todos
    public bool isExplored;
    public bool isTowered;    // todo place towers in the current waypoints and make it not useful to enemy road
    public Waypoint exploredFrom;
    
    public byte GridSize //Propiedad de solo lectura
    {
        get => Grid;
    }

    public Vector2Int GetGridPosition() //Regresa un Vector2Int con la nueva posicion del GameObject
    {
        var realPos = transform.position; //Obtiene la posicion actual del gameObject
        
        return new Vector2Int(Mathf.RoundToInt(realPos.x / 10f), 
                              Mathf.RoundToInt(realPos.z / 10f)); //Asigna el tamaño del incremento redondeado a entero y multiplicado por el factor de la grilla
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topColor = transform.Find("Top").GetComponent<MeshRenderer>();//Obtener el mesh renderer del hijo llamado Top del gameObject
        topColor.material.color = color;
    }
}    
