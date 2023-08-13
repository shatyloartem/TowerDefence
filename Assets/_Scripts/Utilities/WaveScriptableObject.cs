using UnityEngine;

[CreateAssetMenu(fileName = "New wave", menuName = "Waves/New wave data")]
public class WaveScriptableObject : ScriptableObject
{
    public int[] enemiesAtWave;
}
