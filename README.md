# Project Zodiac

## Description

Project Zodiac is a first-person magic-based game where the player controls a wizard capable of shooting ice projectiles, performing melee attacks with a magic wand, sprinting, jumping, and even blinking (short-range teleportation) with a cooldown. The objective is to collect all the crystals scattered around the level to summon a portal to the next stage. In Level 2, the portal takes the player directly to the victory screen.

This is one of my first projects. While it's completely outdated compared to my current knowledge, and the game feel isn't as polished as I'd like, it remains a fairly ambitious project for an early stage of learning. Despite its rough edges, it includes quite a few features and is fully functional.

Note: The game art is not my own.

## Features

* First-person perspective with spellcasting and melee combat.

* Three types of enemies:

		* Brute Boars
Chase the player and attack with physical hits. After being attacked, they become invulnerable for a short time or until receiving a certain number of hits. If the player attacks them while invulnerable, they become enraged, increasing their speed and damage. The player's wand melee attack can immediately dispel their invulnerability.

		* Fire Elks
These enemies also pursue the player but keep their distance, launching fire projectiles using a particle system with collision detection. While attacking, they rotate to follow the player’s movements. Elks also possess invulnerability, but it does not affect their attack speed or damage.

		* Electric Moles
Hidden underground, these enemies emerge when the player is near and start throwing electric balls. If the player moves away, they hide again. Their invulnerability mechanics are similar to the Elks, though they are visually less polished.

* Unique invulnerability mechanics for each enemy type.

*Real-time physics-based traps and interactive environments:
		
		* Spike traps that activate when the player steps on them.

		* Saws and crushers that move across floors and walls.

		* Flying hammers and axes spinning around platforms.

		* Spiked walls that chase the player.

		* Arrow traps embedded in walls that shoot when triggered.

* Short-range teleportation (Blink) for evasive maneuvers.

* Basic inventory system for potions and power-ups:

	  *Health Potion: Restores health.

	  *Defense Potion: Increases defense for a limited time.

      *Attack Potion: Boosts attack damage for a short duration.

      *Greater Health Potion: Heals the player and increases max health temporarily.

## Technologies Used

* Unity

* C#

## Unity Version Recommended: 2019.2.17f1

## How to Interact

* W/A/S/D - Move

* Space - Jump

* Shift - Sprint

* Left Click - Cast ice projectile

* Right Click - Melee attack with wand

* E - Blink (short-range teleport)

* 1/2/3/4 - Use item (if available)

* I - Open/close inventory 

## Future Improvements

* Refactor the code following design patterns and SOLID principles.

* Enhanced visuals and animations.

* Improved game feel and movement responsiveness.

* More polished UI and level transitions.

* Optimization and bug fixing.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.