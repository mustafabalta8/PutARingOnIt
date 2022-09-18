using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class RoadCreation : MonoBehaviour
{
    [BoxGroup("Paths")]
    public List<Vector3> roadPath;
    [BoxGroup("Paths")]
    public List<Vector3> finalPath;

    [BoxGroup("Objects")]
    [SerializeField] private GameObject myRoad;
    [BoxGroup("Parent")]
    [SerializeField] private Transform roadParent;
    [BoxGroup("Created Objects List")]
    [SerializeField] private List<GameObject> roads;
    [BoxGroup("Created Objects List")]
    [SerializeField] private List<GameObject> finalRoads;

    [BoxGroup("Road Settings")]
    public float roadLength = 14f;
    [BoxGroup("Road Settings")]
    public int roadCount;


    private bool isThereFinal = false;

    [Button("Create")]
    private void CreateRoad()
    {
        var index = roads.Count == 0 ? 0 : roads.Count;
        var roadPosition = index * (Vector3.forward * roadLength);
        var newRoad = Instantiate(myRoad, roadPosition, Quaternion.identity, roadParent);
        roads.Add(newRoad);
        roadPath.Add(newRoad.transform.position);

    }

    [Button("Delete")]
    void DeleteRoad()
    {
        if (roads.Count == 0) return;
        DestroyImmediate(roads[roads.Count - 1]);
        roads.RemoveAt(roads.Count - 1);
        roadPath.RemoveAt(roadPath.Count - 1);
    }

    [Button("Delete All")]
    void DeleteAll()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            DestroyImmediate(roads[i]);
        }
        for (int i = 0; i < finalRoads.Count; i++)
        {
            DestroyImmediate(finalRoads[i]);
        }
        isThereFinal = false;
        roads.Clear();
        roadPath.Clear();
        finalRoads.Clear();
        finalPath.Clear();
    }

    [Button("Create Multiple")]
    void CreateMultipleRoad()
    {
        for (int i = 0; i < roadCount; i++)
        {
            CreateRoad();
        }
    }
    /*
    [Button("Create Final")]
    
    void CreateFinal()
    {
        if (isThereFinal) return;
        for (int i = 0; i < finalRoadDataList.Count; i++)
        {
            CreateFinalRoad(i);
        }
        isThereFinal = true;
    }

    private void CreateFinalRoad(int i)
    {
        var index = roads.Count == 0 ? 0 : roads.Count - 1;
        var finalRoadPosition = roads[index].transform.position + ((i + 1) * Vector3.forward * finalRoadLength);
        finalRoad.isFinal = i == 0;
        finalRoad.Initialize(finalRoadDataList[i]);
        var newRoad = Instantiate(finalRoad, finalRoadPosition, Quaternion.identity, roadParent);
        finalRoads.Add(newRoad.gameObject);
        finalPath.Add(newRoad.transform.position);
    }
    */
}
