#include "LScapeHeightmap.h"
#include <stdlib.h>

using namespace LScape;

Heightmap::Heightmap(unsigned int width, unsigned int height)
{
	// Set dimensions
	mWidth = width;
	mHeight = height;

	// Allocate memory
	mHeightData = (double*) malloc(mWidth * mHeight * sizeof(double));
}

Heightmap::~Heightmap()
{
	// Free space and set pointer to null
	free(mHeightData);
	mHeightData = 0;
}