using Utilities.Timers;
using HoloToolkit.MRDL.PeriodicTable;
using UnityEngine;
using Utilities.Extensions;

public class Mole : MonoBehaviour
{
	public float visibleHeight = 2,
				 hiddenHeight = -0.3f,
				 speed = 3,
				 dissapperDuration = 1,
				 probabiltyOfSpecialMole = 1,
				 valueMultiplier = 1;
	public int MainMatIndex;
	public bool Hiding;
	public Renderer rend;
	public Material GoldenMaterial,
					BadMaterial,
					OrigMaterial;
	public AudioClip hitClip;
	public Element elementObj;

	bool isGolden, isEvil;
	Vector3 targetPos;
	Timer timer;
	bool isTimerStarted = false;

	// Use this for initialization
	void Start( )
	{
		if ( rend == null )
			rend = this.Get<Renderer>();
		timer = new Timer( 2 , ( ) =>
		{
			elementObj.gameObject.SetActive( false );
			isTimerStarted = false;
		} );
		targetPos = transform.localPosition;
		targetPos.y = hiddenHeight;
		elementObj.gameObject.SetActive( false );
	}

	// Update is called once per frame
	void Update( )
	{
		transform.localPosition =
			Vector3.Lerp(
				transform.localPosition ,
				targetPos ,
				Time.deltaTime * speed );
		if (
			Mathf.FloorToInt( transform.localPosition.y ) ==
			Mathf.FloorToInt( hiddenHeight ) )
			Hiding = true;
		else Hiding = false;
		if ( isTimerStarted ) timer.OnUpdate();
	}

	public void Rise( )
	{
		if ( !Hiding || elementObj.isActiveAndEnabled ) return;

		if ( Random.value < probabiltyOfSpecialMole )
		{
			if ( Random.Range( 0 , 3 ) == 1 )
			{
				Material[] mats = rend.materials;
				mats[ MainMatIndex ] = GoldenMaterial;
				rend.materials = mats;
				isGolden = true;
				isEvil = false;
			}
			else
			{
				Material[] mats = rend.materials;
				mats[ MainMatIndex ] = BadMaterial;
				rend.materials = mats;
				isGolden = false;
				isEvil = true;
			}
		}
		else
		{
			Material[] mats = rend.materials;
			mats[ MainMatIndex ] = OrigMaterial;
			rend.materials = mats;
			isGolden = false;
			isEvil = false;
		}
		targetPos.y = visibleHeight;
		Invoke( "Hide" , dissapperDuration );
	}

	public void OnHit( )
	{
		if ( !Hiding )
		{
			this.GetAdd<AudioSource>().PlayOneShot( hitClip );
			if ( isGolden )
				FindObjectOfType<GameManager>().
					IncreaseScore( Mathf.RoundToInt( 3 * valueMultiplier ) );
			else if ( isEvil )
				FindObjectOfType<GameManager>().
					DecreaseScore( Mathf.RoundToInt( 15 * valueMultiplier ) );
			else
				FindObjectOfType<GameManager>().
					IncreaseScore( Mathf.RoundToInt( valueMultiplier ) );
			elementObj.gameObject.SetActive( true );
			ElementHandler.I.SetRandomElement( elementObj );
			isTimerStarted = true;
			WindowsVoice.Speak( 
				$"{elementObj.data.name} {elementObj.data.number}." );
			Hide();
		}
	}

	public void Hide( )
	{
		Material[] mats = rend.materials;
		mats[ MainMatIndex ] = OrigMaterial;
		rend.materials = mats;
		targetPos.y = hiddenHeight;
	}
}
