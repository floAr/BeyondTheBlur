#pragma once

#include <stdlib.h>

namespace LScape
{
	/** Abstract base class for a Random Number Generator (RNG).
	  * Each RNG has to provide a way to intialise it with a seed, generate a
	  * random integer value and skip random values. */
	class RandomGenerator
	{
	public:
		/** Destructor */
		virtual ~RandomGenerator() {}

		/** Return a random integer number. */
		virtual unsigned int random() = 0;

		/** Reset the RandomGenerator to its seed. */
		virtual void reset() = 0;

		/** Skip to a specific random value position. */
		virtual void skipTo(unsigned int pos) = 0;

	protected:
		/** The seed used to initialise the RNG. */
		unsigned int mSeed;

		/** The current random value position. */
		unsigned int mPos;
	};
}