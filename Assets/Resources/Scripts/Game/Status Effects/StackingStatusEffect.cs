using UnityEngine;
using System.Collections;

public class StackingStatusEffect : StatusEffect
{
	// The number of stacks currently applied
	public int currentStacks;

	// The maximum number of stacks of this status that can be applied
	public int maxStacks;

	// The scheme to use when decaying stacks
	public StackType decayType;

	public StackingStatusEffect(float dur, Transform t) : base(dur, t)
	{
		maxStacks = 1;
		currentStacks = 1;
		decayType = StackType.communal;
	}

	// Add a new stack to this status
	public void addStack()
	{
		if (currentStacks == maxStacks)
			return;
		
		currentStacks++;
		apply ();
	}

	// Unique Stacking StatusEffect update w/ two decay schemes
	public override void update (float dec)
	{
		duration -= dec;
		if (duration <= 0f) 
		{
			if (decayType == StackType.communal) 
			{
				for(int i = 0; i < currentStacks; i++)
					revert ();
				statusList.Remove (this);
			}

			if (decayType == StackType.serial)
			{
				revert ();
				currentStacks--;
				duration = initDuration;
				if(currentStacks == 0)
					statusList.Remove (this);
			}
		}
	}

	// Required overrides
	public override void apply (){ }
	public override void revert (){ }

	// Create and pass a copy of this stacking Status
	public override StatusEffect Copy (Transform e)
	{
		return new StackingStatusEffect (initDuration, invoker);
	}
}

public enum StackType
{
	//all stacks will decay on duration end
	communal, 

	//stacks will decay one at a time on duration end
	serial
}