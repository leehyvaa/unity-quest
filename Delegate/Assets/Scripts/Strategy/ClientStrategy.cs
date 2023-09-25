using Chapter.Strategy;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ClientStrategy : MonoBehaviour
{
    private GameObject _drone;

    private List<IManeuverBehaviour> _components = new List<IManeuverBehaviour>();

    private void SpawnDrone()
    {
        _drone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _drone.AddComponent<Drone>();
        _drone.transform.position = Random.insideUnitSphere * 10f;

        ApplyRandomStrategies();
    }

    private void ApplyRandomStrategies()
    {
        _components.Add(_drone.AddComponent<WeavingManeuver>());
        _components.Add(_drone.AddComponent<BoppingManeuver>());
        _components.Add(_drone.AddComponent<FallbackManeuver>());

        int index = Random.Range(0, _components.Count);

        _drone.GetComponent<Drone>().ApplyStrategy(_components[index]);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Drone"))
        {
            SpawnDrone();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
