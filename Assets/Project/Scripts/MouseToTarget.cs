using UnityEngine;
// Add "using UnityEngine.InputSystem;" if using the new Input System

public class MouseWorldPosition : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private GameObject obj;
    private Camera mainCam;

    void Start() => mainCam = Camera.main;

    void Update()
    {
        // Legacy Input: Input.mousePosition
        // New Input System: Mouse.current.position.ReadValue()
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, hitLayers))
        {
            Vector3 worldPos = hit.point;
            obj.transform.position = worldPos;
            // Use worldPos (e.g., move an indicator or spawn an object)
        }
    }
}