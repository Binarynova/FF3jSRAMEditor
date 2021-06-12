Final Fantasy 3j SRAM Editor
=

Version 1.3 by Binarynova

This is a save file editor for Final Fantasy 3j (NES). This program should work with any .sav
file regardless of which emulator you're using.

What can be changed?

- The names of each character.
- Strength, Agility, Vitality, Intellect, and Spirit for each character.
- Level, XP, HP, MP and Job of each character.
- Your inventory.
- How much gold and how many capacity points you have.
- How many times you've saved.
- Location of the party on the map.
- Which NPC is following you.
- Which world map your party is on.
- Which crystals you've activated.

How to use the editor:
  The tabs switch between characters, general save information, and the inventory. Start by going to the File menu
and clicking Load, then select your .sav file. Once your save file is loaded you're presented with 6 tabs: General,
1st-4th Characters, and Inventory. Make all the changes you want, then when you're finished in all tabs, click the
"SAVE Changes" button. If you're unhappy with your changes you can click "DISCARD Changes" to re-load the file
(all your changes will be lost!). Both of these buttons will present you with a confirmation box to be sure you want
to make the requested change.

Inventory:
  The inventory screen is separated into two parts. On the left is a list containing all of the items in the game. The
list is ordered by the way they're stored in the game, so you may have to scroll around a bit to find the items you
want. In the middle are two buttons. The top button places the item selected on the left into the first available
empty slot in your inventory, shown on the right. The second button scans the inventory for any items you've reduced
to a quantity of '0' and removes them.

=========
Versions:
=========

1.3

Added magic points to character tabs. Added drop box for which crystal's power you most recently received. Removed airship coordinates; they're implimented oddly. Editor will remind you to add magic points if your character has none when switched to a magic using class. Failure to do so WILL result in your game freezing when that character selects MAGIC in battle. 

1.2

Load file dialog box now filters only .sav files.

1.1

Added the ability to change your party's follower NPC.

1.0

Mostly done!

======
Thanks:
======
Thanks go out to Sanite for his FF3 SRAM hacking guide from 2001, and the website Data Crystal for even more info. Also users at Stackoverflow for answering some questions and to my brother for getting me interested in coding again.

The Visual C# code for this project is available at http://www.github.com.
