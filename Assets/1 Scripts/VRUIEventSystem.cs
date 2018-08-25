using UnityEngine;

namespace Utilities.VRUI
{
	[RequireComponent( 
		typeof( UnityEngine.EventSystems.EventSystem ) , 
		typeof( GvrPointerInputModule ) )]
	public class VRUIEventSystem : MonoBehaviour
	{
		void Update( )
		{
			if (
				GvrPointerInputModule.Pointer.TriggerDown ||
				Input.GetKeyDown( KeyCode.X ) )
				if ( Physics.Raycast(
					  origin: Camera.main.transform.position ,
					  direction: Camera.main.transform.forward ,
					  hitInfo: out RaycastHit hit ) )
					hit.collider.gameObject.
						GetComponent<VRUIButton>()?.OnButtonClicked?.Invoke();
		}
	}
}
