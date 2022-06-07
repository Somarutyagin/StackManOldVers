using UnityEngine;

public class CamChangeColor : MonoBehaviour
{
    public Material[] colors;
    public MeshRenderer[] bg;
    public Levels levels;

    public static int indexOfColor = 0;
    public static int colorsCount;

    private void Start()
    {
        colorsCount = colors.Length;
        indexOfColor = PlayerPrefs.GetInt("IndexOFColor",indexOfColor);
            for (int i = 0; i < colors.Length; i++)
            {
                if (indexOfColor == i)
                {
                    for (int j = 0; j < bg.Length; j++)
                    {
                        bg[j].material = colors[i];
                    }
                }
            }
    }
}
