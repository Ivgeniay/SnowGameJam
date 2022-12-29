using Assets.Scripts.Game.Pause;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Scripts.Units.GlobalTarget
{
    [Required]
    internal class XMasTree : SerializedMonoBehaviour, IGlobalTarget, IGameStateHandler, IRestartable
    {
        public event Action<TreeDamageEventArgs> OnXMasTreeTakeDamage;
        public event Action OnXMasTreeDie;
        private Animator animator;

        [SerializeField] private int MaxHealth;
        private int currentHealth;
        public Transform GetTransform() => this.transform;

        #region Mono
        private void Awake()
        {
            Game.Game.Manager.OnInitialized += OnGameInitialized;
            OnXMasTreeDie += XMasTree_OnXMasTreeDie;
            currentHealth = MaxHealth;
            animator = GetComponentInChildren<Animator>();
        }

        private void OnGameInitialized() {
            Game.Game.Manager.GameStateManager.Register(this);
            Game.Game.Manager.Restart.Register(this);
        }

        private void Start() {
            //Message about current HP of Tree
            OnXMasTreeTakeDamage?.Invoke(new TreeDamageEventArgs() { Damage = 0, Left = currentHealth });
        }
        #endregion

        public void TakeDamage(object sender, int amount)
        {
            if (currentHealth <= 0) return;

            currentHealth = Damage(amount, currentHealth);
            if (currentHealth > 0) {
                OnXMasTreeTakeDamage?.Invoke(new TreeDamageEventArgs() { Damage = amount, Left = currentHealth });
                SetTriggerAnimator(animator, "TakeDamage");
                return;
            }
            OnXMasTreeDie?.Invoke();
        }

        private void XMasTree_OnXMasTreeDie()
        {
            Game.Game.Manager.GameStateManager.SetState(GameState.GameOver);
        }

        private int Damage(int amount, int currentHealth)
        {
            if (currentHealth < amount) {
                currentHealth = 0;
            }
            currentHealth -= amount;

            return currentHealth;
        }

        private void SetTriggerAnimator(Animator animator, string triggerName) => animator.SetTrigger(triggerName);
        

        public void GameStateHandle(GameState gameState)
        {
        }

        public void Restart() {
            animator.ResetTrigger("TakeDamage");
            currentHealth = MaxHealth;
            OnXMasTreeTakeDamage?.Invoke(new TreeDamageEventArgs() { Damage = 0, Left = currentHealth });
        }

    }
}

public class TreeDamageEventArgs
{
    public int Damage;
    public int Left;
}
