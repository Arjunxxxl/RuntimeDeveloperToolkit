using UnityEngine;
using UnityEngine.UI;

namespace RuntimePerformanceMonitor
{
    public class Graph : Graphic
    {
        [SerializeField] private float lineThickness = 2f;

        private float[] values;
        private float minValue = 0f;
        private float maxValue = 120f;

        public void SetData(float[] data, float minVal, float maxVal)
        {
            values = data;
            minValue = minVal;
            maxValue = maxVal;
            SetVerticesDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            if (values == null || values.Length < 2)
                return;

            Rect rect = rectTransform.rect;

            float width = rect.width;
            float height = rect.height;
            Vector2 halfSize = rect.size * 0.5f;
            
            for (int i = 0; i < values.Length - 1; i++)
            {
                float x1 = Mathf.Lerp(
                    0f,
                    width,
                    (float) i / (values.Length - 1)
                );

                float x2 = Mathf.Lerp(
                    0f,
                    width,
                    (float) (i + 1) / (values.Length - 1)
                );

                float y1 = Mathf.InverseLerp(
                    minValue,
                    maxValue,
                    values[i]
                ) * height;

                float y2 = Mathf.InverseLerp(
                    minValue,
                    maxValue,
                    values[i + 1]
                ) * height;

                Vector2 p1 = new Vector2(x1 - halfSize.x, y1 - halfSize.y);
                Vector2 p2 = new Vector2(x2 - halfSize.x, y2 - halfSize.y);

                AddLine(vh, p1, p2, lineThickness);
            }
        }

        private void AddLine(
            VertexHelper vh,
            Vector2 start,
            Vector2 end,
            float thickness)
        {
            Vector2 direction = (end - start).normalized;

            Vector2 normal = new Vector2(
                -direction.y,
                direction.x
            ) * thickness * 0.5f;

            int index = vh.currentVertCount;

            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = color;

            vertex.position = start + normal;
            vh.AddVert(vertex);

            vertex.position = start - normal;
            vh.AddVert(vertex);

            vertex.position = end - normal;
            vh.AddVert(vertex);

            vertex.position = end + normal;
            vh.AddVert(vertex);

            vh.AddTriangle(index, index + 1, index + 2);
            vh.AddTriangle(index, index + 2, index + 3);
        }
    }
}