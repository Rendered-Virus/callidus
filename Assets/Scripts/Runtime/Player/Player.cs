using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        var components = new List<MonoBehaviour>();
        
        if(TryGetComponent<PlayerMovement>(out var playerMovement))
            components.Add(playerMovement);
        
        if(TryGetComponent<PlayerShoot>(out var playerShoot))
            components.Add(playerShoot);
        
        foreach(var component in components)
            component.enabled = false;
        
        GameManager.Instance.OnFightBegin.AddListener(() =>
        {
            foreach(var component in components)
                component.enabled = true;
        });
    }
}
