**NOTE: More research is needed, this are descriptions from what is accesiable at the surface level.**

# In-World Debug Menus
These are the menus that affect only objects inside the world, these appear in the Top Left corner of the screen.

## Top Menu Debug Menu
```
DEBUG : TOP_MENU
CIRCLE    : RETURN
CROSS     : END
CAMERA
BG MODEL
CHARA MODEL
OBJ MODEL
TARGET POINT
LIGHT
PROJECTOR
```
Used to select which sub-menu you wanna go in. `TARGET POINT` and `PROJECTOR` don't do anything.

## Camera Debug Menus
### Mode 1
```
DEBUG : MOVE CAMERA
Len    : %.2f
PosY   : %.2f
RotZ   : %.2f
Fov    : %.2f
```
Is the Mode accesible through TOP_MENU, only used to report info, can't be used to change info.

### Mode 2
```
DEBUG : MOVE2 CAMERA
Rot Len: %.2f
RotZ U : %.2f
RotZ D : %.2f
PosX L : %.2f
PosX R : %.2f
PosZ F : %.2f
PosZ B : %.2f
Fovy   : %.2f
mov_foot      : %.2f
mov_foot_sp   : %.2f
mov_delay_rot : %.2f
mov_rot_y     : %.2f
mov_rot_len   : %.2f
mov_rot_pos   : %.2f
mov_rot       : %.2f
mov_delay     : %.2f
```
Is accesible by enabling a Debug flag. Used in 3D spaces. All `mov_*` proprieties just change the view bobbing.

### Mode 3
```
DEBUG : MOVE2 CAMERA
Rot Len1: %.2f
Rot Len2: %.2f
PosY   : %.2f
Fovy   : %.2f
mov_c_len   : %.1f
map_c_pos.x : %.1f
map_c_pos.y : %.1f
map_c_pos.z : %.1f
```
Is accesible by enabling a Debug flag. Is used in rooms with a locked perspective where your character rotates around the center point (e.g. The Entrance to the gym in DR1)

### Mode 4
```
DEBUG : MOVE2 CAMERA
Rot Len: %.2f
RotZ U : %.2f
RotZ D : %.2f
PosX L : %.2f
PosX R : %.2f
PosY   : %.2f
PosZ F : %.2f
PosZ B : %.2f
Fovy   : %.2f
mov_foot      : %.2f
mov_foot_sp   : %.2f
```
Is accesible by enabling a Debug flag. Is used in rooms with a locked perspective where your character's POV simulates looking left and right (e.g. The park in Strawberry House in DR2).

### Mode 5
```
DEBUG : CAMERA POS MOVE
pos : %.2f:%.2f:%.2f
rot : %.2f:%.2f
ANAROG    : MOVE
TRI+ANAROG: LOOK
UP DOWN   : UP DOWN
L R       : ROT
SQUARE    : RESET
```
Is unaccesible unless you replace a function call to point to it. Is used to move the camera in rooms with a locked perspective.

### Mode 6
```
DEBUG : CAMERA FOVY
fovy  : %.2f
rot_up: %.2f
rot   : %.2f
UP DOWN   : FOVY
LEFT RIGHT: ROT UP
L R       : ROT
SQUARE    : RESET
CIRCLE   : MODE CHANGE
CROSS    : END
```
Is unaccesible unless you replace a function call to point to it. Is used to change the FOV in rooms with a locked perspective.

## BG Model Debug Menu
```
< >       : FILE CHANGE
File No : %d
File No : Non
< >       : SCALE
Scale : %.2f
< >       : ANTI
Anti  : OFF
Anti  : ON
< >       : OFFSCREEN
OffScr : OFF
OffScr : ON
< >       : OVERLAY
Overlay : OFF
Overlay : ON
SQUARE  +< >: R COLOR
TRIANGLE+< >: G COLOR
CIRCLE  +< >: B COLOR
         < >: A COLOR
 Color : %02x%02x%02x%02x
SQUARE  +< >: R COLOR
TRIANGLE+< >: G COLOR
CIRCLE  +< >: B COLOR
         < >: A COLOR
 Color : %02x%02x%02x%02x
< > : ALPHA
 Mode : %d
< > : Slope
Slope : %.3f
DEBUG : BG MODEL
CROSS     : END
```
Used to Debug how Rooms look when loaded.

