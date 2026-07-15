# Dart Online - Multiplayer 3D Game

## 📌 Project Overview
A real-time multiplayer game developed in Unity (Client) and C# .NET (Server) designed to demonstrate custom network architecture, multi-threaded connection handling, and synchronous state management between clients.

## 🛠️ Architecture & Core Technologies
The project implements a dedicated Client-Server architecture built from scratch, utilizing low-level networking concepts rather than high-level engine wrappers:

*   **Custom TCP Sockets & Multi-threading:** Engineered a multithreaded TCP socket server capable of handling multiple concurrent client connections. Each connection is managed on a separate thread to prevent blocking the main server loop.
*   **Packet Serialization & Data Protocol:** Designed a custom byte-level packet structure to serialize and deserialize game data efficiently, minimizing network bandwidth and ensuring strict data integrity.
*   **Database Integration (MySQL):** Integrated a MySQL database backend on the server side to handle persistent data storage, including user authentication (login/register systems) and player profile persistence.
*   **State Synchronization & Game Loop:** Implemented tick-based synchronization mechanics to update player positions, actions, and game states reliably across all connected clients.

## 👥 Contributions (My Role)
*   **Lead Developer & Systems Architect:** Designed and implemented the entire core network backend (Server) and synchronized client-side components in Unity.
*   **Networking Logic:** Wrote low-level socket handling, packet buffers, and thread-safe queues to process incoming/outgoing network messages smoothly.
*   **Database Schema & UI Flow:** Designed the database relational schema and built the authentication flow (Login/Register panels) within the Unity client.
