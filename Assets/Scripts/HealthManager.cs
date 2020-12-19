using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public GameObject thePlayer;

    public float invincibilityLength;
    private float invincibilityCount;

    public Renderer bodyRenderer;
    public Renderer headRenderer;
    private float flashCount;
    public float flashLength;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityCount > 0)
        {
            invincibilityCount -= Time.deltaTime;

            flashCount -= Time.deltaTime;
            if(flashCount <= 0)
            {
                headRenderer.enabled = !headRenderer.enabled;
                bodyRenderer.enabled = !bodyRenderer.enabled;
                flashCount = flashLength;
            }

            if(invincibilityCount <= 0)
            {
                headRenderer.enabled = true;
                bodyRenderer.enabled = true;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCount <= 0)
        {
            currentHealth -= damage;

            if(currentHealth <= 0)
            {
               FindObjectOfType<GameManager>().DeathPlayer();
            }

            FindObjectOfType<GameManager>().SetHealth(currentHealth);

            thePlayer.GetComponent<PlayerController>().KnockBack(direction);

            invincibilityCount = invincibilityLength;

            headRenderer.enabled = false;
            bodyRenderer.enabled = false;
            flashCount = flashLength;
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