* `File no.` - controls the currently loaded `bg_*.pak` map
* `Scale` - controls the scale of the map, unused in normal gameplay
* `Anti` - controls if effects like transparency or vertex color is applied at the engine level, only works in the DR1 DEMO
* `Overlay` - adds a gradient to the screen, top to bottom; can toggle between Addition, Subtraction blending modes; is used extensively in the 2nd game for the sunny feel
* `Mode`, `OffScr` - unknown behaivour
* `Slope` - is completly unused with the variable only used once in this Menu

## Chara Model Debug Menu
```
DEBUG : CHARA MODEL
Chara No : %d
Type  : Non
Type  : %d
Pos   : %d:%d
PosY  : %d
Scale : %f
Pri   : %d
Billbord : %d
Rot   : %.2f
< >       : CHARA CHANGE
< >       : TYPE CHANGE
ANAROG    : POS
< >       : POS Y
ANAROG    : POS Y
< >       : SCALE
< >       : PRI
< >       : BILLBORD
< >       : ROT
CROSS     : END
```
Used to Debug how Characters look when loaded. If the Character Type is set to 32 or 98, the object is deleted from the world. The character has to exist in the world to change said options on it. The anchor of the characters is at their feet.

* `Chara No` - is used to select the character in the array
* `Type` - is used to set the expression
* `Pos` - is X and Z position on the floor, can be moved with the analog stick
* `PosY` - is used to set the offset off the floor from their anchor, no in-game scenario uses this, can be moved with the analog stick
* `Scale` - is used to scale up the characters from their anchor, no in-game scenario uses this
* `Pri` - is used to set a priority of sorts, setting it to anything other than 0, when you click anything else in the world, the camera glitches back to focus on the character with the highest priority
* `Billbord` - is misspelt, used to set if the character is facing you

## Obj Model Debug Menu
```
DEBUG : OBJ MODEL
Obj No : %d
File  : Non
File  : %d
Pos   : %d:%d
PosY  : %d
Scale : %d
ScaleZ: %d
Rot   : %d
RotZ  : %d
Anime : -- / --
AnimeLoop : --
Disp : %d
< >       : CHARA CHANGE
< >       : TYPE CHANGE
ANAROG    : POS
< >       : POS Y
< >       : SCALE
< >       : ROT
< >       : ANIME NO
< >       : ANIME LOOP
< >       : DISP
```
Used to debug objects you can click in the world like doors, cameras, furniture etc. This creates a Square/Circle behind the objects, which represents the area in which you can select it. When the `Obj No` is at the index of said object, the background pulsates orange. When you hover over an object, the background pulsates light blue.

* `Obj No` - is used to select the object in the array
* `File` - is used to select what type the object is, this changes if the object has a square or cicle behind it
* `Pos`, `PosY`, `Scale`, `Scale`, `Rot`, `RotZ` - is pretty self explanatory
* `Anime`, `AnimeLoop` - unknown behaivor, may have been used for the animation when you enter a room
* `Disp` - is used to set if the object is selectable and visible in the world

