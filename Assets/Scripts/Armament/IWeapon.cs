public enum WeaponType
{
	Turret,
	Launcher,
	Thrower,
	Any,
}

public interface IWeapon
{
	WeaponType Type { get; }

	void Active( bool isActive );
	void SetDir( bool isRight );
	void IsPlayerDriving( bool playerDriver );
	void TryToFire( );
	void SwapModel( string mechName );

	UnityEngine.GameObject GetGameObject( );
}
