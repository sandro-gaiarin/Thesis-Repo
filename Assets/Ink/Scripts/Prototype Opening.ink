-> opening
==opening==
I don’t know what I was expecting. Nothing in the news is ever good these days. # playerthoughts
I wonder when mom is gonna be back… Things are starting to get scary out there. # playerthoughts
Knock knock # ambient
I think someone might be at the door # playerthoughts
BANG # ambient
Or in the house now! I should go check on that. # playerthoughts

//next line should trigger when the player enters the living room
Janet! Hunter! # player 
Hey [player], we wanted to stop by and hang. # hunter
Someone decided they should let themselves in… # Janet
Well I always love seeing you guys. Even if I do prefer letting you in myself… # player
How was your day? # Janet
-> day_choice
==day_choice==
* Fine # player
-> after_day_choice
* So-so # player
-> after_day_choice
* Not bad # player
-> after_day_choice
==after_day_choice==
Well at least your day hasn’t been bad! # Janet
Oh, [Player], where’s your mom? I was hoping she could make us some of her world famous cookies. # hunter
You pig. # Janet
What?! # hunter
We all know you have a crush on [player]’s mom. # Janet
I- I… I do not! And I resent that! # hunter
I’d like to apologize for Hunter’s behavior. # Janet
You can’t apologize for me, Janet! # hunter
Then apologize for yourself. # Janet
Whoa, guys, calm down. No one needs to do any apologizing. # player
Thank you. # hunter
But to be honest, I haven’t seen my mom for a few days. # player
Like, she’s been busy at work? # Janet
No, she’s just been… gone? I haven’t heard from her, either. # player
And I’m kind of scared… # playerthoughts
That doesn’t sound good. # hunter
You don’t need to state the obvious, dimwit. # Janet
Hey! # hunter
Guys, cut it out. # player
Yeah, we need to be focused, Hunter. # Janet
Sigh # hunter
Do you have a way you could find out where your mom is? # hunter
I don’t know… # player
Does she have GPS in her car? # Janet
Yeah? # player
Then we can use your phone to ping it. Give me your phone [Player]. # Janet
There’s no real point in resisting Janet when she gets like this, so I hand over my phone. # playerthoughts
One sec, annnnnnnd we’re in. Yup, your mom’s car is… in the warehouse district? Weird. # Janet
Does your mom go to raves in warehouses? Sick. # hunter
No, I… I don’t know why mom would be there. # player
We should go investigate. # Janet
H: Yeah!
-> investigate_choice
==investigate_choice==
*Alright! # player
-> investigate_confirmed
*If you both think we should, why not? # player
-> investigate_confirmed
*I guess I don’t have anything better to do… # player
-> investigate_confirmed
==investigate_confirmed==
Awesome! We get to be like detectives. # Janet
Or spies! # hunter
Hunter, that’s silly. # Janet
How is that any more silly than being detectives! # hunter
Guys, we should really get going. It’s starting to get dark out. # player
Let’s move out! # Janet
Reporting for duty. # hunter
I don’t know what it is, but it feels like something bad is going to happen… # playerthoughts
-> WarehouseExt1

//Triggers at warehouse
==WarehouseExt1==
The warehouse district is pretty scary, even during the day. And it’s almost nighttime. # playerthoughts
There’s your mom’s car! # hunter
Kinda weird that you could recognize her car so fast. # Janet
That’s not fair! I carpool with [Player] a lot! # hunter
Likely story. # Janet
Focus, you two. # player
To be completely honest, their banter is keeping me from freaking out. # playerthoughts
Well, she’s not here. # hunter
Obviously. We’re going to have to go into the warehouse. # Janet
Wait what? How do we know this is even the right one? # hunter
I mean, all the other warehouses on the street are boarded up. This one has lights on inside. # player
That… that is a good point. # hunter
Come on dorks, time’s wasting. You got your magic ready, [Player]? # Janet
Of course, ready for anything.# player
Oh boy, here we go. # playerthoughts
->WarehouseInt1
//Triggers in warehouse
==WarehouseInt1==
It’s kinda creepy in here. # hunter
That doesn’t matter. We need to be quick, and find anything we can about [Player]’s mom. # Janet
Quick… okay, I can get behind that. The sooner we’re outta here the better! # hunter
And we have to be ready to fight if we need to. # Janet
Fight? # player
We’re not supposed to be here. And if whoever’s running this place took your mom, they’re probably going to want to deal with us themselves. # Janet
Is it too late to back out? # hunter

    -> END