## Light Debug Mode
```
CROSS     : END
DIRECTION
POINT
SPOT
DIFFUSE
DIFFUSE & SPECULAR
POWERED DIFFUSE
DEBUG : LIGHT
Init  : OFF
Init  : ON
Init  : ON+Chara
 Color : %02x %02x %02x
 Specular : %.2f
 [Sub Light : %d]
   Type : %s
   Mode : %s
   Ambient : %02x %02x %02x
   Diffuse : %02x %02x %02x
   Specular: %02x %02x %02x
   Rot : %.2f %.2f
   Pos : %.2f %.2f %.2f
   Att : %2.2f %.3f %.3f
   SPOT: %.2f %.2f
   SPOT: S %.2f C %.2f
< >       : INIT CHANGE
< >       : COLOR CHANGE
SQUARE    : R COLOR
TRIANGLE  : G COLOR
CIRCLE    : B COLOR
< >       : SPECULAR CHANGE
< >       : LIGHT NO CHANGE
< >       : TYPE CHANGE
< >       : MODE CHANGE
< >       : COLOR CHANGE
SQUARE    : R COLOR
TRIANGLE  : G COLOR
CIRCLE    : B COLOR
ANAROG    : ROT CHANGE
ANAROG    : POS CHANGE
< >       : ATT CHANGE
SQUARE    : ATTR A
TRIANGLE  : ATTR B
CIRCLE    : ATTR C
ANAROG    : ROT CHANGE
< >       : DATA CHANGE
SQUARE    : S
TRIANGLE  : C
CROSS     : END
```
Used to debug a Light System that is not used in the game. The game always sets this to off. When changing it to ON, the entire world becomes black and you can add up to 4 sub lights. Implementations of the lights are rather subpar, as they only use the vertex colors to simulate light.

## Projector Debug Menu

```
NOISE
CAMERA
TEX_LOCAL
MODEL_ROT
WORLD_ROT
VER_MODEL
VER_WORLD
VER_WORLD_ORTHO
VER_WORLD_PERSPEC
< >       : FILE CHANGE
File No : %d
File No : Non
< >       : ANIME CHANGE
Anime No : %s
< >       : REPEAT CHANGE
Repeat   : %s
< >       : LINEAR CHANGE
Linear   : %s
< >       : ALPHA CHANGE
Alpha    : %d
ANAROG    : POS CHANGE
Pos      : %d:%d
ANAROG    : ROT XY CHANGE
< >       : ROT Z CHANGE
Rot      : %.2f:%.2f:%.2f
ANAROG    : ROT XY CHANGE
< >       : ROT Z CHANGE
From Rot : %.2f:%.2f:%.2f
< >       : SCALE CHANGE
Scale    : %.2f
< >       : GEN CHANGE
Gen      : %d %s
CROSS     : END
```
Honestly, no clue, allows you to select a file (most likely object type) and change an animation value on said type (repeating from 0 to 255 or at random). Seemingly does nothing in-game.

## Target Point Debug Menu
Does nothing. If it worked, it may have been used to test what happens currently when you set the Priority bool on the characters, but for other points of interest

# Game Debug Menus
These are the Debug Menus that affect the game state and not elements in the world. Can be accessed on the MacOS version by just pressing Q+E and V. Values changed by it are the same values that end up in the save file.

## `Episode` Debug Menu
### DR1
```
0  1  2  3  4  5  6  7
%01d  %01d  %01d  %01d  %01d  %01d  %01d  %01d 
```
Only has 0 and 1 for each episode, the game sets the episode value to 1 after each chapter ends. This basically just tells the New Game menu to open up the episode.

### DR2
```
E  N  H  S
0  %01d  %01d  %01d
1  %01d  %01d  %01d
2  %01d  %01d  %01d
3  %01d  %01d  %01d
4  %01d  %01d  %01d
5  %01d  %01d  %01d
6  %01d  %01d  %01d
7  %01d  %01d  %01d
8  %01d  %01d  %01d
S  %01d  %01d  %01d
N  %01d  %01d  
```
Is more granular, this also changes what you can do in the New Game, allows you to change if you can start a New Game (N), a Deadly Life (H) or a Trial (S, from Saiban)

