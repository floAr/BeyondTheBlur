README========================================================
This document describes the versioning and the content of this 
project.
==============================================================
CONTENT=======================================================
This project contains code which is generated along the proto-
type series on http://beyondtheblur.wordpress.com. 
==============================================================
VERSIONING====================================================
Each installment of the series is contained in its own folder.
To compile a special version you need to set the build action
of every source file in the current version to "none" and the
build action for every source file in the wanted folder to
"compile".
==============================================================
CHANGELOG=====================================================
01: First installment, just standard code with included libs,
    readme.txt and versioning folder structure.
==================02: Added some classes for basic game state management and the
	basic game loops.
03: Created the actual Prototype Framework.
04: Physic included and a basic physic sample, still need some
	work as the offset seems wrong
05: Content management included, mostly to prepare for 06 and 
	the batched rendering
06: Batched rendering included. A spritebatch keeps track of 
	our textures and draws them in one batch. Still need a lot 
	improvement when it comes to building the atlas.
==============================================================