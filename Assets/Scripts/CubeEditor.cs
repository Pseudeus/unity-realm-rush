using System;
using UnityEngine;

[ExecuteInEditMode][SelectionBase][RequireComponent(typeof(Waypoint))] //Ejecutar en modo edicion, hacer del GameObject la seleccion base y requerir el componente Waypoint
public class CubeEditor : MonoBehaviour
{
    private Waypoint _waypoint;    //Para almacenar una instancia del componente Waypoint

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();    //Asignando el componente a la variable que se encuentra en el mismo GameObject
    }

    private void SnapConstraint()
    {
        var gridSize = _waypoint.GridSize;    //Obtener un tamaño de grilla constante para todos los cubos
        
        transform.position = new Vector3(_waypoint.GetGridPosition().x * gridSize, 
                                        0f, 
                                        _waypoint.GetGridPosition().y * gridSize); //Dar valor a la transform del gameObject por medio de un new Vector3
    }

    private void LabelHandler()
    {
        var textMesh = GetComponentInChildren<TextMesh>();    //Para representar el nombre del objeto con una etiqueta sobre de el y Obtener el componente TextMesh desde algun hijo
        
        textMesh.text = _waypoint.GetGridPosition().x + 
                        "," + 
                        _waypoint.GetGridPosition().y; //Asignar el texto al textmesh

        gameObject.transform.name = _waypoint.GetGridPosition().x + 
                                    "," + 
                                    _waypoint.GetGridPosition().y; //Cambiar el nombre del GameObject
    }
    private void Update()
    {
        SnapConstraint();    //Llamar a la funcion    
        LabelHandler();      //Llamar a la funcion
    }
}
