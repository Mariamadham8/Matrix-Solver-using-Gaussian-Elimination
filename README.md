# Matrix Solver using Gaussian Elimination

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [How Gaussian Elimination Works](#how-gaussian-elimination-works)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

## Introduction
The **Matrix Solver** is a Windows Forms Application developed in C# that solves systems of linear equations of any size using the Gaussian Elimination method. The tool is designed to provide a simple and intuitive interface for users to input their matrices and obtain solutions.

## Features
- **Solve Systems of Linear Equations**: Supports matrices of any size, allowing users to solve systems with multiple variables and equations.
- **User-Friendly Interface**: Designed as a Windows Forms Application for easy interaction.
- **Step-by-Step Solution**: The application provides a step-by-step breakdown of the Gaussian Elimination process, helping users understand how the solution is derived.
- **Error Handling**: Includes checks for singular matrices and provides informative error messages.

## Prerequisites
Before running the application, ensure you have the following installed on your machine:
- [Microsoft .NET Framework 4.7.2 or later](https://dotnet.microsoft.com/download/dotnet-framework)
- [Visual Studio 2019 or later](https://visualstudio.microsoft.com/vs/), with the .NET desktop development workload installed (if you plan to modify or build the application from source)

## Installation
### Pre-compiled Binary
1. Download the latest release from the [Releases](https://github.com/yourusername/matrix-solver/releases) section.
2. Extract the ZIP file to a location of your choice.
3. Run the `MatrixSolver.exe` file.

##Steps:
Pivoting: Select a pivot element (non-zero) from the current column and, if necessary, swap rows to move it into the diagonal position.
Elimination: Use the pivot element to eliminate all entries below it in the current column by row operations.
Back Substitution: Once the matrix is in upper triangular form, solve for the variables by substituting back from the last row upwards.
This application implements these steps programmatically and handles edge cases such as singular matrices (which have no unique solutions).

##Contributing
Contributions are welcome! Please follow these steps to contribute:

Fork the repository.
Create a new branch with a descriptive name:
bash

git checkout -b feature-name
Make your changes and commit them:
bash

git commit -m "Description of changes"
Push your changes to your fork:
bash

git push origin feature-name
Open a Pull Request against the main branch of this repository.
License
This project is licensed under the MIT License - see the LICENSE file for details.

##Contact
If you have any questions, suggestions, or issues, feel free to contact me:
