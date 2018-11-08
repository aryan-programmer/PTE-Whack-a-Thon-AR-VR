using Utilities.Timers;
using HoloToolkit.MRDL.PeriodicTable;
using UnityEngine;
using Utilities.Extensions;
public
class Mole : MonoBehaviour
{
	#pragma warning disable 0649
	[SerializeField] float visibleHeight = 2, hiddenHeight = -0.3f, speed = 3, dissapperDuration = 1, probabiltyOfSpecialMole = 1, valueMultiplier = 1;
	[SerializeField] int MainMatIndex;
	[SerializeField] Renderer rend;
	[SerializeField] Material GoldenMaterial, BadMaterial, OrigMaterial;
	[SerializeField] AudioClip hitClip;
	[SerializeField] Element elementObj;
	#pragma warning restore 0649
	bool isGolden, isEvil, hiding;
	Vector3 targetPos;
	Timer timer;
	bool isTimerStarted = false;
	public
	bool Hiding
	{

		get
		{
			return hiding;


		}

	}
	public
	AudioSource SAudioSource
	{

		get
		{
			return __audioSource ?? ( __audioSource = this.GetAdd<AudioSource>() );


		}

	}
	AudioSource __audioSource;
	void Start( )
	{

		if(rend == null)
		{
			rend = this.Get<Renderer>();

		}
		timer = new Timer( 2 , delegate ( )
		{
		elementObj.gameObject.SetActive( isTimerStarted = false );
		} );
		targetPos = transform.localPosition;
		targetPos.y = hiddenHeight;
		elementObj.gameObject.SetActive( false );

	}
	void Update( )
	{
		transform.localPosition = Vector3.Lerp( transform.localPosition , targetPos , Time.deltaTime * speed );
		hiding = Mathf.FloorToInt( transform.localPosition.y ) == Mathf.FloorToInt( hiddenHeight );
		if(isTimerStarted)
		{
			timer.OnUpdate();

		}

	}
	public
	void Rise( )
	{

		if(!hiding || elementObj.isActiveAndEnabled)
		{
			return;

		}
		if(Random.value < probabiltyOfSpecialMole)
		{

			if(Random.Range( 0 , 3 ) == 1)
			{
				var mats = rend.materials;
				mats[ MainMatIndex ] = GoldenMaterial;
				rend.materials = mats;
				isGolden = true;
				isEvil = false;

			}
			else
			{
				var mats = rend.materials;
				mats[ MainMatIndex ] = BadMaterial;
				rend.materials = mats;
				isGolden = false;
				isEvil = true;

			}

		}
		else
		{
			var mats = rend.materials;
			mats[ MainMatIndex ] = OrigMaterial;
			rend.materials = mats;
			isGolden = false;
			isEvil = false;

		}
		targetPos.y = visibleHeight;
		Invoke(@"Hide"
		, dissapperDuration );

	}
	public
	void OnHit( )
	{

		if(!hiding)
		{
			SAudioSource.PlayOneShot( hitClip );
			if(isGolden)
			{
				GameManager.I.IncreaseScore( Mathf.RoundToInt( 3 * valueMultiplier ) );

			}
			else if(isEvil)
			{
				GameManager.I.DecreaseScore( Mathf.RoundToInt( 15 * valueMultiplier ) );

			}
			else
			{
				GameManager.I.IncreaseScore( Mathf.RoundToInt( valueMultiplier ) );


			}
			elementObj.gameObject.SetActive( true );
			ElementHandler.I.SetRandomElement( elementObj );
			isTimerStarted = true;
			WindowsVoice.Speak( $@"{elementObj.Data.name} {elementObj.Data.number}."
			);
			Hide();

		}

	}
	public
	void Hide( )
	{
		var mats = rend.materials;
		mats[ MainMatIndex ] = OrigMaterial;
		rend.materials = mats;
		isGolden = false;
		isEvil = false;
		targetPos.y = hiddenHeight;

	}

}
