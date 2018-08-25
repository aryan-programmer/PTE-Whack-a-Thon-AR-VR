using UnityEngine;
using Utilities.Extensions;

public class Player : Singleton<Player>
{
	// Update is called once per frame
	void Update( )
	{
		if ( Physics.Raycast( transform.position , transform.forward , out RaycastHit hit ) )
		{
			Mole mole;
			if ( mole = hit.transform.GetComponent<Mole>() )
			{
				if ( !mole.Hiding )
				{
					if ( GvrPointerInputModule.Pointer.TriggerDown ||
						Input.GetKeyDown( KeyCode.X ) )
					{
						mole.OnHit();
						Hammer.I.Hit( mole.transform.position );
					}
					mole.Get<UnityEngine.EventSystems.EventTrigger>().
						enabled = true;
				}
				else
					mole.Get<UnityEngine.EventSystems.EventTrigger>().
						enabled = false;
			}
		}
	}
}
