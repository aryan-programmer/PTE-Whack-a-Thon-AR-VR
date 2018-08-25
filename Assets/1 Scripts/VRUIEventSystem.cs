using UnityEngine;

namespace Utilities.VRUI
{
	[RequireComponent( typeof( UnityEngine.EventSystems.EventSystem ) )]
	[RequireComponent( typeof( GvrPointerInputModule ) )]
	public class VRUIEventSystem : MonoBehaviour
	{
		void Update( )
		{
			if ( Physics.Raycast(
				origin: Camera.main.transform.position ,
				direction: Camera.main.transform.forward ,
				hitInfo: out RaycastHit hit ) )
			{
				VRUIButton button;
				if ( button = hit.collider.gameObject.GetComponent<VRUIButton>() )
				{
					if ( 
						GvrPointerInputModule.Pointer.TriggerDown || 
						Input.GetKeyDown( KeyCode.X ) )
					{
						button.TriggerAllEvents();
					}
				}
			}
		}
	}
}
