#include "LScapeTerrainGenerator.h"
#include "LScapeHeightData.h"
#define _USE_MATH_DEFINES
#include <math.h>

using namespace LScape;

HeightData* TerrainGenerator::getHeightData(const unsigned int width, const unsigned int height,
			double offsetX, double offsetY,	double scale)
{
	// Create Heightmap object
	HeightData* result = new HeightData(width, height);
	// Calculate the desired height data
	getHeightData(result, offsetX, offsetY, scale);

	return result;
}

double TerrainGenerator::interpolate(double first, double second, double ratio)
{
	// cosine interpolation, comment out for linear
	ratio = (1 - cos(ratio * M_PI)) * 0.5;

	// return interpolated value
	return (1-ratio)*first + ratio*second;
}