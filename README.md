# README.md

## Event Sourcing Example

This repository showcases a simple Account management system using two different approaches: traditional CRUD operations and Event Sourcing. The solution is written in C# and uses an in-memory database for simplicity.

### Project Structure

- **BasicCrudExample**: Demonstrates the use of conventional CRUD operations.
- **BasicEventSourcingExample**: Demonstrates the use of Event Sourcing techniques.

### Prerequisites

- .NET SDK
- Visual Studio or any C# compatible IDE

### Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/username/repo-name.git
   ```
2. Open the solution in your preferred IDE.
3. Set the desired project (`BasicCrudExample` or `BasicEventSourcingExample`) as the startup project.
4. Run the project and step through the `Program.cs` file to see the implementation in action.

### Examples

#### Basic CRUD Example

1. Open an Account
2. Deposit money into the Account
3. Make withdrawals
4. View the persisted state of the Account

#### Basic Event Sourcing Example

1. Open an Account
2. Deposit money into the Account
3. Make withdrawals
4. View the persisted state of the Account
5. View the full audit log of every event that occurred against the Account

### Learn More

For a detailed explanation of Event Sourcing and the code examples, check out my blog post: [Let's Learn Event Sourcing](https://brettsblog.hashnode.dev/lets-learn-event-sourcing-part-2)

### Contributing

Feel free to fork the repository and submit pull requests. For major changes, please open an issue first to discuss what you would like to change.

### License

This project is licensed under the MIT License. See the LICENSE file for details.
