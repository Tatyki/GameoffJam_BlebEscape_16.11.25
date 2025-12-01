EXTERNAL Call(objectName, scriptName, methodName, param)

== Start ==
#name Spider
Hey you
Yeah, you, geek-scent deluxe
Took you long enough to show up
We’ve been grinding here, setting up all the crap you’re gonna need
You do know what’s happening, right? …Right?
-> My_Choices

== My_Choices ==
#color orange
#name none
 + [Rage quit (Ready to suffer)] -> Play
 + [You who? (Play training. You need it)] -> Lore

== Play ==
#name Spider
If you thought this was the exit button, buddy, I’ve got some tragic news for you
If you didn’t - cool, got you, random internet wanderer
Now off you go: save the Bleb Nation from getting wiped off the face of existence
And hey, don’t forget: 
#anim BackGround_DisappearBackgroundAnim
Our whole nation’s riding on your shaky shoulders
-> ToSecondLevelScene

== Lore ==
#name Spider
Alright, listen closely: I’m not repeating myself
I’m about to drop the one and only TRUE story of the greatest spider dynasty that’s been running this place for decades
You are listening, right?
On top of summoning geek spirits, we can also brew up curses that’ll have you hunting for a bathroom like your life depends on it
What? Why do box spiders have more authority than you?
Should’ve listened to your mom more - think about
Oh yeah, go on, try saying that the apple you ditched in the container is still in there
Anyway, we’ve got way more important stuff going down right now
More important than you and me
#anim BackGround_DisappearBackgroundAnim
And here’s how it all went down:
For the first conquerors, this land was a paradise oasis
#anim ComicImage_ImageAppearAnim
~ Call ("ComicController", "ComicController", "SetComic", "1")
From the majestic depths of basement funk, leftover munchies and those uranium-vibe crystals scavenged from cat litter,
our scientists finally cooked up their most peak-performance baby
~ Call ("ComicController", "ComicController", "SetComic", "1")
Perfect
Innovative
Dumb as a brick
Or, well, simply flawless
And its name was . . .
~ Call ("ComicController", "ComicController", "SetComic", "1")
BLEB
~ Call ("ComicController", "ComicController", "SetComic", "1")
And, well, we lived like this peacefully for a long time.
Or rather, until last week, when it appeared in the house
~ Call ("ComicController", "ComicController", "SetComic", "1")
A stinky monster
~ Call ("ComicController", "ComicController", "SetComic", "1")
For the record, it didn’t stay like that for long
But honestly, it didn’t get any better after that either
~ Call ("ComicController", "ComicController", "SetComic", "1")
Its own death gave rise to hellish creatures
For simplicity, we call them The Flies
But don't let them fool you so easily
~ Call ("ComicController", "ComicController", "SetComic", "1")
These fiends of someone's sick mind, in their brief existence, managed to build their fascist regime and arrange a genocide for our blebs.
"A nationality cannot be built on stupidity"
What nonsense
~ Call ("ComicController", "ComicController", "SetComic", "1")
Nevertheless, we managed to preserve a small number of survivors in a temporal refuge
But we must hurry
Rumors in our society spread very fast
Now you are the hope of our entire nation
Lead the survivors to the bunker, where nothing can threaten them
If you succeed, you will receive the gratitude of the great dynasty
Do not fail our nation
~ Call ("ComicController", "ComicController", "SetComic", "1")
-> ToEducationScene


== ToEducationScene ==
~ Call ("SceneChanger", "DiaSceneChanger", "StartEducationComic", "1")
-> END

== ToSecondLevelScene ==
~ Call ("SceneChanger", "DiaSceneChanger", "StartSecondLevel", "1")
-> END