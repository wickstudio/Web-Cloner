# Web Cloner

Web Cloner is a C# application designed to clone web pages, including their HTML, CSS, JavaScript, and fonts, enabling users to view and interact with web pages offline.

## Features

- **Clone HTML, CSS, JS, and Fonts**: Downloads and saves all necessary files to replicate a webpage fully.
- **User-Friendly Interface**: Easy-to-use interface for selecting and cloning websites.
- **Local Viewing**: View cloned web pages locally without an internet connection.

## Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/wickstudio/Web-Cloner.git
   ```
2. **Open the project in Visual Studio**
   - Open Visual Studio.
   - Select `File` > `Open` > `Project/Solution`.
   - Navigate to the cloned repository folder and select the `.sln` file.

3. **Restore NuGet packages**
   - In Visual Studio, go to `Tools` > `NuGet Package Manager` > `Package Manager Console`.
   - Run the following command:
     ```powershell
     Update-Package -reinstall
     ```

4. **Build the project**
   - In Visual Studio, select `Build` > `Build Solution`.

## Usage

1. **Run the application**
   - Press `F5` or click on the `Start` button in Visual Studio to run the application.

2. **Enter the URL**
   - Enter the URL of the web page you want to clone.

3. **Start Cloning**
   - Click the `Clone` button to start downloading the web page's assets.

4. **View Cloned Page**
   - Once cloning is complete, navigate to the output directory to view the cloned web page locally.

## Project Structure

- **/Cloner**: Contains the main application logic for cloning web pages.
- **/Plugins**: Contains plugins used for extending the functionality of the cloner.
- **/Properties**: Contains project properties and settings.

## Dependencies

- **HtmlAgilityPack**: Used for parsing and manipulating HTML documents.
- **Newtonsoft.Json**: Used for handling JSON data.

## Contributing

Contributions are welcome! Please fork the repository and submit pull requests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Authors

- **Wick** - CEO of Wick Studio
- **Sinnerx9** - Moderator of Wick Studio

## Contact

- **Email**: info@wickdev.xyz
- **Website**: [wickdev.xyz](https://wickdev.xyz)
- **Discord**: [Join our Discord](https://discord.gg/wicks)
- **YouTube**: [Wick Studio on YouTube](https://www.youtube.com/@wick_studio)

```
██╗    ██╗██╗ ██████╗██╗  ██╗    ███████╗████████╗██╗   ██╗██████╗ ██╗ ██████╗ 
██║    ██║██║██╔════╝██║ ██╔╝    ██╔════╝╚══██╔══╝██║   ██║██╔══██╗██║██╔═══██╗
██║ █╗ ██║██║██║     █████╔╝     ███████╗   ██║   ██║   ██║██║  ██║██║██║   ██║
██║███╗██║██║██║     ██╔═██╗     ╚════██║   ██║   ██║   ██║██║  ██║██║██║   ██║
╚███╔███╔╝██║╚██████╗██║  ██╗    ███████║   ██║   ╚██████╔╝██████╔╝██║╚██████╔╝
 ╚══╝╚══╝ ╚═╝ ╚═════╝╚═╝  ╚═╝    ╚══════╝   ╚═╝    ╚═════╝ ╚═════╝ ╚═╝ ╚═════╝ 
Copyright (c) 2024 Wick Studio
```