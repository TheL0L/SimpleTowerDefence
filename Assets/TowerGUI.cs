using UnityEngine;

/*
    THIS CODE IS TEMPORARY AND SHOULD NOT STAY FOR LONG.
*/

[RequireComponent(typeof(Tower))]
public class TowerGUI : MonoBehaviour
{
    [Range(3, 100)]
    public const int segments_count = 30;
    private Vector2[] vertecies;
    private Tower tower;
    private LineRenderer circle_renderer;

    // Start is called before the first frame update
    void Start()
    {
        circle_renderer = GetComponent<LineRenderer>();
        circle_renderer.loop = true;
        circle_renderer.positionCount = segments_count;

        tower = GetComponent<Tower>();
        CalculateSegments();
    }

    private void CalculateSegments()
    {
        vertecies = new Vector2[segments_count];
        float radius = tower.properties[tower.level].range;
        float angle_step = 2 * Mathf.PI / segments_count;

        for (int i = 0; i < segments_count; i++)
        {
            float xPos = radius * Mathf.Cos(angle_step * i);
            float yPos = radius * Mathf.Sin(angle_step * i);

            vertecies[i] = new Vector2(xPos, yPos);
            circle_renderer.SetPosition(i, vertecies[i]);
        }
    }
}
