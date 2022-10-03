<div id="header" align="center">
    <h1><em>TODO LIST</em></h1>
</div>

---
<div id="general" align="left">
    <h3>General Fixes</h3>
    <ul>
        <li>Add items to TODO</li>
        <li>Delete unused sprites</li>
    </ul>
</div>

---
<div id="reffiles" align="left">
    <h3>Files to Refactor</h3>
    <ul>
        <li>AnimationController.cs
            <ul>
                <li>Can be fully abstracted so it isn't even aware what it's attached to.</li>
                <li>I should fully abstract the animations as well, so they don't need to be accessed by strings in more than 1 place.</li>
            </ul>
        </li>
        <li>Background.cs
            <ul>
                <li>Make it an observer to SpeedController, which would allow it to be more abstract.</li>
            </ul>
        </li>
        <li>BGManager.cs
            <ul>
                <li>Maybe just get rid of this altogether? Seems like an IMoving interface or something would work better</li>
            </ul>
        </li>
        <li>Remove all aspects of my cosmetics system</li>
        <li>DependencyManager.cs
            <ul>
                <li>Either make a Singleton or remove entirely, probably remove entirely;
            </ul>
        </li>
        <li>Impliment my interfaces that already exist better</li>
        <li>CameraManager.cs
            <ul>
                <li>Abstract the set follow method to be able to follow any gameObject passed in</li>
                <li>Create a namespace, so theres no longer the global enum</li>
            </ul>
        </li>
        <li>SpeedController.cs
            <ul>
                <li>Take some unnecessary stuff out of Update()</li>
            </ul>
        </li>
        <li>Get rid of unnecessary namespaces</li>
        <li>AnimationManager.cs
            <ul>
                <li>Actually impliment this.</li>
            </ul>
        </li>
        <li>ControlManager.cs
            <ul>
                <li>Actually understand this. I implimented it before I was really able to understand what I was writing, so I should go back and figure it out so I can maybe improve it.</li>
            </ul>
        </li>
        <li>Remove FaceDetector.cs and GroundDetector.cs</li>
        <li>Player.cs in progress</li>
        <li>Assets/Save
            <ul>
                <li>Make this more used. I use playerPrefs more than I probably should, and it's mostly because I never really got around to adding it retroactively</li>
            </ul> 
        </li>
        <li>ObjectSpawner.cs
            <ul>
                <li>System could use some tuning, though it's okay in an overall sense. Just general cleaning</li>
            </ul>
        </li>
        <li>Delete PlayerSpawner.cs</li>
        <li>Probably delete TutorialSpawner.cs</li>
        <li>GameManager.cs
            <ul>
                <li>Needs to be broken up and have a lot of unnecessary stuff removed. I'll create an in depth list shortly</li>
            </ul>
        </li>
        <li>StateController.cs and StateUpdater.cs can just be removed. Makes spaghetti code</li>
        <li>Just add some tests in general, I have put it off too long.</li>
        <li>Ears can be updated to be a little cleaner, definitely not important though</li>
        <li>LoadIcon.cs feels more complicated than necessary, and the name is bad. It should be LoadScreen.cs instead.</li>
    </ul>
</div>

---
<div id="art" align="left">
    <h3>Art</h3>
    <ul>
        <li>Player animations</li>
        <li>Target Sprites</li>
        <li>Add more particles
            <ul>
                <li>Dust particles for landing and boosting</li>
            </ul>
        </li>
    </ul>
</div>

---
<div id="interfaces" align="left">
    <h3>Interfaces</h3>
    <ul>
    </ul>
</div>