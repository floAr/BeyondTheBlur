#include "LScapeXorshiftRandomGenerator.h"

using namespace LScape;

XorshiftRandomGenerator::XorshiftRandomGenerator(unsigned int seed)
{
	// The seed can be used as-is and needs no processing
	mSeed = seed;
	reset();
}

unsigned int XorshiftRandomGenerator::random()
{
	// Shift the state somewhat
	mState ^= mState << 13;
	mState ^= mState >> 17;
	mState ^= mState << 5;

	// Save new position
	++mPos;

	return mState;
}

void XorshiftRandomGenerator::reset()
{
	// Reset to seed
	mState = mSeed;

	// Reset position
	mPos = 0;
}

void XorshiftRandomGenerator::skipTo(unsigned int pos)
{
	// Reset if we passed the position
	if (mPos>pos)
		reset();

	// Generate random numbers until we're done
	while (mPos<pos)
		random();
}