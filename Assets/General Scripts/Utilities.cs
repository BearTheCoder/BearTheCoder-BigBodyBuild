using System.Runtime.CompilerServices;
using UnityEngine.UIElements;
using UnityEngine;

public class Utilities 
{
    public static void CreateGrid(Transform transform)
    {
        float Rows = 10;
        float Columns = 20;
        float CellSize = 1f;
        float LineStart = 1f;
        float LineEnd = .25f;

        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        float lineThickness = .005f;

        // Draw horizontal lines
        for (int row = 0; row <= Rows; row++)
        {
            GameObject line = new GameObject("HorizontalLine" + row);
            line.transform.SetParent(transform);
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.sortingOrder = -1;
            lr.material = lineMaterial;
            lr.startWidth = lineThickness;
            lr.endWidth = lineThickness;
            lr.positionCount = 2;

            Vector3 start = new Vector3(0, row * CellSize, 0) - new Vector3(Rows * LineStart, Columns * LineEnd, 0);
            Vector3 end = new Vector3(Columns * CellSize, row * CellSize, 0) - new Vector3(Rows * LineStart, Columns * LineEnd, 0);

            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }

        // Draw vertical lines
        for (int col = 0; col <= Columns; col++)
        {
            GameObject line = new GameObject("VerticalLine" + col);
            line.transform.SetParent(transform);
            LineRenderer lr = line.AddComponent<LineRenderer>();
            lr.sortingOrder = -1;
            lr.material = lineMaterial;
            lr.startWidth = lineThickness;
            lr.endWidth = lineThickness;
            lr.positionCount = 2;

            Vector3 start = new Vector3(col * CellSize, 0, 0) - new Vector3(Rows * LineStart, Columns * LineEnd, 0);
            Vector3 end = new Vector3(col * CellSize, Rows * CellSize, 0) - new Vector3(Rows * LineStart, Columns * LineEnd, 0);

            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
    }

    public static Sprite CreateCircleSprite(int radius, Color color)
    {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(diameter, diameter);

        for (int x = 0; x < diameter; x++)
        {
            for (int y = 0; y < diameter; y++)
            {
                Vector2 center = new Vector2(radius, radius);
                Vector2 pixelPos = new Vector2(x, y);

                if (Vector2.Distance(center, pixelPos) <= radius)
                {
                    texture.SetPixel(x, y, color);
                }
                else
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }

        texture.Apply();
        texture.filterMode = FilterMode.Point;

        return Sprite.Create(texture, new Rect(0, 0, diameter, diameter), new Vector2(0.5f, 0.5f));
    }
}