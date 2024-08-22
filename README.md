# Matrix Solver using Gaussian Elimination

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Usage](#usage)
- [How Gaussian Elimination Works](#how-gaussian-elimination-works)
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
## Usage
-Launch the Matrix Solver application.
-Enter the number of equations (rows) and variables (columns).
-Input the coefficients of each equation into the matrix grid provided.
-Click the Solve button to start the Gaussian Elimination process.
-View the solution displayed in the result section. If the system is inconsistent or has infinite solutions, appropriate messages will be displayed.
-To solve a new system, simply close the program and start again.
  
## Steps:
Pivoting: Select a pivot element (non-zero) from the current column and, if necessary, swap rows to move it into the diagonal position.
Elimination: Use the pivot element to eliminate all entries below it in the current column by row operations.
Back Substitution: Once the matrix is in upper triangular form, solve for the variables by substituting back from the last row upwards.
This application implements these steps programmatically and handles edge cases such as singular matrices (which have no unique solutions).

## How Gaussian Elimination Works:
Gaussian Elimination is a method for solving linear systems by converting the system's augmented matrix into a row-echelon form, and then performing back-substitution to find the solution.

## Contact:
If you have any questions, suggestions, or issues, feel free to contact me:
