TripNova Travel Planner App
===========================

Project Overview
----------------
TripNova is a mobile travel planning application developed using .NET MAUI in Visual Studio. 
The application is designed to help users organise trips, track expenses, explore destinations, 
and receive intelligent travel suggestions within a single integrated platform.

The app provides a complete workflow from trip planning to budget management, combining 
user-friendly design with practical real-world functionality.

Development Framework & Architecture
------------------------------------
TripNova is built using the MVVM (Model–View–ViewModel) architectural pattern, ensuring clear 
separation of concerns, maintainability, and scalability.

- Model Layer: Defines core data structures such as Trip, BudgetItem, Destination, and User.
- View Layer: Implements responsive UI pages using XAML.
- ViewModel Layer: Handles application logic, user interaction, and data binding.

The application also uses Shell navigation to implement a bottom navigation system, allowing 
users to quickly switch between core modules.

Core Features (Final Version)
-----------------------------
- Shell-based bottom navigation (Home, Search, Budget, AI, Trips)

- Full trip management system:
  - Create, edit, delete, and filter trips (Upcoming, Happening, Past)

- Budget management system:
  - Expense tracking with predefined categories
  - Real-time total calculation and visual feedback
  - Budget progress monitoring and over-budget detection

- Persistent data storage:
  - SQLite database integration
  - One-to-many relationship (Trip → BudgetItem)

- Local notification system:
  - Alerts for trip creation and budget exceedance

- AI-style assistant:
  - Mock AI service providing structured budget suggestions
  - Designed to support future integration with real AI APIs

- External service integration:
  - Google Maps for navigation
  - Booking platforms for accommodation planning

- Dynamic UI updates:
  - MVVM data binding with ObservableCollection
  - Real-time updates across the application

Version Control & Development Process
-------------------------------------
The project was developed using an iterative SCRUM-based approach. Progress was tracked through 
GitHub Classroom with incremental commits demonstrating feature evolution, debugging, and refinement.

Code is structured in a modular way, with clear separation between Models, Views, ViewModels, 
and Services to support maintainability and future scalability.

Technology Stack
----------------
- .NET MAUI (C# / XAML)
- SQLite (Local Database)
- Plugin.LocalNotification (Notifications)
- MVVM Architecture Pattern

Future Improvements
-------------------
- Integration of real AI APIs for intelligent recommendations
- External travel APIs for live destination data
- Cloud-based data storage and user authentication
- Enhanced data visualisation and analytics features

Author
------
Student Name: Tian Zhiran  
Course: Mobile Application Programming  
Application Name: TripNova  

Project Status
--------------
Completed Functional Prototype (Final Submission Version)