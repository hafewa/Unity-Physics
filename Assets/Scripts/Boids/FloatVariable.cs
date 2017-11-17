using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : ScriptableObject
{
    [Range(0, 20)]
    public float Value;
}
