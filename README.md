Wikipedia Playwright Test Suite:

Automated test suite for the Playwright (software) Wikipedia article, built with C#, NUnit, Microsoft Playwright, and Allure reporting.

Project Structure:

TestProject2/

├── Configs/

│ └── Config.cs           # variables (URLs, selectors, sections)

├── Pages/

│ └── WikiPage.cs         # Page object for the wikipedia web page

├── Tests/

│ ├── WikiTest.cs         # test cases

│ └── Wikitestbase.cs     # base class with setup/teardown

├── Utils/

│ ├── ApiExtractor.cs     # fetches section text via Wikipedia API

│ ├── TXTNormalizer.cs    # normalizes and extracts unique words

│ └── UITextExtract.cs    # extracts section text from the UI via Playwright

├── allureConfig.json       # Allure results output configuration

└── TestProject2.csproj

Test Cases:

1.WordCount_Match: Compares unique word count in the "Debugging Features" section between UI scrape and Wikipedia API.
2.DevTools_AreLinks: Verifies every item in the Microsoft dev tools table cell has a hyperlink if not fail the test.
3.DarkMode_Applied: Clicks the night mode toggle and confirms the dark theme class is applied to the <html> element

Prerequisites

.NET 10 SDK
Allure CLI
Playwright browsers installed

Reports :

HTML:
<img width="2373" height="1544" alt="{286F6ED0-EA3E-49FB-A9A9-787ED3DFE2C4}" src="https://github.com/user-attachments/assets/4c5e9d78-ae86-457f-84e5-00e257becda0" />

ALLURE:
<img width="2122" height="990" alt="{286102D8-274A-46C2-93B8-AA98176CAB3E}" src="https://github.com/user-attachments/assets/62c3d60b-2c2c-4c12-8084-47a148c15019" />
