using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private Transform spawnPlatePoint;
    [SerializeField] private Transform plateGameObjectVisual;
    [SerializeField] private List<GameObject> plateGameObjectList;

    private void Awake()
    {
        plateGameObjectList = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnRemoved += PlateCounter_OnRemoved;
    }

    private void PlateCounter_OnRemoved(object sender, System.EventArgs e)
    {
        GameObject gameObjectVisual = plateGameObjectList[plateGameObjectList.Count - 1];
        plateGameObjectList.Remove(gameObjectVisual);
        Destroy(gameObjectVisual);
    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        Transform plateVisualTransfrom = Instantiate(plateGameObjectVisual, spawnPlatePoint);
        float plateOffsetY = .1f;
        plateVisualTransfrom.localPosition = new Vector3(0, plateOffsetY * plateGameObjectList.Count, 0);
        plateGameObjectList.Add(plateVisualTransfrom.gameObject);
    }
}
