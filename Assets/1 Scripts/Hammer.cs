using UnityEngine;
public
class Hammer : Singleton<Hammer>
{
	#pragma warning disable 0649
	[SerializeField] float speed;
	#pragma warning restore 0649
	Vector3 initialPosition;
	void Start( )
	{
		initialPosition = transform.position;

	}
	public
	void Hit( Vector3 targetPosition )
	{
		LeanTween.move( gameObject , targetPosition + new Vector3( 0 , 1.0625f , 0 ) , 0.01f );

	}
	void Update( )
	{
		transform.position = Vector3.Lerp( transform.position , initialPosition , Time.deltaTime * speed );

	}

}
