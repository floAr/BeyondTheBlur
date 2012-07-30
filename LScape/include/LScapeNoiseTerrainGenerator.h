#pragma once

#include "LScapeTerrainGenerator.h"

/** \todo comment like crazy! */

namespace LScape
{
	class RandomGenerator;

	class NoiseTerrainGenerator : public TerrainGenerator
	{
	public:
		/** Creates a noise terrain with the given seed. */
		NoiseTerrainGenerator(unsigned int seed);

		/** Get height data from NoiseTerrainGenerator and write it to the given HeightData object.
		  * The height and width are taken from the input parameter heightmap. */
		virtual void getHeightData(HeightData* heightdata, double offsetX, double offsetY,
			double scale);
	private:
		/** Each integer world coordinate has a random value. This function writes all random
		  * values in the specified rectangle to the buffer. */
		virtual void getRandomData(double* buffer, unsigned int offsetX, unsigned int offsetY,
			unsigned int width, unsigned int height);

		/** The random generator used to generate the noise. */
		RandomGenerator* mRandomGenerator;
	};
}