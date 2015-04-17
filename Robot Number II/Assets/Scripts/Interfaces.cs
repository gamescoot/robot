using UnityEngine;
using System.Collections;

public interface ICharacter{

	void SetGrounded(bool grounded);
	void ApplyDamage(float damage);
	int GetDirection();
}