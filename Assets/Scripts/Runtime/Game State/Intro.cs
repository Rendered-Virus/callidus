using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] private Element[] _elements;
    [SerializeField] private Image _banner;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _timeBetweenLetters;
    [SerializeField] private float _extraTime;

    private void Start()
    {
        StartCoroutine(BeginIntro());
    }

    private IEnumerator BeginIntro()
    {
        foreach (var element in _elements)
        {
            _banner.sprite = element.Icon;
            _text.text = "";
            foreach (var c in element.text)
            {
                _text.text += c;
                yield return new WaitForSeconds(_timeBetweenLetters);
            }

            yield return new WaitForSeconds(_extraTime);
        }    
        
        GameManager.Instance.LoadNextScene();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
            GameManager.Instance.LoadNextScene();
            
    }
}

[Serializable]
public struct Element
{
    public Sprite Icon;
    [TextArea] public string text;
}