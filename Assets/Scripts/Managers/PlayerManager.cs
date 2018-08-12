﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // Singleton instance
    private static PlayerManager _instance = null;

    [Header("Stats")]
    [SerializeField]
    private float health;
    [SerializeField]
    private float reduction;
    // Keeps track ingame
    private PlayerHealthbarUI healthUI;

    // Keeps track if player can still do stuff
    public bool isAlive { get; private set; }

    public static PlayerManager Instance
    {
        get
        {
            return _instance;
        }
    }   

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
            Destroy(gameObject);

        // Make sure it persits
        DontDestroyOnLoad(gameObject);
        // Player should be alive at the start
        isAlive = true;
    }

    // Apply a certain amount of damage
    public void ApplyDamage(float amount)
    {
        // Reduce or set to zero
        if (health - amount > 0)
        {
            health -= amount * reduction;
        }
        else
        {
            health = 0;
            isAlive = false;
        }
        // Update UI
        if (healthUI == null)
        {
            // Find script in scene
            healthUI = FindObjectOfType<PlayerHealthbarUI>();
            // If still null return
            if (healthUI == null)
                return;
        }
        // Set Health
        healthUI.SetHealthRemaining(health);
    }
}