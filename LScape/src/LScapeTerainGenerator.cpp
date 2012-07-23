#include "LScapeTerrainGenerator.h"
#include "LScapeHeightmap.h"

using namespace LScape;

Heightmap* TerrainGenerator::getHeightmap(const unsigned int width, const unsigned int height,
			const unsigned int offsetX, const unsigned int offsetY)
{
	// Create Heightmap object
	Heightmap* result = new Heightmap(width, height);
	// Calculate the desired height data
	getHeightmap(result, offsetX, offsetY);

	return result;
}