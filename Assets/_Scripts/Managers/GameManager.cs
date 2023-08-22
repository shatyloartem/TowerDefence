using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int targetFPS = 120;

    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}