## `Poket_Book` Debug Menu
```
POKETBOOK %d
      MAP %d
 KOTODAMA %d
     SAVE %d
 MAP_JUMP %d
      PET %d
 MONOKUMA %d
MAPOPEN00 %d %d %d %d %d %d %d %d %d %d
MAPOPEN10 %d %d %d %d %d %d %d %d %d %d
MAPOPEN20 %d %d %d %d %d %d %d %d %d %d
RULEOPEN0 %d %d %d %d %d %d %d %d 
RULEOPEN8 %d %d %d %d %d %d %d %d 
```
(DR2 ONLY) Is used to set if you can access Menus when you press F1
* `POKETBOOK` - not visible in the menu, sets if you can 
* `MAP` - sets if you can access the map, can be used in cases where it's disabled (like class trials)
* `KOTODAMA` - sets if you can access the Truth Bullets menu, hangs forever if not in Investigation Mode
* `SAVE` - sets if you can save
* `MAP_JUMP` - sets if you are allowed to teleport
* `PET` - Sets if you can access the Pet Menu
* `MONOKUMA` - Unk
* `MAPOPEN00` - Sets what Floors (DR1)/Maps (DR2) you can access, `MAPOPEN10` and `MAPOPEN20` only exist in DR2 becase... you know
* `RULEOPEN0`, `RULEOPEN8` - This actually doesn't set what rule you can see, it just counts how many 1s there are and allows you to access X many indexes, by making all the rules 1, the last four pages are completly white.

## `Mode_Open` Debug Menu
```
MONOMI   %01d
SURVIVAL %01d
NOVEL    %01d
EVENT    %01d
MOVIE    %01d
ARTWORK  %01d
SOUND    %01d
USAMI    %01d
MONOMONO %01d
HANBAIKI %01d
```
(DR2 ONLY) Used to set what menus are availiable to be opened in the Main Menu. 
* `USAMI` is only in DR2. 
* `MONOMONO` is the gacha capsule BS
* `HANBAIKI` is the vending machine

## `Script_Debug` Debug Menu
### DR1
```
POKETBOOK %d
      MAP %d
 KOTODAMA %d
     SAVE %d
 MAP_JUMP %d
  MAPOPEN %d %d %d %d %d %d %d %d %d %d
RulePageLimt %d
SkillPoint   %d
SkillPointUs %d
MonoMono     %d
MainMenuLoad %d
Action_Dif   VERY EASY
Action_Dif   EASY
Action_Dif   NORMAL
Inferenc_Dif VERY EASY
Inferenc_Dif EASY
Inferenc_Dif NORMAL
Monokuma Medal %03d
Hispeed Skip %d
```
In DR1, the previous 2 menus from above didn't exist, thus, those options are in this menu. `MAPOPEN` allows you to access all floors on the MAP (but not unused ones, there's no code for them), spots where you can teleports are glitched but using them crashes the game.

### DR2
```
SkillCoin    %d
SkillPoint   %d
SkillPointUs %d
monomonoyashin %03d
EXP            %06d
SET_MAP_NO     %03d
W[19] %05d
W[20] %05d
W[22] %05d
MainMenuLoad %d
Action_Dif   VERY EASY
Action_Dif   EASY
Action_Dif   NORMAL
Inferenc_Dif VERY EASY
Inferenc_Dif EASY
Inferenc_Dif NORMAL
Monokuma Medal %03d
[%d] %04d %04d %04d %04d %04d %04d %04d %04d
[%03d]%04d %04d %04d %04d %04d %04d %04d %04d
KKRM %04d %04d %04d %04d %04d %04d %04d %04d
```
Oh boy. I'll just be honest and say I don't understand these, I'll note the only ones I understand.
* `W[19]`, `W[20]`, `W[22]` - is used to set a bitmask flag for story flags, for example if you talked to a Character, it sets a flag for it so next time you have a dialouge with them, the .lin file checks that and skips the dialouge so it doesn't repeat what they've said again. A flag is also set for 
* `KKRM` - unknown
* `MainMenuLoad` - is a bit useless, for example, when you start the game, you can't really access the Main Menu in the prolouge. This value only reports that value, but changing it does nothing, changing it aftet you have the menu availiable also does nothing.
* `SET_MAP_NO` - unknown, most likely sets flags for what should be visible in maps

