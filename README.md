<h1>Simuga - A Simple Multiplayer Game</h1>
<h5>By Vo Phu Thanh</h5>

<h3>Set Up:</h3>
<p>- Open multiple instances of the game for testing (default maximum players are 5, can be changed via FusionController inspector), for example open one instance in Unity Editor and one with the Windows build.</p>
<p>- Player on each instance can change Name and Color at the start of the game or during ingame time. All changes are expected to be observable for every instances within the shared network. Each player's movement and position are also synced for all players.</p>

<h3>About the project:</h3>
<p>- Project is built using simple logic of Photon Fusion 2, and is setup specifically for Shared mode.</p>
<p>- Utilise Fusion's Networked and Rpc methods for properties synchronisation.</p>
<p>- Other libraries used: TextMeshPro, DOTween</p>