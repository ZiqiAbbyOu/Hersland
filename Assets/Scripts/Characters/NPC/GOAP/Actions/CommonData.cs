using CrashKonijn.Goap.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : IActionData
{
    public ITarget Target {  get; set; }
    public float Timer { get; set; }
}
