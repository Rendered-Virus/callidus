using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SharedHealthBar : MonoBehaviour
{
   public int CurrentHealth;
   public int MaxHealth;

   public void UpdateUI(float _healthBarUpdateRate, float _healthBarUpdateDeltaTime, Slider _slider)
   {
      StopAllCoroutines();
      StartCoroutine(UpdateUICoroutine(_healthBarUpdateRate, _healthBarUpdateDeltaTime, _slider));
   }

   private IEnumerator UpdateUICoroutine(float _healthBarUpdateRate, float _healthBarUpdateDeltaTime, Slider _slider)
   {
      while (_slider.value > CurrentHealth)
      {
         _slider.value -= _healthBarUpdateRate * _healthBarUpdateDeltaTime;
         yield return new WaitForSeconds(_healthBarUpdateDeltaTime);
      }
   }
}
