#pragma once

#include <stdlib.h>

namespace LScape
{
	/** Abstract base class for a Random Number Generator (RNG).
	  * Each RNG has to provide a way to intialise it with a seed, generate a
	  * random integer value and skip a number of random values. */
	class RandomGenerator
	{
	public:
		/** Destructor */
		virtual ~RandomGenerator() {}

		/** Initialise the RandomGenerator with a given seed. You have to set mSeed to a
		  * random seed yourself. */
		virtual void initialise(unsigned int seed) = 0;

		/** Return a random integer number. */
		virtual int random() = 0;

		/** Reset the RandomGenerator to its seed. */
		virtual void reset() = 0;

		/** Skip ahead any number of random values. */
		virtual void skip(unsigned int count) = 0;

	protected:
		/** The seed used to initialise the RNG. */
		unsigned int mSeed;
	};
}