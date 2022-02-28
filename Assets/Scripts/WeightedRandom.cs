using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedRandom<T>
{
    Dictionary<T, float> _weights = new Dictionary<T, float>();

    public int Count => _weights.Count;

    public void Add(T key, float weight)
    {
        _weights.Add(key, weight);
    }

    public T TakeOne()
    {
        var random = Random.Range(0f, SumWeights());
        var select = default(T);

        foreach(var weight in _weights)
        {
            random -= weight.Value;

            if (random > 0)
                continue;

            select = weight.Key;
            break;
        }

        _weights.Remove(select);
        return select;
    }

    float SumWeights()
    {
        var result = 0f;

        foreach (var weight in _weights)
            result += weight.Value;

        return result;
    }
}
