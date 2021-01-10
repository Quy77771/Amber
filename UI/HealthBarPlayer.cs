using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{


    [Tooltip("Time to reduce hp")]
    [SerializeField] float speedSlideFill_2 = 0.8f;
	[SerializeField] Image fill;
	[SerializeField] private Slider healthBar,slideFill_2;
	[SerializeField] Gradient gradientHp;
	
    private void Start()
    {
		healthBar.maxValue = PlayerControl.Instance.maxHp;;
		healthBar.value = PlayerControl.Instance.hp;;
		fill.color = gradientHp.Evaluate(1f);

		GameManager.Instance.OnHpChanged += SetHealth;
    }



	private void SetHealth(float hp){
		healthBar.value += hp;
		fill.color = gradientHp.Evaluate(healthBar.normalizedValue);
        StartCoroutine(SetFill_2(healthBar.normalizedValue, slideFill_2.value));
	}

    private IEnumerator SetFill_2(float healthBarValue, float currentSlideFill_2){
        while(slideFill_2.value > healthBarValue){
            slideFill_2.value -= (currentSlideFill_2 - healthBarValue) * Time.deltaTime / speedSlideFill_2;
            yield return null;
        }
    }
}
