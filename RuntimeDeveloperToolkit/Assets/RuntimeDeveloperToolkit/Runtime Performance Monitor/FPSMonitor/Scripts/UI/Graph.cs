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
            snapshotCount = _snapshotCount;

            showFPS = _showFPS;
            showFrameTime = _showFrameTime;

            // Prevent invalid ranges.
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

            int sampleCount = snapshotCount;

            // We draw from right to left.
            // i = 0 represents the newest sample.
            for (int i = 0; i < sampleCount - 1; i++)
            {
                FPSSnapshot currentSnapshot = GetSnapshotFromNewest(i);
                FPSSnapshot previousSnapshot = GetSnapshotFromNewest(i + 1);

                float currentValue = GetSnapshotValue(currentSnapshot);
                float previousValue = GetSnapshotValue(previousSnapshot);

                // Newest sample starts at the right.
                float x1 = Mathf.Lerp(
                    width,
                    0f,
                    (float)i / (sampleCount - 1)
                );

                float x2 = Mathf.Lerp(
                    width,
                    0f,
                    (float)(i + 1) / (sampleCount - 1)
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

        #region Snapshot Data

        private FPSSnapshot GetSnapshotFromNewest(int offset)
        {
            // writeIndex points to the next position that will be written.
            //
            // writeIndex - 1 = newest snapshot
            // writeIndex - 2 = second newest snapshot
            //
            // Wrap the index around the circular buffer.

            int index = writeIndex - 1 - offset;

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
                return snapshot.FrameTime * 1000f;
            }

            // Invalid or ambiguous selection.
            return 0f;
        }

        private float NormalizeValue(float value)
        {
            if (Mathf.Approximately(maxValue, minValue))
            {
                return 0.5f;
            }

            return Mathf.Clamp01(
                Mathf.InverseLerp(minValue, maxValue, value)
            );
        }

        #endregion

        #region Validation

        private bool IsDataValid()
        {
            if (values == null)
                return false;

            if (historySize <= 0)
                return false;

            if (snapshotCount < 2)
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

            // Avoid invalid normalization when both points overlap.
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

            vh.AddTriangle(index, index + 1, index + 2);
            vh.AddTriangle(index, index + 2, index + 3);
        }

        #endregion
    }
}