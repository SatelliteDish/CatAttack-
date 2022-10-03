<section id="header" align="center">
    <h1><em>TODO LIST</em></h1>
    <h5>🟦 = New Feature
    <br>🟥 = High priority - Game will not function properly
    <br>🟨 = Medium Priority - Not SOLID, bad practices, spaghetti code
    <br>🟩 = Low Priority - Unnecessary performance fixes, formatting issues, etc.
    <br>🔴 = Hard - Requires lots of changes or more complex logic
    <br>🟡  = Medium - Generally contained to a few classes, simple logic
    <br>🟢 = Easy - Only effects a couple methods, only bread and butter logic</h5>
</section>

---
<section id="general" align="left">
    <h3>General Fixes</h3>
    <ul>
        <li>🟩🟢Delete unused sprites</li>
    </ul>
</section>

---
<section id="reffiles" align="left">
    <h3>Files to Refactor</h3>
    <ul>
        <li>AnimationController.cs
            <ul>
                <li>🟨🟢Can be fully abstracted so it isn't even aware what it's attached to.</li>
                <li>🟨🟢I should fully abstract the animations as well, so they don't need to be accessed by strings in more than 1 place.</li>
            </ul>
        </li>
        <li>Background.cs
            <ul>
                <li>🟨🟢Make it an observer to SpeedController, which would allow it to be more abstract.</li>
            </ul>
        </li>
        <li>BGManager.cs
            <ul>
                <li>🟩🟡Maybe just get rid of this altogether? Seems like an IMoving interface or something would work better</li>
            </ul>
        </li>
        <li>🟨🔴Remove all aspects of my cosmetics system</li>
        <li>DependencyManager.cs
            <ul>
                <li>🟨🔴Either make a Singleton or remove entirely, probably remove entirely;
            </ul>
        </li>
        <li>🟩🟡Impliment my interfaces that already exist better</li>
        <li>CameraManager.cs
            <ul>
                <li>🟩🟢Abstract the set follow method to be able to follow any gameObject passed in</li>
                <li>🟨🟢Create a namespace, so theres no longer the global enum</li>
            </ul>
        </li>
        <li>SpeedController.cs
            <ul>
                <li>🟩🟢Take some unnecessary stuff out of Update()</li>
            </ul>
        </li>
        <li>🟩🟢Get rid of unnecessary namespaces</li>
        <li>AnimationManager.cs
            <ul>
                <li>🟨🟡Actually impliment this.</li>
            </ul>
        </li>
        <li>ControlManager.cs
            <ul>
                <li>🟢🟩Actually understand this. I implimented it before I was really able to understand what I was writing, so I should go back and figure it out so I can maybe improve it.</li>
            </ul>
        </li>
        <li>🟩🟢Remove FaceDetector.cs and GroundDetector.cs</li>
        <li>🟥🔴Player.cs in progress</li>
        <li>Assets/Save
            <ul>
                <li>🟨🟡Make this more used. I use playerPrefs more than I probably should, and it's mostly because I never really got around to adding it retroactively</li>
            </ul> 
        </li>
        <li>ObjectSpawner.cs
            <ul>
                <li>🟩🟡System could use some tuning, though it's okay in an overall sense. Just general cleaning</li>
            </ul>
        </li>
        <li>🟩🟢Delete PlayerSpawner.cs</li>
        <li>Probably delete TutorialSpawner.cs</li>
        <li>GameManager.cs
            <ul>
                <li>🟨🟡Needs to be broken up and have a lot of unnecessary stuff removed. I'll create an in depth list shortly</li>
            </ul>
        </li>
        <li>🟨🟡StateController.cs and StateUpdater.cs can just be removed. Makes spaghetti code</li>
        <li>🟨🟡Just add some tests in general, I have put it off too long.</li>
        <li>🟩🟢Ears can be updated to be a little cleaner, definitely not important though</li>
        <li>🟩🟢LoadIcon.cs feels more complicated than necessary, and the name is bad. It should be LoadScreen.cs instead.</li>
        <li>MenuController.cs
            <ul>
                <li>🟩🟢Break CanvasType into it's own namespace, or just delete it entirely. I could give each canvas an id or something instead, there's a plethura of options.</li>
                <li>🟩🟢This can use a lot of cleaning using my new knowledge. Not high priority but it needs some care.</li>
            </ul>
        </li>
        <li>🟩🟢OptionsMenu.cs, can use general refactoring</li>
        <li>🟩🟢SmallTreat.cs can just use some formatting touchups and minor updates. Not High Priority because it's not implimented yet anyways.</li>
        <li>🟩🟢Probably just delete StartMenu.cs, it doesn't need to exist.</li>
        <li>🟥🟡TutorialScreen.cs needs to be implimemted, waiting on me to decide how I want it to play out.</li>
        <li>🟨🟡Rework the whole tweening system. I can Abstract it somehow to make the tweener, the object being tweened, and the tween animation itself fully agnostic of eachother.</li>
        <li>🟩🟡Bounding.cs, just remove the whole system and use screen coordinates or something instead.</li>
        <li>🟨🟢Coins.cs could use some minor refactors to be safer and more modular but that's all it needs.</li>
        <li>🟨🟢Maybe just remove Deletion.cs?</li>
        <li>🟨🟡Ground.cs could be improved heavily, might not even need to be it's own file.</li>
        <li>🟨🟢RespawnPlatform.cs can have IObservable be removed, I think</li>
        <li>🟨🟢Target.cs can use some major refactoring. Will add a list to detail exactly what I mean.</li>
        <li>🟩🟢Treats.cs is needlessly complicated, just simplify it</li>
    </ul>
</section>

---
<section id="art" align="left">
    <h3>Art</h3>
    <ul>
        <li>🟨🔴Player animations</li>
        <li>🟨🟢Target Sprites</li>
        <li>Add more particles
            <ul>
                <li>🟨🟢Dust particles for landing and boosting</li>
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