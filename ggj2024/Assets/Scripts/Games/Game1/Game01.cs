using UnityEngine;
using UnityEngine.Events;

public class Game01 : MonoBehaviour
{
    public UnityEvent<int> OnFinishGame { get; private set; }

    private void Start()
    {
        OnFinishGame = new UnityEvent<int>();
    }
}
