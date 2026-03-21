# What is this?
I am completly in awe what whatever this structure is, or how it was even programmed in the first place. From address `0x1407b73d0` to `0x1407b744f` we have the addresses for global variables that actually control some crucial parts of the game (like the current character, the number of bullets etc.) The values here are laid out like this.

```cpp
// not the actual name
enum UDG_CanChangePlayerAndOtherStuff {
	CanChangeToAndFromSyo = 1 << 3
}

/* 0x1407b73d0 */ static UDG_CanChangePlayerAndOtherStuff PlayerChangeEnum;
/* 0x1407b73d4 */ static short CurrentPlayer; // 1 - Komaru, 5 - Syo
/* 0x1407b73d6 */ static short FirstChaser; // 1 - Komaru, 4 - Toko
/* 0x1407b73d8 */ static short SecondChaser;
// ...
/* 0x1407b73dc */ static byte KomaruLevel; // 1-99
/* 0x1407b73dd */ static byte BigBullet; // that big bullet from time to time
/* 0x1407b73de */ static short KomaruHealth; // 1-6
/* 0x1407b73e0 */ static short SomeFuckingBullshitForClothesRip;
/* 0x1407b73e2 */ static short CurrentBullet; // 0 - Break, 1 - Burn etc.
/* 0x1407b73e4 */ static short BulletBreakAmmo;
/* 0xblah blah */ static short BulletBurnAmmo;
/* 0xblah blah */ static short BulletParalyzeAmmo;
/* 0xblah blah */ static short BulletKnockbackAmmo;
/* 0xblah blah */ static short BulletDanceAmmo;
/* 0xblah blah */ static short BulletLinkAmmo;
/* 0xblah blah */ static short BulletMoveAmmo;
/* 0x1407b73f2 */ static short BulletDetectAmmo;
// ...
/* 0x1407b7440 */ static short TokoBatteryPower; // how many batteries she has
// ...
/* 0x1407b7444 */ static float TokoBatteryFloat; // 100.0 to 0.0
// ...
```
That's fine and all, it's just how the game works. As you might have noticed however, they are not part of a struct or anything, they are just global variables in the code that simply get caled and changed (e.g. `66 89 3d e7 21 71 00		MOV word ptr [CurrentPlayer],DI` to change the `CurrentPlayer` value).

There's a problem however... the functions around `0x140099fc0` (which I called `UDG_TradeStruct_1`, they are used when loading, saving and changing the map because the game always copies the player by value for some parts of the game), which have a loop for a struct that starts at `0x1407b73d0`, the player change enum from above, and the struct size is `0x80` bytes, the assembly instruction for it is `0f 28 02        MOVAPS     XMM0,xmmword ptr [0x1407b73d0]` (with the pointer being loaded in a `RAX` above before this).

If we look at how save files works, all they do is just save the next `0x32868` bytes from `0x1407b7320`, it's certinately a choice but this is how it's done. So like, in these `0x32868` bytes, it must mean that everything in here has to all come from a single class/sturct/namespace just so it makes snese that everything is stored sequentially, right?

As you've seen above, those variables in the code are treated as global variables, however the `UDG_TradeStruct_1` function calls this region of code as if it's in a struct. So then... how the hell does this work?

Normally, if we were to make the above variables a struct, then the assembly for every single field called would absoluley not look like that, all instructions would have been executed with stuff like `MOV CL,[rdx+rcx*4]` to move something from the struct to where we want, but it isn't.

## First idea
My first idea was that that they did something like:

```cpp
// Size: 0x80
static struct UDG_Gamer {
	UDG_CanChangePlayerAndOtherStuff PlayerChangeEnum;
	short CurrentPlayer = 1;
	short FirstChaser = 0;
	short SecondChaser = 0;
	// ...
} OurPlayer;

OurPlayer.CurrentPlayer = 5
```

However, at least on the latest MSVC x86_64 dissasembly (UDG was done in VS 2012 but it's not on Compile Explorer sooo...), we see in the code that it calls the class constructor for `OurPlayer` variable, this doesn't exist at all in UDG's code, so it's not this (and even if it was, somehow not even the leaked symbol names from the classes have constructors like this... which means Ghidra may not decompile them correctly).

Even after that, the line where we change the current player does include the `[rdx+rcx*4]` thing I was talking about, so it means they didn't use this method.

This is not how they did it in the slightest, so it must mean it's in another way.

## Second idea
Ok, it's still a struct, but what if it's done differently, what if they did it like this:

```cpp
short CurrentID = 1;
short Chaser1 = 4;
short Chaser2 = 0;

// Size: 0x80
static struct UDG_Gamer {
	UDG_CanChangePlayerAndOtherStuff PlayerChangeEnum;
	short CurrentPlayer = CurrentID;
	short FirstChaser = Chaser1;
	short SecondChaser = Chaser2;
	// ...
};
```

If we did it like this, then looks at MSVC's dissasembly, the result is similar to the game's code, we can change the global variable easy, and if we ever make a function that uses the struct, it will use `ptr word [CurrentID]`, so we're probably pretty close. Is the preprocessor somehow removing the vaiables ouside the struct though?

Now the question is... why would you make it like this in the first place, what benefit does it offer over just not making it a struct in the first place, did different people write this part of the code?

...

OOOOHHHHHHHHH... different people indeed wrote these parts of the code at different times.

## Third secret idea
Ok but what if it's `(UDG_Gamer*)&PlayerChangeEnum`, then that mea- *\*gets dragged offstage by a very long cane\**

# All of this still has me questioning
Funny thing is, I still have no idea what the functions that look similar to the one I named `UDG_TradeStruct_1` even do individuall, again they do trigger due to loading, map change, retry etc. and exist to just free the entity before spawning a new one, but I can't be arsed to find which one is which. They also exist for the `.savinf` file to save transcripts and other bullshit that file has.

I guess I can note some funny things after the loop iterates 1614 times (not kidding btw, `MOV        EAX,1614; DEC        RAX; JNZ        LAB_140099472`) they start to call the struct as if it's an array, but there's no way it's an array, as one of the variables following the end of the struct is `0x1407b7454` which is an `int` controlling how much money you have (and right after the number of retries, which fun fact maxes at 99). It makes sense why it stops at the place where it stops though, it's `0x80` bytes in size and everything inside it seems to be related ONLY to the player.

This nicely reveals that all of the variables from above (the ones inside the `UDG_Gamer` struct and then money, retries, unlockables) are all in a single header too, for whatever deranged soul decompiles this game or reimplements this.

This still raises the quesion though, if for the save file all the game does is just save N number of bytes from a location (`memcpy(&DAT_1407b7320,DAT_1403407d0,0x32860)`), then still, how the hell is it keeping all of these toghether so they are in the order in which the game can deserialize them? I haven't even questioned, do instances where it uses the `mov` instuction which use the address value work the way they do because the variable is local? It's obvious they stay in the same place because of the `static` keyword, but what is the entire thing?

Anyways thanks for coming to my Ted Talk and I wish to never use C++ again.