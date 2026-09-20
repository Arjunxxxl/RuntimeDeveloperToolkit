using UnityEngine;
using UnityEngine.UI;

namespace RuntimePerformanceMonitor
{
    public class Graph : Graphic
    {
        [SerializeField] private float lineThickness = 2f;

        private FPSSnapshot[] values;

        private int historySize;
        private int writeIndex;
        private int snapshotCount;

        private float minValue;
        private float maxValue;

        private bool showFPS;
        private bool showFrameTime;

        #region Public API

        public void SetData(
            FPSSnapshot[] snapshotHistory,
            int _historySize,
            int _writeIndex,
            int _snapshotCount,
            bool _showFPS,
            bool _showFrameTime,
            float minVal,
            float maxVal)
        {
            values = snapshotHistory;

            historySize = _historySize;
            writeIndex = _writeIndex;
            snapshotCount = Mathf.Clamp(
                _snapshotCount,
                0,
                _historySize
            );

            showFPS = _showFPS;
            showFrameTime = _showFrameTime;

            if (maxVal <= minVal)
            {
                maxVal = minVal + 1f;
            }

            minValue = minVal;
            maxValue = maxVal;

            SetVerticesDirty();
        }

        #endregion

        #region Mesh Generation

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            if (!IsDataValid())
                return;

            Rect rect = rectTransform.rect;

            float width = rect.width;
            float height = rect.height;

            Vector2 halfSize = rect.size * 0.5f;

            // Draw across the complete history.
            // This ensures that the graph always occupies the
            // full width of the UI element.
            int pointCount = historySize;

            if (pointCount < 2)
                return;

            for (int i = 0; i < pointCount - 1; i++)
            {
                // i = 0 represents the newest point on the right.
                // i = historySize - 1 represents the oldest point
                // on the left.
                //
                // If a point has not been calculated yet, its value
                // is replaced with the oldest available snapshot.

                float currentValue = GetGraphValue(i);
                float previousValue = GetGraphValue(i + 1);

                float x1 = Mathf.Lerp(
                    width,
                    0f,
                    (float)i / (pointCount - 1)
                );

                float x2 = Mathf.Lerp(
                    width,
                    0f,
                    (float)(i + 1) / (pointCount - 1)
                );

                float y1 = NormalizeValue(currentValue) * height;
                float y2 = NormalizeValue(previousValue) * height;

                Vector2 p1 = new Vector2(
                    x1 - halfSize.x,
                    y1 - halfSize.y
                );

                Vector2 p2 = new Vector2(
                    x2 - halfSize.x,
                    y2 - halfSize.y
                );

                AddLine(vh, p1, p2, lineThickness);
            }
        }

        #endregion

        #region Graph Data

        private float GetGraphValue(int offsetFromNewest)
        {
            // No samples have been calculated yet.
            // Keep the entire graph at the minimum value.
            if (snapshotCount <= 0)
            {
                return minValue;
            }

            // Uncalculated points stay flat at the minimum value.
            if (offsetFromNewest >= snapshotCount)
            {
                return minValue;
            }

            FPSSnapshot snapshot = GetSnapshotFromNewest(offsetFromNewest);

            return GetSnapshotValue(snapshot);
        }

        private FPSSnapshot GetSnapshotFromNewest(int offset)
        {
            // writeIndex points to the next position to be written.
            //
            // writeIndex - 1 = newest snapshot
            // writeIndex - 2 = second newest snapshot
            //
            // offset is clamped by GetGraphValue(), so it never
            // accesses an uncalculated snapshot.

            int index = writeIndex - 1 - offset;

            // Handle circular buffer wrapping.
            index %= historySize;

            if (index < 0)
            {
                index += historySize;
            }

            return values[index];
        }

        private float GetSnapshotValue(FPSSnapshot snapshot)
        {
            if (showFPS && !showFrameTime)
            {
                return snapshot.FPS;
            }

            if (showFrameTime && !showFPS)
            {
                // FrameTime is stored in seconds.
                // Convert to milliseconds for the graph.
                return snapshot.FrameTime * 1000f;
            }

            return 0f;
        }

        private float NormalizeValue(float value)
        {
            if (Mathf.Approximately(maxValue, minValue))
            {
                return 0.5f;
            }

            return Mathf.Clamp01(
                Mathf.InverseLerp(
                    minValue,
                    maxValue,
                    value
                )
            );
        }

        #endregion

        #region Validation

        private bool IsDataValid()
        {
            if (values == null)
                return false;

            if (historySize < 2)
                return false;

            if (snapshotCount <= 0)
                return false;

            if (snapshotCount > historySize)
                return false;

            if (values.Length < historySize)
                return false;

            if (!showFPS && !showFrameTime)
                return false;

            if (showFPS && showFrameTime)
                return false;

            return true;
        }

        #endregion

        #region Line Rendering

        private void AddLine(
            VertexHelper vh,
            Vector2 start,
            Vector2 end,
            float thickness)
        {
            Vector2 direction = end - start;

            if (direction.sqrMagnitude < 0.000001f)
            {
                direction = Vector2.right;
            }
            else
            {
                direction.Normalize();
            }

            Vector2 normal = new Vector2(
                -direction.y,
                direction.x
            ) * (thickness * 0.5f);

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

            vh.AddTriangle(
                index,
                index + 1,
                index + 2
            );

            vh.AddTriangle(
                index,
                index + 2,
                index + 3
            );
        }

        #endregion
    }
}