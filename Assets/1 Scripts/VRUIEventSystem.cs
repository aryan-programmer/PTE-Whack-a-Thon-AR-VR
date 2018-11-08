using UnityEngine;
namespace Utilities.VRUI
{
	[RequireComponent( typeof( UnityEngine.EventSystems.EventSystem ) , typeof( GvrPointerInputModule ) )]
	public
	class VRUIEventSystem : MonoBehaviour
	{

		void Update( )
		{

			if((GvrPointerInputModule.Pointer.TriggerDown || Input.GetKeyDown( KeyCode.X )) &&
			Physics.Raycast( Camera.main.transform.position , Camera.main.transform.forward , out RaycastHit hit ))
			{
				hit.collider.gameObject.GetComponent<VRUIButton>()?.OnButtonClicked?.Invoke();

			}

		}

	}

}
