## FE-Engine
This is a turn-based strategy game framework based on Fire Emblem. Made with Unity and C#.
# Player Turn
- On your turn, you take action by moving your units, either to attack or to move to safer position.
- In this example, there are three units: Red (Swordfighter), Purple (Archer) and Turquoise (Pegasus Knight).
- If we hover the cursor (controlled by arrow keys) over Red and hit the A key (select), tiles on the map light up blue.
- These tiles are where our red unit can traverse.
- Similarily, if we select Purple,
- Or turquoise,
- Blue tiles light up on the map. As shown, each unit has differing moblity based on their class.
- Furthermore, the terrain on the map affects each unit as well. Open grass (light green tiles) cost only 1 movement points, but dark green tiles (forests) cost 2.
- Wall/Barrier tiles (grey) are impassable by land units, and in essence cost 99 movement points (no unit has that many).
- However, flying units, such as Turquoise (Pegasus Knight) are unaffected completely by Wall/Barrier tiles. Flying units also boast extreme mobility, but are balanced out by a severe weakness to Archer units.
- The tiles that light up are computed by a variant of the A* pathfinding algorithm, which takes in unit class (to calculate movement points and land/air type) and values representing the different tiles on the map.
- The Red unit (Swordfighter) has the least amount of movement points, but makes up for it with defensive and offensive capabilities.
- The Purple unit (Archer) has much more moblity, but lacks defensive capability.
# Attacking Enemy Units
- Let's move red up close to an enemy unit (Black), and hit A (to confirm position/view available actions at a tile for a specific unit).
- Player UI pops up, and we can see we now have four actions: Attack, Wait, Items, and Abilities.
- Furthermore, new tiles light up blue. This is our attack range for Red, which is only 1 tile as he is a swordsman. (If no enemies were in our range of attack, which differs from unit to unit, the Attack button would not be an option).
- Wait means that we want the unit to take no action. It is often selected when mocing units to safety. Selecting Items allows you to use items that heal your units, or place a temporary buff on them.
- Lastly, Abilities are actions unique to each unit. Some are attacks that deal large damage or splash damage, and other ablities include high level buffs.
- Let's select attack for now.
- We are now taken to the attack view, where can select a basic attack, a skill attack, or an offensive ability (provided the unit has one).
- Let's select attack.
- Damage is done to the enemy, and damage is taken by Red. These values are calculated based on unit stats, weapon stats, and attack stats. Some weapons deal major damage to specific units, e.g. bow type weapons to Flying units as previously mentioned.
- Which unit attacks first is dependent on speed. Some Skills allow a unit to move first regardless of speed.
# Attacking More Enemy Units
- After we have made our attack, the game returns us to the map view. As you can see, health bars have now been modified.
- Let's use Purple to attack another unit.
- As you can see, Purple has a much greater attack range than Red.
- Lastly, let's mobilize Turquoise, fly over the Wall tiles, and attack an enemy unit.
# Enemy Turn
- After we have moved all out units, it is now the enemy's turn (CPU) to move.
- Enemy units will automatically move and attack our units based on an algorithm that finds their optimal move, taking in account of their health, location, location of other enemy units, their class advantages/disadvantages.
- The enemy fighing Red re-engages.
- The enemey fighting Turquoise also re-engages because it is certain he will die the next turn, even if he tries to run do to Turquoise's mobility, so his best move is to deal as much damage as possible before perishing. A sad fate.
- After the enemy has taken their moves, the player can now move again.
- The cycle repeats until all units on one side have been routed.
# Parameters
- Inside the Unity inspector, you can toggle unit classes, stats, weapons, and mobility and see how that affects gameplay.



