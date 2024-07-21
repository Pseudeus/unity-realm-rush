using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> _queue = new Queue<Waypoint>();
    private bool _isRunning = true;
    private List<Waypoint> path = new List<Waypoint>();
    
    private readonly Vector2Int[] _directions =    //Todos los movimientos posibles del algoritmo van aqui
    {
        Vector2Int.up, 
        Vector2Int.right, 
        Vector2Int.down, 
        Vector2Int.left
    };

    public Transform GetStartPos()
    {
        return startWaypoint.transform;
    }

    public void FillDictionary() //Llamar metodo desde enemyCreator
    {
        
    }

    public List<Waypoint> GetPath()
    {
        LoadBlocks();        //Cargar bloques en el diccionario
        // SetStartAndEnd();    //identificar el inicio y fin
        BreadthFirstSearch();//Pathfinding baby
        MakePath();          //Armar el camino
        return path;
    }
    private void MakePath()
    {
        path.Add(endWaypoint);
        while (path.Last().exploredFrom != null)
        {
            path.Add(path.Last().exploredFrom);
        }
        path.Reverse();
    }
    private void BreadthFirstSearch()
    {
        _queue.Enqueue(startWaypoint); //se encola el waypoint de inicio

        while (_queue.Count > 0 && _isRunning) //mientras la cola tenga mas de 0 y este corriendo
        {
            var searchCenter = _queue.Dequeue(); //Entrega el valor frontal de la cola FIFO 
            OnEndFound(searchCenter);    //En caso en encontrar el fin
            ExploreNeighbours(searchCenter); //Base del algoritmo
            searchCenter.isExplored = true; //marcar como explorado este waypoint
        }
    }

    private void OnEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
        {
            _isRunning = false;
        }
    }

    private void ExploreNeighbours(Waypoint from)
    {
        if (!_isRunning) return; //revalidar si esta corriendo si no, get out da here!
        
        foreach (Vector2Int direction in _directions) //usamos las direcciones de exploracion
        {
            var explorationCoordinates = from.GetGridPosition() + direction; //utilizamos la direccion de iteracion
            if(_grid.ContainsKey(explorationCoordinates))
            {
                Waypoint neighbour = _grid[explorationCoordinates];  //buscamos en el diccionario con el uso de la TKey para obtener su respectivo TValue
                
                if (neighbour.isExplored || _queue.Contains(neighbour)) continue; // si el waypoint esta marcado como explorado o ya esta en cola salta a la siguiente iteracion
                
                _queue.Enqueue(neighbour);     //Ponemos en cola el waypoint de la iteracion
                neighbour.exploredFrom = from;    // Le indicamos al Waypoint desde donde fue explorado
            }
        }
    }

    private void SetStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.magenta);
    }

    private void LoadBlocks()         //Se encarga de añadir elementos al diccionario grid
    {
        var waypoints = FindObjectsOfType<Waypoint>(); //crea un array de la clase Waypoint

        foreach (var waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPosition(); //Obtiene la posicion del Waypoint de la iteracion actual

            if (_grid.ContainsKey(gridPos)) //Si ya existe la key en el diccionario entonces se evita añadirlo
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                _grid.Add(gridPos, waypoint); // se añade el waypoint con la Tkey y el TValue 
                //waypoint.SetTopColor(Color.gray); //Se colorea el top de gris
            }
        }
    }
}
