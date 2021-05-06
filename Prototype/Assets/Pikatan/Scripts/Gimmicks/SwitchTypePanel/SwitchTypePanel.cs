using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private MeshRenderer meshRenderer;
    private PanelVisibleState visibleState = PanelVisibleState.Visible;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(!isDefaultVisible)
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
            meshRenderer.enabled = true;
        }
        else if(visibleState == PanelVisibleState.Invisible)
        {
            meshRenderer.enabled = false;
        }
        Singleton<NavMeshBaker>.Instance.Bake();
    }
}
