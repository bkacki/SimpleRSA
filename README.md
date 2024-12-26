# SimpleRSA

SimpleRSA is a console application that demonstrates the basic principles of RSA encryption and decryption. It allows users to generate RSA keys, encrypt messages, and decrypt messages using those keys.

## Features

- Generate RSA public and private keys
- Encrypt messages using the public key
- Decrypt messages using the private key

## Requirements

- .NET 8.0

## Getting Started

### Prerequisites

- .NET 8.0 SDK

### Installation

1. Clone the repository:
`git clone https://github.com/yourusername/SimpleRSA.git`
2. Navigate to the project directory:
`cd SimpleRSA`
3. Build the project:
`dotnet build`

### Usage

1. Run the application:
`dotnet run --project SimpleRSA`
2. Follow the on-screen menu to generate keys, encrypt messages, and decrypt messages.

### Example

```
Public key: (5,152653157057)
Private key: (122121848285,152653157057)

Message: Hello World!
Public key: (5,152653157057)
Encrypted message: JewTfhu2j7PKF6lwb2Yi8QI/NRY=

Message to decrypt: JewTfhu2j7PKF6lwb2Yi8QI/NRY=
Private key: (122121848285,152653157057)
Decrypted message: Hello World!
```


## Project Structure

- `SimpleRSA.csproj` - Project file for the SimpleRSA application.
- `Program.cs` - Main entry point of the application.
- `RSAKey.cs` - Contains the RSA key generation and encryption/decryption logic.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License.
