using System;
using System.Collections.Generic;
using uClicker;
using UnityEngine;

public class PopulateBuildings : MonoBehaviour
{
    [SerializeField] private ClickerManager clickerManager;
    [SerializeField] private GameObject prefab;

    [NonSerialized] private List<Binder> _objects = new List<Binder>();

    void Start()
    {
        for (int i = 0; i < clickerManager.Config.AvailableBuildings.Length; i++)
        {
            Building availableBuilding = clickerManager.Config.AvailableBuildings[i];
            GameObject go = Instantiate(prefab, this.transform);
            Binder binder = go.GetComponent<Binder>();
            binder.Bind(availableBuilding);
            go.SetActive(availableBuilding.Unlocked);
            _objects.Add(binder);
        }

        clickerManager.OnBuyBuilding.AddListener(UpdateBuildingCost);
        clickerManager.OnTick.AddListener(OnTick);
    }

    private void UpdateBuildingCost()
    {
        for (int i = 0; i < clickerManager.Config.AvailableBuildings.Length; i++)
        {
            _objects[i].Bind(clickerManager.Config.AvailableBuildings[i]);
        }
    }

    private void OnTick()
    {
        for (int i = 0; i < clickerManager.Config.AvailableBuildings.Length; i++)
        {
            _objects[i].gameObject.SetActive(clickerManager.Config.AvailableBuildings[i].Unlocked);
        }
    }
}