using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerIntro : MonoBehaviour
{
    [SerializeField] private float _introRunTime;
    [SerializeField] private float _introEndPointX;
    private void Start()
    {
        ComponentHandling();
        
        var anim = GetComponent<Animator>();
        transform.DOMoveX(_introEndPointX,  _introRunTime).OnComplete(()=> anim.SetTrigger("Begin"));
    }

    private void ComponentHandling()
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
