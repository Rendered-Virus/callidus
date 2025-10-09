using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
   public UnityEvent OnFightBegin;
   public UnityEvent OnFightWin;
   public bool FightBegin = false;
   private void Start()
   {
      OnFightBegin.AddListener(()=> FightBegin = true);      
   }
}
