using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseReader : MonoBehaviour
{
    [SerializeField] private InputActionReference shootInput;

    private void Start()
    {
        shootInput.action.Enable();
        shootInput.action.performed += SendBallHitInput;
    }
    
    private void SendBallHitInput(InputAction.CallbackContext ctx) => ExecuteEvents.Execute<IGameEventMessageTarget>
    (gameObject, null, (handler, data) => handler.OnBallHitInput());

    private void OnDestroy()
    {
        shootInput.action.Disable();
        shootInput.action.performed -= SendBallHitInput;
    }

    public static Vector3? CastMouseRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        int UILayer = LayerMask.NameToLayer("UI");
        List<RaycastResult> UIRaycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current){position = Input.mousePosition}, UIRaycastResults);
        
        if (Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, 
                out var hit, float.PositiveInfinity) && UIRaycastResults.All(x => x.gameObject.layer != UILayer))
        {
            return hit.point;
        }
        else return null;
    }
}
