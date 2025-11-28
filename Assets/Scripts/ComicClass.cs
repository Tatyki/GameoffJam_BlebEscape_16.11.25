using System;
using UnityEngine;

[Serializable]
public class ComicClass
{
    public int Id;
    public Pages[] pages;
}

[Serializable]
public class Pages
{
    public Sprite sprite;
    [TextArea]
    public string imageText;
}
