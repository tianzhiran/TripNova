TripNova Travel Planner App
===========================

Project Overview
----------------
TripNova is a prototype mobile travel planning application developed using .NET MAUI in Visual Studio. 
The application aims to help users organise travel plans, track expenses, explore destinations, and 
receive intelligent travel assistance in a simple and user-friendly interface.

This project is currently under active development as part of a mobile application programming course. 
Core features are being progressively implemented following an iterative SCRUM-based development approach.

Development Framework & Architecture
------------------------------------
The application is built using the MVVM (Model–View–ViewModel) architectural pattern to improve code 
maintainability, scalability, and separation of concerns.

- Model Layer: Manages core data structures such as Trips and Budget Items.
- View Layer: Provides responsive user interface pages designed using XAML.
- ViewModel Layer: Handles business logic, UI interaction flow, and data binding.

The app also uses Shell navigation to create a structured bottom navigation system that allows users 
to quickly access key functional areas.

Current Implemented Features (Prototype Stage)
----------------------------------------------
- Shell-based bottom navigation (Home, Search, Budget, AI, Trips)
- Trip creation and basic trip management functionality
- Budget dashboard displaying total expenses and category breakdown
- Expense tracking using ObservableCollection (temporary in-memory storage)
- Predefined expense categories to simplify input and support financial analysis
- MVVM data binding for real-time UI updates
- Responsive layout design improvements

Features In Progress
--------------------
- Linking budget records to specific trips
- SQLite database integration for persistent data storage
- Destination search functionality using external APIs
- AI-based travel suggestion module
- UI/UX optimisation and interaction refinement

Version Control & Development Process
-------------------------------------
Development progress is managed using GitHub Classroom with incremental commits that demonstrate 
prototype evolution. Code comments are included to explain in-progress logic and design decisions.

Future Development Direction
----------------------------
- Implement structured database services and repository pattern
- Enhance navigation flow and user experience consistency
- Improve data visualisation for travel expense planning
- Expand intelligent travel planning capabilities

Author
------
Student Name: Tian Zhiran
Course: Mobile Application Programming
Application Name: TripNova
Technology Stack: .NET MAUI (C# / XAML)

Project Status
--------------
Prototype – In Progress
