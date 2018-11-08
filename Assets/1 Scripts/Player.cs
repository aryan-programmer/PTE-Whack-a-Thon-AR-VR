using UnityEngine;
public
class Player : Singleton<Player>
{

	void Update( )
	{

		if(GvrPointerInputModule.Pointer.TriggerDown || Input.GetKeyDown( KeyCode.X ))
		{

			if(Physics.Raycast( transform.position , transform.forward , out RaycastHit hit ))
			{

				if(hit.transform.GetComponent<Mole>() is Mole mole)
				{

					if(!mole.Hiding)
					{
						Hammer.I.Hit( mole.transform.position );
						mole.OnHit();

					}

				}

			}

		}

	}

}