## `Adv_Kotodama` Debug Menu
```
   00 01 02 03 04 05 06 07 08 09
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d 
```
Represents which Truth Bullets are visible in the menu and in-game when you have to select them. At the start of a Chapter or Trial, all the bullets' corresponding IDs are set to `-1` and get set to `1` or `2` while the Trial happens.

## `Adv_Item` Debug Menu
```
%02d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d 
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d 
```
Represents what gifts you have collected. 

Has special code in DR1 where you can increment all items by pressing a button
```
ALL Increment is SQUARE Button
ALL Decrement is SQUARE Button + L Button
```

Now I know this is loser shit, but they programmed panties different between games. 

In DR2 they are normal items, maximum is 99. You can set the character gifts, such as... *(sighs)* the number of panties to 99. But in DR1 they are special tiems, as such you can't get them to 99 and instead can only have one, which are special in this menu and have
```
Pa %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d 
Pa %02d %02d %02d %02d 
```
in DR1 only.

## `Adv_Skill` Debug Menu
```
%02d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d 
```
Represents what Skills are availiable on Makoto's Report Card in DR1 and the Usami Menu in the Report Card in DR2... what you forgot that existed? Anyways the skills also get unlocked by completing.

Has special code in DR1 where you can set all skills by pressing a button
```
ALL SET is SQUARE Button
```

## `Adv_SkillEqu` Debug Menu
```
   00 01 02 03 04 05 06 07 08 09
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d 
```
Is used to equip the skills, regardless of if you've unlocked them in the menu above or in-game.

Has special code in DR1 where you can set all skills by pressing a button
```
ALL SET is SQUARE Button
```

## `Adv_Prof Lev` Debug Menu
```
%03d %02d 
```
Unknown, related to the Report Card, setting this to higher than 6 adds a star (DR1)/golden petal (DR2) to the Character Report Card. Has garbage strings at the top.

## `Adv_Prof Goo` Debug Menu
```
%03d %02d 
```
Unknown, related to the Report Card, setting this to higher than 6 adds a star (DR1)/golden petal (DR2) to the Character Report Card. Has garbage strings at the top.

## `Adv_Report` Debug Menu
```
%03d %02d 
```
Used to set how many pages are availiable to read for which character in the Report Card. Has garbage strings at the top.

## `Adv_Pet` Debug Menu
```
pet_no                %01d
pet_unchi_walk    %05d
pet_despair_walk  %05d
pet_next_walk     %05d
pet_total_walk    %05d
pet_unchi             %01d
pet_hope             %02d
pet_despair          %02d
pet_status            %01d
pet_first             %01d
pet_glow              %01d
```
(DR2 ONLY) Used to set various settings for the useless Tomogachi distraction with Usami. `pet_no` is outside the screen.

## `Surv_Char` Debug Menu
```
GoTikt  %02d
TDays  %03d
ENDF0-   %d   %d   %d   %d   %d   %d   %d   %d
ENDF8-   %d   %d   %d   %d   %d   %d   %d   %d
Good0- %03d %03d %03d %03d %03d %03d %03d %03d
Good8- %03d %03d %03d %03d %03d %03d %03d %03d
Strs0- %03d %03d %03d %03d %03d %03d %03d %03d
Strs8- %03d %03d %03d %03d %03d %03d %03d %03d
Spot0- %03d %03d %03d %03d %03d %03d %03d %03d
Spot8- %03d %03d %03d %03d %03d %03d %03d %03d
```

## `Surv_Materia` Debug Menu
```
%02d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
Yes, they ate the L, deserved after making UDG, allows you to set how many of each material in School/Island Mode.

## `Surv_Item` Debug Menu
```
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
Unsure of what this does as I've never played School/Island Mode past the second objective. Does not seem to set Trip Tickets in here.

