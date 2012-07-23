#pragma once

#include "LScapeRandomGenerator.h"

namespace LScape
{
	/** RNG implementing the Xorshift algorithm by George Marsaglia.
	  * \todo Describe what the algorithm is suitable for. */
	class XorshiftRandomGenerator : public RandomGenerator
	{
	public:
		/** Constructor, initialising the RandomGenerator with a given seed. */
		XorshiftRandomGenerator(unsigned int seed) { initialise(seed); }

		/** Return a random integer number. */
		virtual int random();

		/** Reset the RandomGenerator to its seed. */
		virtual void reset();

		/** Skip ahead any number of random values. */
		virtual void skip(unsigned int count);

		/** Skip to a specific random value position. */
		virtual void skipTo(unsigned int pos);

	protected:
		/** Initialise the RandomGenerator with a given seed. */
		virtual void initialise(unsigned int seed);

		/** Curred state of the Xorshift algorithm (last given random number). */
		unsigned int mState;
	};
}