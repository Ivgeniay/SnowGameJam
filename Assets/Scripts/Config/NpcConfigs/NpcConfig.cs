using UnityEngine;

[CreateAssetMenu(fileName = "NewConfig", menuName = "Npc/Config")]
public class NpcConfig : ScriptableObject
{

    [SerializeField] private float _speed;

    [SerializeField] private float _damage;
    [SerializeField] private int _attackType;
    [SerializeField] private float _attackDelayIsSeconds;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackSpeed;

    [SerializeField] private float _walkDelayIsSeconds;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private int _walkType;


    public float speed { get => _speed; }

    
    public void Initialize() 
    {

    }


}