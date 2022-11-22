using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private int snowBalls;

    public bool isEmpty() => snowBalls <= 0;
    public void AddSnowballs(int snowball) => this.snowBalls += snowball;
    public void decrimentSnowball() {
        if (isEmpty()) return;
        snowBalls--;
    }
    
}