## `Gyal_Event` Debug Menu
```
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
Allows you to select which flashes are visible in the Extra Event menu.
* `0` - represents unknown/unfound
* `1` - represents found, you have to pay for it
* `2` - represents found, you have paid for it

## `Gyal_Movie` Debug Menu
```
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
Allows you to select which movies are visible in the Extra Movie menu.
* `0` - represents unknown/unfound
* `1` - represents found, you have to pay for it
* `2` - represents found, you have paid for it

## `Gyal_Art` Debug Menu
```
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
Allows you to select which artworks are visible in the Extra Artwork menu.
* `0` - represents unknown/unfound
* `1` - represents found, you have to pay for it
* `2` - represents found, you have paid for it

## `Gyal_Bgm` Debug Menu
```
%03d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
Allows you to select which songs are visible in the Extra Music menu.
* `0` - represents unknown/unfound
* `1` - represents found, you have to pay for it
* `2` - represents found, you have paid for it

## `Usami_mode` Debug Menu
```
%02d %02d %02d %02d %02d %02d %02d %02d %02d %02d %02d
```
(DR2 ONLY) Let's you set what items you can find/buy/use in the Usami minigame.
* `0` - represents unknown/unfound
* `1` - represents found, you have to pay for it
* `2` - represents found, you have paid for it

# School
## DR1
```
 0:Makoto Naegi
 1:Kiyotaka Ishimaru
 2:Byakuya Togami
 3:Mondo Ohwada
 4:Leon Kuwata
 5:Hifumi Yamada
 6:Yasuhiro Hagakure
 7:Sayaka Maizono
 8:Kyoko Kirigiri
 9:Aoi Asahina
10:Toko Fukawa
11:Sakura Ohgami
12:Celestia
13:Junko Enoshima
14:Chihiro Fujisaki
15:School Ending (Good)
16:School Ending (Bad1)
17:School Ending (Bad2)
1:e08 Event
2:e09 Event [music room]
3:e09 Event [rec room]
4:e09 Event [school store]
5:e09 Event [garden]
6:e09 Event [dinig hall]
7:e09 Event [library]
8:e09 Kokoronpa
9:e09 Ending
Push CIRCLE BUTTON to Start
%s [Type.%d]
Select L / R Button
Change Level L / R Pad
```
Allows you to play respective story beats from School Mode, Type 49 and 50 seem to be Kokoronpa and Ending for the respective characters. Works but the index is off by one. It's impossible to understand what L/R do for changing the level and selecting the type.

## DR2
```
 0:Hajime Hinata
 1:Nagito Komaeda
 2:Byakuya Togami
 3:Gundham Tanaka
 4:Kazuichi Soda
 5:Teruteru Hanamura
 6:Nekomaru Nidai
 7:Fuyuhiko Kuzuryu
 8:Akane Owari
 9:Chiaki Nanami
10:Sonia Nevermind
11:Hiyoko Saionji
12:Mahiru Koizumi
13:Mikan Tsumiki
14:Ibuki Mioda
15:Peko Pekoyama
16:Island mode Ending
1:e08 Event
2:e09 Event [Jabberwock Park]
3:e09 Event [the beach]
4:e09 Event [the library]
5:e09 Event [the movie theater]
6:e09 Event [Nezumi Castle]
7:e09 Event [the military base]
8:e09 Kokoronpa
9:e09 Ending
Push CIRCLE BUTTON to Start
%s [Type.%d]
Select L / R Button
Change Level L / R Pad
```
Allows you to play respective story beats from School Mode, Type 49 and 50 seem to be Kokoronpa and Ending for the respective characters. Crashes after selecting anything. It's impossible to understand what L/R do for changing the level and selecting the type.

## Oh yeah some menus also have this text
```
 [maru++]
 [sankaku--]
```

# Beta Main Menu
(DR1 and DR1 DEMO only) As seen in the first Development Trivia video. These are all the strings for it. Again, this menu has never been worked on more than what you saw.
```
PRESS ANY BUTTON
NEW GAME
LOAD GAME
OPTION
DATA INSTALL
MAIN MENU
CONTINUE
CHAPTER SELECT
EXTRA
ADHOC MODE
```
