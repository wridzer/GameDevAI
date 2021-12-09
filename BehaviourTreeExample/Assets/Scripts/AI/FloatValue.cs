using System.Collections;
using UnityEngine;

[System.Serializable]
public class FloatValue
{
    public event System.Action<float> ValueChanged;

    private float value;
    public float Value { 
        get { return value; }
        set
        {
            if (value != this.value)
            {
                this.value = value;
                ValueChanged?.Invoke(value);
            }
            else
            {
                this.value = value;
            }
        }
    }

    public float MaxValue { get; internal set; }
}