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