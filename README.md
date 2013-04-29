============================
Final Fantasy 3j SRAM Editor
============================

Version 1.2 by Thomas Knight

This is a save file editor for Final Fantasy 3j (NES). This program should work with any .sav
file regardless of which emulator you're using.

What can be changed?

- The names of each character.
- Strength, Agility, Vitality, Intellect, and Spirit for each character.
- Level, XP, and Job of each character.
- Your inventory.
- How much gold you have.
- Location of the party and any airships on the map.
- Which world map you're party is on.

How to use the editor:
  The tabs switch between characters, general save information, and the inventory. Start by going to the File menu
and clicking Load, then select your .sav file. Once your save file is loaded you're presented with 6 tabs: General,
1st-4th Characters, and Inventory. Make all the changes you want, then when you're finished in all tabs, click the
"SAVE Changes" button. If you're unhappy with your changes you can click "DISCARD Changes" to re-load the file
(all your changes will be lost!). Both of these buttons will present you with a confirmation box to be sure you want
to make the requested change.

Inventory:
  The inventory screen is separated into two parts. On the left is a list containing all of the items in the game. The
list is ordered by the hex value of each item in the file, so you may have to scroll around a bit to find the item you
want. In the middle are two buttons. The top button places the item selected on the left into the first available
empty slot in your inventory, shown on the right. The second button scans the inventory for any items you've reduced
to a quantity of '0' and removes them.

=========
Versions:
=========

1.2

Load file dialog box now filters only .sav files.

1.1

Added the ability to change your party's follower NPC.

1.0

Release!
