#pragma once

#include "LScapeRandomGenerator.h"
#include "LScapeHeightmap.h"

namespace LScape
{
	/** Abstract base class for various TerrainGeneratorGenerator types. */
	class TerrainGenerator
	{
	public:
		/** Deletes a TerrainGenerator. */
		virtual ~TerrainGenerator() {}

		/** Get height data from the TerrainGenerator. The memory is automatically allocated
		  * using C++'s new construct. */
		Heightmap* getHeightmap(const unsigned int width, const unsigned int height,
			const unsigned int offsetX, const unsigned int offsetY);

		/** Get height data from TerrainGenerator and write it to the given Heightmap object.
		  * The height and width are taken from the input parameter heightmap. */
		virtual void getHeightmap(Heightmap* heightmap, const unsigned int offsetX,
			const unsigned int offsetY) = 0;
	};
}