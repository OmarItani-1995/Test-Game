🧩 Project Overview

This project was developed over approximately 12 hours as a prototype to demonstrate a modular game architecture built with flexibility and scalability in mind.
It focuses on clean system separation, event-driven communication, and extendable core utilities that can easily evolve into production-grade systems.

⚙️ Core Features
1. Dependency Injection (DI)

A lightweight Dependency Injection system is included to manage object creation and dependencies.
It currently supports singleton-style injections, but can be replaced with a more advanced DI framework for larger or more complex projects.

Use Case:
DI is used when a class requires a specific implementation of a service or module (e.g., game systems or utilities) while maintaining loose coupling.

2. Message System (Msg)

A simple message broadcasting system that allows decoupled communication between game systems.

Each message can have multiple listeners (observers).

Creating a new message type is easy—just inherit from the Message class.

Any class can register as a listener and will receive a callback when the message is triggered.

Example Usage:
The Msg system is ideal for event-driven scenarios, such as updating the score when two cards match.
It complements DI by handling global events that multiple systems may need to respond to.

3. Card System

The Card System is the core of the project and manages all gameplay logic related to cards.

Main Components & Flow:

Card_Manager → Card_Container → Card_Holder → Card_View → Card


Card_Manager: The central entry point controlling the overall card behavior and interactions.

Card_Container: Manages different zones — Grid, Top, and Left — where cards are distributed or moved.

Card_Holder: Defines where a Card_View can be placed and acts as the clickable element in the UI.

Card_View: Handles the visual presentation of the card, including flipping and transition animations.

Card: The data representation of a single card.

Behavior Overview:

Cards transition between containers (e.g., Grid → Top) depending on gameplay state.

When two cards match, they move to the top container; upon completing the game, all cards return to the grid with a short transition animation.

Transitions are smooth and seamless thanks to simple position resets managed by the Card_Holder.

4. Score System

A modular Score System that reacts entirely through the Msg event system.

Listens to: MatchSuccess and MatchFail messages.

On Match Success: Increases score and combo based on predefined GameRules.

On Match Fail: Resets combo and decreases score.

Design Benefit:
The score system is fully decoupled—it can be modified, replaced, or removed without affecting other modules.

5. Save & Load System

A lightweight save/load system designed for flexibility and testing.

Includes an Editor Tool located at Tools/SaveLoad Tester to simulate saving and loading.

Uses a Bridge Pattern to route save/load calls to the correct implementation:

Editor: Uses PlayerPrefs

Mobile: Can be extended to use PlayFab or Firebase

Note:
Although the game has a small data footprint, this setup provides a foundation for scalable persistence logic across platforms.

6. Value Lerp Utility

A minimal Tween/Animation utility for value interpolation.

Designed using generics for flexibility and reusability.

Can be easily extended to include:

Lifetime management

Automatic update registration

A builder pattern for chaining animations

Purpose:
Provides smooth transitions or simple time-based effects without depending on third-party tweening libraries.

7. Pooling System

A simple generic object pooling implementation.

Accepts a creation function and an initial pool size.

Helps reduce runtime allocations and improve performance in frequently instantiated objects like cards or particles.

Benefit:
Reduces garbage collection overhead and provides predictable runtime behavior during gameplay.

🧠 Design Philosophy

This project emphasizes modularity, maintainability, and clarity.
Each system serves a distinct purpose and communicates through well-defined interfaces or messages.
The goal was to create a clean architectural foundation that can be easily expanded for future gameplay or production use.
