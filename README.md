# Cybersecurity Awareness Bot - Part 2

## Project Description
This project is a WPF GUI version of the Cybersecurity Awareness Chatbot. It expands the Part 1 console application by adding a graphical interface, keyword recognition, random responses, conversation flow, memory and recall, sentiment detection, and error handling.

## Features
- WPF graphical user interface
- Voice greeting on startup using `Assets/greeting.wav`
- ASCII art translated into the GUI
- Keyword recognition for cybersecurity topics
- Random responses using lists
- Conversation follow-up support such as "tell me more" and "give me another tip"
- Memory and recall for the user's name and favourite cybersecurity topic
- Sentiment detection for worried, curious and frustrated messages
- Default response for unknown input
- Clean organisation using dictionaries, lists and methods

## Cybersecurity Topics
The chatbot can respond to:
- Password safety
- Phishing
- Scams
- Privacy
- Malware
- Social engineering

## How to Run
1. Open the project folder in Visual Studio.
2. Make sure the target framework is `.NET 8.0`.
3. Make sure `Assets/greeting.wav` exists.
4. Press **Start** or **F5** to run the application.

## Example Questions
- Tell me about password safety.
- Give me a phishing tip.
- I am worried about online scams.
- My name is Yannis.
- I am interested in privacy.
- What do you remember about me?
- Tell me more.

## GitHub Submission Notes
The repository should include:
- Complete project folder
- Source code
- README file
- Assets folder with the WAV greeting file
- At least 6 meaningful commits
- At least 2 releases/tags

## Suggested Commit Messages
1. Initial WPF project setup
2. Add GUI layout and chatbot interface
3. Add voice greeting and ASCII logo
4. Add keyword recognition responses
5. Add random responses and conversation flow
6. Add memory and sentiment detection
7. Add error handling and README documentation

## Video Presentation Guide
In the video, explain:
- The GUI design
- How the voice greeting works
- How keyword recognition works
- How random responses are selected
- How memory and recall works
- How sentiment detection changes the bot's response
- How error handling prevents crashes
This update was added for Part 2 of the POE/formative assessment.
