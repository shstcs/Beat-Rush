using System.Linq;

public class StageData
{
    public int[] MaxScoreArray = new int[4];

    public void SetData(int score)
    {
        MaxScoreArray[3] = score;
        MaxScoreArray = MaxScoreArray.OrderByDescending(n => n).ToArray();
        Managers.Data.SaveStageData();
    }
}
