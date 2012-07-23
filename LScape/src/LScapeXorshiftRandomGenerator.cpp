#include "LScapeXorshiftRandomGenerator.h"

using namespace LScape;

void XorshiftRandomGenerator::initialise(unsigned int seed)
{
	// The seed can be used as-is and needs no processing
	mSeed = seed;
	reset();
}

int XorshiftRandomGenerator::random()
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

void XorshiftRandomGenerator::skip(unsigned int count)
{
	// Just generate random numbers until we're done
	for (unsigned int i=0; i<count; ++i)
		random();
}

void XorshiftRandomGenerator::skipTo(unsigned int pos)
{
	// Reset if we passed the position
	if (pos>mPos)
		reset();

	// Generate random numbers until we're done
	while (mPos<pos)
		random();
}