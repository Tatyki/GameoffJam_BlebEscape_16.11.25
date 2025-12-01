EXTERNAL Call(objectName, scriptName, methodName, param)

== Start ==
#name Spider
Look, it's simple here
To command a member of the great dynasty, you need to focus your thought energy on it and give it your order
. . .
No dice?
Fine, whatever. Point at it and squeeze that fluffy mammal imitation on your desk
I've heard other geeks call this RMB
~ Call("ComicController", "ComicController", "SetComic", "1")
No idea what that is
-> END

== SpiderActive ==
#name Spider
Hey, not bad, you're catching on.
Check it out – our dynasty dude here is all pumped full of juice
Means he's good to go and take your orders
~ Call ("ComicController", "ComicController", "SetComic", "1")
Point at him again and keep that same mouse button mashed down. Don't let go
-> END

== SpiderWasMoved ==
#name Spider
Boom. Nailed it
Now, let's kick it up a notch
Aim for that corkboard button on the side and this time, slap the left side of your mouse
~ Call("ComicController", "ComicController", "SetComic", "1")
Left, right, who cares? Just keep clicking like your life depends on it until stuff happens
-> END

== WebIsActive ==
#name Spider
If your mind went to the gutter, that's a you problem
And don't even ask where this thing comes from
Anyway, just give this white string a yank and let it fly
~ Call("TutorialSpawner", "TutorialSpawner", "SpawnBleb", "1")
Chuck our little Bleb pal right into that hole over there
~ Call("ComicController", "ComicController", "SetComic", "1")
Think slingshot. This here's your ghetto-rigged slingshot. Make it work
-> END

== BlebInSafe ==
#name Spider
Nice one. You're getting the hang of it
Right now, it's just practice, but soon this is gonna be your whole job
~ Call("ComicController", "ComicController", "SetComic", "1")
Now, smash the biggest, baddest key on your keyboard like you mean it and snap this thread
-> END

== WebWasDestroyed ==
#name Spider
Shhh . . . you hear that?
See that creepy hole on the left?
Yeah, the Flies don't exactly RSVP for their surprise visits
But this once, we got a heads-up
~ Call("ComicController", "ComicController", "SetComic", "1")
Any second now, Fly goons are gonna drop into this zone
~ Call("TutorialSpawner", "TutorialSpawner", "SpawnMyha", "1")
If no Blobs are around, you can just ignore the buggers. But don't you dare let 'em near the prize
-> END

== FliesAreGone ==
#name Spider
Alright, champ. The ball's in your court now
To get the Blobs moving, hook a line up to the Blob icon
Just don't go wild and release a whole bunch at once – these little guys are way too hyped about jumping and purring on webs
All that purring junk stresses the line out
One Blob? No biggie. A whole squad of 'em? That'll snap it for sure
Remember the gig: get our Blobs out in one piece
The whole crew's riding on you. Don't screw it up
-> ToSecondLevelScene

== ToSecondLevelScene ==
~ Call ("SceneChanger", "DiaSceneChanger", "StartSecondLevel", "1")
-> END