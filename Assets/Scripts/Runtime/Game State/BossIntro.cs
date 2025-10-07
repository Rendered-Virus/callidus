using System;
using System.Collections;
using UnityEngine;

public class BossIntro : MonoBehaviour
{
    [SerializeField] private float _introDuration;
    [SerializeField] private Camera _introCamera;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        
        _introCamera.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(_introDuration);
        
        _introCamera.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        GameManager.Instance.OnFightBegin?.Invoke();
    }
}
