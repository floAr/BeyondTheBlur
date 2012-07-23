#pragma once

namespace LScape
{
	/** This class holds calculated height data. It can be used to generate
	  * a greyscale heigth map and light map and a RGB-colored normal map. */
	class Heightmap
	{
	public:
		/** Constructor, creates a Heightmap with the given width and height. */
		Heightmap(unsigned int width, unsigned int height);

		/** Destructor, frees allocated space. */
		~Heightmap();

		/** Get data from the specified position. */
		double getData(unsigned int x, unsigned int y);

		/** Set data at the specified position. */
		void setData(unsigned int x, unsigned int y, double value);

		/* \todo Get height map, light map and normal map */

	private:
		/** The width of the heightmap image. */
		unsigned int mWidth;

		/** The height of the heightmap image. */
		unsigned int mHeight;

		/** A pointer to the height data. The memory region must be
		  * exactly mWidth*mHeight*sizeof(double) big. */
		double* mHeightData;
	};
}