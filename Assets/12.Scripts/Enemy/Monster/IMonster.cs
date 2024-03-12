public interface IMonster
{
    void SortPattern();
    (int length, float delay, BGM bgm) GetPatternData();
    void RandomAttack();
    void EndStage();
}
