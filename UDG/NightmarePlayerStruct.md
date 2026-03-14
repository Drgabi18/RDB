# What is this?
I am completly in awe what whatever this structure is, or how it was even programmed in the first place. From address `0x1407b73d0` to `0x1407b744f` we have the addresses for global variables that actually control some crucial parts of the game (like the current character, the number of bullets etc.) The values here are laid out like this.
```cpp
// not the actual name
enum UDG_CanChangePlayerAndOtherStuff {
	CanChangeToAndFromGenocider = 1 << 4
	UNK = 1 << 6
}

/* 0x1407b73d0 */ UDG_CanChangePlayerAndOtherStuff PlayerChangeEnum;
/* 0x1407b73d4 */ short CurrentPlayer; // 1 - Komaru, 5 - Syo
/* 0x1407b73d6 */ short FirstChaser; // 1 - Komaru, 4 - Toko
/* 0x1407b73d8 */ short SecondChaser;
// ...
/* 0x1407b73dc */ byte KomaruLevel; // 1-99
/* 0x1407b73dd */ byte BigBullet; // that big bullet from time to time
/* 0x1407b73de */ short KomaruHealth; // 1-6
/* 0x1407b73e0 */ short SomeFuckingBullshitForClothesRip;
/* 0x1407b73e2 */ short CurrentBullet; // 0 - Break, 1 - Burn etc.
/* 0x1407b73e4 */ short BulletBreakAmmo;
/* 0xblah blah */ short BulletBurnAmmo;
/* 0xblah blah */ short BulletParalyzeAmmo;
/* 0xblah blah */ short BulletKnockbackAmmo;
/* 0xblah blah */ short BulletDanceAmmo;
/* 0xblah blah */ short BulletLinkAmmo;
/* 0xblah blah */ short BulletMoveAmmo;
/* 0x1407b73f2 */ short BulletDetectAmmo;
// ...
/* 0x1407b7440 */ short TokoBatteryPower; // how many batteries she has
// ...
/* 0x1407b7444 */ float TokoBatteryFloat;
// ...
```
That's fine and all, it's just how the game works. As you might have noticed however, they are not part of a struct or anything, they are just global variables in the code that simply get caled and changed (e.g. `66 89 3d e7 21 71 00		MOV word ptr [CurrentPlayer],DI` to change the `CurrentPlayer` value).
There's a problem however... the functions at `0x140099fc0` and `0x140099450`, which have a loop for a struct that starts at `0x1407b73d0` (the assembly instruction is `0f 28 02        MOVAPS     XMM0,xmmword ptr [0x1407b73d0]`). As you've seen above, those variables in the code are treated as global variables, however this function calls this region of code as if it's a struct.
So then... how the hell does this work?
Normally, if we were to make the above variables a struct, then the assembly for every single function called would absoluley not look like that, all instructions would have been executed with stuff like `MOV CL,[rdx+rcx*4]` to move something from the struct to where we want, but it isn't.

## First idea
My first idea was that that they did something like
```cpp
// Size: 0x80
struct UDG_Gamer {
	UDG_CanChangePlayerAndOtherStuff PlayerChangeEnum;
	short CurrentPlayer = 1;
	short FirstChaser = 0;
	short SecondChaser = 0;
	// ...
} OurPlayer;

OurPlayer.CurrentPlayer = 5
```
However, if we did this and looked at the dissasembly, we see code that calls the class constructor for `OurPlayer`, this doesn't exist at all in UDG's code, so it's not that.
Even after that, the line where we change the current player does include the `[rdx+rcx*4]` thing I was talking about.
This is not how they did it in the slightes, so it must mean it's another way.

## Second idea
Ok, it's still a struct, but what if it's done differently, what if they did it like this
```cpp
short CurrentID = 1;
short Chaser1 = 4;
short Chaser2 = 0;

// Size: 0x80
struct UDG_Gamer {
	UDG_CanChangePlayerAndOtherStuff PlayerChangeEnum;
	short CurrentPlayer = CurrentID;
	short FirstChaser = Chaser1;
	short SecondChaser = Chaser2;
	// ...
};
```
If we did it like this, then looks in the dissasembly, the result is similar to the game's code, we can change the global variable easy, and if we ever make a function that uses the struct, it will compose the instructions with offsets and all that Jazz.
Now the question is... why would you make it like this in the first place, what benefit does it offer over just not making it a struct in the first place, did different people write this part of the code?
...
OHHHHHHHHH... different people indeed wrote these parts of the code.

Funny thing is, I still have no idea what the functions at `0x140099fc0` and `0x140099450` even do, after the loop iterates 1614 times doing `MOV        EAX,1614; DEC        RAX; JNZ        LAB_140099472` they start to call the struct as if it's an array, but there's no way it's an array, as one of the variables following the end of the struct is `0x1407b7454` which is an `int` controlling how much money you have (and right after the number of retries, which fun fact maxes at 99).

This nicely reveals that all of the variables from above are all in a single file too, however, out of all things, the variable that controls if Komaru holds a weapon or her running animaion confidence isn't here... I still have no idea where they are.
