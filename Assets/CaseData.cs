using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public List<Case> cases;
}

[Serializable]
public class Case
{
    public string id;
    public string title;
    public string description;
    public Suspects suspects;
    public Clues clues;
    public Solution solution;
    public Score score;
}

[Serializable]
public class Suspects
{
    public Suspect suspect_a;
    public Suspect suspect_b;
}

[Serializable]
public class Suspect
{
    public string id;
    public string portrait;
    public string name;
}

[Serializable]
public class Clues
{
    public TextClue text_clue;
    public ImageClue image_clue;
}

[Serializable]
public class TextClue
{
    public string content;
}

[Serializable]
public class ImageClue
{
    public string image;
    public string required_item;
}

[Serializable]
public class Solution
{
    public string culprit_id;
    public string innocent_id;
    public string explanation;
}

[Serializable]
public class Score
{
    public int points_on_correct;
    public int penalty_per_clue_used;
}
