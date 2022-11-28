using Assets.Scripts.EventArgs;
using Assets.Scripts.Units.DamageMech;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public void TakeDamage(object sender, TakeDamagePartEventArgs e) {
        if (isDead) return;

        var head = sender as IUltimateDamageArea;
        if (head is not null) {
            health = 0;
            isDead = true;
            OnDeath?.Invoke(this, EventArgs.Empty);
            return;
        }

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
    private void OnTakeDamageHandler(object sender, TakeDamagePartEventArgs e) => TakeDamage(sender, e);
}
