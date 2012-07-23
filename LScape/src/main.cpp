#include "LScapeXorshiftRandomGenerator.h"
#include "LScapeHeightmap.h"
#include <iostream>
#include <SFML/Graphics.hpp>

int main()
{
	/****************************************
	 * ==== MAIN FUNCTION for TESTING! ==== *
	 ****************************************/

	/**************************
	 * BEGIN LSCAPE TEST CODE *
	 **************************/

	// create buffer for heightmap RGBA image
	unsigned char heights[256*256*4];

	// create and fill the heightmap with Xorshift RNG random values
	LScape::Heightmap* heightmap = new LScape::Heightmap(256, 256);
	LScape::XorshiftRandomGenerator* rng = new LScape::XorshiftRandomGenerator((unsigned int) time(NULL));
	for (unsigned int y=0; y<256; ++y)
	{
		for (unsigned int x=0; x<256; ++x)
		{
			double c = (rng->random() % 65536) / 65536.0;
			heightmap->setData(x, y, c);
		}
	}

	// Draw black rectangle
	for (unsigned int y=10; y<80; ++y)
		for (unsigned int x=10; x<30; ++x)
			heightmap->setData(x, y, 0);

	// Save height map to buffer
	heightmap->getHeightMap(0, 1, &heights[0]);

	/**************************
	 *  END LSCAPE TEST CODE  *
	 **************************/


	// create SFML window
    sf::RenderWindow window(sf::VideoMode(256, 256), "SFML works!");

	// create texture
	sf::Texture texture;
	if (!texture.create(256, 256)) return -1;
	sf::Sprite sprite(texture);

	// draw the window
    while (window.isOpen())
    {
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear();
		texture.update(heights, 256, 256, 0, 0);
		window.draw(sprite);
        window.display();
    }

	// say goodbye
    return 0;
}