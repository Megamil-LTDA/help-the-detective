# 🕵️ Help the Detective

A detective game developed for the [Tiny Game Jam (12/25)](https://itch.io/jam/tiny-game-jam-12-25).

## 📖 About the Project

**Help the Detective** is a deduction game where you take on the role of a detective at the beginning of your career, trying to enter a highly restricted field of private investigation. Before being accepted, you must solve a series of old, unsolved cases where the evidence seems to point to an obvious truth — but is wrong.

This is a special project created in **partnership between father and son**:
- **Eduardo dos Santos** - Software Developer
- **Eduardo dos Santos Jr.** (11 years old) - Designer and Artist

### 🎂 A Special Date

The project was submitted on **December 28th**, a date that coincides with Eduardo's (father) birthday, making this project even more meaningful. Being able to create something side by side with my son and finish it on this date is already a reward in itself.

---

## 🎮 About the Game

### Core Mechanic

Each case presents **two suspects** and you must discover which one is lying. The game tests your ability to:

- 🔍 Look beyond appearances
- 🧩 Understand context
- ❓ Question easy conclusions

### Game Features

- **3 investigative cases** with unique stories
- **Clue system** (text and image) with point penalties
- **Scoring system** based on correct answers and clues used
- **Two languages**: Portuguese (PT-BR) and English (EN)
- **Different endings** based on performance (70% accuracy for victory)

### Screenshots

<p align="center">
  <img src="Assets/screenshots/ss_1.png" width="200" alt="Screenshot 1">
  <img src="Assets/screenshots/ss_2.png" width="200" alt="Screenshot 2">
  <img src="Assets/screenshots/ss_3.png" width="200" alt="Screenshot 3">
  <img src="Assets/screenshots/ss_4.png" width="200" alt="Screenshot 4">
</p>

<p align="center">
  <img src="Assets/screenshots/ss_5.png" width="200" alt="Screenshot 5">
  <img src="Assets/screenshots/ss_6.png" width="200" alt="Screenshot 6">
  <img src="Assets/screenshots/ss_7.png" width="200" alt="Screenshot 7">
  <img src="Assets/screenshots/ss_8.png" width="200" alt="Screenshot 8">
</p>

---

## 🎯 Tiny Game Jam (12/25)

This game was developed for the **[Tiny Game Jam (12/25)](https://itch.io/jam/tiny-game-jam-12-25)**, a game jam focused on small and concise projects.

### Development Timeline

- **12/26/2024** - Project start, brainstorming and visual concepts
- **12/27/2024** - Main mechanics development
- **12/28/2024** - Finalization and submission

### Jam Focus

The goal was to keep the scope **small and focused**, while experimenting with storytelling through visual interpretation and context — fitting the spirit of a short game jam.

---

## 🛠️ Technologies Used

### Engine and Tools

- **Unity** - Game Engine
- **C#** - Programming Language
- **TextMeshPro** - Text System
- **Input System** - Control System

### Assets and Resources

- Some sprites and sounds were obtained from **copyright-free resources**
- Not all images were AI-generated — **several drawings and ideas came directly from Eduardo Jr.**

### AI Tools

We used AI tools such as **Gemini (Nano Banana)** and **ChatGPT**, mainly to:

- Help translate story and texts to English
- Assist with image concepts and refinement
- Improve clarity and consistency of written content

> **Important:** AI was used as a **supporting tool**, not as a replacement for creativity. The project combines human ideas, learning, experimentation, and fun.

---

## 🎨 Technical Features

### Localization System

- **Two complete languages**: Portuguese and English
- **Real-time language switching** without needing to restart
- **Translated texts**: Interface, story, cases, and solutions

### Scoring System

```
Points Earned = Base Points - (Clues Used × Penalty per Clue)
```

- Case 001: 130 points (penalty: 30 per clue)
- Case 002: 120 points (penalty: 25 per clue)
- Case 003: 150 points (penalty: 30 per clue)

**Maximum total:** 400 points

### Gameplay Features

- 🎲 **Shuffled cases** - Random order each playthrough
- 🔀 **Randomized positions** - Suspects appear in different positions
- 💡 **Clue system** with penalty
- 🏆 **Victory/defeat system** based on 70% performance

---

## 🎓 Lessons Learned

This project was an incredible opportunity to:

- **Father and son working together** on a creative project
- Learn about **game development** in practice
- Experiment with **narrative and puzzle design**
- Manage **scope and time** in a game jam
- Use **AI as a supporting tool**, not as a replacement

---

## 👨‍👦 About the Creators

**Eduardo dos Santos**
- Software developer at a large Brazilian multinational
- Doesn't work professionally with games
- This game exists out of curiosity, creativity, and the desire to create something meaningful with his son

**Eduardo dos Santos Jr. (11 years old)**
- Designer and artist of the project
- Responsible for several original drawings and ideas
- First experience in game development

---

## 📝 License and Credits

This is an educational and experimental project created for the Tiny Game Jam.

**Credits:**
- Development and Design: Eduardo dos Santos & Eduardo dos Santos Jr.
- AI Tools: Gemini (Nano Banana), ChatGPT
- Assets: Copyright-free resources + original art

---

## 🎮 How to Play

1. Read the case description
2. Observe the two suspects
3. Use clues if necessary (but you'll lose points!)
4. Choose who you think is the culprit
5. See the explanation and find out if you were right

**Controls:**
- Mouse/Touch to select options
- Space to close clues and modals
- Intuitive interface

---

## 🚀 Getting Started

### Prerequisites

- **Unity 2021.3 LTS** or newer
- Basic knowledge of Unity Editor (optional)

### How to Test the Project

1. **Clone the repository**
   ```bash
   git clone https://github.com/Megamil-LTDA/help-the-detective.git
   cd help-the-detective
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" → Select the project folder
   - Open the project with Unity 2021.3 LTS or newer

3. **Play the game**
   - In Unity, open the scene `Assets/Scenes/Home.unity`
   - Press the Play button ▶️
   - Enjoy!

---

## 🎨 Create Your Own Version

The game is designed to be easily customizable! You can create your own detective cases by simply editing JSON files and adding images.

### Step 1: Edit the Cases (JSON)

The game data is stored in two files:
- `Assets/Resources/Data-en.json` (English version)
- `Assets/Resources/Data-pt.json` (Portuguese version)

**JSON Structure:**

```json
{
    "cases": [
        {
            "id": "case_001",
            "title": "Your Case Title",
            "description": "Case description that appears at the start",
            "suspects": {
                "suspect_a": {
                    "id": "case_1_a",
                    "portrait": "case_1_a.png",
                    "name": "Suspect Name"
                },
                "suspect_b": {
                    "id": "case_1_b",
                    "portrait": "case_1_b.png",
                    "name": "Suspect Name"
                }
            },
            "clues": {
                "text_clue": {
                    "content": "Text clue content"
                },
                "image_clue": {
                    "image": "clue_1.png",
                    "required_item": "magnifying_glass"
                }
            },
            "solution": {
                "culprit_id": "case_1_b",
                "innocent_id": "case_1_a",
                "explanation": "Explanation of why this suspect is guilty"
            },
            "score": {
                "points_on_correct": 130,
                "penalty_per_clue_used": 30
            }
        }
    ]
}
```

### Step 2: Add Your Images

Place your images in the correct folders:

#### Suspect Portraits
- **Location**: `Assets/Resources/Imagens/suspects/`
- **Format**: PNG
- **Naming**: Must match the `portrait` field in JSON (e.g., `case_1_a.png`)

#### Clue Images
- **Location**: `Assets/Resources/Imagens/clues/`
- **Format**: PNG
- **Naming**: Must match the `image` field in JSON (e.g., `clue_1.png`)

### Step 3: Test Your Changes

1. Save your JSON files
2. Add your images to the correct folders
3. Open Unity and press Play ▶️
4. Your new cases should appear in the game!

### Tips for Creating Cases

- ✅ Keep descriptions concise and intriguing
- ✅ Make sure the visual clues make sense with the story
- ✅ Balance the difficulty - not too obvious, not impossible
- ✅ Test both language versions if using two languages
- ✅ Adjust points and penalties to balance the game

### Sample Template

Check `Assets/Resources/Data-Sample.json` for a template you can use to create new cases!

---

## 🌟 Acknowledgments

Thank you for playing **Help the Detective**! This project represents much more than a game — it represents quality time, learning, and creativity shared between father and son.

If you enjoyed it, consider leaving feedback on the [jam page](https://itch.io/jam/tiny-game-jam-12-25)!

---

**Made with ❤️ by Eduardo dos Santos and Eduardo dos Santos Jr.**

*Developed between December 26-28, 2024*
