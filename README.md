# DUO User Exporter 

**DUO User Exporter** is a simple and efficient executable solution designed for querying any DUO tenant configured in the application and exporting user data to a CSV file. The application supports pagination, allowing it to handle more than 100 users seamlessly. You can specify the output filename and path, and add multiple tenants in the configuration file. The solution is ideal for running as a scheduled task, ensuring regular data exports without manual intervention. The installer is currently being refined for an even smoother setup process.

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![License](https://img.shields.io/badge/license-MIT-blue)

## Key Features

- 🔄 **Multi-Tenant Support:** Easily add and configure multiple DUO tenants.
- 📁 **CSV Export:** Export user data to a specified CSV file.
- 📈 **Pagination Support:** Handles more than 100 users effortlessly.
- 🛠️ **Configurable:** Set file names, paths, and tenant details in the configuration file.
- ⏱️ **Scheduled Task Compatible:** Designed to run as a scheduled task for automated data export.
- ⚙️ **Reliable Performance:** Works seamlessly and efficiently.

## Getting Started

### Prerequisites

- .NET Framework 4.7.2 or later
- DUO Admin API credentials

### Installation

1. **Clone the repository:**
   ```sh
   git clone https://github.com/yourusername/DUO-User-Exporter.git
