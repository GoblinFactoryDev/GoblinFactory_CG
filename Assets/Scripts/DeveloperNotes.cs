using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DeveloperNotes : MonoBehaviour
{

    [TextArea(10, 3)]
    [SerializeField] private List<string> allNotes = new List<string>();

    public void MakeNote()
    {
        string newNote = string.Empty;
        allNotes.Add(newNote);
    }
}
