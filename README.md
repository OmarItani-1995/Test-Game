🧩 Project Overview

This project was developed over approximately 12 hours as a prototype to demonstrate a modular and scalable game architecture in Unity.
It focuses on clean system separation, event-driven communication, and extensible core utilities that can easily evolve into production-ready systems.

⚙️ Core Features
1. Dependency Injection (DI)

A lightweight Dependency Injection system is included to manage object creation and dependencies.
It currently supports singleton-style injections, but can be replaced with a more advanced DI framework for larger or more complex projects.

Use Case:
DI is used when a class requires a specific implementation of a service or module (e.g., game systems or utilities) while maintaining loose coupling.

2. Message System (Msg)

A simple message broadcasting system that enables decoupled communication between game systems.

Each message can have multiple listeners (observers).

Creating a new message type is easy—just inherit from the Message class.

Any class can register as a listener and will receive a callback when the message is triggered.

Example Usage:
The Msg system is ideal for event-driven scenarios, such as updating the score when two cards match.
It complements DI by handling global events that multiple systems may need to respond to.

3. Card System

The Card System is the heart of the project, managing all gameplay logic related to cards.

Main Components & Flow:

Card_Manager → Card_Container → Card_Holder → Card_View → Card


Card_Manager: The central entry point controlling overall card behavior and interactions.

Card_Container: Manages different zones — Grid, Top, and Left — where cards are distributed or moved.

Card_Holder: Defines where a Card_View can be placed and acts as the clickable element in the UI.

Card_View: Handles the visual representation of a card, including flipping and transition animations.

Card: Represents the card’s data and identity.

Behavior Overview:

Cards move between containers (e.g., Grid → Top) depending on gameplay state.

When two cards match, they are moved to the top container.

Once the game is completed, all cards return to the grid with a short transition animation.

Transitions are seamless due to a simple position reset mechanism managed by the Card_Holder.

4. Score System

A modular Score System that reacts entirely through the Msg event system.

Listens to: MatchSuccess and MatchFail messages.

On Match Success: Increases score and combo based on predefined GameRules.

On Match Fail: Resets combo and decreases score.

Design Benefit:
The score system is fully decoupled, meaning it can be modified, replaced, or removed without affecting other modules.

5. Save & Load System

A lightweight save/load framework designed for flexibility and easy testing.

Includes an Editor Tool at Tools/SaveLoad Tester to simulate save/load functionality.

Uses a Bridge Pattern to route save/load calls to the correct implementation:

Editor: Uses PlayerPrefs

Mobile: Can be extended to use PlayFab or Firebase

Note:
Although the game has a small data footprint, this setup provides a scalable structure for persistence across platforms.

6. Value Lerp Utility

A small, generic tweening utility for smooth value interpolation.

Designed using generics for flexibility and reusability.

Can be extended to include:

Lifetime and automatic update handling

A builder pattern for chaining animations together

Purpose:
Provides lightweight transitions or animations without relying on third-party libraries.

7. Pooling System

A simple generic object pooling solution.

Accepts a creation function and an initial pool size.

Helps reduce runtime allocations and improve performance for frequently instantiated objects.

Benefit:
Improves memory efficiency and reduces garbage collection spikes during gameplay.

8. Optimizations

Several small optimizations were implemented to keep the game lightweight and performant:

Card_MaterialCreator:
Used to cache materials that have already been created. Since each material can appear twice, this prevents unnecessary instantiation and keeps memory usage low.

Cached Transforms:
Card_View accesses its transform during movement updates, so the transform reference is cached to avoid repeated GetComponent calls each frame.

Performance Results:

Average frame time: ~3 ms total

2 ms for rendering

0.3 ms for player update

Achieved 200 FPS in testing with no noticeable performance issues.

Typical Optimization Approach:
In larger projects, optimization steps would include:

More aggressive caching

Reducing string manipulations

Using NativeArray and Span for array operations in performance-critical update loops

Overall, the project currently performs well within the desired frame budget.

🧠 Design Philosophy

This project emphasizes modularity, maintainability, and clarity.
Each system serves a distinct purpose and communicates through well-defined interfaces or messages.
The goal was to establish a clean and flexible architectural foundation that can easily scale with new gameplay features or platform requirements.
