#include "LScapeXorshiftRandomGenerator.h"
#include <iostream>
#include <SFML/Graphics.hpp>

int main()
{
	/****************************************
	 * ==== MAIN FUNCTION for TESTING! ==== *
	 ****************************************/

	// create SFML window
    sf::RenderWindow window(sf::VideoMode(256, 256), "SFML works!");

	// get heightmap with the Xorshift RNG
	sf::Uint8 heights[256*256*4];
	LScape::XorshiftRandomGenerator* rng = new LScape::XorshiftRandomGenerator((unsigned int) time(NULL));
	for (int x=0; x<256*256*4; x+=4)
	{
		unsigned char c = rng->random() % 256;
		heights[x]   = c;
		heights[x+1] = c;
		heights[x+2] = c;
		heights[x+3] = 255;
	}

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