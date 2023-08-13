using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Minerals", menuName = "Inventory/Minerals")]
[System.Serializable]
public class Mineral:Item
{
    public int moltentemperaturee;

    public Color color;

    public Color AverageColorFromTexture()
    {
        Color[] textureColors = this.image.texture.GetPixels();
        Rect rect = this.image.rect;

        int total = (int)(rect.width * rect.height);

        float r = 0;
        float g = 0;
        float b = 0;
        for (int i = (int)rect.position.x; i < (int)rect.position.x+ (int)rect.width; i++)
        {
            for(int j = (int)rect.position.y; j < (int)rect.position.y+(int)rect.height; j++)
            {                
                r += textureColors[i + j * this.image.texture.width].r;
                g += textureColors[i + j * this.image.texture.width].g;
                b += textureColors[i + j * this.image.texture.width].b;
            }
        }
        return new Color(r / total,g / total,b / total, 255);
    }
}