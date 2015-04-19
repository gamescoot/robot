using UnityEngine;
using System.Collections;

public interface ICharacter{

	void Die();
	void SetGrounded(bool grounded);
	void ApplyDamage(float damage);
	int GetDirection();
	string GetTag();
	Vector3 GetPosition();
}