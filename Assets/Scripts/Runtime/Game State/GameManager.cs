using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
   public UnityEvent OnFightBegin;
   public UnityEvent OnFightWin;
   public UnityEvent OnFightLose;
   
   public bool FightBegin = false;
   [SerializeField] private Image _fade;
   [SerializeField] private float _fadeDuration;
   [SerializeField] private CanvasGroup _gameOverScreen;
   private bool _canRestart;
   [SerializeField] private TextMeshProUGUI _restartText;

   protected override void Awake()
   {
      base.Awake();
      _fade.color = new Color(0, 0, 0, 1);
   }

   private void Start()
   {
      OnFightBegin.AddListener(()=> FightBegin = true);  
      _fade.DOFade(0, _fadeDuration);
   }

   public void PlayerDeath()
   {
      _gameOverScreen.DOFade(1, _fadeDuration);
      OnFightLose?.Invoke();

      Invoke(nameof(EnableRestart),2f);
   }

   public void EnableRestart()
   {
      _canRestart = true;
      _restartText.DOFade(.2f, _fadeDuration);
   }
   public void LoadNextScene()
   {
      if(SceneManager.GetActiveScene().buildIndex >= 4) return;
      
      _fade.DOFade(1, _fadeDuration).OnComplete(() =>
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      });
   }
   

   public void ReloadCurrentScene()
   {
      _fade.DOFade(1, _fadeDuration).OnComplete(() =>
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      });
   }

   private void Update()
   {
      if(_canRestart && Input.GetKeyDown(KeyCode.Space))
         ReloadCurrentScene();
   }
}
