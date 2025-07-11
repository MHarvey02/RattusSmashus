using System.Collections.Generic;
using UnityEngine;

public class TestToolStatic
{

    
    public static int death { get; set; }
    public static List<int> deathList { get; set; } = new();
    public static List<float> timeList { get; set; } = new();
    TestToolStatic()
    {
        death = 0;
    }
}
