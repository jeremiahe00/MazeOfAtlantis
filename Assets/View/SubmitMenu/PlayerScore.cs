using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerScore : MonoBehaviour
{
    private ArrayList leaderBoard = new ArrayList();

    private Obstacles obscore; //inheritance of public variables in Obstacles.cs

    private const int MaxScores = 7;
    private string logText = "";
    private new string name = "";

    private float scoreE;
    private float score = 100;

    public Text scoreEarned;
    public Text displayScores;
    public Text nameText;

    public InputField inputToClear;

    const int kMaxLogSize = 16382;
    private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    public void Start()
    {
        //score update from scoremanagement scorecount variable
        obscore = FindObjectOfType<Obstacles>();

        //leaderBoard.Clear();
        //leaderBoard = new ArrayList();
        leaderBoard.Add("Firebase Top " + MaxScores.ToString() + " Scores");

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

    }

    //clears input field
    public void ClearFields()
    {
        inputToClear.text = "";
    }

    // Initialize the Firebase database:
    protected virtual void InitializeFirebase()
    {
    FirebaseApp app = FirebaseApp.DefaultInstance;
    // NOTE: You'll need to replace this url with your Firebase App's database
    // path in order for the database connection to work correctly in editor.
    app.SetEditorDatabaseUrl("https://mazeofatlantis.firebaseio.com/");
    if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
    StartListener();
    }

protected void StartListener()
{

            FirebaseDatabase.DefaultInstance
          .GetReference("Leaders").OrderByChild("score").LimitToLast(7)
          .ValueChanged += (object sender, ValueChangedEventArgs e2) =>
          {

              if (e2.DatabaseError != null)
              {
                  Debug.LogError(e2.DatabaseError.Message);
                  return;
              }
              Debug.Log("Received values for Leaders.");
              string title = leaderBoard[0].ToString();
              leaderBoard.Clear();
              leaderBoard.Add(title);
              if ((e2.Snapshot != null) && (e2.Snapshot.ChildrenCount > 0))
              {
                  foreach (DataSnapshot childSnapshot in e2.Snapshot.Children)
                  {
                      if (childSnapshot.Child("score") == null
                        || childSnapshot.Child("score").Value == null)
                      {
                          Debug.LogError("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
                          break;
                      }
                      else
                      {
                          Debug.Log("Leaders entry : " +
                            childSnapshot.Child("name").Value + " - " +
                            childSnapshot.Child("score").Value);
                         
                          leaderBoard.Insert(1, childSnapshot.Child("score").Value
                          + "          " + childSnapshot.Child("name").Value);
                          leaderBoard.Sort();
                          leaderBoard.Reverse();
                          //leaderBoard.Sort();

                          //set leaderboard text box with updated values
                          displayScores.text = "";

                          foreach (string item in leaderBoard)
                          {
                              displayScores.text += "\n" + item;
                          }
                      }
                  }
              }
          };

}
    public class DescendingIntSorter : IComparer
    {
        public int Compare(object x, object y)
        {
            return ((float)y).CompareTo((float)x);
        }
    }

    // Exit if escape (or back, on mobile) is pressed.
    public void Update()
    {
        if (PlayerPrefs.HasKey("scoreVariable"))
        {
            scoreE = PlayerPrefs.GetFloat("scoreVariable");
        }

        if (scoreEarned != null)
        {
            scoreEarned.text = "Score: " + (Mathf.Round(scoreE * 100f) / 100f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    // Output text to the debug log text field, as well as the console.
    public void DebugLog(string s)
    {
        Debug.Log(s);
        logText += s + "\n";

        while (logText.Length > kMaxLogSize)
        {
            int index = logText.IndexOf("\n", System.StringComparison.Ordinal);
            logText = logText.Substring(index + 1);
        }
    }

    //sends name and score to the database
    public void WriteNewScore(string userid, float score)
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Leaders");
        Dictionary<string, object> entryValue = new Dictionary<string, object>();
        Dictionary<string, object> entryWrapper = new Dictionary<string, object>();

        string key = reference.Push().Key;

        entryValue["score"] = score;
        entryValue["name"] = userid;

        entryWrapper[key] = entryValue;
        reference.UpdateChildrenAsync(entryWrapper);

    }

    // connected to the button that initiates the data being sent to the database
    public void AddScore()
    {
        name = nameText.text;
        score = Mathf.Round(scoreE * 100f) / 100f;

        if (score < 0.1f || string.IsNullOrEmpty(name))
        {
            DebugLog("invalid score or name.");
            return;
        }

        WriteNewScore(name, score);



        //reference.ContinueWith(task => {
        //    if (task.Exception != null)
        //    {
        //        DebugLog(task.Exception.ToString());
        //    }
        //    else if (task.IsCompleted)
        //    {
        //        DebugLog("Transaction complete.");
        //    }
        //});

        ClearFields();
    }

    public void GameoverSceneClick()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void MainMenuSceneClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

}