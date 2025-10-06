using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
   public UnityEvent OnFightBegin;
   public bool FightBegin = false;
   private IEnumerator Start()
   {
      yield return new WaitForSeconds(2);
      OnFightBegin?.Invoke();
      FightBegin = true;
   }
}
