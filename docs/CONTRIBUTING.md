# Contributing to [Inventory, Billing & GST System]

Thank you for your interest in contributing to **[Inventory, Billing & GST System]**! We welcome all contributions, whether you're reporting a bug, suggesting a feature, or improving documentation. Please take a moment to review these guidelines to help keep the process smooth and collaborative.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [How to Contribute](#how-to-contribute)
  - [Reporting Bugs](#reporting-bugs)
  - [Suggesting Features](#suggesting-features)
  - [Improving Documentation](#improving-documentation)
  - [Code Contributions](#code-contributions)
- [Branching Strategy and Branch Protection](#branching-strategy-and-branch-protection)
- [Pull Request Process](#pull-request-process)
- [License](#license)

## Code of Conduct

Please read our [Code of Conduct](../CODE_OF_CONDUCT.md) to understand how to interact in this project. By participating, you agree to follow these guidelines, ensuring a respectful environment.

## Getting Started

Before you start contributing, ensure you have the following:

- .NET Framework 4.8.1 SDK installed
- Basic knowledge of C# and .NET Core development
- Familiarity with Git and GitHub

### Fork the Repository

1. Fork the repository by clicking the **Fork** button at the top right of the GitHub page.
2. Clone your fork locally:

    ```bash
    git clone https://github.com/JicoDotNet/Full-Inventory-by-Asp-Net-Azure.git
    cd your-repo-name
    ```

3. Set up the main project dependencies:

    ```bash
    dotnet restore
    ```

4. Create a new branch for your work:

    ```bash
    git checkout -b your-feature-branch
    ```

## How to Contribute

### Reporting Bugs

If you discover a bug, please help us by submitting an issue:

- Provide a clear and descriptive title.
- Include detailed steps to reproduce the issue.
- Include screenshots or code snippets, if applicable.
- Mention the version of .NET (4.8.1), OS, and any other relevant environment details.

### Suggesting Features

We are always looking for new ideas to improve the project. To suggest a feature:

- Search the issue tracker to ensure the feature hasn't already been suggested.
- If not, create a new issue with a detailed description of the proposed feature.
- Explain the problem your feature would solve and possible implementations.

### Improving Documentation

We welcome improvements to our documentation! You can help by:

- Fixing typos or grammatical errors.
- Expanding explanations or adding new sections where clarification is needed.
- Updating out-of-date content as the project evolves.

### Code Contributions

For code changes or feature additions:

1. Create a branch following the [Branching Strategy](#branching-strategy-and-branch-protection).
2. Write clean, readable, and maintainable code.
3. Follow the [coding conventions](https://docs.microsoft.com/dotnet/csharp/programming-guide/inside-a-program/coding-conventions).
4. Add unit tests for any new functionality or fix existing failing tests.

## Branching Strategy and Branch Protection

### Master Branch (`master`)

The `master` branch contains the stable, production-ready code. No one can directly push to `master`. All changes must be done via pull requests (PRs) and require code review approval before merging.

### Development Branch (`develop`)

The `develop` branch is where active development takes place. Contributors should branch off from `develop` and open PRs to merge changes back into `develop`.

### Branch Protection Rules

1. **No direct commits** are allowed to the `master` or `develop` branches.
2. **All PRs** must pass the following checks before merging:
   - CI build (via GitHub Actions)
   - Code review by at least one maintainer
   - Unit tests pass (Optional)
   - Feature branches should be up-to-date with `develop` before merging
3. **Require signed commits** for any branch.
4. **No force pushes** or reverts are allowed on `master` or `develop` without approval from the maintainers.

## Pull Request Process

1. **Ensure your branch is up-to-date** with the latest changes from `develop`.
2. Submit a pull request to merge your feature branch into `develop`.
3. Clearly describe what the PR does and reference any related issues (e.g., `Fixes #123`).
4. Wait for code review and make changes if requested.
5. Once all checks pass and reviews are approved, a maintainer will merge the PR.

### PR Guidelines

- Follow the existing code style and structure.
- Limit each PR to one feature or bug fix. If you have multiple changes, submit separate PRs.
- Write meaningful commit messages explaining the changes in detail.

## License

By contributing, you agree that your contributions will be licensed under the [GNU General Public License v3.0](../LICENSE).
