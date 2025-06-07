using UnityEngine;

[System.Serializable]
public class Line
{
    public string text;
    public Camera camera;
}

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class Dialogue : ScriptableObject
{
    public Line[] lines;
}