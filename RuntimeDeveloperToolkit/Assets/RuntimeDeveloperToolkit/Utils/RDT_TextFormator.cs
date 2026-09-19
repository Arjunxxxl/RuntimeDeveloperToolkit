using UnityEngine;

public class RDT_TextFormator : MonoBehaviour
{
    public static string GetFormatedString(int value)
    {
        if (value >= 1_000_000_000)
            return $"{value / 1_000_000_000f:0.##}B";

        if (value >= 1_000_000)
            return $"{value / 1_000_000f:0.##}M";

        if (value >= 1_000)
            return $"{value / 1_000f:0.##}K";

        return value.ToString();
    }
}
