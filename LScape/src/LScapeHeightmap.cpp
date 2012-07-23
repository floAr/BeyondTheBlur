#include "LScapeHeightmap.h"
#include <stdlib.h>
#include <math.h>

using namespace LScape;

Heightmap::Heightmap(unsigned int width, unsigned int height, double* data)
{
	// Set dimensions
	mWidth = width;
	mHeight = height;

	// Check if a pointer to the data buffer has been passed
	if (data != 0)
	{
		// Use passed pointer
		mHeightData = data;
	}
	else
	{
		// Allocate memory
		mHeightData = (double*) malloc(mWidth * mHeight * sizeof(double));
	}
}

Heightmap::~Heightmap()
{
	// Free space and set pointer to null
	free(mHeightData);
	mHeightData = 0;
}

double Heightmap::getData(unsigned int x, unsigned int y)
{
	// Get height data from memory
	return mHeightData[y*mWidth+x];
}

void Heightmap::setData(unsigned int x, unsigned int y, double value)
{
	// Write height data to memory
	mHeightData[y*mWidth+x] = value;
}

unsigned char* Heightmap::getHeightMap(double clipMin, double clipMax, unsigned char* buffer)
{
	// Catch bad parameters
	if (clipMax <= clipMin)
		return 0;

	// Create buffer, if it hasn't been already
	if (buffer == 0)
		buffer = (unsigned char*) malloc(mWidth*mHeight*sizeof(unsigned char)*4);

	// Write pixels to memory
	for (unsigned int y=0; y<mHeight; ++y)
	{
		for (unsigned int x=0; x<mWidth; ++x)
		{
			// Save current RGBA starting position to pointer px
			unsigned char* px = buffer + (y*mWidth+x)*sizeof(unsigned char)*4;
			
			// Calculate brightness value
			// The positive 0.5 bias does the rounding, since a cast to char only cuts off decimal places
			unsigned char val = (unsigned char) (0.5 + (getData(x, y) - clipMin)*255/(clipMax-clipMin));

			// Clip the value to [0, 255]
			val = val <   0 ?   0 : val;
			val = val > 255 ? 255 : val;

			// Write data to memory
			px[0] = val; // R
			px[1] = val; // G
			px[2] = val; // B
			px[3] = 255; // A
		}
	}

	// Return a pointer to the Height Map
	return buffer;
}