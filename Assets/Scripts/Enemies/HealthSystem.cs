using Assets.Scripts.Enemies.DamageMech;
using Assets.Scripts.EventArgs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDeath;
    public event EventHandler<TakeDamagePartEventArgs> OnTakeDamage;

    [SerializeField] private float maxHealth;

    private List<IDamageable> damageablesParts;
    public float MaxHealth { get => maxHealth; }
    public float health { get; private set; }
    public bool isDead { get; private set; }  = false;

    private void Awake() {
        health = maxHealth;
        damageablesParts = gameObject.GetComponentsInChildren<IDamageable>().ToList();
        foreach (var el in damageablesParts) el.OnTakeDamage += OnTakeDamageHandler;
    }

    public void TakeDamage(TakeDamagePartEventArgs e) {
        if (isDead) return;

        if (health - e.Damage <= 0) {
            health = 0;
            isDead = true;
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
        else {
            health = health - e.Damage;
            OnTakeDamage?.Invoke(this, new TakeDamagePartEventArgs() { Damage = e.Damage, Direction = e.Direction, Shooter = e.Shooter, currentHealth = health});
        }
    }
    private void OnTakeDamageHandler(object sender, TakeDamagePartEventArgs e) => TakeDamage(e);
}
