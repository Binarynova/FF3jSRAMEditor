// Main code for Final Fantasy 3j SRAM Editor
//
// TODO:
//       Condense as much code as possible especially the loop that adds spaces to names.
//       Change airship checkboxes into radio buttons.
//          

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace Hex_Edit_Window_Test
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        

        public Label[] defineInvSlotList()
        {
            Label[] invSlotList = new Label[32]
                {this.invSlot1, this.invSlot2, this.invSlot3, this.invSlot4, this.invSlot5, this.invSlot6, this.invSlot7, this.invSlot8, this.invSlot9, this.invSlot10,
                 this.invSlot11, this.invSlot12, this.invSlot13, this.invSlot14, this.invSlot15, this.invSlot16, this.invSlot17, this.invSlot18, this.invSlot19, this.invSlot20,
                 this.invSlot21, this.invSlot22, this.invSlot23, this.invSlot24, this.invSlot25, this.invSlot26, this.invSlot27, this.invSlot28, this.invSlot29, this.invSlot30,
                 this.invSlot31, this.invSlot32};

            return invSlotList;
        }

        public NumericUpDown[] defineInvQuanList()
        {
            NumericUpDown[] invQuanList = new NumericUpDown[32]
                {this.invQuan1, this.invQuan2, this.invQuan3, this.invQuan4, this.invQuan5, this.invQuan6, this.invQuan7, this.invQuan8, this.invQuan9, this.invQuan10,
                 this.invQuan11, this.invQuan12, this.invQuan13, this.invQuan14, this.invQuan15, this.invQuan16, this.invQuan17, this.invQuan18, this.invQuan19, this.invQuan20,
                 this.invQuan21, this.invQuan22, this.invQuan23, this.invQuan24, this.invQuan25, this.invQuan26, this.invQuan27, this.invQuan28, this.invQuan29, this.invQuan30,
                 this.invQuan31, this.invQuan32};

            return invQuanList;
        }

        public ComboBox[] defineJobBoxList()
        {
            ComboBox[] jobBoxList = new ComboBox[4]
                {this.charJobBox1, this.charJobBox2, this.charJobBox3, this.charJobBox4};

            return jobBoxList;
        }


        string[] itemNames = new string[256]
        {"-",                 "Kaiser (Claw)",     "CatClaw (Claw)",    "Dragon (Claw)",      "Elven (Claw)",      "HellClaw (Claw)",    "Nunchuck",          "Tonfa (Nunchuck)",   "3-Part (Nunchuck)",
        "Mithril (Rod)",      "Flame (Rod)",       "Ice (Rod)",         "Light (Rod)",        "Ultimate (Rod)",    "Staff",              "Burning (Staff)",   "Freezing (Staff)",   "Shining (Staff)",
        "Golem (Staff)",      "Rune (Staff)",      "Eldest (Staff)",    "Hammer",             "Thor (Hammer)",     "Battle (Axe)",       "Great Axe",         "M. Star (Axe)",      "Thunder (Spear)",
        "Wind (Spear)",       "Blood (Spear)",     "Holy (Spear)",      "Knife",              "Dagger",            "Mithril (Knife)",    "M. Gauche (Knife)", "Orialcon (Knife)",   "Airknife",
        "Long (Sword)",       "W. Slayer (Sword)", "Shiny (Sword)",     "Mithril (Sword)",    "Serpent (Sword)",   "Ice Blade (Sword)",  "Tyrving (Sword)",   "Salamand (Sword)",   "King (Sword)",
        "Tomahawk (Axe)",     "Ancient (Sword)",   "Ashura (Sword)",    "Blood (Sword)",      "Defender (Sword)",  "Triton (Hammer)",    "Kotetsu (Sword)",   "Kiku (Sword)",       "Break (Sword)",
        "Excalibur (Sword)",  "Masamune (Sword)",  "Ragnarök (Sword)",  "Onion (Sword)",      "Flame (Magic)",     "Ice (Magic)",        "Inferno (Magic)",   "Light (Magic)",      "Illumina (Magic)",
        "Boomerang (Throw)",  "Full Moon (Throw)", "Shuriken (Throw)",  "Blizzard (Magic)",   "Giyaman (Bell)",    "Earth (Bell)",       "Rune (Bell)",       "Madora (Harp)",      "Dream (Harp)",
        "Lamia (Harp)",       "Loki (Harp)",       "Bow",               "Great Bow",          "Killer (Bow)",      "Rune (Bow)",         "Yoichi (Bow)",      "Wooden (Bow)",       "Holy (Arrow)",
        "Iron (Arrow)",       "Bolt (Arrow)",      "Fire (Arrow)",      "Ice (Arrow)",        "Medusa (Arrow)",    "Yoichi (Arrow)",     "**NULL**",          "Leather (Shield)",   "Onion (Shield)",
        "Mithril (Shield)",   "Ice (Shield)",      "Hero (Shield)",     "Demon (Shield)",     "Diamond (Shield)",  "Aegis (Shield)",     "Genji (Shield)",    "Crystal (Shield)",   "Leather (Helmet)",
        "Onion (Helmet)",     "Mithril (Helmet)",  "Carapace (Helmet)", "Ice (Helmet)",       "Headband (Helmet)", "Scholar (Helmet)",   "Darkhood (Helmet)", "Chakra (Helmet)",    "Viking (Helmet)",
        "Dragon (Helmet)",    "Feather (Helmet)",  "Diamond (Helmet)",  "Genji (Helmet)",     "Crystal (Helmet)",  "Ribbon (Helmet)",    "Cloth (Armor)",     "Leather (Armor)",    "Onion (Armor)",
        "Mithril (Armor)",    "Carapace (Armor)",  "Ice (Armor)",       "Flame Mail (Armor)", "Kenpo (Armor)",     "Dark Suit (Armor)",  "Wizard (Armor)",    "Viking (Armor)",     "Black Belt (Armor)",
        "Knight (Armor)",     "Dragon (Armor)",    "Bard (Armor)",      "Scholar (Armor)",    "Gaia (Armor)",      "Demon (Armor)",      "Diamond (Armor)",   "Reflect (Armor)",    "White Robe (Armor)",
        "Black Robe (Armor)", "Genji (Armor)",     "Crystal (Armor)",   "Rusted (Armor)",     "Copper (Glove)",    "Onion (Glove)",      "Mithril (Glove)",   "Mithril (Gauntlet)", "Thief (Glove)",
        "Gauntlet (Glove)",   "Power (Glove)",     "Rune (Glove)",      "Diamond (Gauntlet)", "Diamond (Glove)",   "Protect (Glove)",    "Genji (Glove)",     "Crystal (Glove)",    "Magic Key",
        "Carrot",             "Horn",              "Eye",               "Time Gear",          "Eureka Key",        "Wind Fang",          "Fire Fang",         "Water Fang",         "Earth Fang",
        "Lute",               "Sylx Key",          "Midge Bread",       "?",                  "Potion",            "HiPotion",           "Elixir",            "Fenix Down",         "Soft",
        "Maiden Kiss",        "Echo Herb",         "Luck Mallet",       "Eyedrop",            "Antidote",          "Otter Head",         "Bomb Shard",        "South Wind",         "Zeus' Rage",
        "BombR.Arm",          "Northwind",         "Gods' Rage",        "Earth Drum",         "Lamia Scl.",        "Gods' Wine",         "Turtle Shell",      "Devil's Sigh",       "Black Hole",
        "Dark Scent",         "Lilith Kiss",       "Imp's Yawn",        "Split Shell",        "Paralyzer",         "Mute Charm",         "Pillow",            "Bomb Head",          "Barrier",
        "Choco Rage",         "White Scent",       "Flare (Magic)",     "Death (Magic)",      "Meteo (Magic)",     "Whirl Wind (Magic)", "Life 2 (Magic)",    "Holy (Magic)",       "Bahamut (Magic)",
        "Quake (Magic)",      "Break 2 (Magic)",   "Drain (Magic)",     "Cure 4 (Magic)",     "Heal (Magic)",      "Wall (Magic)",       "Levia (Magic)",     "Fire 3 (Magic)",     "Bio (Magic)",
        "Warp (Magic)",       "Aero 2 (Magic)",    "Soft (Magic)",      "Haste (Magic)",      "Odin (Magic)",      "Bolt 3 (Magic)",     "Kill (Magic)",      "Erase (Magic)",      "Cure 3 (Magic)",
        "Life (Magic)",       "Safe (Magic)",      "Titan (Magic)",     "Break (Magic)",      "Ice 3 (Magic)",     "Shade (Magic)",      "Libra (Magic)",     "Confuse (Magic)",    "Mute (Magic)",
        "Ifrit (Magic)",      "Fire 2 (Magic)",    "Ice 2 (Magic)",     "Bolt 2 (Magic)",     "Cure 2 (Magic)",    "Exit (Magic)",       "Wash (Magic)",      "Ramuh (Magic)",      "Bolt (Magic)",
        "Venom (Magic)",      "Blind (Magic)",     "Aero (Magic)",      "Toad (Magic)",       "Mini (Magic)",      "Shiva (Magic)",      "Fire (Magic)",      "Ice (Magic)",        "Sleep (Magic)",
        "Cure (Magic)",       "Pure (Magic)",      "Sight (Magic)",     "Chocobo (Magic)"};
        
        string[] worldNames = new string[5] {"Floating Continent", "Floating Continent B?", "Surface World (Flooded)", "Surface World", "Surface World (Underwater)"};

        string[] jobNames = new string[22] {"Onion Kid", "Fighter", "Monk",      "White Wizard", "Black Wizard", "Red Wizard", "Hunter",       "Knight",
                                            "Thief",     "Scholar", "Geomancer", "Dragoon",      "Viking",       "Karateka",   "Magic Knight", "Conjurer",
                                            "Bard",      "Warlock", "Shaman",    "Summoner",     "Sage",         "Ninja"};

        string[] followerNames = new string[9] { "None", "Sara", "Cid", "Desh", "\"Shadow\"", "Elia", "Allus", "Dorga", "Unne" };

        string[] crystalLevels = new string[6] { "Job Menu Locked", "Wind Crystal", "Fire Crystal", "Water Crystal", "Earth Crystal", "Forbidden Land Eureka" };
        
        public void ItemLoopRead(System.IO.FileStream stream)
        {
            Label[] invSlotList = defineInvSlotList();
            NumericUpDown[] invQuanList = defineInvQuanList();
                        
            for (int j = 0; j < 32; j++) // For each inventory slot...
            {
                for (int i = 0; i < 256; i++) // ...cycle through each item ID/name pair...
                {
                    stream.Position = 0x4c0 + j; // ... find it in the file...
                    if (stream.ReadByte() == i) { invSlotList[j].Text = itemNames[i]; }     // ... then convert each item ID to its name and put it in the table...
                    stream.Position = 0x4e0 + j; invQuanList[j].Value = stream.ReadByte();  // ... then find the quantity and put it in the table.
                }
            }
        }

        public void ItemLoopWrite(System.IO.FileStream stream)
        {
            Label[] invSlotList = defineInvSlotList();
            NumericUpDown[] invQuanList = defineInvQuanList();

            for (int j = 0; j < 32; j++)
            {
                for (int i = 0; i < 256; i++)
                {
                    stream.Position = 0x4c0 + j;
                    if (invSlotList[j].Text == itemNames[i]) {stream.WriteByte((byte)i); }
                }
                stream.Position = 0x4e0 + j; stream.WriteByte((byte)invQuanList[j].Value);
            }            
        }

        public void WriteToSRAM(System.IO.FileStream stream)
        {
            /////////////////////////
            ///// Write GOLD to file.
            int[] goldbytes = new int[3];

            // Split large integer amount into three separate bytes
            goldbytes[0] = (int)goldBox.Value / 65536;
            goldbytes[1] = (int)goldBox.Value / 256;
            goldbytes[2] = (int)goldBox.Value;

            // Change value in save file. Note: Counting forwards through offsets and
            // backwards through goldbytes array because gold value is
            // stored right -> left in file.
            stream.Position = 0x41c;    stream.WriteByte((byte)goldbytes[2]);
            stream.WriteByte((byte)goldbytes[1]);
            stream.WriteByte((byte)goldbytes[0]);
            ///// End of GOLD Section.
            //////////////////////////

            ///////////////////////////////
            ///// Write CAPACITIES to file.
            stream.Position = 0x41b;    stream.WriteByte((byte)capacityBox.Value);
            ///// End of CAPACITIES Section.
            //////////////////////////

            ///////////////////////////////////
            ///// Write NAMES to file.
            string[] writeCharNames = new string[4];
            writeCharNames[0] = nameBox1.Text;
            writeCharNames[1] = nameBox2.Text;
            writeCharNames[2] = nameBox3.Text;
            writeCharNames[3] = nameBox4.Text;

            for (int j = 0; j < 4; j++) // Loop adds spaces to ends of names before inserting into data. Need to generalize this later.
            {
                int count = 0;
                foreach (char c in writeCharNames[j])
                {
                    count++;
                }
                if (count == 1)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        writeCharNames[j] = writeCharNames[j] + "     ";
                    }
                }
                else if (count == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        writeCharNames[j] = writeCharNames[j] + "    ";
                    }
                }
                else if (count == 3)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        writeCharNames[j] = writeCharNames[j] + "   ";
                    }
                }
                else if (count == 4)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        writeCharNames[j] = writeCharNames[j] + "  ";
                    }
                }
                else if (count == 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        writeCharNames[j] = writeCharNames[j] + " ";
                    }
                }
            }

            int[,] nameWriteOne = new int[4, 6];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 6; j++)
                {

                    if (writeCharNames[i][j] == Convert.ToChar("A")) { nameWriteOne[i, j] = 138; }
                    else if (writeCharNames[i][j] == Convert.ToChar("B")) { nameWriteOne[i, j] = 139; }
                    else if (writeCharNames[i][j] == Convert.ToChar("C")) { nameWriteOne[i, j] = 140; }
                    else if (writeCharNames[i][j] == Convert.ToChar("D")) { nameWriteOne[i, j] = 141; }
                    else if (writeCharNames[i][j] == Convert.ToChar("E")) { nameWriteOne[i, j] = 142; }
                    else if (writeCharNames[i][j] == Convert.ToChar("F")) { nameWriteOne[i, j] = 143; }
                    else if (writeCharNames[i][j] == Convert.ToChar("G")) { nameWriteOne[i, j] = 144; }
                    else if (writeCharNames[i][j] == Convert.ToChar("H")) { nameWriteOne[i, j] = 145; }
                    else if (writeCharNames[i][j] == Convert.ToChar("I")) { nameWriteOne[i, j] = 146; }
                    else if (writeCharNames[i][j] == Convert.ToChar("J")) { nameWriteOne[i, j] = 147; }
                    else if (writeCharNames[i][j] == Convert.ToChar("K")) { nameWriteOne[i, j] = 148; }
                    else if (writeCharNames[i][j] == Convert.ToChar("L")) { nameWriteOne[i, j] = 149; }
                    else if (writeCharNames[i][j] == Convert.ToChar("M")) { nameWriteOne[i, j] = 150; }
                    else if (writeCharNames[i][j] == Convert.ToChar("N")) { nameWriteOne[i, j] = 151; }
                    else if (writeCharNames[i][j] == Convert.ToChar("O")) { nameWriteOne[i, j] = 152; }
                    else if (writeCharNames[i][j] == Convert.ToChar("P")) { nameWriteOne[i, j] = 153; }
                    else if (writeCharNames[i][j] == Convert.ToChar("Q")) { nameWriteOne[i, j] = 154; }
                    else if (writeCharNames[i][j] == Convert.ToChar("R")) { nameWriteOne[i, j] = 155; }
                    else if (writeCharNames[i][j] == Convert.ToChar("S")) { nameWriteOne[i, j] = 156; }
                    else if (writeCharNames[i][j] == Convert.ToChar("T")) { nameWriteOne[i, j] = 157; }
                    else if (writeCharNames[i][j] == Convert.ToChar("U")) { nameWriteOne[i, j] = 158; }
                    else if (writeCharNames[i][j] == Convert.ToChar("V")) { nameWriteOne[i, j] = 159; }
                    else if (writeCharNames[i][j] == Convert.ToChar("W")) { nameWriteOne[i, j] = 160; }
                    else if (writeCharNames[i][j] == Convert.ToChar("X")) { nameWriteOne[i, j] = 161; }
                    else if (writeCharNames[i][j] == Convert.ToChar("Y")) { nameWriteOne[i, j] = 162; }
                    else if (writeCharNames[i][j] == Convert.ToChar("Z")) { nameWriteOne[i, j] = 163; }
                    else if (writeCharNames[i][j] == Convert.ToChar("a")) { nameWriteOne[i, j] = 164; }
                    else if (writeCharNames[i][j] == Convert.ToChar("b")) { nameWriteOne[i, j] = 165; }
                    else if (writeCharNames[i][j] == Convert.ToChar("c")) { nameWriteOne[i, j] = 166; }
                    else if (writeCharNames[i][j] == Convert.ToChar("d")) { nameWriteOne[i, j] = 167; }
                    else if (writeCharNames[i][j] == Convert.ToChar("e")) { nameWriteOne[i, j] = 168; }
                    else if (writeCharNames[i][j] == Convert.ToChar("f")) { nameWriteOne[i, j] = 169; }
                    else if (writeCharNames[i][j] == Convert.ToChar("g")) { nameWriteOne[i, j] = 170; }
                    else if (writeCharNames[i][j] == Convert.ToChar("h")) { nameWriteOne[i, j] = 171; }
                    else if (writeCharNames[i][j] == Convert.ToChar("i")) { nameWriteOne[i, j] = 172; }
                    else if (writeCharNames[i][j] == Convert.ToChar("j")) { nameWriteOne[i, j] = 173; }
                    else if (writeCharNames[i][j] == Convert.ToChar("k")) { nameWriteOne[i, j] = 174; }
                    else if (writeCharNames[i][j] == Convert.ToChar("l")) { nameWriteOne[i, j] = 175; }
                    else if (writeCharNames[i][j] == Convert.ToChar("m")) { nameWriteOne[i, j] = 176; }
                    else if (writeCharNames[i][j] == Convert.ToChar("n")) { nameWriteOne[i, j] = 177; }
                    else if (writeCharNames[i][j] == Convert.ToChar("o")) { nameWriteOne[i, j] = 178; }
                    else if (writeCharNames[i][j] == Convert.ToChar("p")) { nameWriteOne[i, j] = 179; }
                    else if (writeCharNames[i][j] == Convert.ToChar("q")) { nameWriteOne[i, j] = 180; }
                    else if (writeCharNames[i][j] == Convert.ToChar("r")) { nameWriteOne[i, j] = 181; }
                    else if (writeCharNames[i][j] == Convert.ToChar("s")) { nameWriteOne[i, j] = 182; }
                    else if (writeCharNames[i][j] == Convert.ToChar("t")) { nameWriteOne[i, j] = 183; }
                    else if (writeCharNames[i][j] == Convert.ToChar("u")) { nameWriteOne[i, j] = 184; }
                    else if (writeCharNames[i][j] == Convert.ToChar("v")) { nameWriteOne[i, j] = 185; }
                    else if (writeCharNames[i][j] == Convert.ToChar("w")) { nameWriteOne[i, j] = 186; }
                    else if (writeCharNames[i][j] == Convert.ToChar("x")) { nameWriteOne[i, j] = 187; }
                    else if (writeCharNames[i][j] == Convert.ToChar("y")) { nameWriteOne[i, j] = 188; }
                    else if (writeCharNames[i][j] == Convert.ToChar("z")) { nameWriteOne[i, j] = 189; }
                    else if (writeCharNames[i][j] == Convert.ToChar("0")) { nameWriteOne[i, j] = 0x80; }
                    else if (writeCharNames[i][j] == Convert.ToChar("1")) { nameWriteOne[i, j] = 0x81; }
                    else if (writeCharNames[i][j] == Convert.ToChar("2")) { nameWriteOne[i, j] = 0x82; }
                    else if (writeCharNames[i][j] == Convert.ToChar("3")) { nameWriteOne[i, j] = 0x83; }
                    else if (writeCharNames[i][j] == Convert.ToChar("4")) { nameWriteOne[i, j] = 0x84; }
                    else if (writeCharNames[i][j] == Convert.ToChar("5")) { nameWriteOne[i, j] = 0x85; }
                    else if (writeCharNames[i][j] == Convert.ToChar("6")) { nameWriteOne[i, j] = 0x86; }
                    else if (writeCharNames[i][j] == Convert.ToChar("7")) { nameWriteOne[i, j] = 0x87; }
                    else if (writeCharNames[i][j] == Convert.ToChar("8")) { nameWriteOne[i, j] = 0x88; }
                    else if (writeCharNames[i][j] == Convert.ToChar("9")) { nameWriteOne[i, j] = 0x89; }
                    else if (writeCharNames[i][j] == Convert.ToChar("!")) { nameWriteOne[i, j] = 0xc4; }
                    else if (writeCharNames[i][j] == Convert.ToChar("?")) { nameWriteOne[i, j] = 0xc5; }
                    else if (writeCharNames[i][j] == Convert.ToChar("'")) { nameWriteOne[i, j] = 0xbf; }
                    else if (writeCharNames[i][j] == Convert.ToChar("-")) { nameWriteOne[i, j] = 0xc2; }
                    else if (writeCharNames[i][j] == Convert.ToChar(" ")) { nameWriteOne[i, j] = 255; }
                }
            }

            int[] namePos = new int[4];            

            for (int i = 0; i < 4; i++)
            {
                namePos[i] = 0x506 + (i * 0x40); //namePos[0] = 0x506; namePos[1] = 0x546; namePos[2] = 0x586; namePos[3] = 0x5c6;
                for (int j = 0; j < 6; j++)
                {
                    stream.Position = namePos[i] + j;   stream.WriteByte((byte)nameWriteOne[i, j]);
                }
            }
            ///// End of NAMES section.
            ///////////////////////////////////


            ///////////////////////////////////
            ///// Write JOBS to file.
            ComboBox[] jobBoxList = defineJobBoxList();
             
            int[] charJobNumber = new int[4];

            for (int i = 0; i < 4; i++)
            {                
                for (int j = 0; j < 22; j++)
                {
                    if ((string)(jobBoxList[i].SelectedItem) == jobNames[j]) { charJobNumber[i] = j; }
                }

                stream.Position = 0x500 + (i * 0x40);
                stream.WriteByte((byte)charJobNumber[i]);
            }
            ///// End of JOBS section.
            ///////////////////////////////////

            ///////////////////////////////////////
            ///// Write LEVEL to file.
            stream.Position = 0x501;    stream.WriteByte((byte)(charLevelBox1.Value - 1));
            stream.Position = 0x541;    stream.WriteByte((byte)(charLevelBox2.Value - 1));
            stream.Position = 0x581;    stream.WriteByte((byte)(charLevelBox3.Value - 1));
            stream.Position = 0x5c1;    stream.WriteByte((byte)(charLevelBox4.Value - 1));
            ///// End of LEVEL section.
            ///////////////////////////////////////

            ///////////////////////////////////////////
            ///// Write HP to file.
            int[] hpbytes = new int[2];
            
            hpbytes[0] = (int)charHP1.Value / 256;
            hpbytes[1] = (int)charHP1.Value;
            stream.Position = 0x50c;     
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charMaxHP1.Value / 256;
            hpbytes[1] = (int)charMaxHP1.Value;
            stream.Position = 0x50e; 
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charHP2.Value / 256;
            hpbytes[1] = (int)charHP2.Value;
            stream.Position = 0x54c; 
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charMaxHP2.Value / 256;
            hpbytes[1] = (int)charMaxHP2.Value;
            stream.Position = 0x54e; 
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charHP3.Value / 256;
            hpbytes[1] = (int)charHP3.Value;
            stream.Position = 0x58c; 
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charMaxHP3.Value / 256;
            hpbytes[1] = (int)charMaxHP3.Value;
            stream.Position = 0x58e;
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charHP4.Value / 256;
            hpbytes[1] = (int)charHP4.Value;
            stream.Position = 0x5cc;
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);

            hpbytes[0] = (int)charMaxHP4.Value / 256;
            hpbytes[1] = (int)charMaxHP4.Value;
            stream.Position = 0x5ce; 
            stream.WriteByte((byte)hpbytes[1]);
            stream.WriteByte((byte)hpbytes[0]);
            ///// End of HP section.
            ///////////////////////////////////////////

            //////////////////////////
            ///// Write XP to file.
            int[] xpbytes = new int[3];

            xpbytes[0] = (int)charXP1.Value / 65536;
            xpbytes[1] = (int)charXP1.Value / 256;
            xpbytes[2] = (int)charXP1.Value;

            stream.Position = 0x503;   
            stream.WriteByte((byte)xpbytes[2]);
            stream.WriteByte((byte)xpbytes[1]);
            stream.WriteByte((byte)xpbytes[0]);

            xpbytes[0] = (int)charXP2.Value / 65536;
            xpbytes[1] = (int)charXP2.Value / 256;
            xpbytes[2] = (int)charXP2.Value;

            stream.Position = 0x543;   
            stream.WriteByte((byte)xpbytes[2]);
            stream.WriteByte((byte)xpbytes[1]);
            stream.WriteByte((byte)xpbytes[0]);

            xpbytes[0] = (int)charXP3.Value / 65536;
            xpbytes[1] = (int)charXP3.Value / 256;
            xpbytes[2] = (int)charXP3.Value;

            stream.Position = 0x583;   
            stream.WriteByte((byte)xpbytes[2]);
            stream.WriteByte((byte)xpbytes[1]);
            stream.WriteByte((byte)xpbytes[0]);

            xpbytes[0] = (int)charXP4.Value / 65536;
            xpbytes[1] = (int)charXP4.Value / 256;
            xpbytes[2] = (int)charXP4.Value;

            stream.Position = 0x5c3;   
            stream.WriteByte((byte)xpbytes[2]);
            stream.WriteByte((byte)xpbytes[1]);
            stream.WriteByte((byte)xpbytes[0]);
            ///// End of XP section.
            //////////////////////////

            ///////////////////////////////////////
            ///// Write STATS to file.
            stream.Position = 0x512;   
            stream.WriteByte((byte)(charStr1.Value));
            stream.WriteByte((byte)(charAgi1.Value));
            stream.WriteByte((byte)(charVit1.Value));
            stream.WriteByte((byte)(charInt1.Value));
            stream.WriteByte((byte)(charSpi1.Value));

            stream.Position = 0x552;   
            stream.WriteByte((byte)(charStr2.Value));
            stream.WriteByte((byte)(charAgi2.Value));
            stream.WriteByte((byte)(charVit2.Value));
            stream.WriteByte((byte)(charInt2.Value));
            stream.WriteByte((byte)(charSpi2.Value));

            stream.Position = 0x592;   
            stream.WriteByte((byte)(charStr3.Value));
            stream.WriteByte((byte)(charAgi3.Value));
            stream.WriteByte((byte)(charVit3.Value));
            stream.WriteByte((byte)(charInt3.Value));
            stream.WriteByte((byte)(charSpi3.Value));

            stream.Position = 0x5d2;    
            stream.WriteByte((byte)(charStr4.Value));
            stream.WriteByte((byte)(charAgi4.Value));
            stream.WriteByte((byte)(charVit4.Value));
            stream.WriteByte((byte)(charInt4.Value));
            stream.WriteByte((byte)(charSpi4.Value));
            ///// End of STATS section.
            ///////////////////////////////////////

            ///////////////////////////////////////
            ///// Write X&Y to file.
            //Player
            stream.Position = 0x409; 
            stream.WriteByte((byte)(charX.Value));
            stream.WriteByte((byte)(charY.Value));            

            //Airship
            stream.Position = 0x401;
            int airshipXTest = (int)airshipX.Value + 7;
            int airshipYTest = (int)airshipY.Value + 7;

            if (airshipXTest > 255) { airshipXTest = 0x00 + airshipXTest; }
            else { airshipXTest = (int)airshipX.Value + 7; }
            if (airshipYTest > 255) { airshipYTest = 0x00 + airshipYTest; }
            else { airshipYTest = (int)airshipY.Value + 7; }

            stream.WriteByte((byte)(airshipXTest));
            stream.WriteByte((byte)(airshipYTest));
            stream.Position = 0x400;
            if (airshipCheck.Checked == true) { stream.WriteByte(0x01); }
            else { stream.WriteByte(0x00); }

            //Big Airship
            stream.Position = 0x405; 
            stream.WriteByte((byte)(bigAirshipX.Value));
            stream.WriteByte((byte)(bigAirshipY.Value));
            stream.Position = 0x404;
            if (bigAirshipCheck.Checked == true) { stream.WriteByte(0x01); }
            else { stream.WriteByte(0x00); }
            ///// End of X&Y section.
            ///////////////////////////////////////

            //////////////////////////////////////////
            ///// Write WORLD MAP to file.
            stream.Position = 0x408;
            for (int i = 0; i < 5; i++)
            {
                if(worldMapBox.Text == worldNames[i]) { stream.WriteByte((byte)(i));}
            }
            ///// End WORLD MAP section.
            /////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////
            ///// Write ITEMS to file.
            ItemLoopWrite(stream);
            ///// End of ITEMS section.
            /////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////
            ///// Write SAVE COUNT
            stream.Position = 0x414;
            stream.WriteByte((byte)saveCountBox.Value);
            ///// End SAVE COUNT section.
            ///////////////////////////////////////////

            ///////////////////////////////////////////
            ///// Write CRYSTAL LEVEL
            stream.Position = 0x421;
            string tempWriteCrystal = crystalLevelBox.Text;
            
            if (tempWriteCrystal == crystalLevels[0]) { stream.WriteByte(0); }
            else if (tempWriteCrystal == crystalLevels[1]) { stream.WriteByte(1); }
            else if (tempWriteCrystal == crystalLevels[2]) { stream.WriteByte(3); }
            else if (tempWriteCrystal == crystalLevels[3]) { stream.WriteByte(7); }
            else if (tempWriteCrystal == crystalLevels[4]) { stream.WriteByte(15); }
            else if (tempWriteCrystal == crystalLevels[5]) { stream.WriteByte(31); }

            ///// End CRYSTAL LEVEL section.
            ///////////////////////////////////////////

            ///////////////////////////////////////////
            ///// Write FOLLWOING
            stream.Position = 0x40b;
            for (int i = 0; i < 9; i++)
            {
                if (followerBox.Text == followerNames[i]) { stream.WriteByte((byte)i); break; }
            }
            ///// End FOLLOWING section.
            ///////////////////////////////////////////

            //////////////////////////////
            ///// ADD NEW EDITS HERE!!!
            //////////////////////////////

            //////////////////////////////////////////////////////////////
            ///// Evaluate CHECKSUM
            int checksum = 0x0;

            for (int i = 0x400; i <= 0x7ff; i++)
            {
                stream.Position = i;
                checksum = (checksum + stream.ReadByte());
            }

            // Is checksum 255(0xFF)?
            if (checksum == 0xff)
            {
                // Yes, do nothing.
                System.Windows.Forms.MessageBox.Show("No correction needed!");
            }
            else
            {
                // No, correct it.

                // Read current checksum byte
                stream.Position = 0x41a;    int checksumModifierByte = stream.ReadByte();

                // Calculate and set the new byte
                stream.Position = 0x41a;    stream.WriteByte((byte)((255 - checksum) + checksumModifierByte));
            }
            ///// End CHECKSUM Section
            //////////////////////////////////////////////////////////////////
        }

        public void ReadFromSRAM(System.IO.FileStream stream)
        {
            //////////////////////////
            ///// Read GOLD from file.
            stream.Position = 0x41c;    
            int goldValue1 = stream.ReadByte();
            int goldValue2 = stream.ReadByte();
            int goldValue3 = stream.ReadByte();

            goldBox.Value = goldValue1 + 256 * goldValue2 + 65536 * goldValue3; // Convert bytes to readable integer value
            ///// End of GOLD section.
            //////////////////////////

            ////////////////////////////////
            ///// Read CAPACITIES from file.
            stream.Position = 0x41b;    capacityBox.Value = stream.ReadByte();
            ///// End of CAPACITIES section.
            ////////////////////////////////

            //////////////////////////////////
            ///// Read NAMES from file.
            string[] charNames = new string[4];
            charNames[0] = null; charNames[1] = null; charNames[2] = null; charNames[3] = null;

            int[] namePos = new int[4];
            namePos[0] = 0x506; namePos[1] = 0x546; namePos[2] = 0x586; namePos[3] = 0x5c6;

            for (int j = 0; j <= 3; j++)
            {
                for (int i = 0; i <= 5; i++)
                {
                    stream.Position = namePos[j] + i;    int nameOne1 = stream.ReadByte();

                    if (nameOne1 == 138) { charNames[j] = charNames[j] + "A"; }
                    else if (nameOne1 == 139) { charNames[j] = charNames[j] + "B"; }
                    else if (nameOne1 == 140) { charNames[j] = charNames[j] + "C"; }
                    else if (nameOne1 == 141) { charNames[j] = charNames[j] + "D"; }
                    else if (nameOne1 == 142) { charNames[j] = charNames[j] + "E"; }
                    else if (nameOne1 == 143) { charNames[j] = charNames[j] + "F"; }
                    else if (nameOne1 == 144) { charNames[j] = charNames[j] + "G"; }
                    else if (nameOne1 == 145) { charNames[j] = charNames[j] + "H"; }
                    else if (nameOne1 == 146) { charNames[j] = charNames[j] + "I"; }
                    else if (nameOne1 == 147) { charNames[j] = charNames[j] + "J"; }
                    else if (nameOne1 == 148) { charNames[j] = charNames[j] + "K"; }
                    else if (nameOne1 == 149) { charNames[j] = charNames[j] + "L"; }
                    else if (nameOne1 == 150) { charNames[j] = charNames[j] + "M"; }
                    else if (nameOne1 == 151) { charNames[j] = charNames[j] + "N"; }
                    else if (nameOne1 == 152) { charNames[j] = charNames[j] + "O"; }
                    else if (nameOne1 == 153) { charNames[j] = charNames[j] + "P"; }
                    else if (nameOne1 == 154) { charNames[j] = charNames[j] + "Q"; }
                    else if (nameOne1 == 155) { charNames[j] = charNames[j] + "R"; }
                    else if (nameOne1 == 156) { charNames[j] = charNames[j] + "S"; }
                    else if (nameOne1 == 157) { charNames[j] = charNames[j] + "T"; }
                    else if (nameOne1 == 158) { charNames[j] = charNames[j] + "U"; }
                    else if (nameOne1 == 159) { charNames[j] = charNames[j] + "V"; }
                    else if (nameOne1 == 160) { charNames[j] = charNames[j] + "W"; }
                    else if (nameOne1 == 161) { charNames[j] = charNames[j] + "X"; }
                    else if (nameOne1 == 162) { charNames[j] = charNames[j] + "Y"; }
                    else if (nameOne1 == 163) { charNames[j] = charNames[j] + "Z"; }
                    else if (nameOne1 == 164) { charNames[j] = charNames[j] + "a"; }
                    else if (nameOne1 == 165) { charNames[j] = charNames[j] + "b"; }
                    else if (nameOne1 == 166) { charNames[j] = charNames[j] + "c"; }
                    else if (nameOne1 == 167) { charNames[j] = charNames[j] + "d"; }
                    else if (nameOne1 == 168) { charNames[j] = charNames[j] + "e"; }
                    else if (nameOne1 == 169) { charNames[j] = charNames[j] + "f"; }
                    else if (nameOne1 == 170) { charNames[j] = charNames[j] + "g"; }
                    else if (nameOne1 == 171) { charNames[j] = charNames[j] + "h"; }
                    else if (nameOne1 == 172) { charNames[j] = charNames[j] + "i"; }
                    else if (nameOne1 == 173) { charNames[j] = charNames[j] + "j"; }
                    else if (nameOne1 == 174) { charNames[j] = charNames[j] + "k"; }
                    else if (nameOne1 == 175) { charNames[j] = charNames[j] + "l"; }
                    else if (nameOne1 == 176) { charNames[j] = charNames[j] + "m"; }
                    else if (nameOne1 == 177) { charNames[j] = charNames[j] + "n"; }
                    else if (nameOne1 == 178) { charNames[j] = charNames[j] + "o"; }
                    else if (nameOne1 == 179) { charNames[j] = charNames[j] + "p"; }
                    else if (nameOne1 == 180) { charNames[j] = charNames[j] + "q"; }
                    else if (nameOne1 == 181) { charNames[j] = charNames[j] + "r"; }
                    else if (nameOne1 == 182) { charNames[j] = charNames[j] + "s"; }
                    else if (nameOne1 == 183) { charNames[j] = charNames[j] + "t"; }
                    else if (nameOne1 == 184) { charNames[j] = charNames[j] + "u"; }
                    else if (nameOne1 == 185) { charNames[j] = charNames[j] + "v"; }
                    else if (nameOne1 == 186) { charNames[j] = charNames[j] + "w"; }
                    else if (nameOne1 == 187) { charNames[j] = charNames[j] + "x"; }
                    else if (nameOne1 == 188) { charNames[j] = charNames[j] + "y"; }
                    else if (nameOne1 == 189) { charNames[j] = charNames[j] + "z"; }

                    else if (nameOne1 == 0xc2) { charNames[j] = charNames[j] + "-"; }
                    else if (nameOne1 == 0xc4) { charNames[j] = charNames[j] + "!"; }
                    else if (nameOne1 == 0xc5) { charNames[j] = charNames[j] + "?"; }
                    else if (nameOne1 == 0xbf) { charNames[j] = charNames[j] + "'"; }

                    else if (nameOne1 == 0x80) { charNames[j] = charNames[j] + "0"; }
                    else if (nameOne1 == 0x81) { charNames[j] = charNames[j] + "1"; }
                    else if (nameOne1 == 0x82) { charNames[j] = charNames[j] + "2"; }
                    else if (nameOne1 == 0x83) { charNames[j] = charNames[j] + "3"; }
                    else if (nameOne1 == 0x84) { charNames[j] = charNames[j] + "4"; }
                    else if (nameOne1 == 0x85) { charNames[j] = charNames[j] + "5"; }
                    else if (nameOne1 == 0x86) { charNames[j] = charNames[j] + "6"; }
                    else if (nameOne1 == 0x87) { charNames[j] = charNames[j] + "7"; }
                    else if (nameOne1 == 0x88) { charNames[j] = charNames[j] + "8"; }
                    else if (nameOne1 == 0x89) { charNames[j] = charNames[j] + "9"; }

                }
                nameBox1.Text = charNames[0];
                nameBox2.Text = charNames[1];
                nameBox3.Text = charNames[2];
                nameBox4.Text = charNames[3];
            }
            ///// End of NAMES section
            //////////////////////////////////


            //////////////////////////////////////
            ///// Read JOBS from file.
            ComboBox[] jobBoxList = defineJobBoxList();
            
            int[] jobValue = new int[4];
            string[] charJob = new string[4];            

            for (int i = 0; i < 4; i++)
            {
                stream.Position = 0x500 + (i*0x40);
                jobValue[i] = stream.ReadByte();
                
                for (int j = 0; j < 22; j++)
                {
                    if (jobValue[i] == j) { charJob[i] = jobNames[j]; }
                }

                jobBoxList[i].SelectedItem = charJob[i];               
            } 
            ///// End of JOBS section.
            //////////////////////////////////////

            ///////////////////////////////////////
            ///// Read LEVEL from file.
            stream.Position = 0x501;    charLevelBox1.Value = stream.ReadByte() + 1;
            stream.Position = 0x541;    charLevelBox2.Value = stream.ReadByte() + 1;
            stream.Position = 0x581;    charLevelBox3.Value = stream.ReadByte() + 1;
            stream.Position = 0x5c1;    charLevelBox4.Value = stream.ReadByte() + 1;
            ///// End of LEVEL section.
            ///////////////////////////////////////

            //////////////////////////
            ///// Read XP from file.
            stream.Position = 0x503;
            int xpValue1 = stream.ReadByte(); 
            int xpValue2 = stream.ReadByte(); 
            int xpValue3 = stream.ReadByte();
            charXP1.Value = xpValue1 + 256 * xpValue2 + 65536 * xpValue3; // Convert bytes to readable integer value

            stream.Position = 0x543;    
            xpValue1 = stream.ReadByte(); 
            xpValue2 = stream.ReadByte(); 
            xpValue3 = stream.ReadByte();
            charXP2.Value = xpValue1 + 256 * xpValue2 + 65536 * xpValue3; // Convert bytes to readable integer value

            stream.Position = 0x583;    
            xpValue1 = stream.ReadByte();
            xpValue2 = stream.ReadByte();
            xpValue3 = stream.ReadByte();
            charXP3.Value = xpValue1 + 256 * xpValue2 + 65536 * xpValue3; // Convert bytes to readable integer value

            stream.Position = 0x5c3;
            xpValue1 = stream.ReadByte();
            xpValue2 = stream.ReadByte();
            xpValue3 = stream.ReadByte();
            charXP4.Value = xpValue1 + 256 * xpValue2 + 65536 * xpValue3; // Convert bytes to readable integer value
            ///// End of XP section.
            //////////////////////////

            //////////////////////////
            ///// Read HP from file.
            stream.Position = 0x50c; 
            int hpValue1 = stream.ReadByte();
            int hpValue2 = stream.ReadByte();
            int hpMaxValue1 = stream.ReadByte();
            int hpMaxValue2 = stream.ReadByte();
            charHP1.Value = hpValue1 + 256 * hpValue2; // Convert bytes to readable integer value
            charMaxHP1.Value = hpMaxValue1 + 256 * hpMaxValue2; // Convert bytes to readable integer value

            stream.Position = 0x54c; 
            hpValue1 = stream.ReadByte();
            hpValue2 = stream.ReadByte();
            hpMaxValue1 = stream.ReadByte();
            hpMaxValue2 = stream.ReadByte();
            charHP2.Value = hpValue1 + 256 * hpValue2; // Convert bytes to readable integer value
            charMaxHP2.Value = hpMaxValue1 + 256 * hpMaxValue2; // Convert bytes to readable integer value

            stream.Position = 0x58c; 
            hpValue1 = stream.ReadByte();
            hpValue2 = stream.ReadByte();
            hpMaxValue1 = stream.ReadByte();
            hpMaxValue2 = stream.ReadByte();
            charHP3.Value = hpValue1 + 256 * hpValue2; // Convert bytes to readable integer value
            charMaxHP3.Value = hpMaxValue1 + 256 * hpMaxValue2; // Convert bytes to readable integer value

            stream.Position = 0x5cc; 
            hpValue1 = stream.ReadByte();
            hpValue2 = stream.ReadByte();
            hpMaxValue1 = stream.ReadByte();
            hpMaxValue2 = stream.ReadByte();
            charHP4.Value = hpValue1 + 256 * hpValue2; // Convert bytes to readable integer value
            charMaxHP4.Value = hpMaxValue1 + 256 * hpMaxValue2; // Convert bytes to readable integer value
            ///// End of HP section.
            //////////////////////////

            ///////////////////////////////////////
            ///// Read STATS from file.
            stream.Position = 0x512;    
            charStr1.Value = stream.ReadByte();
            charAgi1.Value = stream.ReadByte();
            charVit1.Value = stream.ReadByte();
            charInt1.Value = stream.ReadByte();
            charSpi1.Value = stream.ReadByte();

            stream.Position = 0x552;    
            charStr2.Value = stream.ReadByte();
            charAgi2.Value = stream.ReadByte();
            charVit2.Value = stream.ReadByte();
            charInt2.Value = stream.ReadByte();
            charSpi2.Value = stream.ReadByte();

            stream.Position = 0x592;    
            charStr3.Value = stream.ReadByte();
            charAgi3.Value = stream.ReadByte();
            charVit3.Value = stream.ReadByte();
            charInt3.Value = stream.ReadByte();
            charSpi3.Value = stream.ReadByte();

            stream.Position = 0x5d2;    
            charStr4.Value = stream.ReadByte();
            charAgi4.Value = stream.ReadByte();
            charVit4.Value = stream.ReadByte();
            charInt4.Value = stream.ReadByte();
            charSpi4.Value = stream.ReadByte();
            ///// End of STATS section.
            ///////////////////////////////////////

            ///////////////////////////////////////
            ///// Read X&Y from file.
            // Player
            stream.Position = 0x409;    
            charX.Value = stream.ReadByte();
            charY.Value = stream.ReadByte();

            // Airship
            stream.Position = 0x401; 
            int airshipXTest = stream.ReadByte() - 7;
            int airshipYTest = stream.ReadByte() - 7;

            if (airshipXTest < 0) { airshipX.Value = 0xFF + airshipXTest; }
            else { airshipX.Value = airshipXTest; }

            if (airshipYTest < 0) { airshipY.Value = 0xFF + airshipYTest; }
            else { airshipY.Value = airshipYTest; }

            stream.Position = 0x400;
                if (stream.ReadByte() == 0x01) { airshipCheck.Checked = true; }
                else { airshipCheck.Checked = false; }

            // Big Airship
            stream.Position = 0x405;
            bigAirshipX.Value = stream.ReadByte();
            bigAirshipY.Value = stream.ReadByte();
            stream.Position = 0x404;
                if (stream.ReadByte() == 0x01) { bigAirshipCheck.Checked = true; }
                else { bigAirshipCheck.Checked = false; }
            ///// End of X&Y section.
            ///////////////////////////////////////

            //////////////////////////////////////////
            ///// Read WORLD MAP from file.
            stream.Position = 0x408;
            int tempWorldID = stream.ReadByte();
            for (int i = 0; i < 5; i++)
            {
                if (tempWorldID == i) { worldMapBox.Text = worldNames[i]; } // World IDs are sequential from 0 - 4, so tempWorldID == i works. :)
            }
            ///// End WORLD MAP section.
            /////////////////////////////////////////

            ////////////////////////////////////////////////////////////
            ///// Read INVENTORY from file.
            ItemLoopRead(stream);
            ///// End INVENTORY section.
            ///////////////////////////////////////////////  
         
            ///////////////////////////////////////////
            ///// Read SAVE COUNT from file.
            stream.Position = 0x414;
            saveCountBox.Value = stream.ReadByte();
            ///// End SAVE COUNT section.
            ///////////////////////////////////////////

            ///////////////////////////////////////////
            ///// Read CRYSTAL LEVEL from file.
            stream.Position = 0x421;
            int tempCrystalLevel = stream.ReadByte();

            if (tempCrystalLevel == 0) { crystalLevelBox.Text = crystalLevels[0]; }
            else if (tempCrystalLevel == 1) { crystalLevelBox.Text = crystalLevels[1]; }
            else if (tempCrystalLevel == 3) { crystalLevelBox.Text = crystalLevels[2]; }
            else if (tempCrystalLevel == 7) { crystalLevelBox.Text = crystalLevels[3]; }
            else if (tempCrystalLevel == 15) { crystalLevelBox.Text = crystalLevels[4]; }
            else if (tempCrystalLevel == 31) { crystalLevelBox.Text = crystalLevels[5]; }
            ///// End CRYSTAL LEVEL section.
            ///////////////////////////////////////////

            ///////////////////////////////////////////
            ///// Read FOLLOWING from file.
            stream.Position = 0x40b;
            int followerID = stream.ReadByte();

            for (int i = 0; i < 9; i++)
            {
                if (followerID == i) { followerBox.SelectedItem = followerNames[i]; break; }
            }            
            ///// End FOLLWOING section.
            ///////////////////////////////////////////            
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "NES Save Files|*.sav";
            openFileDialog1.FilterIndex = 1;
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                
                
                using (var stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    ReadFromSRAM(stream);

                    labelFileLoaded.Text = "File loaded!";
                    labelFileLoaded.ForeColor = System.Drawing.Color.ForestGreen;

                    tabControl1.Enabled = true; // Enable Controls                 
                }                
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
         
        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Save all changes?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (var stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    WriteToSRAM(stream);                    
                }
            }
            if (result == DialogResult.No) { }
        }

        private void discardButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Discard all changes?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                using (var stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    ReadFromSRAM(stream);                    
                }
            }
            if (result == DialogResult.No) { }
        }

        private void moveToInvButton_Click(object sender, EventArgs e)
        {
            Label[] invSlotList = defineInvSlotList();
            NumericUpDown[] invQuanList = defineInvQuanList();
            
            string itemName = (string)listBox1.SelectedItem;
            int itemHexNum;
            for (int i = 0; i < 256; i++)
            {
                if (itemName == itemNames[i]){ itemHexNum = i; }
            }

            for (int i = 0; i < 32; i++)
            {
                if (invSlotList[i].Text == "-") { invSlotList[i].Text = itemName; invQuanList[i].Value = 1; break; }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.AddRange(itemNames);
            worldMapBox.Items.AddRange(worldNames);
            followerBox.Items.AddRange(followerNames);
            crystalLevelBox.Items.AddRange(crystalLevels);
        }

        private void rmZerosButton_Click(object sender, EventArgs e)
        {
            Label[] invSlotList = defineInvSlotList();
            NumericUpDown[] invQuanList = defineInvQuanList();

            for (int i = 0; i < 32; i++)
            {
                if(invQuanList[i].Value == 0) { invSlotList[i].Text = "-";}
            }            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }
    }
}