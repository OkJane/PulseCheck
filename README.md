# PulseCheck-
PulseCheck is a lightweight API health monitoring system that helps small teams detect downtime early. It periodically checks registered endpoints, tracks latency and failures.

## Features
- Periodic health checks via hosted background service
- Failure tracking and threshold-based status updates
- Persistent health logs
- Clean separation between API and Worker

## Architecture
PulseCheck follows a layered architecture
- API: registers endpoints and exposes status data
- Worker: performs scheduled health checks and updates databases
- Core: domain models, enums, interfaces
- Infrastructure: dababase access, repositories and external http clients

## How to Run
1. Configure appsettings.json
2. Run PulseCheck.API
3. Run HealthChecker

## Future Improvements
- Authentication and Authorization
- Dashboard UI
- Metrics and Graphs
- Alerting
- Support for authenticated endpoints
