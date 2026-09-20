using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RuntimePerformanceMonitor
{
    public class StressTestManager : MonoBehaviour
    {
        [Header("Stress Test")]

        [SerializeField]
        private bool playOnStart = false;

        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private Transform spawnParent;

        [SerializeField]
        private int maxInstances = 100;

        [SerializeField]
        private float spawnInterval = 0.1f;

        [SerializeField]
        private float objectLifetime = 5f;

        [Header("Spawn Box")]

        [SerializeField]
        private Vector3 boxCenter = Vector3.zero;

        [SerializeField]
        private Vector3 boxSize = new Vector3(10f, 5f, 10f);

        [Header("Spawn Settings")]

        [SerializeField]
        private bool randomizeRotation = false;

        [SerializeField]
        private bool randomizeScale = false;

        [SerializeField]
        private Vector2 scaleRange = new Vector2(0.8f, 1.2f);

        private readonly List<GameObject> activeInstances = new();

        private Coroutine spawnCoroutine;

        private bool isRunning;

        #region Unity Lifecycle

        private void Start()
        {
            if (playOnStart)
            {
                StartStressTest();
            }
        }

        private void OnDisable()
        {
            StopStressTest();
        }

        #endregion

        #region Public API

        public void StartStressTest()
        {
            if (isRunning)
            {
                return;
            }

            if (!ValidateSettings())
            {
                return;
            }

            isRunning = true;

            spawnCoroutine = StartCoroutine(SpawnRoutine());
        }

        public void StopStressTest()
        {
            if (!isRunning)
            {
                return;
            }

            isRunning = false;

            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }

        public void ClearInstances()
        {
            for (int i = activeInstances.Count - 1; i >= 0; i--)
            {
                GameObject instance = activeInstances[i];

                if (instance != null)
                {
                    Destroy(instance);
                }
            }

            activeInstances.Clear();
        }

        public void StopAndClear()
        {
            StopStressTest();
            ClearInstances();
        }

        public int GetActiveInstanceCount()
        {
            CleanupDestroyedInstances();

            return activeInstances.Count;
        }

        public bool IsRunning()
        {
            return isRunning;
        }

        #endregion

        #region Spawn Logic

        private IEnumerator SpawnRoutine()
        {
            while (isRunning)
            {
                CleanupDestroyedInstances();

                if (activeInstances.Count < maxInstances)
                {
                    SpawnObject();
                }

                if (spawnInterval <= 0f)
                {
                    yield return null;
                }
                else
                {
                    yield return new WaitForSecondsRealtime(
                        spawnInterval
                    );
                }
            }

            spawnCoroutine = null;
        }

        private void SpawnObject()
        {
            if (prefab == null)
            {
                return;
            }

            Vector3 spawnPosition = GetRandomSpawnPosition();

            Quaternion spawnRotation = randomizeRotation
                ? Random.rotation
                : prefab.transform.rotation;

            GameObject instance = Instantiate(
                prefab,
                spawnPosition,
                spawnRotation,
                spawnParent
            );

            if (randomizeScale)
            {
                ApplyRandomScale(instance);
            }

            activeInstances.Add(instance);

            if (objectLifetime > 0f)
            {
                Destroy(instance, objectLifetime);
            }
        }

        #endregion

        #region Spawn Position

        private Vector3 GetRandomSpawnPosition()
        {
            Vector3 halfSize = boxSize * 0.5f;

            Vector3 localPosition = new Vector3(
                Random.Range(-halfSize.x, halfSize.x),
                Random.Range(-halfSize.y, halfSize.y),
                Random.Range(-halfSize.z, halfSize.z)
            );

            // The box is local to this manager's transform.
            return transform.TransformPoint(
                boxCenter + localPosition
            );
        }

        #endregion

        #region Random Scale

        private void ApplyRandomScale(GameObject instance)
        {
            float minScale = Mathf.Min(
                scaleRange.x,
                scaleRange.y
            );

            float maxScale = Mathf.Max(
                scaleRange.x,
                scaleRange.y
            );

            float scale = Random.Range(
                minScale,
                maxScale
            );

            instance.transform.localScale =
                prefab.transform.localScale * scale;
        }

        #endregion

        #region Cleanup

        private void CleanupDestroyedInstances()
        {
            for (int i = activeInstances.Count - 1; i >= 0; i--)
            {
                if (activeInstances[i] == null)
                {
                    activeInstances.RemoveAt(i);
                }
            }
        }

        #endregion

        #region Validation

        private bool ValidateSettings()
        {
            if (prefab == null)
            {
                Debug.LogError(
                    $"{nameof(StressTestManager)}: " +
                    "Prefab is not assigned.",
                    this
                );

                return false;
            }

            if (maxInstances <= 0)
            {
                Debug.LogError(
                    $"{nameof(StressTestManager)}: " +
                    "Max instances must be greater than zero.",
                    this
                );

                return false;
            }

            if (boxSize.x < 0f ||
                boxSize.y < 0f ||
                boxSize.z < 0f)
            {
                Debug.LogError(
                    $"{nameof(StressTestManager)}: " +
                    "Box size cannot contain negative values.",
                    this
                );

                return false;
            }

            return true;
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmosSelected()
        {
            Matrix4x4 previousMatrix = Gizmos.matrix;

            Gizmos.matrix = transform.localToWorldMatrix;

            Gizmos.color = new Color(
                1f,
                0.5f,
                0f,
                0.35f
            );

            Gizmos.DrawWireCube(
                boxCenter,
                boxSize
            );

            Gizmos.matrix = previousMatrix;
        }

        #endregion
    }
}