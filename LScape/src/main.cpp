#include "LScapeXorshiftRandomGenerator.h"
#include "LScapeHeightData.h"
#include "LScapeNoiseTerrainGenerator.h"
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

	// create and fill the heightmap with Xorshift RNG random values
	LScape::NoiseTerrainGenerator* noise = new LScape::NoiseTerrainGenerator(461563);
	LScape::HeightData* heightdata = new LScape::HeightData(256, 256);

	// Draw black rectangle
	/*for (unsigned int y=10; y<80; ++y)
		for (unsigned int x=10; x<30; ++x)
			heightmap->setData(x, y, 0);*/

	/**************************
	 *  END LSCAPE TEST CODE  *
	 **************************/


	// create SFML window
    sf::RenderWindow window(sf::VideoMode(256, 256), "SFML works!");

	// create texture
	sf::Texture texture;
	if (!texture.create(256, 256)) return -1;
	sf::Sprite sprite(texture);

	sf::Clock clock;
	float time = 0;

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

		time += clock.getElapsedTime().asSeconds();
		clock.restart();

		noise->getHeightData(heightdata,0,0,2.052+sin(time/2)*1.052);
		unsigned char* heights = heightdata->getHeightmap();
		texture.update(heights, 256, 256, 0, 0);
		window.draw(sprite);
        window.display();
		free(heights);
    }

	// say goodbye
    return 0;
}