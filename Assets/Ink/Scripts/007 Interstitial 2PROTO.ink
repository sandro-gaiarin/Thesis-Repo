VAR docSet2 = 1
//This all works, you just need to replace the two with something that reads a variable from unity on how many docs we picked up.
{ docSet2:
- 1: -> 2ndInt1
- 2: -> 2ndInt2
- 3: -> 2ndInt2
- 4: -> 2ndInt3
}

==2ndInt1==
Our second evening in the warehouse was more gruelling than the last. It’s starting to take its toll on me, I think. We get home exhausted. # playerthoughts
Hunter: Please tell me that’s the last time we’re going in there. # hunter
Janet: I don’t know if we can rule that out just yet. # Janet
Hunter: Really? Why would we possibly need to go back there? # hunter
Janet: For one, we don’t know where [Player]’s mom is, still. # Janet
Hunter: I can’t believe I’m the one saying this, but maybe now we go to the police. # hunter
Janet: … # Janet
Janet: I don’t know if it’s safe for us to do that. # Janet
Player: What do you mean? # player
Janet: These guys, this Echelon Institute, they seem pretty well connected. To have the firepower they do in there… they must know people in high places. # Janet
Hunter: All the more reason to call the police and trust in good old law and order. # hunter
Janet: Hunter, you’re an idiot. # Janet
Hunter: Wha-? [Player], that was uncalled for, right? Can you tell her that was uncalled for? # hunter
Player: Janet- # player
Janet: It was completely deserved. If this Echelon Institute has the power to get those weapons in there, they certainly have the power to arrest us for breaking and entering, and assault, regardless of their wrongdoings. # Janet
Player: Okay, so what do we do now? # player
Janet: Let’s take stock of what we found. # Janet
Hunter: It’s just this one memo. # hunter
Janet: I looked it over, it’s more than just a memo. This keeps coming up. # Janet
Player: What keeps coming up? # player
Janet: Operation 8450. Whatever it is, it’s important to them. # Janet
Hunter: And unpopular. # hunter
Janet: That’s right. They’re trying hard to keep this under wraps. # Janet
Player: But what does it mean? # player
Janet: Well, they say they’re spending a lot of money on ads against magic. I think I’ve even seen some of these ads; saying it’s unnatural and dangerous. # Janet
Hunter: Well, isn’t it? # hunter
Janet: Hunter, [Player] is a magic user! # Janet
Hunter: But we can acknowledge that magic isn’t ‘normal,’ right? # hunter
Janet: I- I can’t have this argument right now. [Player]? # Janet

-> Int2Choice

==Int2Choice==
*Hunter, you’re out of line, but let’s move on. # player
    Janet: Thank you. # Janet
    -> AfterChoice
*I kind of agree, Hunter, but let’s stay on track. # player
   Hunter: Wow, okay. Yeah let’s go on. # hunter
    -> AfterChoice


==2ndInt2==
Our second evening in the warehouse was more gruelling than the last. It’s starting to take its toll on me, I think. We get home exhausted. # playerthoughts
Please tell me that’s the last time we’re going in there. # hunter
I don’t know if we can rule that out just yet. # Janet
Really? Why would we possibly need to go back there? # hunter
For one, we don’t know where [Player]’s mom is, still. # Janet
I can’t believe I’m the one saying this, but maybe now we go to the police. # hunter
… # Janet
I don’t know if it’s safe for us to do that. # Janet
What do you mean? # player
These guys, this Echelon Institute, they seem pretty well connected. To have the firepower they do in there… they must know people in high places. # Janet
All the more reason to call the police and trust in good old law and order. # hunter
Hunter, you’re an idiot. # Janet
Wha-? [Player], that was uncalled for, right? Can you tell her that was uncalled for? # hunter
Janet- # player
It was completely deserved. If this Echelon Institute has the power to get those weapons in there, they certainly have the power to arrest us for breaking and entering, and assault, regardless of their wrongdoings. # Janet
Okay, so what do we do now? # player
Let’s take stock of what we found. # Janet
We’ve got a few documents here. # hunter
There are a few things in here I find concerning. First there’s this Operation 8450, which we’ve seen a few times now. # Janet
It sounds like it’s pretty unpopular? # hunter
Unpopular but important to them. And it seems to be closely linked to anti-magic efforts. And it sounds like it’s the one policy proposal they’re comfortable being public about. # Janet
They want people to think magic is unnatural and dangerous. # Janet
Well, isn’t it? # hunter
Hunter, [Player] is a magic user! # Janet
But we can acknowledge that magic isn’t ‘normal,’ right? # hunter
I- I can’t have this argument right now. [Player]? # Janet
-> Int2Choice

==2ndInt3==
Our second evening in the warehouse was more gruelling than the last. It’s starting to take its toll on me, I think. We get home exhausted. # playerthoughts
Please tell me that’s the last time we’re going in there. # hunter
I don’t know if we can rule that out just yet. # Janet
Really? Why would we possibly need to go back there? # hunter
For one, we don’t know where [Player]’s mom is, still. # Janet
I can’t believe I’m the one saying this, but maybe now we go to the police. # hunter
… # Janet
I don’t know if it’s safe for us to do that. # Janet
What do you mean? # player
These guys, this Echelon Institute, they seem pretty well connected. To have the firepower they do in there… they must know people in high places. # Janet
All the more reason to call the police and trust in good old law and order. # hunter
Hunter, you’re an idiot. # Janet
Wha-? [Player], that was uncalled for, right? Can you tell her that was uncalled for? # hunter
Janet- # player
It was completely deserved. If this Echelon Institute has the power to get those weapons in there, they certainly have the power to arrest us for breaking and entering, and assault, regardless of their wrongdoings. # Janet
Okay, so what do we do now? # player
Let’s take stock of what we found. # Janet
I think we hit the motherlode. Look at all these documents. # hunter
Yes, there’s quite a bit to parse through in here. Mechs to suppress magic users, stripping of rights for magic users… this is some dystopian shit. # Janet
What’s this ‘Operation 8450’ that keeps coming up? # hunter
I’m not certain, but whatever it is, it’s important. # Janet
And unpopular, it looks like. # hunter
Yes, and closely linked to their anti-magic efforts. It sounds like this is the one aspect of their project that they think is popular. # Janet
They’re banking on people fearing magic as unnatural and dangerous. # Janet
Well, isn’t it? # hunter
Hunter, [Player] is a magic user! # Janet
But we can acknowledge that magic isn’t ‘normal,’ right? # hunter
I- I can’t have this argument right now. [Player]? # Janet
-> Int2Choice

    ==AfterChoice==
Janet: Anyway, there’s definitely a lot going on at the Echelon Institute that we don’t fully understand. # Janet
Hunter: So what do we do? # hunter
Janet: I’m thinking we need to go back. Even if we don’t figure out what they’re doing, we need to find [Player]’s mom. # Janet
Hunter: And if she’s not there? # hunter
Janet: We look for clues that point us in her direction. [Player]? # Janet
Player: … # player
Player: Okay, we go back one more time. But that’s it. Things are getting dangerous. # player
Janet: Great, we’ll meet up here at the same time tomorrow. # Janet
Hunter: Wait, don’t I get a say? # hunter
Janet: Do you want to skip out on tomorrow? # Janet
Hunter: ... # hunter
Hunter: No, I’ll go with you guys. # hunter
Janet: Good. Now let’s let [Player] get some rest. # Janet
I bid Janet and Hunter good night, and they head home. # playerthoughts
    -> END