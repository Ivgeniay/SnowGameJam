using Assets.Scripts.EventArgs;
using Assets.Scripts.Units;
using Assets.Scripts.Units.DamageMech;
using Assets.Scripts.Units.StateMech;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler<OnNpcDieEventArg> OnDied;
    public event EventHandler<TakeDamagePartEventArgs> OnTakeDamage;

    private UnitConfiguration unitConfiguration;
    private List<IDamageable> damageablesParts;


    private float maxHealth = 1;
    public float MaxHealth { get => maxHealth; }
    public float health { get; private set; }
    public bool isDead { get; private set; }  = false;
    private void Awake() {
        unitConfiguration = GetComponent<UnitConfiguration>();
        if (unitConfiguration is not null) {
            maxHealth = unitConfiguration.Health;
        }
        health = maxHealth;
        damageablesParts = gameObject.GetComponentsInChildren<IDamageable>().ToList();
        if (damageablesParts.Count > 0)
            foreach (var el in damageablesParts) el.OnTakeDamage += OnTakeDamageHandler;
    }
    private void Start() {
    }

    private void OnTakeDamageHandler(object sender, TakeDamagePartEventArgs e) => TakeDamage(sender, e);
    public void TakeDamage(object sender, TakeDamagePartEventArgs e) {
        if (isDead) return;

        OnTakeDamage?.Invoke(this, new TakeDamagePartEventArgs() {
            Damage = e.Damage,
            Direction = e.Direction,
            Shooter = e.Shooter,
            currentHealth = health,
            SenderPartOfBody = sender
        });

        var head = sender as IUltimateDamageArea;
        if (head is not null) {
            health = 0;
            isDead = true;
            OnDied?.Invoke(this, new OnNpcDieEventArg() { UnitBehavior = transform.GetComponent<UnitBehavior>() });
            return;
        }

        if (health - e.Damage <= 0) {
            health = 0;
            isDead = true;
            OnDied?.Invoke(this, new OnNpcDieEventArg() { UnitBehavior = transform.GetComponent<UnitBehavior>() });
        }
        else {
            health -= e.Damage;
        }
    }
}
