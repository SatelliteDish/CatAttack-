<section id="header" align="center">
    <h1><em>TODO LIST</em></h1>
    <h5>π¦ = New Feature
    <br>π₯ = High priority - Game will not function properly
    <br>π¨ = Medium Priority - Not SOLID, bad practices, spaghetti code
    <br>π© = Low Priority - Unnecessary performance fixes, formatting issues, etc.
    <br>π΄ = Hard - Requires lots of changes or more complex logic. Not necessarily difficult, very annoying counts too
    <br>π‘  = Medium - Generally contained to a few classes, simple logic
    <br>π’ = Easy - Only effects a couple methods, only bread and butter logic</h5>
</section>

---
<section id="general" align="left">
    <h3>General Fixes</h3>
    <ul>
        <li>π©π’Delete unused sprites</li>
    </ul>
</section>

---
<section id="reffiles" align="left">
    <h3>Files to Refactor</h3>
    <ul>
        <li>AnimationController.cs
            <ul>
                <li>π¨π’I should fully abstract the animations as well, so they don't need to be accessed by strings in more than 1 place. I could make a wrapper class for each Animation and give it an ID, so I could have some semblance of type safety.</li>
            </ul>
        </li>
        <li>Background.cs
            <ul>
                <li>π¨π’Have a backgroundFactory create the backgrounds, keep track of them in a Queue. When the first element in the queue is null it dequeues, and you can iterate over the queue using foreach to stop or restore each background's speed.</li>
            </ul>
        </li>
        <li>BGManager.cs
            <ul>
                <li>π©π’Turn into BackgroundFactory that my background point talks about. I should make an actual background factory to handle the creation logic, and only let it be accessible by BGManager (or whatever I rename it). I'll need a queue, a BackgroundFactory class, and a public BGStart and BGStop function that changes the speed of all BG objects, for when the player dies.</li>
            </ul>
        </li>
        <li>π¦π‘BackgroundFactory.cs: Add a factory for the BackDrops.
            <ul>
                <li>Can create a random backdrop, or can create a specific one when passed a request</li>
            </ul>
        </li>
        <li>π¨π΄Remove all aspects of my cosmetics system</li>
        <li>DependencyManager.cs
            <ul>
                <li>π¨π΄Burn this spaghetti factory to the ground</li>
            </ul>
        </li>
        <li>π©π‘Impliment my interfaces that already exist better
            <ul>
                <li>IDestructable: Maybe it should be renamed. Should be added to Target.cs, and Coin.cs as well</li>
                <li>IObservable: Add to SpeedController and maybe more</li>
            </ul>
        </li>
        <li>SpeedController.cs
            <ul>
                <li>π©π’Take some unnecessary stuff out of Update()</li>
            </ul>
        </li>
        <li>π©π’Get rid of unnecessary namespaces</li>
        <li>ControlManager.cs
            <ul>
                <li>π’π©Actually understand this. I implimented it before I was really able to understand what I was writing, so I should go back and figure it out so I can maybe improve it.</li>
            </ul>
        </li>
        <li>π©π’Remove FaceDetector.cs and GroundDetector.cs</li>
        <li>π₯π΄Player.cs in progress
            <ul>
                <li>Create a PlayerFactory, a little overkill right now but when I reimpliment cosmetics it will be a huge help. Can also use to inform the player where they are ie. in tutorial</li>
            </ul>
        </li>
        <li>Assets/Save
            <ul>
                <li>π¨π‘Make this more used. I use playerPrefs more than I probably should, and it's mostly because I never really got around to adding it retroactively</li>
            </ul> 
        </li>
        <li>AudioManager.cs
            <ul>
                <li>Break start into some other file that fits it better</li>
            </ul>
        </li>
        <li>ObjectSpawner.cs
            <ul>
                <li>π©π‘System could use some tuning, though it's okay in an overall sense. Just general cleaning</li>
            </ul>
        </li>
        <li>π©π’Delete PlayerSpawner.cs</li>
        <li>Probably delete TutorialSpawner.cs</li>
        <li>GameManager.cs
            <ul>
                <li>π¨π‘Needs to be broken up and have a lot of unnecessary stuff removed. I'll create an in depth list shortly
                    <ul>
                        <li>Remove all control it has over UI, break that out</li>
                        <li>Make a scene navigator, who only deals with scene management.</li>
                        <li>Break out control of Canvases.</li>
                    </ul>
                </li>
            </ul>
        </li>
        <li>π¨π‘StateController.cs and StateUpdater.cs can just be removed. Makes spaghetti code</li>
        <li>π¨π‘Just add some tests in general, I have put it off too long.</li>
        <li>π©π’Ears can be updated to be a little cleaner, definitely not important though</li>
        <li>π©π’LoadIcon.cs feels more complicated than necessary, and the name is bad. It should be LoadScreen.cs instead.</li>
        <li>MenuController.cs
            <ul>
                <li>π©π’Break CanvasType into it's own namespace, or just delete it entirely. I could give each canvas an id or something instead, there's a plethura of options.</li>
                <li>π©π’This can use a lot of cleaning using my new knowledge. Not high priority but it needs some care.</li>
            </ul>
        </li>
        <li>π©π’OptionsMenu.cs, can use general refactoring</li>
        <li>π©π’SmallTreat.cs can just use some formatting touchups and minor updates. Not High Priority because it's not implimented yet anyways.</li>
        <li>π©π’Probably just delete StartMenu.cs, it doesn't need to exist.</li>
        <li>π₯π‘TutorialScreen.cs needs to be implimemted, waiting on me to decide how I want it to play out.</li>
        <li>π¨π‘Rework the whole tweening system. I can Abstract it somehow to make the tweener, the object being tweened, and the tween animation itself fully agnostic of eachother.</li>
        <li>π©π‘Bounding.cs, just remove the whole system and use screen coordinates or something instead.</li>
        <li>π¨π’Coins.cs could use some minor refactors to be safer and more modular but that's all it needs.</li>
        <li>π¨π’Maybe just remove Deletion.cs?</li>
        <li>π¨π‘Ground.cs could be improved heavily, might not even need to be it's own file.</li>
        <li>π¨π’RespawnPlatform.cs can have IObservable be removed, I think</li>
        <li>π¨π’Target.cs can use some major refactoring. Will add a list to detail exactly what I mean.</li>
        <li>π©π’Treats.cs is needlessly complicated, just simplify it</li>
        <li>π¦π’Make an exceptions folder, where I can write custom exceptions.</li>
        <li>CameraManager.cs put the start function somewhere else</li>
        <li>I can probably remove the seperation between classes like Background.cs and Ground.cs if I add a generic factory for both. All you'd need is to send a request that has a bool called isBackground or something.</li>
    </ul>
</section>

---
<section id="art" align="left">
    <h3>Art</h3>
    <ul>
        <li>π¨π΄Player animations</li>
        <li>π¨π’Target Sprites</li>
        <li>Add more particles
            <ul>
                <li>π¨π’Dust particles for landing and boosting</li>
            </ul>
        </li>
    </ul>
</div>

---
<section id="interfaces" align="left">
    <h3>Interfaces</h3>
    <ul>
    </ul>
</section>
<section id="tests" align="left">
    <h3>Tests</h3>
</section>