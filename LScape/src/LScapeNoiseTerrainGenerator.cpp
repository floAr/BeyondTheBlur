#include "LScapeNoiseTerrainGenerator.h"
#include "LScapeXorshiftRandomGenerator.h"
#include <iostream>

using namespace LScape;

NoiseTerrainGenerator::NoiseTerrainGenerator(unsigned int seed)
{
	mRandomGenerator = new XorshiftRandomGenerator(seed);
}

void NoiseTerrainGenerator::getHeightData(HeightData* heightdata, double offsetX,
	double offsetY,	double scale)
{
	// get dimensions
	unsigned int height = heightdata->getHeight();
	unsigned int width = heightdata->getWidth();

	// get all required world data, namely a random
	// value at each integer world coordinate
	unsigned int randWidth = (unsigned int) (width/scale)+2;
	unsigned int randHeight = (unsigned int) (height/scale)+2;
	double* random = (double*)malloc(sizeof(double) * randWidth * randHeight);
	getRandomData(random, (unsigned int) (offsetX/scale), (unsigned int) (offsetY/scale),
		randWidth, randHeight);

	// fill heightmap data
	for (unsigned int y=0; y<height; ++y)
	{
		// get integer world y coordinate
		unsigned int iY = (unsigned int) (y/scale);
		// get ratio between iY and iY+1
		double ratioY = (y/scale)-iY;

		for (unsigned int x=0; x<width; ++x)
		{
			// get integer world x coordinate
			unsigned int iX = (unsigned int) (x/scale);
			// get ratio between iX and iX+1
			double ratioX = (x/scale)-iX;

			// interpolate
			double left  = interpolate(random[iY*randWidth+iX],   random[(iY+1)*randWidth+iX],   ratioY);
			double right = interpolate(random[iY*randWidth+iX+1], random[(iY+1)*randWidth+iX+1], ratioY);
			heightdata->setData(x, y, interpolate(left, right, ratioX));
		}
	}

	// free memory
	free(random);
}

void NoiseTerrainGenerator::getRandomData(double* buffer, unsigned int offsetX, unsigned int offsetY,
			unsigned int width, unsigned int height)
{
	for (unsigned int y=0; y<height; ++y)
	{
		for (unsigned int x=0; x<width; ++x)
		{
			// \bug for now, we just suppose single 512² chunk
			// \todo get data in correct order
			mRandomGenerator->skipTo((unsigned int)(y*512+x));
			buffer[y*width+x] = (mRandomGenerator->random() % 65536) / 65535.0;
		}
	}
}