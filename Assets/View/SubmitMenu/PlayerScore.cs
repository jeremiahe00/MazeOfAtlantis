using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerScore : MonoBehaviour
{
    ArrayList leaderBoard;

    private Obstacles obscore; //inheritance of public variables in Obstacles.cs

    private const int MaxScores = 5;
    private string logText = "";
    private new string name = "";

    private float scoreE;
    private float score = 100;

    public Text scoreEarned;
    public Text displayScores;
    public Text nameText;

    public InputField inputToClear;

    //private System.Random random = new System.Random();

    //User user = new User();
    //public static int playerScore;
    //public static string playerName;

    const int kMaxLogSize = 16382;
    private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;

    // When the app starts, check to make sure that we have
    // the required dependencies to use Firebase, and if not,
    // add them if possible.
    void Start()
    {
        //score update from scoremanagement scorecount variable
        obscore = FindObjectOfType<Obstacles>();

        leaderBoard = new ArrayList();
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
      .GetReference("Leaders").OrderByChild("score")
      .ValueChanged += (object sender2, ValueChangedEventArgs e2) => {
          if (e2.DatabaseError != null)
          {
              Debug.LogError(e2.DatabaseError.Message);
              return;
          }
          Debug.Log("Received values for Leaders.");
          string title = leaderBoard[0].ToString();
          leaderBoard.Clear();
          leaderBoard.Add(title);
          if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0)
          {
              foreach (var childSnapshot in e2.Snapshot.Children)
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
                        childSnapshot.Child("name").Value.ToString() + " - " +
                        childSnapshot.Child("score").Value.ToString());
                      leaderBoard.Insert(1, childSnapshot.Child("score").Value.ToString()
                        + "  " + childSnapshot.Child("name").Value.ToString());
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

// Exit if escape (or back, on mobile) is pressed.
void Update()
{
    if (PlayerPrefs.HasKey("score"))
        {
            scoreE = PlayerPrefs.GetFloat("score");
        }
        scoreEarned.text = "Score: " + (Mathf.Round(scoreE * 100f) / 100f);    
                
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Application.Quit();
    }
       /*
        if (addScorePressed)
            {
                displayScores.text = "";
                foreach (string item in leaderBoard)
                {
                    displayScores.text += "\n" + item;
                }
             addScorePressed = false;
            }
             */
        //score = Mathf.Round(theScoreManager.scoreCount * 100f) / 100f;
        //scoreText.text = "Score: " + score;

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

// A realtime database transaction receives MutableData which can be modified
// and returns a TransactionResult which is either TransactionResult.Success(data) with
// modified data or TransactionResult.Abort() which stops the transaction with no changes.
TransactionResult AddScoreTransaction(MutableData mutableData)
{
    List<object> leaders = mutableData.Value as List<object>;

    if (leaders == null)
    {
        leaders = new List<object>();
    }
    else if (mutableData.ChildrenCount >= MaxScores)
    {
        // If the current list of scores is greater or equal to our maximum allowed number,
        // we see if the new score should be added and remove the lowest existing score.
        long minScore = long.MaxValue;
        object minVal = null;
        foreach (var child in leaders)
        {
            if (!(child is Dictionary<string, object>))
                continue;
            long childScore = (long)((Dictionary<string, object>)child)["score"];
            if (childScore < minScore)
            {
                minScore = childScore;
                minVal = child;
            }
        }
        // If the new score is lower than the current minimum, we abort.
        if (minScore > score)
        {
            return TransactionResult.Abort();
        }
        // Otherwise, we remove the current lowest to be replaced with the new score.
        leaders.Remove(minVal);
    }

    // Now we add the new score as a new entry that contains the email address and score.
    Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
    newScoreMap["score"] = score;
    newScoreMap["name"] = name;
    leaders.Add(newScoreMap);

    // You must set the Value to indicate data at that location has changed.
    mutableData.Value = leaders;
    //return and log success
    return TransactionResult.Success(mutableData);
}

public void AddScore()
{

    name = nameText.text;
    score = (Mathf.Round(scoreE * 100f) / 100f);
    //(int)Mathf.Round(theScoreManager.scoreCount * 100f) / 100f;

        if ((int)score == 0 || string.IsNullOrEmpty(name))
        {
            DebugLog("invalid score or name.");
            return;
        }
    DebugLog(string.Format("Attempting to add score {0} {1}",
      name, score.ToString()));

    DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Leaders");

    DebugLog("Running Transaction...");
    // Use a transaction to ensure that we do not encounter issues with
    // simultaneous updates that otherwise might create more than MaxScores top scores.
    reference.RunTransaction(AddScoreTransaction)
      .ContinueWith(task => {
          if (task.Exception != null)
          {
              DebugLog(task.Exception.ToString());
          }
          else if (task.IsCompleted)
          {
              DebugLog("Transaction complete.");
          }
      });
        //update UI
        //addScorePressed = true;
        ClearFields();
    }

    public void GameoverSceneClick()
    {
        SceneManager.LoadScene("GameOverScene");
    }

}