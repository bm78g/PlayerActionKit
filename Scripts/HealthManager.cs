using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
	public int maxHealth = 5;
	public int currentHealth = 5;

	private bool _invincible = false;
	[SerializeField] private float invincibleTimer = 1.5f;

	[SerializeField] private bool debugToggle = false;
	[SerializeField] private char damageKey = 'Q';
	[SerializeField] private char healKey = 'E';

	private void Update()
	{
		if (debugToggle)
			HealthDebug();
	}

	private int Damage (int damageAmount)
	{
		currentHealth -= damageAmount;

		_invincible = true;
		StartCoroutine("OnHit");

		if (currentHealth <= 0) //Game over sequence
			Destroy(gameObject);

		return currentHealth;
	}

	private IEnumerator OnHit ()
	{
		var sRenderer = GetComponent<SpriteRenderer>();

		for (var i = 0; i < invincibleTimer/.1f; i++)
		{
			sRenderer.enabled = !sRenderer.enabled;
			yield return new WaitForSeconds(.1f);
		}
		sRenderer.enabled = true;

		_invincible = false;
	}

	private int Heal (int healAmount)
	{
		if (currentHealth < maxHealth)
			currentHealth += healAmount;

		return currentHealth;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (_invincible) return;

		if (collision.gameObject.CompareTag("Damage"))
		{
			Damage(1);
		}
		if (collision.gameObject.CompareTag("Heal"))
		{
			Heal(1);
		}
	}

	private void HealthDebug ()
	{
		if (Input.GetKeyDown(CharToKeyCode(damageKey)))
			Damage(1);
		if (Input.GetKeyDown(CharToKeyCode(healKey)))
			Heal(1);
	}

	private KeyCode CharToKeyCode (char input)
	{
		return (KeyCode)System.Enum.Parse(typeof(KeyCode), char.ToString(input), true);
	}
}
