<QC>
Using UnityEngine

Public <Class> Hammer : Singleton[[Hammer]]
#pragma warning disable 0649 _
	[SerializeField] Float speed
#pragma warning restore 0649 _

	Vector3 initialPosition
	<Function> Start( )
		initialPosition = transform.position
	</>

	Public <Function> Hit( Vector3 targetPosition )
		LeanTween.move( gameObject , targetPosition + new Vector3( 0 , 1.0625f , 0 ) , 0.01f )
	</>

	<Function> Update( )
		transform.position = Vector3.Lerp( transform.position , initialPosition , Time.deltaTime * speed )
	</>
</>
</>