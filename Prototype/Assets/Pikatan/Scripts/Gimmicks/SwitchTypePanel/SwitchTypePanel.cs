using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshModifier))]
public class SwitchTypePanel : MonoBehaviour
{
    private enum PanelVisibleState
    { 
        Visible,
        Invisible
    }

    [SerializeField]
    private bool isDefaultVisible = false;
    [SerializeField]
    private SwitchTypePanel switchPanel;

    private PanelVisibleState visibleState = PanelVisibleState.Visible;
    private NavMeshModifier navMeshMod;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        navMeshMod = GetComponent<NavMeshModifier>();
        if (!isDefaultVisible)
        {
            OnStateChange(PanelVisibleState.Invisible);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnStateChange(PanelVisibleState.Invisible);
            switchPanel.OnStateChange(PanelVisibleState.Visible);
        }
    }

    private void OnStateChange(PanelVisibleState newState)
    {
        visibleState = newState;
        if(visibleState == PanelVisibleState.Visible)
        {
            navMeshMod.ignoreFromBuild = false;
            Color color = mat.color;
            color.a = 1.0f;
            mat.color = color;
        }
        else if(visibleState == PanelVisibleState.Invisible)
        {
            navMeshMod.ignoreFromBuild = true;
            Color color = mat.color;
            color.a = 0.5f;
            mat.color = color;
        }
        Singleton<NavMeshBaker>.Instance.Bake();
    }
}
