USE [SongsDB]
GO
CREATE TABLE [Songs](
[SongId] [varchar](8) NOT NULL PRIMARY KEY,
[Title] [varchar](32) NOT NULL,
[Lyrics] [varchar](5000) NOT NULL,
[CoverPath] [varchar](32) NOT NULL,
[AuthorName] [varchar](32) NOT NULL,
)

GO
INSERT INTO[Songs]
VALUES
('AHGR2891', 'Conversations', '
All our ties stay severed
that was when we were young,
she''s in love and and he''s
a tough guy, that leads you on.

The one I can''t explain:
It''s severed
You''re vendetta 
New stilettos
Poking on me like berettas
', 'conversations', 'Aries')

GO
INSERT INTO[Songs]
VALUES
('JIEN0019', 'Fool''s Gold', '
(Vice)
Yeah

I was made in gold, fall asleep, this on my resume
Feelin'' low, pop the ceiling, please don''t hesitate
Peelin'' off, chasin'' racks, keep me in shape
I don''t wan'' wrap my mind around her every day, any night
Any way, I''ma ride for you, love, dollar signs
Pockets swole like a swine, many fold, many tried
Many go, I survived episodes I shouldn''t have
Sell me some love drugs I can make an album out of
Feet up, targetin'' me like a problem
Be your sovereign, what you want I got
And in my hindsight, all of this becomin'' silly
If I made a milli'', then my mommy madе a milli
Clearly on my next step
Took mе for a host? Why, you can be my guest
Hard to catch sunshine waitin'' by my desk, yeah
You want fun times, I need nothing less

I don''t wanna wait too long, do I have to?
A lot of racin'' thoughts in my head
Messed around and played your part, send you back to him
One shot like I never miss (Tres, dos, uno)

How long can we dance for? I got many plans
In the eye of the storm, why you huggin'' the fence?
I wish I''d have known, ''cause my feet grippin'' the sand
Used to run with the crown, tell me
Have you seen a man down?
Picture this, I''m headed to the land now, hold my breath
Every second made me stand out
Differences between you and the rest

Clearly on my next step
Took me for a host? Why, you can be my guest
Hard to catch sunshine waitin'' by my desk, yeah
You want fun time- (Fun time-, fun time-)

I don''t wanna wait too long, do I have to?
Racin'' thoughts in my head
Messed around and played your part (Played your part), send you back to him
One shot like I never miss (One shot like I never miss)', 'foolsGold', 'Aries')

GO
INSERT INTO[Songs]
VALUES
('IEOA6153', 'Baba O''Riley', '
Out here in the fields
I fight for my meals
I get my back into my living.
I don''t need to fight
To prove I''m right
I don''t need to be forgiven.
Yeah, yeah, yeah, yeah, yeah
Don''t cry
Don''t raise your eye
It''s only teenage wasteland
Sally, take my hand
We''ll travel south cross land
Put out the fire
And don''t look past my shoulder.
The exodus is here
The happy ones are near
Let''s get together
Before we get much older.
Teenage wasteland
It''s only teenage wasteland.
Teenage wasteland
Oh, yeah
Its only teenage wasteland
They''re all wasted!
', 'baba-oriley', 'The Who')