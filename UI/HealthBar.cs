using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	private float maxHealth;
	private float currentHealth, lateCurrentHealth;
	private Enemy thisEnemy;
	[SerializeField] Image fill;
	[SerializeField] private Slider healthBar;
	[SerializeField] Gradient gradientHp;
	

    // Start is called before the first frame update
    private void Start()
    {
		thisEnemy = gameObject.GetComponent<Enemy>();
		maxHealth = thisEnemy.Hp;
		currentHealth = maxHealth;
		lateCurrentHealth = currentHealth;
		healthBar.maxValue = maxHealth;
		healthBar.value = currentHealth;
		fill.color = gradientHp.Evaluate(1f);
    }

	private void Update() {
		currentHealth = thisEnemy.Hp;
		if(currentHealth != lateCurrentHealth)
			SetHealth();
		lateCurrentHealth = currentHealth;
	}

	private void SetHealth(){
		healthBar.value = currentHealth;
		fill.color = gradientHp.Evaluate(healthBar.normalizedValue);
	}
}
