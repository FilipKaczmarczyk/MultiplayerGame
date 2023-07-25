using System;
using System.Collections.Generic;
using UnityEngine;

namespace Counters.Visuals
{
    public class PlatesCounterVisual : MonoBehaviour
    {
        [SerializeField] private PlatesCounter platesCounter;
        [SerializeField] private Transform counterTopPoint;
        [SerializeField] private Transform plateVisualPrefab;

        private List<GameObject> _plateVisualGameObjects = new List<GameObject>();
        
        private void OnEnable()
        {
            platesCounter.OnSpawnPlate += PlatesCounterOnOnSpawnPlate;
            platesCounter.OnPickPlate += PlatesCounterOnOnPickPlate;
        }

        private void OnDisable()
        {
            platesCounter.OnSpawnPlate -= PlatesCounterOnOnSpawnPlate;
            platesCounter.OnPickPlate -= PlatesCounterOnOnPickPlate;
        }
        
        private void PlatesCounterOnOnPickPlate(object sender, EventArgs e)
        {
            var plateToPick = _plateVisualGameObjects[^1];
            _plateVisualGameObjects.Remove(plateToPick);
            Destroy(plateToPick);
        }
        
        private void PlatesCounterOnOnSpawnPlate(object sender, EventArgs e)
        {
            var plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

            var plateOffsetY = .2f;
            plateVisualTransform.localPosition =
                new Vector3(0, plateOffsetY * _plateVisualGameObjects.Count * plateOffsetY, 0);
            
            _plateVisualGameObjects.Add(plateVisualTransform.gameObject);
        }
    }
}
