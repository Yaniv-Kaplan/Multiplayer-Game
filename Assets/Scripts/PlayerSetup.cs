using UnityEngine.Networking;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] m_componentsToDisable;

    Camera m_sceneCamera;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            foreach(Behaviour b in m_componentsToDisable)
            {
                b.enabled = false;
            }
        }
        else
        {
            m_sceneCamera = Camera.main;
            if(m_sceneCamera != null)
            {
                m_sceneCamera.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if(m_sceneCamera != null)
        {
            m_sceneCamera.gameObject.SetActive(true);
        }
    }
}
