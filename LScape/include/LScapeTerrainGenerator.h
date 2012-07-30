#pragma once

#include "LScapeRandomGenerator.h"
#include "LScapeHeightData.h"

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
		HeightData* getHeightData(const unsigned int width, const unsigned int height,
			double offsetX, double offsetY, double scale);

		/** Get height data from TerrainGenerator and write it to the given HeightData object.
		  * The height and width are taken from the input parameter heightmap. */
		virtual void getHeightData(HeightData* heightdata, double offsetX, double offsetY,
			double scale) = 0;

		/** Interpolate between two double values using cosine interpolation. */
		static double interpolate(double first, double second, double ratio);
	};
}