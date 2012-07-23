#include "LScapeXorshiftRandomGenerator.h"

using namespace LScape;

void XorshiftRandomGenerator::initialise(unsigned int seed)
{
	// The seed can be used as-is and needs no processing
	mSeed = seed;
	mState = seed;
}

int XorshiftRandomGenerator::random()
{
	// Shift the state somewhat
	mState ^= mState << 13;
	mState ^= mState >> 17;
	mState ^= mState << 5;

	return mState;
}

void XorshiftRandomGenerator::reset()
{
	// Reset to seed
	mState = mSeed;
}

void XorshiftRandomGenerator::skip(unsigned int count)
{
	// Just generate random numbers until we're done
	for (unsigned int i=0; i<count; ++i) random();
}