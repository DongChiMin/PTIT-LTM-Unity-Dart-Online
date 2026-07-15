# Dart Online - Multiplayer 3D Game
Server side: https://github.com/NguyenThHien/LTM.git
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

## Screenshot & Gameplay
<img width="1918" height="1078" alt="Image" src="https://github.com/user-attachments/assets/a96f7887-1984-4f64-98f4-1e5f8b53acec" />
<img width="1918" height="1078" alt="Image" src="https://github.com/user-attachments/assets/ab5cb615-30e9-4046-b690-ad8ed3ff8b16" />
<img width="1918" height="849" alt="Image" src="https://github.com/user-attachments/assets/f02e31b5-09f9-4d24-b01a-bc8438b9fb57" />
<img width="1918" height="933" alt="Image" src="https://github.com/user-attachments/assets/ad3f4534-c56d-4b75-9a80-70f027e21547" />
<img width="1918" height="933" alt="Image" src="https://github.com/user-attachments/assets/62867eb6-a303-4624-bbef-8d81c95834a4" />
<!-- Video 1 -->
<video src="https://github.com/user-attachments/assets/130a98c0-0f04-42a2-b8e4-e30004300e84" width="100%" autoplay loop muted></video>

<!-- Video 2 -->
<video src="https://github.com/user-attachments/assets/64711232-01ab-4fce-b0cb-d9beb8fd3846" width="100%" autoplay loop muted></video>

<!-- Video 3 -->
<video src="https://github.com/user-attachments/assets/43c541ab-2d67-4280-8994-8741e16a9056" width="100%" autoplay loop muted></video>
