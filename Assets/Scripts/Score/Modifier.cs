using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public string Text { get; }
    public int Value { get; }

    public Modifier(string text, int value)
    {
        Text = text;
        Value = value;
    }
}
