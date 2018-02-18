using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff {

	public int durationLeft;
	public ItemModifier modifier;

	
	public bool UpdateCooldown() {
		durationLeft--;
		return durationLeft > 0;
	}

}
