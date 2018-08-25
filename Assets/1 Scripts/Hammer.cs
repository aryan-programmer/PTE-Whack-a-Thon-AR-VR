using UnityEngine;

public class Hammer : Singleton<Hammer>
{
	public float speed;
	private Vector3 initialPosition;
	void Start( ) => initialPosition = transform.position;

	public void Hit( Vector3 targetPosition ) => 
		LeanTween.move( 
			gameObject , 
			targetPosition + new Vector3( 0 , 1.0625f , 0 ) , 
			0.01f );

	void Update( ) => 
		transform.position = Vector3.Lerp( 
			transform.position , 
			initialPosition , 
			Time.deltaTime * speed );
}