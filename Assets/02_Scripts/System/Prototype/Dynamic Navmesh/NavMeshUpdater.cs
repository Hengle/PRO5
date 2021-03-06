using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshUpdater : MonoBehaviour
{
    public NavMeshSurface surface => GetComponent<NavMeshSurface>();
    Transform m_Tracked;
    // The size of the build bounds
    public Vector3 m_Size = new Vector3(80.0f, 20.0f, 80.0f);
    public NavMeshData m_NavMesh;
    AsyncOperation m_Operation;
    NavMeshDataInstance m_Instance;
    List<NavMeshBuildSource> m_Sources = new List<NavMeshBuildSource>();
    private void Start()
    {
        MyEventSystem.instance.onUpdateNavMesh += UpdateNavMeshAsync;
        if (surface.navMeshData == null)
            surface.BuildNavMesh();
        m_NavMesh = surface.navMeshData;

        UpdateNavMesh();
        StartCoroutine(RepeatUpdate());
    }

    void StartLoad()
    {

    }

    IEnumerator RepeatUpdate()
    {
        while (true)
        {
            UpdateNavMeshAsync();
            yield return m_Operation;
            yield return new WaitForSeconds(0.3f);
        }
    }

    void OnEnable()
    {
        GlobalEventSystem.instance.onLoadFinish += StartLoad;

        ScriptCollection.RegisterScript(this);
    }

    private void OnDisable()
    {
        ScriptCollection.RemoveScript(this);
        m_Instance.Remove();
        MyEventSystem.instance.onUpdateNavMesh -= UpdateNavMeshAsync;
        GlobalEventSystem.instance.onLoadFinish -= StartLoad;
    }

    void UpdateNavMeshAsync()
    {
        NavMeshSourceTag.Collect(ref m_Sources);
        var defaultBuildSettings = NavMesh.GetSettingsByID(0);
        var bounds = QuantizedBounds();
        m_Operation = NavMeshBuilder.UpdateNavMeshDataAsync(m_NavMesh, defaultBuildSettings, m_Sources, bounds);
        NavMesh.AddNavMeshData(m_NavMesh);
    }

    void UpdateNavMesh()
    {
        NavMeshSourceTag.Collect(ref m_Sources);
        var defaultBuildSettings = NavMesh.GetSettingsByID(0);
        var bounds = QuantizedBounds();

        NavMeshBuilder.UpdateNavMeshData(m_NavMesh, defaultBuildSettings, m_Sources, bounds);
        NavMesh.AddNavMeshData(m_NavMesh);
    }

    // IEnumerator UpdateNav(NavMeshBuildSettings defaultBuildSettings, Bounds bounds)
    // {
    //     yield return NavMeshBuilder.UpdateNavMeshDataAsync(m_NavMesh, defaultBuildSettings, m_Sources, bounds);
    // }

    static Vector3 Quantize(Vector3 v, Vector3 quant)
    {
        float x = quant.x * Mathf.Floor(v.x / quant.x);
        float y = quant.y * Mathf.Floor(v.y / quant.y);
        float z = quant.z * Mathf.Floor(v.z / quant.z);
        return new Vector3(x, y, z);
    }

    Bounds QuantizedBounds()
    {
        // Quantize the bounds to update only when theres a 10% change in size
        var center = m_Tracked ? m_Tracked.position : transform.position;
        return new Bounds(Quantize(center, 0.1f * m_Size), m_Size);
    }

    void OnDrawGizmosSelected()
    {
        if (m_NavMesh)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(m_NavMesh.sourceBounds.center, m_NavMesh.sourceBounds.size);
        }

        Gizmos.color = Color.yellow;
        var bounds = QuantizedBounds();
        Gizmos.DrawWireCube(bounds.center, bounds.size);

        Gizmos.color = Color.green;
        var center = m_Tracked ? m_Tracked.position : transform.position;
        Gizmos.DrawWireCube(center, m_Size);
    }
}
