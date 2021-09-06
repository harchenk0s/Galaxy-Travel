using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public string Name { get; }
    public int Value { get; }

    public Modifier(string name, int value)
    {
        Name = name;
        Value = value;
    }
}
