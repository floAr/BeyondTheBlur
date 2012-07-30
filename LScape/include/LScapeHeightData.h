#pragma once

namespace LScape
{
	/** This class holds calculated height data. It can be used to generate
	  * a greyscale heigth map and light map and a RGB-colored normal map. */
	class HeightData
	{
	public:
		/** Constructor, creates a HeightData with the given width and height. */
		HeightData(unsigned int width, unsigned int height, double* data = 0);

		/** Destructor, frees allocated space. */
		~HeightData();

		/** Get data from the specified position. */
		double getData(unsigned int x, unsigned int y);

		/** Set data at the specified position. */
		void setData(unsigned int x, unsigned int y, double value);

		/** Get the width of the HeightData. */
		const unsigned int getWidth();

		/** Get the height of the HeightData. */
		const unsigned int getHeight();

		/** Creates a greyscale Heightmap in RGBA format from the height data.
		  * \param clipMin The minimum value, below which everything is pure black.
		  * \param clipMin The maximum value, below which everything is pure white.
		  * \param buffer Pointer to a block of memory to write the Height Map to. The block must
		  *   be at least mWidth*mHeight*4 bytes in size. If you don't pass a pointer, the memory
		  *   is allocated using malloc and a pointer is returned. If you pass a pointer, the return
		  *   value is just the pointer you passed. */
		unsigned char* getHeightmap(double clipMin = 0.0, double clipMax = 1.0, unsigned char* buffer = 0);

		/* \todo Get light map and normal map */

	private:
		/** The width of the heightmap image. */
		unsigned int mWidth;

		/** The height of the heightmap image. */
		unsigned int mHeight;

		/** A pointer to the height data. The memory region must be
		  * exactly mWidth*mHeight*sizeof(double) big. */
		double* mData;
	};
}